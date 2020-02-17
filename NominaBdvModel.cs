using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFUI.Models
{
    public class NominaBdvModel
    {
        public string CEDULA { get; set; }
        public string CARGO { get; set; }
        public string NOMBRE { get; set; }
        public string FECHA_ING { get; set; }
        public double SALARIO { get; set; }
        public double DEDUCCIONES { get; set; }
        public double SALDO_CREDINOMINA { get; set; }
        public double CUOTA_HIPOTECARIO { get; set; }
        public double PAGO_MIN_CALC { get; set; }
        public double PAGO_MIN_TDC { get; set; }
        public double PAGO_MIN_TDC_SELECT { get; set; }
        public int CONSOLIDADO { get; set; }
        public double LIMITE_TDC { get; set; }
    }
}
