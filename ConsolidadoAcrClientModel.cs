using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.Models
{
    public class ConsolidadoAcrClientModel
    {
        public string CEDULA { get; set; }
        public double CARGA_BDV { get; set; }
        public int NRO_PRODUCTOS { get; set; }
        public int MAX_MORA { get; set; }
        public double MES_1 { get; set; }
        public double MES_2 { get; set; }
        public double MES_3 { get; set; }
        public double SALARIO { get; set; }
        public string RIF_EMPRESA { get; set; }
        public string NOMBRE_EMPRESA { get; set; }
        public int FEVE_BIN { get; set; }
        public int BLQ_TDC_BIN { get; set; }
        public int MARCA_TDC_BIN { get; set; }
        public int MORA_30_BIN { get; set; }
        public int CASTIGO_ACT_BIN { get; set; }
        public int CASTIGO_VCT_BIN { get; set; }
        public int MARCA_BIN { get; set; }
        public int CONSOLIDADO { get; set; }

    }
}
