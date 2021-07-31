using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using controleDePontoV1.Context;
using controleDePontoV1.Models;

namespace controleDePontoV1.Pages.ProjetoPage
{
    public class DeleteModel : PageModel
    {
        private readonly controleDePontoV1.Context.PontoDBContext _context;

        public DeleteModel(controleDePontoV1.Context.PontoDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Projeto Projeto { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Projeto = await _context.projetos.FirstOrDefaultAsync(m => m.codigo == id);

            if (Projeto == null)
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

            Projeto = await _context.projetos.FindAsync(id);

            if (Projeto != null)
            {
                _context.projetos.Remove(Projeto);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
