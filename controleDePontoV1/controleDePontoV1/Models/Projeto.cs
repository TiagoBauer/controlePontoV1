using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace controleDePontoV1.Models
{
    public class Projeto
    {
        [Key]
        public int codigo { get; set; }
        public string descrição { get; set; }
    }
}
