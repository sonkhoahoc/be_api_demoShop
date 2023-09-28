
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VHUX.Extensions
{
    public static class Common
    {
        public enum TableName
        {
            News
        }
        public enum Status : byte
        {
            contract_Nhap = 1, // lwu nháp 

        }
        public enum PaymentStatus : byte
        {

            CHUATHANHTOAN = 0,
            CHOTHANHTOAN = 1,
            THANHTOANLOI = 5,
            DATHANHTOAN_CHUAXACNHAN = 10,
            DAXACNHAN = 20,
            DOIXOAT = 30,
            HOANTHANH = 50

        }
    }

    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssZ"));
        }
    }
   


}
