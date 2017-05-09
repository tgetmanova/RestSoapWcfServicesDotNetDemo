using System;
using System.ServiceModel;

using Microsoft.Owin.Hosting;

using UserRepositoryServiceApp.Services;

namespace UserRepositoryServiceApp
{
    public class Program
    {
        /// <summary>
        /// The base address
        /// </summary>
        static string baseAddress = "http://localhost:2828/";

        static void Main(string[] args)
        {
            using (WebApp.Start<AppStartup>(baseAddress))
            using (ServiceHost host = new ServiceHost(typeof(UserInfoProviderService), new Uri($"{baseAddress}UserInfoProviderService")))
            {
                host.Open();

                Console.WriteLine($"Services have been launched at {baseAddress}. Press any key to exit");
                Console.ReadKey();

                host.Close();
            }
        } 
    }
}
