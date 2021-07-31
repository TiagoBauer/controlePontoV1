using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace controleDePontoV1.Pages.Menu
{
    public class IndexModel : PageModel
    {
        public MenuController menuController { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost(string newPage)
        {
            
            if (newPage == "Colaboradores")
            {
                return Redirect("~/ColaboradorPage");
            } else if (newPage == "Equipes")
            {
                return Redirect("~/EquipePage");
            }
            else if (newPage == "Papeis")
            {
                return Redirect("~/PapelPage");
            }
            else if (newPage == "Projetos")
            {
                return Redirect("~/ProjetoPage");
            }
            else if (newPage == "Apontamentos")
            {
                return Redirect("~/PontoPage");
            }
            else
            {
                return Redirect("~/Menu");
            }
        }

        public class MenuController
        {
            public int codigo { get; set; }

        }
    }
}
