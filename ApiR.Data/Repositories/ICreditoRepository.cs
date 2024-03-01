using ApiR.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiR.Data.Repositories
{
    public interface ICreditoRepository
    {
        Task<bool> InsertCredit(Credito cliente);
    }
}
