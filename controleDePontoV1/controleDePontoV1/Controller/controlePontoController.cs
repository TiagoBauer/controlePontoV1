using controleDePontoV1.Context;
using controleDePontoV1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

        [HttpPost]
        [Route("api/returnWork")]
        public async Task<ActionResult> returnWorkHours(int codigoProjeto, int codigoEquipe, int codigoColaborador, DateTime dataInicial, DateTime DataFinal)
        {
            string query = "SELECT codigo_Projeto, codigo_Equipe, codigo_colaborador, dateTime, dia_Fim FROM controleApontamento ";
            string where = "";
            if ((codigoProjeto != 0) && (codigoProjeto != null))
            {
                where += " AND codigo_Projeto = " + codigoProjeto;
            }

            if ((codigoEquipe != 0) && (codigoEquipe != null))
            {
                where += " AND codigo_Equipe = " + codigoEquipe;
            }

            if ((codigoColaborador != 0) && (codigoColaborador != null))
            {
                where += " AND codigo_Colaborador = " + codigoColaborador;
            }

            if((dataInicial != null) && (dataInicial != DateTime.MinValue))
            {
                where += " AND dateTime >= '" + dataInicial + "'";
            }

            if ((DataFinal != null) && (DataFinal != DateTime.MinValue))
            {
                where += " AND dia_Fim <= '" + DataFinal + "'";
            }

            if(where != "")
            {
                query += " WHERE " + where.Substring(4, (where.Length-4));
            }
                
            List<ControleApontamento> result = await _pontoDBContext.controleApontamento
                                                                     .FromSqlRaw(query)
                                                                     .ToListAsync();
            if(result.Count > 0)
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            } else
            {
                return Ok(new
                {
                    success = false,
                    data = result
                });
            }
            
        }

    }
}
