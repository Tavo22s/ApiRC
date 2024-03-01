using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiR.Entity
{
    public class Credito
    {
        public int IdCredito { get; set; }
        public Cliente cliente { get; set; }
        public int Plazo { get; set; }
        public decimal Monto { get; set; }
        public string FrecuenciaPago { get; set; }
        public string Producto { get; set; }
        public string FechaDesembolso { get; set; }
        public string FechaPrimeraCuota { get; set; }
        public int DiaPagoMensual { get; set; }
    }
}
