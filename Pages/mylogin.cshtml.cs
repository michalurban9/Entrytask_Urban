using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Entrytask_Urban.Models;
using Entrytask_Urban.Repository;
using Entrytask_Urban.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using NuGet.Protocol;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Entrytask_Urban.Pages
{
    
    public class myloginModel : PageModel 
    {
       
        /*private readonly TokenRepository tokenRepository;

        public myloginModel( TokenRepository tokenRepository)
        {
          
            this.tokenRepository = tokenRepository;
        }*/




        public async Task <IActionResult> OnPostLogin(HttpClient httpClient,string username,string password)
        {
            using StringContent jsonContent = new(
                    //JsonSerializer.Serialize(new
                    JsonSerializer.Serialize(new
                    {
                        Username = username,
                        Password = password
                    }),
                    Encoding.UTF8,
                    "application/json"
            );


            using HttpResponseMessage response = await httpClient.PostAsync(
                "http://localhost:27807/login",
                jsonContent);

            if (response.IsSuccessStatusCode.Equals(true))
            {
                string newToken = await response.Content.ReadAsStringAsync();
                string json = JsonConvert.SerializeObject(newToken);
                System.IO.File.WriteAllText(@"C:\Users\gr8tk\Desktop\praca\Entrytask_Urban\Repository\tokenList.json", json);

                return RedirectToPage("/error");
            }

            else
            {
                return RedirectToPage("/privacy");
            }
        }
    }
}