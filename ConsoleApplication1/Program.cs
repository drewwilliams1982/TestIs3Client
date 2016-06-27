
using System;
using System.Data.SqlClient;
using System.Net.Http;
using System.Transactions;
using IdentityModel.Client;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = GetClientToken();
            CallApi(t);
        }

        static TokenResponse GetClientToken()
        {
            var client = new TokenClient(
                "http://new/main/sso/idp/connect/token",
                "silicon",
                "F621F470-9731-4A25-80EF-67A6F7C5F4B8");
            
            return client.RequestClientCredentialsAsync("isamsscope").Result;
        }

        static void CallApi(TokenResponse response)
        {
            var client = new HttpClient();
            client.SetBearerToken(response.AccessToken);

            var req = client.GetStringAsync("http://new/Main/newapi/private/framework/prereqstatus");

            Console.WriteLine(req.Result);
        }
    }
    
}
