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
        Task<IEnumerable<Cliente>> GetAllClientsWithCredits();
        Task<bool> InsertClient(Cliente cliente);
        Task<bool> UpdateClient(Cliente cliente);
        Task<IEnumerable<Cliente>> SearchClienteWithCredits(string search);
    }
}
