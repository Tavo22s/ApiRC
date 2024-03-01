using ApiR.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiR.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private Conexion _connectionString;
        public ClienteRepository(Conexion connectionString)
        {
            _connectionString = connectionString;
        }

        protected Conexion dbConnection()
        {
            return new Conexion(_connectionString.ConexionString);
        }

        public async Task<IEnumerable<Cliente>> GetAllClients()
        {
            using (var oconexion = new SqlConnection(dbConnection().ConexionString))
            {
                string query = "SELECT * FROM CLIENTE;";
                oconexion.Open();

                return await oconexion.QueryAsync<Cliente>(query, new { });         
            }
        }

        public async Task<Cliente> GetClientDetails(int id)
        {
            using (var oconexion = new SqlConnection (dbConnection().ConexionString))
            {
                string query = "SELECT * FROM CLIENTE WHERE IdCliente = @Id";
                oconexion.Open();

                return await oconexion.QueryFirstAsync<Cliente>(query, new { Id = id });
            }
        }

        public async Task<bool> InsertClient(Cliente cliente)
        {
            using(var oconexion = new SqlConnection(dbConnection().ConexionString))
            {
                string procedure = "SP_REGISTRARCLIENTE";
                oconexion.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@TipoDocumento", cliente.TipoDocumento);
                parameters.Add("@NroDocumento", cliente.NroDocumento);
                parameters.Add("@Apellidos", cliente.Apellidos);
                parameters.Add("@Nombres", cliente.Nombres);
                parameters.Add("@NombreCompleto", cliente.NombreCompleto);
                parameters.Add("@FechaNacimiento", cliente.FechaNacimiento);
                parameters.Add("@LugarNacimiento", cliente.LugarNacimiento);
                parameters.Add("@PaisResidencia", cliente.PaisResidencia);

                var result = await oconexion.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public async Task<bool> UpdateClient(Cliente cliente)
        {
            using (var oconexion = new SqlConnection(dbConnection().ConexionString))
            {
                string procedure = "SP_EDITARCLIENTE";
                oconexion.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@IdCliente", cliente.IdCliente);
                parameters.Add("@Apellidos", cliente.Apellidos);
                parameters.Add("@Nombres", cliente.Nombres);
                parameters.Add("@NombreCompleto", cliente.NombreCompleto);
                parameters.Add("@FechaNacimiento", cliente.FechaNacimiento);
                parameters.Add("@LugarNacimiento", cliente.LugarNacimiento);
                parameters.Add("@PaisResidencia", cliente.PaisResidencia);

                var result = await oconexion.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public async Task<IEnumerable<Cliente>> SeachCliente(string search)
        {
            using (var oconexion = new SqlConnection(dbConnection().ConexionString))
            {
                string query = "SELECT * FROM CLIENTE WHERE TipoDocumento LIKE '%' + @Search + '%' OR NroDocumento LIKE '%' + @Search + '%';";
                
                oconexion.Open();

                return await oconexion.QueryAsync<Cliente>(query, new { Search = search });
            }
        }

        public async Task<IEnumerable<Cliente>> GetAllClientsWithCredits()
        {
            using (var oconexion = new SqlConnection(dbConnection().ConexionString))
            {
                string query = @"
                    SELECT 
                        C.*,
                        CR.IdCredito,
                        CR.Plazo,
                        CR.Monto,
                        CR.FrecuenciaPago,
                        CR.Producto,
                        CR.FechaDesembolso,
                        CR.FechaPrimeraCuota,
                        CR.DiaPagoMensual
                    FROM 
                        CLIENTE C 
                        LEFT JOIN CREDITO CR ON C.IdCliente = CR.IdCliente;";

                oconexion.Open();

                var clientesDictionary = new Dictionary<int, Cliente>();

                var resultados = await oconexion.QueryAsync<Cliente, Credito, Cliente>(
                    query,
                    (cliente, credito) =>
                    {
                        if (!clientesDictionary.TryGetValue(cliente.IdCliente, out var clienteExistente))
                        {
                            clienteExistente = cliente;
                            clienteExistente.Creditos = new List<Credito>();
                            clientesDictionary.Add(cliente.IdCliente, clienteExistente);
                        }

                        if (credito != null)
                        {
                            credito.cliente = clienteExistente;
                            clienteExistente.Creditos.Add(credito);
                        }

                        return clienteExistente;
                    },
                    splitOn: "IdCredito"
                );

                return clientesDictionary.Values.ToList();
            }
        }

        public async Task<Cliente> GetClientDetailsWithCredits(int id)
        {
            using (var oconexion = new SqlConnection(dbConnection().ConexionString))
            {
                string query = @"
                    SELECT 
                        C.*,
                        CR.IdCredito,
                        CR.Plazo,
                        CR.Monto,
                        CR.FrecuenciaPago,
                        CR.Producto,
                        CR.FechaDesembolso,
                        CR.FechaPrimeraCuota,
                        CR.DiaPagoMensual
                    FROM 
                        CLIENTE C 
                        LEFT JOIN CREDITO CR ON C.IdCliente = CR.IdCliente
                        WHERE C.IdCliente=@IdCliente;";

                oconexion.Open();

                var clientesDictionary = new Dictionary<int, Cliente>();

                var resultados = await oconexion.QueryAsync<Cliente, Credito, Cliente>(
                    query,
                    (cliente, credito) =>
                    {
                        if (!clientesDictionary.TryGetValue(cliente.IdCliente, out var clienteExistente))
                        {
                            clienteExistente = cliente;
                            clienteExistente.Creditos = new List<Credito>();
                            clientesDictionary.Add(cliente.IdCliente, clienteExistente);
                        }

                        if (credito != null)
                        {
                            credito.cliente = clienteExistente;
                            clienteExistente.Creditos.Add(credito);
                        }

                        return clienteExistente;
                    },
                    new {IdCliente =  id},
                    splitOn: "IdCredito"
                );

                var clienteEspecifico = clientesDictionary.Values.FirstOrDefault(c => c.IdCliente == id);

                return clienteEspecifico;
            }
        }


        public async Task<IEnumerable<Cliente>> SearchClienteWithCredits(string search)
        {
            using (var oconexion = new SqlConnection(dbConnection().ConexionString))
            {
                string query = @"
                    SELECT 
                        C.*,
                        CR.IdCredito,
                        CR.Plazo,
                        CR.Monto,
                        CR.FrecuenciaPago,
                        CR.Producto,
                        CR.FechaDesembolso,
                        CR.FechaPrimeraCuota,
                        CR.DiaPagoMensual
                    FROM 
                        CLIENTE C 
                        LEFT JOIN CREDITO CR ON C.IdCliente = CR.IdCliente
                    WHERE TipoDocumento LIKE '%' + @Search + '%' OR NroDocumento LIKE '%' + @Search + '%';";

                oconexion.Open();

                var clientesDictionary = new Dictionary<int, Cliente>();

                var resultados = await oconexion.QueryAsync<Cliente, Credito, Cliente>(
                    query,
                    (cliente, credito) =>
                    {
                        if (!clientesDictionary.TryGetValue(cliente.IdCliente, out var clienteExistente))
                        {
                            clienteExistente = cliente;
                            clienteExistente.Creditos = new List<Credito>();
                            clientesDictionary.Add(cliente.IdCliente, clienteExistente);
                        }

                        if (credito != null)
                        {
                            credito.cliente = clienteExistente;
                            clienteExistente.Creditos.Add(credito);
                        }

                        return clienteExistente;
                    },
                    new { Search = search},
                    splitOn: "IdCredito"
                );

                return clientesDictionary.Values.ToList();
            }
        }
    }
}
