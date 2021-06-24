namespace diodocs.Models
{
    public class ConvertRequest
    {
        public byte[] template { get; set; }
        public byte[] backgroundImage { get; set; }
    }
    public class ConvertResponse
    {
        public byte[] OutputData { get; set; }
    }
}