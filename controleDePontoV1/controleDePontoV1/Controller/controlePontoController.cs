using controleDePontoV1.Context;
using controleDePontoV1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace controleDePontoV1.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class controlePontoController : ControllerBase
    {
        private readonly PontoDBContext _pontoDBContext;

        public controlePontoController(PontoDBContext pontoDBContext)
        {
            _pontoDBContext = pontoDBContext;
        }

        [HttpGet]
        [Route("api/logon")]
        public async Task<IActionResult> GetLogon(int codigo, string password)
        {
            bool login = false;
            var context = _pontoDBContext;
            {
                try
                {
                    var colaborador = context.colaboradores
                                    .Single(b => b.password == password && b.codigo == codigo);
                    if (colaborador.codigo != 0)
                    {
                        login = true;
                    }
                    else
                    {
                        login = false;
                    }
                } catch
                {
                    login = false;
                }
                
            }
            return Ok(new
            {
                success = login,
            }); ;
        }

        /// <summary>
        /// Inclui papéis na corporação
        /// </summary>
        /// <param name="papeis"></param>
        /// <returns> Papel incluído </returns>
        /// 
        [HttpPost]
        [Route("api/papeis")]
        public async Task<IActionResult> SetPapeis(Papel papeis)
        {
            _pontoDBContext.papeis.Add(papeis);
            await _pontoDBContext.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                data = papeis
            });
        }

        [HttpPost]
        [Route("api/equipe")]
        public async Task<IActionResult> SetEquipe(Equipe equipes)
        {
            _pontoDBContext.equipes.Add(equipes);
            await _pontoDBContext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                data = equipes
            });
        }

        [HttpPost]
        [Route("api/colaborador")]
        public async Task<IActionResult> SetPapeis(Colaborador colaborador)
        {
            _pontoDBContext.colaboradores.Add(colaborador);
            await _pontoDBContext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                data = colaborador
            });
        }
    }
}
