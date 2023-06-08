using KoalitionAndroidClient.Helpers;
using KoalitionAndroidClient.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;

namespace KoalitionAndroidClient.ViewModels.Chat;

public class PrivateChatPageViewModel : BaseViewModel
{
    private int _recipientId;
    private string _messageText;
    private ObservableCollection<SendMessageRequest> _messages;
    private UserBasicInfo _selectedUser;
    private ObservableCollection<ChatMessageResponse> _getMessages;

    public UserBasicInfo SelectedUser { get; set; }

    public ObservableCollection<SendMessageRequest> Messages
    {
        get { return _messages; }
        set
        {
            _messages = value;
            OnPropertyChanged(nameof(Messages));
        }
    }
    public ObservableCollection<ChatMessageResponse> GetMessages
    {
        get { return _getMessages; }
        set
        {
            _getMessages = value;
            OnPropertyChanged(nameof(GetMessages));
        }
    }

    public string MessageText
    {
        get { return _messageText; }
        set
        {
            _messageText = value;
            OnPropertyChanged(nameof(MessageText));
        }
    }

    public ICommand SendMessageCommand { get; }

    public PrivateChatPageViewModel(UserBasicInfo user)
    {
        //_recipientId = recipientId;
        SendMessageCommand = new Command(async () => await SendMessage());
        _selectedUser = user;
        LoadMessages();
        
    }

    public async Task SendMessage()
    {
        using HttpClient httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Token);

        var message = new SendMessageRequest
        {
            Text = MessageText,
            Id = _selectedUser.UserId,
        };
        var messageJson = JsonConvert.SerializeObject(message);
        var content = new StringContent(messageJson, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync($"{ApiPlatformUrlHelper.GetPlatformApiUrl()}/api/privatechats/sendMessage?recipientId={_selectedUser.UserId}", content);
        if (response != null)
        {
            // Update the UI or perform any necessary actions
            MessageText = string.Empty;
            await LoadMessages();
        }
    }

    public async Task LoadMessages()
    {
        using HttpClient httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Token);

        var response = await httpClient.GetAsync($"{ApiPlatformUrlHelper.GetPlatformApiUrl()}/api/privatechats?recipientId={_selectedUser.UserId}");
        var privateChatJson = await response.Content.ReadAsStringAsync();
        var usersAndMessages = JsonConvert.DeserializeObject<List<ChatMessageResponse>>(privateChatJson);

        foreach (var message in usersAndMessages)
        {
            var sender = usersAndMessages.FirstOrDefault(u => u.UserId == message.UserId);
            if (sender != null)
            {
                message.Name = sender.Name;
            }
        }
        GetMessages = new ObservableCollection<ChatMessageResponse>(usersAndMessages);
    }
}
