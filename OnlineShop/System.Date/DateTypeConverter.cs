using System.ComponentModel;
using System.Globalization;

namespace System
{
    public sealed class DateTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof(string);

        public override object? ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string str)
                return Date.Parse(str);
            return null;
        }
    }
}