using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.Models
{
    public class ConsolidadoAcrModel
    {
        public string CEDULA { get; set; }
        public string PRODUCTO { get; set; }
        public string NUMERO_CREDITO { get; set; }
        public string HISTORICO { get; set; }
        public double MONTO_LIQUIDADO { get; set; }
        public string FECHA_LIQUIDACION { get; set; }
        public string FECHA_ULT_AUMENTO { get; set; }
        public int MORA { get; set; }
    }
}
