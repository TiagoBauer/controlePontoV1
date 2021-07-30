using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace controleDePontoV1.Models
{
    public class Colaborador
    {
        [Key]
        public int codigo { get; set; }
        public int active { get; set; }
        public string nome { get; set; }
        public string sobreNome { get; set; }
        public int papel { get; set; }
        public int equipe { get; set; }
        public string password { get; set; }

    }
}
