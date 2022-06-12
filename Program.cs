// Michal MIkl
// 3.B
// 12.6 2022
// PVA
// CLICryptoTracker

using System;
using System.Globalization;
using System.Text.Json.Nodes;
using RestSharp;
using RestSharp.Authenticators;


namespace CLICryptoTracker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Init
            bool running = true;

            // Config
            var client = new RestClient("https://api.nomics.com/v1/");
            string apiKey = "3d05460afd5363d37a455e3a29aa82251d3c8728";

            // Hlavni loop pro beh programu
            while (running)
            {
                Console.WriteLine("========== Crypto Tracker ==========");
                Console.WriteLine("1. Top 10 kryptoměn");
                Console.WriteLine("2. Vyhledávání (BTC, ADA, ...) ");
                Console.WriteLine("3. Náhodná kryptoměna");
                Console.WriteLine("4. Ukončít");

                Console.Write("\nVolba: ");

                // Volba moznosti
                int userChoice = int.Parse(Console.ReadLine());

                // Podminky pro volbu
                switch (userChoice)
                {
                    case 1:
                        Console.WriteLine("");

                        // Parse
                        dynamic data = JsonNode.Parse(await GetMany(client, apiKey, "10"));
                        
                        // Vypíše data o každé kryptoměně
                        foreach (var item in data)
                        {
                            string maxSupply = item["max_supply"] == null ? "unlimited" : (string) item["max_supply"];

                            Console.WriteLine("Název: ".PadRight(15) + item["name"]);
                            Console.WriteLine("Symbol: ".PadRight(15) + item["symbol"]);
                            Console.WriteLine("Cena: ".PadRight(15) + "$" + item["price"]);
                            Console.WriteLine("Cirk. zásoba: ".PadRight(15) + item["circulating_supply"]);
                            Console.WriteLine($"Max. zásoba: ".PadRight(15) + maxSupply);
                            Console.WriteLine("Rank: ".PadRight(15) + item["rank"]);
                            
                            Console.WriteLine("--------------------------------");
                            Console.WriteLine("");
                        }
                        continue;

                    case 2:
                        Console.Write("Zadej název tokenu: ");
                        string tokenName = Console.ReadLine();

                        Console.WriteLine("");

                        // Kontrola inputu
                        if (tokenName == "") {
                            Console.WriteLine("Prosím zadej název tokenu");
                            return;
                        }

                        // Parse
                        dynamic response = JsonNode.Parse(await GetSearched(client, apiKey, tokenName))[0];

                        string maxSupplyResponse = response["max_supply"] == null ? "unlimited" : (string) response["max_supply"];

                        // Vypíše data o kryptoměně
                        Console.WriteLine("Název: ".PadRight(15) + response["name"]);
                        Console.WriteLine("Symbol: ".PadRight(15) + response["symbol"]);
                        Console.WriteLine("Cena: ".PadRight(15) + "$" + response["price"]);
                        Console.WriteLine("Cirk. zásoba: ".PadRight(15) + response["circulating_supply"]);
                        Console.WriteLine($"Max. zásoba: ".PadRight(15) + maxSupplyResponse);
                        Console.WriteLine("Rank: ".PadRight(15) + response["rank"]);

                        Console.WriteLine("");

                        continue;

                    case 3:
                        // List random tokenu
                        var tokens = new List<string>();

                        // Parse
                        dynamic resMany = JsonNode.Parse(await GetMany(client, apiKey, "100"));

                        // Přidá tokeny do listu
                        foreach (var item in resMany)
                        {
                            tokens.Add((string)item["symbol"]);
                        }

                        // Random cislo v rozmezi od 0 do poctu pozadovanych tokenu
                        Random random = new Random();
                        int randomNum = random.Next(0, tokens.Count);

                        Console.WriteLine("");

                        // Delay for api requesty
                        Thread.Sleep(1000);

                        // Parse
                        dynamic res = JsonNode.Parse(await GetSearched(client, apiKey, tokens[randomNum]))[0];

                        string maxSupplyRes = res["max_supply"] == null ? "unlimited" : (string) res["max_supply"];

                        // Vypíše data o kryptoměně
                        Console.WriteLine("Název: ".PadRight(15) + res["name"]);
                        Console.WriteLine("Symbol: ".PadRight(15) + res["symbol"]);
                        Console.WriteLine("Cena: ".PadRight(15) + "$" + res["price"]);
                        Console.WriteLine("Cirk. zásoba: ".PadRight(15) + res["circulating_supply"]);
                        Console.WriteLine($"Max. zásoba: ".PadRight(15) + maxSupplyRes);
                        Console.WriteLine("Rank: ".PadRight(15) + res["rank"]);

                        Console.WriteLine("");

                        continue;

                    case 4:
                        Console.WriteLine("Děkujeme za využití CryptoTrackeru");

                        // Nacte jmeno
                        Console.Write("\nVaše jměno: ");
                        string name = Console.ReadLine();

                        // Vypise jmeno
                        Console.Write("Vaše jměno je: ");
                        foreach (var character in name)
                        {
                            Console.Write(character);
                        }

                        Console.WriteLine("");

                        running = false;
                        
                        continue;
                }
            }
        }

        // Získá data o počtu požadovaných kryptoměn a vrátí je
        static async Task<string> GetMany(RestClient client, string apiKey, string count)
        {
            var request = new RestRequest("currencies/ticker");

            request.AddQueryParameter("key", apiKey);
            request.AddQueryParameter("interval", "30d");
            request.AddQueryParameter("convert", "USD");
            request.AddQueryParameter("page", "1");
            request.AddQueryParameter("per-page", count);
            request.AddQueryParameter("sort", "rank");

            var response = await client.GetAsync(request);

            return response.Content;
        }

        // Získá data o vyhledané kryptoměně a vrátí je
        static async Task<string> GetSearched(RestClient client, string apiKey, string tokenName)
        {
            var request = new RestRequest("currencies/ticker");

            request.AddQueryParameter("key", apiKey);
            request.AddQueryParameter("interval", "30d");
            request.AddQueryParameter("convert", "USD");
            request.AddQueryParameter("ids", tokenName);

            var response = await client.GetAsync(request);

            return response.Content;
        }
    }
}