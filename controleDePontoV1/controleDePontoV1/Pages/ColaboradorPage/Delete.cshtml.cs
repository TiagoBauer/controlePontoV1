using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using controleDePontoV1.Context;
using controleDePontoV1.Models;

namespace controleDePontoV1.Pages.ColaboradorPage
{
    public class DeleteModel : PageModel
    {
        private readonly controleDePontoV1.Context.PontoDBContext _context;

        public DeleteModel(controleDePontoV1.Context.PontoDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Colaborador Colaborador { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Colaborador = await _context.colaboradores.FirstOrDefaultAsync(m => m.codigo == id);

            if (Colaborador == null)
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

            Colaborador = await _context.colaboradores.FindAsync(id);

            if (Colaborador != null)
            {
                _context.colaboradores.Remove(Colaborador);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
