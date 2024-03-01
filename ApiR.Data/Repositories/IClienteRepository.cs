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
        Task<IEnumerable<Cliente>> GetAllClientsWithCredits();
        Task<Cliente> GetClientDetails(int id);
        Task<Cliente> GetClientDetailsWithCredits(int id);
        Task<bool> InsertClient(Cliente cliente);
        Task<bool> UpdateClient(Cliente cliente);
        Task<IEnumerable<Cliente>> SeachCliente(string search);
        Task<IEnumerable<Cliente>> SearchClienteWithCredits(string search);
    }
}
