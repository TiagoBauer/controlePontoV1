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

namespace controleDePontoV1.Pages.PontoPage
{
    public class EditModel : PageModel
    {
        private readonly controleDePontoV1.Context.PontoDBContext _context;

        public EditModel(controleDePontoV1.Context.PontoDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ControleApontamento ControleApontamento { get; set; }

        public async Task<IActionResult> OnGetAsync(int projeto, int equipe, int colaborador, DateTime diaMarcacao, bool backToMenu)
        {
            if (backToMenu)
            {
                return Redirect("~/Menu");
            }
            ControleApontamento cP = _context.controleApontamento.Find(projeto, equipe, colaborador, diaMarcacao);
            if (cP == null)
            {
                return NotFound();
            }

            ControleApontamento = await _context.controleApontamento.FirstOrDefaultAsync(
                m => m == cP);

            if (ControleApontamento == null)
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

            _context.Attach(ControleApontamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ControleApontamentoExists(ControleApontamento.codigo_Projeto,
                    ControleApontamento.codigo_Equipe,
                    ControleApontamento.codigo_Colaborador,
                    ControleApontamento.dia_Marcao))
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

        private bool ControleApontamentoExists(int projeto, int equipe, int colaborador, DateTime diaMarcacao)
        {
            ControleApontamento cP = _context.controleApontamento.Find(projeto, equipe, colaborador, diaMarcacao);
            return _context.controleApontamento.Any(e => e == cP);
        }
    }
}
