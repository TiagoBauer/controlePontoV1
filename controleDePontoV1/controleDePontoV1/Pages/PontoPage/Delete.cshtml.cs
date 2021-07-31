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
    public class DeleteModel : PageModel
    {
        private readonly controleDePontoV1.Context.PontoDBContext _context;

        public DeleteModel(controleDePontoV1.Context.PontoDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ControleApontamento ControleApontamento { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ControleApontamento = await _context.controleApontamento.FirstOrDefaultAsync(m => m.codigo_Colaborador == id);

            if (ControleApontamento == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ControleApontamento = await _context.controleApontamento.FindAsync(id);

            if (ControleApontamento != null)
            {
                _context.controleApontamento.Remove(ControleApontamento);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
