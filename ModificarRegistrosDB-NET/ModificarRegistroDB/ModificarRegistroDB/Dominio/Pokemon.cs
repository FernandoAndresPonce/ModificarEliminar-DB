using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Pokemon
    {
        //Para poder cambiar los nombres de las grillas, con sus detalles, ejemplo acento,
        //debe ir arriba de la propiedad o atributo.

        [DisplayName("Número")]// son ANOTATION.
        public string Nombre { get; set; }
        public int Numero { get; set; }
        [DisplayName("Descrip ción")]
        public string Descripcion { get; set; }

        public string UrlImagen { get; set; }

        public Elemento Tipo { get; set; }

        public Elemento Debilidad { get; set; }
    }
}
