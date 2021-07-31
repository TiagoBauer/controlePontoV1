using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using controleDePontoV1.Context;
using controleDePontoV1.Models;

namespace controleDePontoV1.Pages
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
        public Papel Papel { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.papeis.Add(Papel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
