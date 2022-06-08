using System;
using RestSharp;
using RestSharp.Authenticators;


namespace CLICryptoTracker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new RestClient("https://api.nomics.com/v1/");
            var request = new RestRequest("currencies/ticker");
            request.AddQueryParameter("key", "8aec736bde47fc88b2dfce890bd3c5bf00717dd2");
            request.AddQueryParameter("ids", "BTC,ETH,ADA,SOL,XRP,SHIB");
            request.AddQueryParameter("interval", "30d");
            request.AddQueryParameter("convert", "USD");
            var response = await client.GetAsync(request);

            Console.WriteLine("asdas");

            Console.WriteLine(response.Content);
        }

    }
}