using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using controleDePontoV1.Context;
using controleDePontoV1.Models;

namespace controleDePontoV1.Pages.PapelPage
{
    public class IndexModel : PageModel
    {
        private readonly controleDePontoV1.Context.PontoDBContext _context;

        public IndexModel(controleDePontoV1.Context.PontoDBContext context)
        {
            _context = context;
        }

        public IList<Papel> Papel { get;set; }

        public async Task OnGetAsync()
        {
            Papel = await _context.papeis.ToListAsync();
        }
    }
}
