using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using controleDePontoV1.Migrations;
using controleDePontoV1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Web.Helpers;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace controleDePontoV1.Pages.relatorioApontamento
{
    [BindProperties]
    public class IndexModel : PageModel
    {

        private HttpClient _client;

        public IList<ControleApontamento> apontamento { get; set; }


        public Apontamento apt { get; set; }

        public class Apontamento
        {
            [Display(Name = "Projeto")]
            public int codigo_Projeto { get; set; }
            [Display(Name = "Equipe")]
            public int codigo_Equipe { get; set; }
            [Display(Name = "Colaborador")]
            public int codigo_Colaborador { get; set; }
            [Display(Name = "Data inicial")]
            public DateTime dia_Inicial { get; set; }
            [Display(Name = "Data final")]
            public DateTime dia_final { get; set; }

        }

        public IndexModel(HttpClient httpclient)
        {
            _client = httpclient;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(int projeto, int equipe, int colaborador, DateTime dataInicio, DateTime dataFim, bool backToMenu)
        {
            if (backToMenu)
            {
                return Redirect("~/Menu");
            }
            IConfiguration configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", false, true)
           .Build();

            try
            {
                string baseUrl = configuration.GetConnectionString("baseUrl");
                string url = "";

                if (projeto > 0)
                {
                    url += "&codigo_Projeto=" + projeto;
                }
                if (equipe > 0)
                {
                    url += "&codigo_Equipe=" + equipe;
                }
                if (colaborador > 0)
                {
                    url += "&codigo_Colaborador=" + colaborador;
                }
                if (dataInicio > DateTime.MinValue)
                {
                    url += "&dateTime=" + dataInicio;
                }
                if (dataFim > DateTime.MinValue)
                {
                    url += "&dia_Fim=" + dataFim;
                }

                if (url != "")
                {
                    url = baseUrl + "/returnWork?" + url.Substring(1, (url.Length - 1));
                }
                else
                {
                    url = baseUrl + "/returnWork";
                }

                var response = await _client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                apontamento = JsonConvert.DeserializeObject<List<ControleApontamento>>(result);

                return Page();
            }
            catch (Exception)
            {
                return Page();
            }
        }
    }
}












