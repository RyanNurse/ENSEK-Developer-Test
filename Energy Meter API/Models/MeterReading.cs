using System;

namespace Energy_Meter_API
{
    public class MeterReading
    {
        public int AccountId { get; set; }
        public DateTime DateTime { get; set; }
        public int ReadValue { get; set; }

        public MeterReading() { }

        public MeterReading(int accId, DateTime dateTime, int readValue)
        {
            AccountId = accId;
            DateTime = dateTime;
            ReadValue = readValue;
        }

        public static MeterReading CreateFromCsv(string csvLine)
        {
            var values = csvLine.Split(',');

            //parse and validate values
            if (!int.TryParse(values[0], out int accId))
            {
                return null;
            }

            if (!DateTime.TryParse(values[1], out DateTime dateTime))
            {
                return null;
            }

            if (!int.TryParse(values[2], out int readValue))
            {
                return null;
            }

            if (readValue > 99999 || readValue < 0)
            {
                return null;
            }

            return new MeterReading(accId, dateTime, readValue);
        }
    }
}
