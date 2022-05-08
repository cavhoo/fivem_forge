namespace Server.Models
{
    public class Config
    {
        public string ConnectionString { get; set; }
        public string JobConfigPath { get; set; }
        public int InitialBankAmountBalance { get; set; }
    }
}