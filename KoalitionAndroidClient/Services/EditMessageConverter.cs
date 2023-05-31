using KoalitionAndroidClient.ViewModels.Chat;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalitionAndroidClient.Services
{
    public class EditMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isEditing)
            {
                if (parameter is GroupChatPageViewModel viewModel)
                {
                    return isEditing ? (viewModel.EditMessageText ?? string.Empty) : string.Empty;
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value; // One-way binding, no need to convert back
        }
    }

}
