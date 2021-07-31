using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using controleDePontoV1.Context;
using controleDePontoV1.Models;

namespace controleDePontoV1.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly controleDePontoV1.Context.PontoDBContext _context;

        public DetailsModel(controleDePontoV1.Context.PontoDBContext context)
        {
            _context = context;
        }

        public Papel Papel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Papel = await _context.papeis.FirstOrDefaultAsync(m => m.codigo == id);

            if (Papel == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
