using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio // si copio y paso  de un PROYECTO A OTRO , debo cambiar el name space que venia, cuando refactorizo
{
    public class Elemento
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public override string ToString()
        {
            return Descripcion ;
        }
    }
}
