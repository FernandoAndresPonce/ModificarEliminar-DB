using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ElementoNegocio
    {
        public List<Elemento> Listar()
        {
            List<Elemento> listaElementos = new List<Elemento>();

            AccesoDatos Acceder = new AccesoDatos();


            try 
            {
                Acceder.setearConsulta("Select Id, Descripcion from ELEMENTOS");
                Acceder.ejecutarLectura();

                while (Acceder.Lector.Read())
                {
                    Elemento auxElemento = new Elemento();
                    auxElemento.Id = (int)Acceder.Lector["Id"];
                    auxElemento.Descripcion = (string)Acceder.Lector["Descripcion"];

                    listaElementos.Add(auxElemento);
                }

                return listaElementos;
            }
            catch (Exception)
            {

                throw;
            }

            finally 
            {
                Acceder.cerrarConexion();
            }
        }

            
    }
}
