using ApiR.Entity;
using System;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ApiR.Data.Repositories
{
    public class CreditoRepository:ICreditoRepository
    {
        private Conexion _connectionString;
        public CreditoRepository(Conexion connectionString)
        {
            _connectionString = connectionString;
        }

        protected Conexion dbConnection()
        {
            return new Conexion(_connectionString.ConexionString);
        }

        public async Task<bool> InsertCredit(Credito credito)
        {
            using (var oconexion = new SqlConnection(dbConnection().ConexionString))
            {
                string procedure = "SP_REGISTRARCREDITO";
                oconexion.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@IdCliente", credito.cliente.IdCliente);
                parameters.Add("@Plazo", credito.Plazo);
                parameters.Add("@Monto", credito.Monto);
                parameters.Add("@Producto", credito.Producto);
                parameters.Add("@FechaDesembolso", credito.FechaDesembolso);
                parameters.Add("@FechaPrimeraCuota", credito.FechaPrimeraCuota);
  

                var result = await oconexion.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }
    }
}
