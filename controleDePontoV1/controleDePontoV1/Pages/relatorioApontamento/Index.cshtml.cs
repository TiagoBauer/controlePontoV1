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
        public IList<RelatorioApt> reportApontamento { get; set; }

        public Apontamento apt { get; set; }
        public RelatorioApt rApt { get; set; }

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

        public class RelatorioApt
        {
            [Display(Name = "Projeto")]
            public int codigo_Projeto { get; set; }
            [Display(Name = "Equipe")]
            public int codigo_Equipe { get; set; }
            [Display(Name = "Minutos trabalhadas")]
            public int minTrab { get; set; }
            [Display(Name = "Minutos trabalhadas")]
            public int horTrab { get; set; }
        }

        public IndexModel(HttpClient httpclient)
        {
            _client = httpclient;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(bool backToMenu)
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
                reportApontamento = new List<RelatorioApt>();
                string baseUrl = configuration.GetConnectionString("baseUrl");
                string url = "";

                if (apt.codigo_Projeto > 0)
                {
                    url += "&codigoProjeto=" + apt.codigo_Projeto;
                }
                if (apt.codigo_Equipe > 0)
                {
                    url += "&codigoEquipe=" + apt.codigo_Equipe;
                }
                if (apt.codigo_Colaborador > 0)
                {
                    url += "&codigoColaborador=" + apt.codigo_Colaborador;
                }
                if (apt.dia_Inicial > DateTime.MinValue)
                {
                    url += "&dataInicial=" + apt.dia_Inicial;
                }
                if (apt.dia_final > DateTime.MinValue)
                {
                    url += "&DataFinal=" + apt.dia_final;
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

                reportApontamento = returnTotalHoursProjetcTeam(apontamento);

                return Page();
            }
            catch (Exception)
            {
                return Page();
            }
        }
        public IList<RelatorioApt> returnTotalHoursProjetcTeam(IList<ControleApontamento> apt)
        {
            var oldProject = 0;
            var oldTeam = 0;
            RelatorioApt temp = null;
            IList<RelatorioApt> newList = new List<RelatorioApt>();
            foreach (var cP in apt)
            {

                if ((cP.codigo_Projeto != oldProject) || (cP.codigo_Equipe != oldTeam))
                {
                    temp = new RelatorioApt();
                    temp.codigo_Projeto = cP.codigo_Projeto;
                    temp.codigo_Equipe = cP.codigo_Equipe;
                    if (cP.dia_Fim == DateTime.MinValue)
                    {
                        cP.dia_Fim = new DateTime();
                    }
                    TimeSpan ts = cP.dia_Fim - cP.dia_Marcao;
                    temp.minTrab = (int) (ts.Minutes);
                    temp.horTrab = (int) ts.TotalHours;
                    newList.Add(temp);
                }
                else
                {

                    foreach (var nL in newList.Where(w =>
                                             w.codigo_Projeto == cP.codigo_Projeto &&
                                             w.codigo_Equipe == cP.codigo_Equipe))
                    {
                        if (cP.dia_Fim == DateTime.MinValue)
                        {
                            cP.dia_Fim = new DateTime();
                        }
                        TimeSpan ts = cP.dia_Fim - cP.dia_Marcao;
                        nL.minTrab += (int)(ts.Minutes);
                        if(nL.minTrab >= 60)
                        {
                            nL.horTrab++;
                            nL.minTrab -= 60;
                        }
                        nL.horTrab += (int)ts.TotalHours;
                    };

                } 

                
                oldProject = cP.codigo_Projeto;
                oldTeam = cP.codigo_Equipe;
            }            

            return newList;
        }
    }
}












