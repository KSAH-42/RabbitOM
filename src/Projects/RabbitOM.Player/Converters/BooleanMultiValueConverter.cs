using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace RabbitOM.Player.Converters
{
    public sealed class BooleanMultiValueConverter : IMultiValueConverter
    {
        public object Convert( object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values?.All(value => bool.TryParse( value?.ToString() , out var result ) && ! result );
        }

        public object[] ConvertBack( object value , Type[] targetTypes, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
