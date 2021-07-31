using controleDePontoV1.Controller;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
 

namespace controleDePontoV1.Pages
{
    [BindProperties]

    public class IndexModel : PageModel
    {
        private HttpClient _client;
        public IndexModel(HttpClient httpclient)
        {
            _client = httpclient;
        }

        public Logon logon { get; set; }

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            IConfiguration configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", false, true)
           .Build();

            try
            {
                string baseUrl = configuration.GetConnectionString("baseUrl");
                string url = baseUrl + "/logon?codigo=" + logon.codigo + "&password=" + logon.password;
                var response = await _client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                JObject obj = JObject.Parse(result);
                bool Jfield = (bool)obj["success"];
                if ((obj != null) && (Jfield == true))
                {
                    return Redirect("~/Menu");
                }
                else
                {
                    return Content("Logon inválido");
                }
            } catch (Exception)
            {
                return Content("Erro");
            }
        }

        public class Logon
        {
            [Required]
            [Display(Name = "User Name")]
            public int codigo { get; set; }
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string password { get; set; }

        }
    }
}
