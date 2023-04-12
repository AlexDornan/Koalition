using KoalitionAndroidClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalitionAndroidClient.ViewModels.Chat
{
    public class GroupChatPageViewModel : BaseViewModel
    {
        public GroupChatResponce _selectedGroupChat;

        public GroupChatResponce SelectedGroupChat
        {
            get => _selectedGroupChat;
            set
            {
                _selectedGroupChat = value;
                OnPropertyChanged(); // Raise PropertyChanged event for data binding
            }
        }

        // Other properties and commands as needed

        public GroupChatPageViewModel(GroupChatResponce selectedGroupChat)
        {
            _selectedGroupChat = selectedGroupChat;
        }

        // Command to send message or perform other actions in the group chat
        public async void SendMessage()
        {
            // Use _selectedGroupChat.Id and other properties to perform actions in the group chat
            // For example, send a message to the group chat using the selected group chat's ID
            int groupId = _selectedGroupChat.Id;
            // Perform action with the group chat ID
        }
    }
}
