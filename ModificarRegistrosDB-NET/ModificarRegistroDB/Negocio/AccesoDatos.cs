using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        //porque es privado, necesito leerlo.
        public SqlDataReader Lector
        {
            get { return lector; }
        }


        //Crear una conexion, creando un constructor, cuando nace el objeto AccesoDatos, ya tiene sus Estado (informacion)
        // y ya le paso por parametro lo que debe tener dentro, o donde debe conectarse.
        public AccesoDatos()
        {
            conexion = new SqlConnection("server= .\\SQLEXPRESS; database= POKEDEX_DB; integrated security= true");
            //Ahora hacer consulta, a la base de dato, lo realizo con comando.
            comando = new SqlCommand();

        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;

        }

        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                //hace la lectura, y la guarda en el lector.
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Este metodo seria para insertar la informacion que ingresamos desde  app a la base de datos.
        public void ejecutarAccion()
        {
            comando.Connection = conexion;

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // la siguiente funcion, es lo que me permitira, usar parametros COMANDO, para poder manipular
        // los argumento, cuando ejecutamosAccion, que deseamos mandar registros a la base de datos

        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }
        public void cerrarConexion()
        //Hay que cerrar tambien el lector.
        {
            if (lector != null)
                lector.Close();

            conexion.Close();
        }


         


    }
}
