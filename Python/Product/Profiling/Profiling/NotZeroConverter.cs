using System;
using System.Globalization;
using System.Windows.Data;

namespace Microsoft.PythonTools.Profiling {
    public class NotZeroConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return (value as int? ?? 0) != 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
