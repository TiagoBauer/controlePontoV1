using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using controleDePontoV1.Context;
using controleDePontoV1.Models;

namespace controleDePontoV1.Pages.PontoPage
{
    public class DetailsModel : PageModel
    {
        private readonly controleDePontoV1.Context.PontoDBContext _context;

        public DetailsModel(controleDePontoV1.Context.PontoDBContext context)
        {
            _context = context;
        }

        public ControleApontamento ControleApontamento { get; set; }

        public async Task<IActionResult> OnGetAsync(int projeto, int equipe, int colaborador, DateTime diaMarcacao)
        {
            ControleApontamento cP = _context.controleApontamento.Find(projeto, equipe, colaborador, diaMarcacao); 
            if (cP == null)
            {
                return NotFound();
            }

            ControleApontamento = await _context.controleApontamento.FirstOrDefaultAsync(m => m == cP);

            if (ControleApontamento == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
