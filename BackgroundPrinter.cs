using System.Text;
using System.Text.Json;
using Entrytask_Urban.Repository;
using System.Net.Http.Headers;
using Microsoft.DotNet.MSIdentity.Shared;
using Entrytask_Urban.Pages;
using Root_Namespace.Pages;
using Microsoft.AspNetCore.Mvc;
using Entrytask_Urban.Services;
using NuGet.Common;
using Microsoft.Extensions.DependencyInjection;
using Entrytask_Urban.Models;
using Microsoft.Extensions.Logging;
using System.Linq;
using Newtonsoft.Json;

using NuGet.DependencyResolver;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Azure;

namespace Entrytask_Urban
{

    public class BackgroundPrinter : BackgroundService
    {
        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("http://localhost:27807"),
        };
        private static bool hasran = false;
        private readonly ILogger<BackgroundPrinter> logger;
        private readonly IServiceProvider serviceProvider;
        public BackgroundPrinter(ILogger<BackgroundPrinter> logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
            //_loginModel = loginModel;
        }
        string lasttoken = System.IO.File.ReadAllText(@"C:\Users\gr8tk\Desktop\praca\Entrytask_Urban\Repository\tokenList.json");

        /* public BackgroundPrinter(ILogger<BackgroundPrinter> logger, GetSalesTransactionsDB context)
         {
             this.logger = logger;
             _context = context;
         }*/

        private readonly PeriodicTimer _timer = new(TimeSpan.FromMilliseconds(6000));
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)

        {
            /*
          logger.LogInformation("tusom");
          var result = await loginModel.OnPostLogin(sharedClient, "michal", "123", token);

               if (result is ContentResult contentResult && contentResult.Content is string newToken)
               {
                   // Store the token and break the loop
                   token = newToken;
              logger.LogInformation("ddd");
              logger.LogInformation($"{token}\n");
              break;
               }*/


            while (await _timer.WaitForNextTickAsync(stoppingToken)
                && !stoppingToken.IsCancellationRequested)
            {
                string token = System.IO.File.ReadAllText(@"C:\Users\gr8tk\Desktop\praca\Entrytask_Urban\Repository\tokenList.json");

                if (token != lasttoken)
                {
                    logger.LogInformation($"{token}\n");
                    token = token.Remove(0, 3);
                    token = token.Remove(213, 3);
                    logger.LogInformation($"{token}\n");
                    await Task.Delay(200);
                    await GetAsync(sharedClient, logger, token);
                    await Task.Delay(200);
                    await PostAsync(logger, token);

                }

                else
                {
                    logger.LogInformation("User not authenticated\n");
                }
            }
        }
        /*

        public static async Task Postcredentials(HttpClient httpClient, ILogger<BackgroundPrinter> logger)
        {



                using StringContent jsonContent = new(
                    JsonSerializer.Serialize(new
                    {
                        Username = "michal",
                        Password = "123"
                    }),
                    Encoding.UTF8,
                    "application/json"

                    );


                using HttpResponseMessage response = await httpClient.PostAsync(
                    "/login",
                    jsonContent);


                response.EnsureSuccessStatusCode();
                //.WriteRequestToConsole();

                string newToken = await response.Content.ReadAsStringAsync();
                logger.LogInformation($"{newToken}\n");
                generatedToken = newToken;
                generatedToken = generatedToken.Remove(0, 1);
                generatedToken = generatedToken.Remove(213, 1);



                //await PostAsync(sharedClient);
                await Task.Delay(1000);
                //string token = newToken;
                //logger.LogInformation($"{token}\n");
                //private string token = newToken;





        }*/

        public static async Task PostAsync(ILogger<BackgroundPrinter> logger, string generatedToken)
        {
            //{ 


            using (HttpClient client = new HttpClient())
            {
                // Set the bearer token in the Authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", generatedToken);

                // Prepare the request payload
                var requestData = new { };
                var jsonPayload = JsonSerializer.Serialize(requestData);
                //var jsonPayload = NewtonsoftJson.Serialize(requestData);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                // Send the POST request
                HttpResponseMessage responsesales = await client.PostAsync("http://localhost:27807/create", content);
                HttpResponseMessage responsesalelines = await client.PostAsync("http://localhost:27807/createsalines", content);

                // Check the response status
                //responsesales.EnsureSuccessStatusCode();
                //responsesalelines.EnsureSuccessStatusCode();
                if (responsesales.IsSuccessStatusCode && responsesalelines.IsSuccessStatusCode)
                {
                    logger.LogInformation($"Request succes with status code: {responsesales.StatusCode}");
                    logger.LogInformation($"Request succes with status code: {responsesalelines.StatusCode}");
                }


                else
                {
                    logger.LogInformation($"Request failed with status code: {responsesales.StatusCode}");
                    logger.LogInformation($"Request failed with status code: {responsesalelines.StatusCode}");
                    logger.LogInformation($"token is: {generatedToken}");

                }
            }
            /*
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                }),
                Encoding.UTF8,
                "application/json"

                );
            //ContentDispositionHeaderValue contentDisposition = new ContentDispositionHeaderValue("Authorization", "Bearer " + generatedToken);
            AuthenticationHeaderValue.Equals("Authorization", "Bearer " + generatedToken);
            // httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            //httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + generatedToken);
            using HttpResponseMessage response = await httpClient.PostAsync(
                    "/create",
                    jsonContent);


            response.EnsureSuccessStatusCode();
            //.WriteRequestToConsole();

            //var jsonResponse = await response.Content.ReadAsStringAsync();
            //Console.WriteLine($"{jsonResponse}\n");
            */

        }

        public static async Task GetAsync(HttpClient httpClient, ILogger<BackgroundPrinter> logger, string generatedToken)
        {
            /* using HttpResponseMessage response = await httpClient.GetAsync("/get?transactionkey=" + SalesRepository.GetSalesTransactionsList.Count.ToString());
             httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
             httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + generatedToken);
             response.EnsureSuccessStatusCode();
             var jsonResponse = await response.Content.ReadAsStringAsync();
             logger.LogInformation($"{jsonResponse}\n");*/





            using (HttpClient client = new HttpClient())
            {
                // Set the bearer token in the Authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", generatedToken);

                // Send the GET request
                HttpResponseMessage response = await client.GetAsync("http://localhost:27807/get?transactionkey=" + SalesRepository.GetSalesTransactionsList.Count.ToString());

                // Check the response status
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    logger.LogInformation($"{jsonResponse}\n");



                }
                else
                {
                    logger.LogInformation($"Request failed with status code: {response.StatusCode}");

                }

            }





        }

    }

}
