using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
