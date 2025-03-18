using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace TRB.Converters
{
	[ValueConversion(typeof(bool), typeof(Cursors))]
	public class BooleanToCursorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (value is bool isConverting && isConverting) ? Cursors.Wait : Cursors.Arrow;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}

}
