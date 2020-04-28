using Culqi;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulqiSampleConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press Enter");
            Console.ReadLine();

            //CulqiConfiguration.ApiKey = "pk_test_8MtrpF4Kw1pAjdYX";

            CulqiConfiguration.ApiKey = "sk_test_iSCxCg7THuZB2DwG";

            var tokenOptions = new TokenCreateOptions()
            {
                CardNumber = "4111111111111111",
                ExpirationMonth = "09",
                ExpirationYear = "2029",                
                Cvv = "123",
                Email = "richard@piedpiper.com"
            };

            var tokenService = new TokenService();
            //var tokens = await tokenService.Create(tokenOptions);

            //var tokens = await tokenService.Get("tkn_test_rk0lMDeGolf9fBa5");

            var tokenUpdateOptions = new TokenUpdateOptions {  
                Metadata = new Dictionary<string, string> 
                {
                    { "email", "wilsonvargas_6@outlook.com"},
                    { "active", "false" }
                }
            };
            //var tokens = await tokenService.Update("tkn_test_rk0lMDeGolf9fBa5", tokenUpdateOptions);

            var tokenListOptions = new TokenListOptions
            {
                Limit = 2,
                Bin = 411111,
                CountryCode = "PE"
            };
            //var tokens = await tokenService.List(tokenListOptions);


            ChargeService chargeService = new ChargeService();

            var chargeCreateOptions = new ChargeCreateOptions 
            {
                Amount = 100000,
                CurrencyCode = "PEN",
                Email = "wilsonvargas_6@outlook.com",
                SourceId = "tkn_test_o1tYygTfUzugALDq",
                Description = "Test Charge from .net platform"
            };
            //chr_test_QezSVuBoRuSsrslL
            //var charge = await chargeService.Create(chargeCreateOptions);

            //var charge = await chargeService.Get("chr_test_QezSVuBoRuSsrslL");

            //var charge = await chargeService.Capture("chr_test_Wmr3PB18xmwyMtOq");

            var chargeListOptions = new ChargeListOptions
            {
                Limit = 3,
                Amount = 10000
            };

            //var charge = await chargeService.List(chargeListOptions);

            var chargeUpdateOptions = new ChargeUpdateOptions
            {
                Metadata = new Dictionary<string, string>
                {
                    { "email", "wilsonvargas_6@outlook.com"},
                    { "dni", "77777770" }
                }
            };

            var charge = await chargeService.Update("chr_test_QezSVuBoRuSsrslL", chargeUpdateOptions);

            Console.WriteLine(charge);
            Console.ReadLine();
        }
    }
}
