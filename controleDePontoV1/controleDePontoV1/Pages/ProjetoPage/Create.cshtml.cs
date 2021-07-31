using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using controleDePontoV1.Context;
using controleDePontoV1.Models;

namespace controleDePontoV1.Pages.ProjetoPage
{
    public class CreateModel : PageModel
    {
        private readonly controleDePontoV1.Context.PontoDBContext _context;

        public CreateModel(controleDePontoV1.Context.PontoDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Projeto Projeto { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.projetos.Add(Projeto);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
