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
            //string localResponse = "[{\"id\":\"BTC\",\"currency\":\"BTC\",\"symbol\":\"BTC\",\"name\":\"Bitcoin\",\"logo_url\":\"https://s3.us-east-2.amazonaws.com/nomics-api/static/images/currencies/btc.svg\",\"status\":\"active\",\"price\":\"30504.70890529\",\"price_date\":\"2022-06-08T00:00:00Z\",\"price_timestamp\":\"2022-06-08T15:05:00Z\",\"circulating_supply\":\"19061831\",\"max_supply\":\"21000000\",\"market_cap\":\"581475605857\",\"market_cap_dominance\":\"0.4508\",\"num_exchanges\":\"457\",\"num_pairs\":\"89698\",\"num_pairs_unmapped\":\"9152\",\"first_candle\":\"2011-08-18T00:00:00Z\",\"first_trade\":\"2011-08-18T00:00:00Z\",\"first_order_book\":\"2017-01-06T00:00:00Z\",\"rank\":\"1\",\"rank_delta\":\"0\",\"high\":\"67597.94247419\",\"high_timestamp\":\"2021-11-08T00:00:00Z\",\"30d\":{\"volume\":\"1619885537274.83\",\"price_change\":\"340.45333621\",\"price_change_pct\":\"0.0113\",\"volume_change\":\"175707925520.19\",\"volume_change_pct\":\"0.1217\",\"market_cap_change\":\"7294687610.77\",\"market_cap_change_pct\":\"0.0127\"}},{\"id\":\"ETH\",\"currency\":\"ETH\",\"symbol\":\"ETH\",\"name\":\"Ethereum\",\"logo_url\":\"https://s3.us-east-2.amazonaws.com/nomics-api/static/images/currencies/eth.svg\",\"status\":\"active\",\"platform_currency\":\"ETH\",\"price\":\"1808.00322270\",\"price_date\":\"2022-06-08T00:00:00Z\",\"price_timestamp\":\"2022-06-08T15:05:00Z\",\"circulating_supply\":\"121096515\",\"market_cap\":\"218942890053\",\"market_cap_dominance\":\"0.1698\",\"num_exchanges\":\"524\",\"num_pairs\":\"89185\",\"num_pairs_unmapped\":\"69328\",\"first_candle\":\"2015-08-07T00:00:00Z\",\"first_trade\":\"2015-08-07T00:00:00Z\",\"first_order_book\":\"2018-08-29T00:00:00Z\",\"rank\":\"2\",\"rank_delta\":\"0\",\"high\":\"4811.59476407\",\"high_timestamp\":\"2021-11-08T00:00:00Z\",\"30d\":{\"volume\":\"838166777342.50\",\"price_change\":\"-427.76012141\",\"price_change_pct\":\"-0.1913\",\"volume_change\":\"111327103112.95\",\"volume_change_pct\":\"0.1532\",\"market_cap_change\":\"-50935510594.66\",\"market_cap_change_pct\":\"-0.1887\"}},{\"id\":\"ADA\",\"currency\":\"ADA\",\"symbol\":\"ADA\",\"name\":\"Cardano\",\"logo_url\":\"https://s3.us-east-2.amazonaws.com/nomics-api/static/images/currencies/ada.svg\",\"status\":\"active\",\"price\":\"0.65502554\",\"price_date\":\"2022-06-08T00:00:00Z\",\"price_timestamp\":\"2022-06-08T15:05:00Z\",\"circulating_supply\":\"33820262544\",\"max_supply\":\"45000000000\",\"market_cap\":\"22153135650\",\"market_cap_dominance\":\"0.0172\",\"num_exchanges\":\"177\",\"num_pairs\":\"986\",\"num_pairs_unmapped\":\"666\",\"first_candle\":\"2017-10-01T00:00:00Z\",\"first_trade\":\"2017-11-22T00:00:00Z\",\"first_order_book\":\"2018-08-29T00:00:00Z\",\"rank\":\"6\",\"rank_delta\":\"0\",\"high\":\"2.96752939\",\"high_timestamp\":\"2021-09-03T00:00:00Z\",\"30d\":{\"volume\":\"47291011855.81\",\"price_change\":\"0.049228834\",\"price_change_pct\":\"0.0813\",\"volume_change\":\"11177466915.21\",\"volume_change_pct\":\"0.3095\",\"market_cap_change\":\"1664932097.11\",\"market_cap_change_pct\":\"0.0813\"}},{\"id\":\"XRP\",\"currency\":\"XRP\",\"symbol\":\"XRP\",\"name\":\"XRP\",\"logo_url\":\"https://s3.us-east-2.amazonaws.com/nomics-api/static/images/currencies/XRP.svg\",\"status\":\"active\",\"price\":\"0.40142321\",\"price_date\":\"2022-06-08T00:00:00Z\",\"price_timestamp\":\"2022-06-08T15:05:00Z\",\"circulating_supply\":\"48343101197\",\"max_supply\":\"99989535142\",\"market_cap\":\"19406042903\",\"market_cap_dominance\":\"0.0150\",\"num_exchanges\":\"305\",\"num_pairs\":\"2881\",\"num_pairs_unmapped\":\"121\",\"first_candle\":\"2013-05-09T00:00:00Z\",\"first_trade\":\"2013-05-09T00:00:00Z\",\"first_order_book\":\"2018-08-29T00:00:00Z\",\"rank\":\"7\",\"rank_delta\":\"0\",\"high\":\"2.75927229\",\"high_timestamp\":\"2018-01-07T00:00:00Z\",\"30d\":{\"volume\":\"66921083358.94\",\"price_change\":\"-0.087212743\",\"price_change_pct\":\"-0.1785\",\"volume_change\":\"5120145890.53\",\"volume_change_pct\":\"0.0828\",\"market_cap_change\":\"-4216134462.52\",\"market_cap_change_pct\":\"-0.1785\"}},{\"id\":\"SOL\",\"currency\":\"SOL\",\"symbol\":\"SOL\",\"name\":\"Solana\",\"logo_url\":\"https://nomics-api.s3.us-east-2.amazonaws.com/static/images/currencies/SOL2.jpg\",\"status\":\"active\",\"platform_currency\":\"SOL\",\"price\":\"39.62669262\",\"price_date\":\"2022-06-08T00:00:00Z\",\"price_timestamp\":\"2022-06-08T15:05:00Z\",\"circulating_supply\":\"341751135\",\"max_supply\":\"508180964\",\"market_cap\":\"13542467159\",\"market_cap_dominance\":\"0.0105\",\"num_exchanges\":\"135\",\"num_pairs\":\"3205\",\"num_pairs_unmapped\":\"1336\",\"first_candle\":\"2019-06-07T00:00:00Z\",\"first_trade\":\"2020-04-10T00:00:00Z\",\"first_order_book\":\"2020-04-07T00:00:00Z\",\"rank\":\"9\",\"rank_delta\":\"0\",\"high\":\"259.02891864\",\"high_timestamp\":\"2021-11-06T00:00:00Z\",\"30d\":{\"volume\":\"107547737068.47\",\"price_change\":\"-22.94833605\",\"price_change_pct\":\"-0.3667\",\"volume_change\":\"1025182606.77\",\"volume_change_pct\":\"0.0096\",\"market_cap_change\":\"-7521705009.25\",\"market_cap_change_pct\":\"-0.3571\"}},{\"id\":\"SHIB\",\"currency\":\"SHIB\",\"symbol\":\"SHIB\",\"name\":\"Shiba Inu\",\"logo_url\":\"https://s3.us-east-2.amazonaws.com/nomics-api/static/images/currencies/SHIB.png\",\"status\":\"active\",\"platform_currency\":\"ETH\",\"price\":\"0.000010724779\",\"price_date\":\"2022-06-08T00:00:00Z\",\"price_timestamp\":\"2022-06-08T15:05:00Z\",\"circulating_supply\":\"556363639382376\",\"max_supply\":\"999991495840789\",\"market_cap\":\"5966876900\",\"market_cap_dominance\":\"0.0046\",\"num_exchanges\":\"146\",\"num_pairs\":\"368\",\"num_pairs_unmapped\":\"45\",\"first_candle\":\"2020-07-31T00:00:00Z\",\"first_trade\":\"2020-07-31T00:00:00Z\",\"first_order_book\":\"2021-02-01T00:00:00Z\",\"rank\":\"19\",\"rank_delta\":\"0\",\"high\":\"0.000079562104\",\"high_timestamp\":\"2021-10-27T00:00:00Z\",\"30d\":{\"volume\":\"19998211864.66\",\"price_change\":\"-0.0000033442714\",\"price_change_pct\":\"-0.2377\",\"volume_change\":\"-8699171738.18\",\"volume_change_pct\":\"-0.3031\",\"market_cap_change\":\"-1816356894.87\",\"market_cap_change_pct\":\"-0.2334\"}}]";
            bool running = true;


            while (running)
            {
                Console.WriteLine("========== Crypto Tracker ==========");
                Console.WriteLine("1. Top 10 kryptoměn");
                Console.WriteLine("2. Vyhledávání (BTC, ADA, ...) ");
                Console.WriteLine("3. Ukončít");

                Console.Write("\nVolba: ");

                int userChoice = int.Parse(Console.ReadLine());

                switch (userChoice)
                {
                    case 1:
                        dynamic data = JsonNode.Parse(await GetTopTen());
                        foreach (var item in data)
                        {
                            Console.WriteLine($"Název: {item["name"]}");
                            Console.WriteLine($"Cena: ${decimal.Round(decimal.Parse(item["price"].ToString(), NumberStyles.Float, CultureInfo.InvariantCulture), 2)}");
                            Console.WriteLine($"Cirkulující zásoba: {item["circulating_supply"]}");
                            Console.WriteLine($"Max. zásoba: {item["max_supply"]}");
                            Console.WriteLine($"Rank: {item["rank"]}");
                        }
                        return;

                    case 2:
                        return;

                    case 3:
                        running = false;
                        return;
                }
            }
        }

        static async Task<string> GetTopTen()
        {
            var client = new RestClient("https://api.nomics.com/v1/");
            var request = new RestRequest("currencies/ticker");
            request.AddQueryParameter("key", "8aec736bde47fc88b2dfce890bd3c5bf00717dd2");
            request.AddQueryParameter("interval", "30d");
            request.AddQueryParameter("convert", "USD");
            request.AddQueryParameter("page", "1");
            request.AddQueryParameter("per-page", "10");
            request.AddQueryParameter("sort", "rank");
            var response = await client.GetAsync(request);

            return response.Content;
        }
    }
}