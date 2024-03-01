using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiR.Entity
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string TipoDocumento { get; set; }
        public int NroDocumento { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string NombreCompleto { get; set; }
        public string FechaNacimiento { get; set; }
        public string LugarNacimiento { get; set; }
        public string PaisResidencia { get; set; }
    }
}
    