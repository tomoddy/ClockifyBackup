using Newtonsoft.Json.Converters;

namespace ClockifyBackup
{
    class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string dateTimeFormat)
        {
            DateTimeFormat = dateTimeFormat;
        }
    }
}
