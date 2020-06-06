using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace ProductosMeLi
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Hace falta por lo menos un parametro.");
                Console.Read();
            }
            var users = new List<dynamic>();
            foreach(string user in args)
            {
                if(user.Split('~').Length != 2)
                {
                    users.Clear();
                    Console.WriteLine("Error de parametros.");
                    Console.Read();
                    break;
                }
                else
                {
                    users.Add(new { sellerId = user.Split('~')[0], siteId = user.Split('~')[1]});
                }
            }
            generateLogProdsBySeller(users);
        }

        static void generateLogProdsBySeller(List<dynamic> users)
        {
            try
            {
                foreach (dynamic user in users)
                {
                    string uri = "https://api.mercadolibre.com/sites/" + user.siteId + "/search?seller_id=" + user.sellerId;
                    var result = getResultFromApi(uri);
                    StringBuilder sb = new StringBuilder();
                    foreach (dynamic a in result.results)
                    {
                        var categoryName = getCategoryNameById(a.category_id.ToString());
                        string linea = a.id + "|" + a.title + "|" + a.category_id + "|" + categoryName;
                        sb.AppendLine(linea);
                    }
                    File.WriteAllText("./log_" + user.sellerId + ".log", sb.ToString());
                }
            }catch(Exception ex)
            {
                Console.WriteLine("Error. " + ex.Message);
                Console.Read();
            }
        }

        static string getCategoryNameById(string categoryID)
        {
            string uri = "https://api.mercadolibre.com/categories/" + categoryID;
            dynamic result = getResultFromApi(uri);
            return result.name;
        }

        static dynamic getResultFromApi(string uri)
        {
                var client = new HttpClient();
                Task<HttpResponseMessage> response = client.GetAsync(uri);
                response.Wait();
                var content = response.Result.Content.ReadAsStringAsync();
                content.Wait();
                return JsonConvert.DeserializeObject<dynamic>(content.Result);            
        }
    }
}
