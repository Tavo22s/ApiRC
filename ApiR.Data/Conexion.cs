using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiR.Data
{
    public class Conexion
    {
        public Conexion(string conexionString) => ConexionString = conexionString;

        public string ConexionString { get; set; }
    }
}
