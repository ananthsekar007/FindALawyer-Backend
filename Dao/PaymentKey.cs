using Microsoft.Extensions.Configuration;

namespace FindALawyer.Dao
{
    public class PaymentKey
    {
        private readonly IConfiguration _configuration;
        public PaymentKey(IConfiguration configuration)
        {
            _configuration = configuration;
            Key = _configuration.GetSection("PaymentSettings:KEY").Value;
            Secret = _configuration.GetSection("PaymentSettings:SECRET").Value;
        }

        public string Key { get; private set; }
        public string Secret { get; private set; }
    }
}

