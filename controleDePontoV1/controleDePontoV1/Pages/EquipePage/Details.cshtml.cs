using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using controleDePontoV1.Context;
using controleDePontoV1.Models;

namespace controleDePontoV1.Pages.EquipePage
{
    public class DetailsModel : PageModel
    {
        private readonly controleDePontoV1.Context.PontoDBContext _context;

        public DetailsModel(controleDePontoV1.Context.PontoDBContext context)
        {
            _context = context;
        }

        public Equipe Equipe { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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
    }
}
