using System.Globalization;

namespace DrakeAzureDevopsMobile.Converters
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int index)
            {
                if (index % 2 == 0)
                {
                    return Color.FromArgb("#E8E8E8");
                }
                else
                {
                    return Color.FromArgb("#FFFFFF");
                }
            }
            return Color.FromArgb(((int)value).ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
