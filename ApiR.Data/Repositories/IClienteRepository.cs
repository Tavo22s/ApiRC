using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiR.Entity;

namespace ApiR.Data.Repositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAllClients();
        Task<Cliente> GetClientDetails(int id);
        Task<bool> InsertClient(Cliente cliente);
        Task<bool> UpdateClient(Cliente cliente);
        Task<IEnumerable<Cliente>> SeachCliente(string search);
    }
}
