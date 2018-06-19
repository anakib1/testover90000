using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
namespace bot
{
    class Stocks
    {
        double AvgFromDynamic(dynamic a)
        {
            double res = 0;
            for (int i = 0; i < 100; i++)
            {
                res += Convert.ToDouble(a["ETH_USD"][i]["price"]);
            }
            return res / 100;
        }
        double AvgFromDynamic(dynamic a, string additional)
        {
            double res = 0;
            for (int i = 0; i < 100; i++)
            {
                res += Convert.ToDouble(a[additional][i]["price"]);
            }
            return res / 100;
        }
        public string RequestToApi(string url)
        {
            var webRequest = WebRequest.Create(url);
            webRequest.Method = "POST";
          

            string answer;
            using (var webResponse = webRequest.GetResponse())
            {
                var responseStream = webResponse.GetResponseStream();
                if (responseStream == null) return null;

                using (var streamReader = new StreamReader(responseStream))
                {
                    answer = streamReader.ReadToEnd();
                }
            }

            return answer;
        }
        dynamic ethUSD;
        dynamic btcUSD;
        dynamic dogeBTC;
        public Stocks()
        {
            var Serializer = new JavaScriptSerializer();
            ethUSD = Serializer.DeserializeObject(RequestToApi(@"https://api.exmo.com/v1/trades/?pair=ETH_USD"));
            btcUSD = Serializer.DeserializeObject(RequestToApi(@"https://api.exmo.com/v1/trades/?pair=BTC_USD"));
            dogeBTC = Serializer.DeserializeObject(RequestToApi(@"https://api.exmo.com/v1/trades/?pair=DOGE_BTC"));

        }
        public string GetAvg(string currency)
        {
            try
            {
                if (currency == "ETH")
                {
                    return AvgFromDynamic(ethUSD).ToString()+ " #ETH";
                }
                if(currency=="BTC")
                {
                    return AvgFromDynamic(btcUSD,"BTC_USD").ToString() + " #BTC";
                }
                if (currency == "DOGE")
                {
                    return (AvgFromDynamic(dogeBTC,"DOGE_BTC") * AvgFromDynamic(btcUSD,"BTC_USD")).ToString()+" #DOGE";
                }
                else
                {
                    try
                    {
                        var Serializer = new JavaScriptSerializer();
                        return AvgFromDynamic(Serializer.DeserializeObject(RequestToApi(@"https://api.exmo.com/v1/trades/?pair=" + currency + "_USD")), currency + "_USD").ToString()+ " #"+currency;
                    }
                    catch (Exception e4)
                    {
                        return "error S2, no such currency in bot yet " + e4.Message;
                    }
                }
            }
            catch(Exception e3)
            {
                return "error S1( "+e3.Message+" )";
            }
        }
    }
}
