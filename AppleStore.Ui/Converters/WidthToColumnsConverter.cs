using System;
using System.Globalization;
using System.Windows.Data;

namespace AppleStore.Ui.Converters
{
    public class WidthToColumnsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double width)
            {
                // Предполагаемая ширина одного элемента. Настройте это значение по мере необходимости.
                double itemWidth = 180;
                return Math.Max(1, (int)(width / itemWidth));
            }
            return 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}