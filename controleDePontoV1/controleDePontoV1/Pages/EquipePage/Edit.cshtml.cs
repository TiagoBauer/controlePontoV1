using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using controleDePontoV1.Context;
using controleDePontoV1.Models;

namespace controleDePontoV1.Pages.EquipePage
{
    public class EditModel : PageModel
    {
        private readonly controleDePontoV1.Context.PontoDBContext _context;

        public EditModel(controleDePontoV1.Context.PontoDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Equipe Equipe { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool backToMenu)
        {
            if (backToMenu)
            {
                return Redirect("~/Menu");
            }
            if (id == null)
            {
                return NotFound();
            }

            Equipe = await _context.equipes.FirstOrDefaultAsync(m => m.codigo == id);

            if (Equipe == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Equipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipeExists(Equipe.codigo))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EquipeExists(int id)
        {
            return _context.equipes.Any(e => e.codigo == id);
        }
    }
}
