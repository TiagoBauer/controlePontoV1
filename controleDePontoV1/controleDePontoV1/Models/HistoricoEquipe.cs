using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace controleDePontoV1.Models
{
    public class HistoricoEquipe
    {
        [Key]
        public int codigo_Colaborador { get; set; }
        [Key]
        public int codigo_Equipe { get; set; }
        public DateTime dataDaAlteracao { get; set; }
    }
}
