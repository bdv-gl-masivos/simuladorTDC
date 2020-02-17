using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WPFUI.Models
{
    public class SqliteDataAccess
    {

        public static List<ConsolidadoAcrModel> CargaCedula(string CEDULA)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ConsolidadoAcrModel>("Select * from CONSOLIDADO_ACR where CEDULA = '" + CEDULA + "' ORDER BY PRODUCTO", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<ConsolidadoAcrClientModel> CargaCedulaFull(string CEDULA)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ConsolidadoAcrClientModel>("Select * from CONSOLIDADO_ACR_CLIENTE where CEDULA = '" + CEDULA + "'", new DynamicParameters());
                return output.ToList();
            }

        }

        public static List<SaldosTdcModel> SaldosTdcs(string CEDULA)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<SaldosTdcModel>("Select * from SALDOS_TDC_ADMISION_BDV where CEDULA = '" + CEDULA + "'", new DynamicParameters());
                return output.ToList();
            }

        }

        public static List<NominaBdvModel> CargaCedulaEmp(string CEDULA)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<NominaBdvModel>("Select * from NOMINA_BDV where CEDULA = '" + CEDULA + "'", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void Archivar(string NM, string CEDULA, string NOMBRE, string FECHA_HORA, 
            double LIMITE_CONSOLIDADO, string REFERIDO, string OBSERVACION, string EMPLEADO, 
            string REFERIDO_POR, string CARGAS_BDV, string INGRESOS, string OTRAS_CARGAS, 
            double LIMITE_1, double LIMITE_2, double LIMITE_3, double LIMITE_4, string NRO_TDC_1, 
            string NRO_TDC_2, string NRO_TDC_3, string NRO_TDC_4, string CALIFICA_ARCHIVO)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into ARCHIVO_TDC (NM, CEDULA, NOMBRE, FECHA_HORA, LIMITE_CONSOLIDADO, OBSERVACION, " +
                    "REFERIDO, EMPLEADO, REFERIDO_POR, CARGAS_BDV, INGRESOS, OTRAS_CARGAS, LIMITE_1, LIMITE_2, LIMITE_3, LIMITE_4, " +
                    "NRO_TDC_1, NRO_TDC_2, NRO_TDC_3, NRO_TDC_4, CALIFICA_ARCHIVO) values ('" + NM + "', '" + CEDULA + "', '" + NOMBRE + "', '" + 
                    FECHA_HORA + "', " + LIMITE_CONSOLIDADO + "," +
                    "'" + OBSERVACION + "', '" + REFERIDO + "', '" + EMPLEADO + "', '" + 
                    REFERIDO_POR + "', " + CARGAS_BDV + ", " + INGRESOS + ", " + OTRAS_CARGAS + ", " +
                    "" + LIMITE_1 + ", " + LIMITE_2 + ", " + LIMITE_3 + ", " + LIMITE_4 + ", '" + NRO_TDC_1 + 
                    "', '" + NRO_TDC_2 + "', '" + NRO_TDC_3 + "', '" + NRO_TDC_4 + "'," +
                    "'" + CALIFICA_ARCHIVO + "')");
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
