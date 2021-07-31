using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace controleDePontoV1.Models
{
    public class ControleApontamento
    {
        [Key]
        public int codigo_Projeto { get; set; }
        [Key]
        public int codigo_Equipe { get; set; }
        [Key]
        public int codigo_Colaborador { get; set; }
        [Key]
        public DateTime dia_Marcao { get; set; }
        public DateTime dia_Fim { get; set; }
    }
}
