using System;
using System.ServiceModel;
using System.Configuration;

using Microsoft.Owin.Hosting;

using UserRepositoryServiceApp.Services;

namespace UserRepositoryServiceApp
{
    public class Program
    {
        /// <summary>
        /// The base address
        /// </summary>
        static string BaseAddress = ConfigurationManager.AppSettings["BaseAddress"];

        static void Main(string[] args)
        {
            using (WebApp.Start<AppStartup>(BaseAddress))
            using (ServiceHost host = new ServiceHost(typeof(UserInfoProviderService), new Uri($"{BaseAddress}UserInfoProviderService")))
            {
                host.Open();

                Console.WriteLine($"Services have been launched at {BaseAddress}. Press any key to exit");
                Console.ReadKey();

                host.Close();
            }
        } 
    }
}
