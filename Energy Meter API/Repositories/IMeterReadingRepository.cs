using System.Data;
using System.Data.SqlClient;

namespace Energy_Meter_API
{
    public interface IMeterReadingRepository
    {
        public static bool Insert(MeterReading reading)
        {
            bool result;

            using (var conn = new SqlConnection(@"..\Database\ENSEK_DB"))
            {
                conn.Open();

                var cmd = new SqlCommand("dbo.SaveReading", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@accountId", reading.AccountId);
                cmd.Parameters.AddWithValue("@dateTime", reading.DateTime);
                cmd.Parameters.AddWithValue("@readValue", reading.ReadValue);

                var returnParameter = cmd.Parameters.Add("@success", SqlDbType.Bit);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                cmd.ExecuteNonQuery();

                result = (bool)returnParameter.Value;
            }

            return result;
        }
    }
}