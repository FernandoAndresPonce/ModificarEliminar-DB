using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class PokemonNegocio
    {

        //Para LISTAR...
        public List<Pokemon> Listar()
        {
            List<Pokemon> listaPokemones = new List<Pokemon>();

            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server= .\\SQLEXPRESS; database= POKEDEX_DB; integrated security= true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select P.Nombre, P.Numero, P.Descripcion, P.UrlImagen, E.Descripcion Tipo, T.Descripcion Debilidad from POKEMONS P, ELEMENTOS E, ELEMENTOS T where P.IdTipo = E.Id and T.Id = P.IdDebilidad";

                comando.Connection = conexion;
                conexion.Open();

                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Pokemon auxPokemon = new Pokemon();
                    auxPokemon.Nombre = (string)lector["Nombre"];
                    auxPokemon.Numero = (int)lector["Numero"];
                    auxPokemon.Descripcion = (string)lector["Descripcion"];

                    //validar si un registo esta NULL...
                    //if (!(lector.IsDBNull(lector.GetOrdinal("UrlImagen"))))// decimos SI NO es null, leelo.
                    //    auxPokemon.UrlImagen = (string)lector["UrlImagen"];
                    // o

                    if (!(lector["UrlImagen"] is DBNull))
                        auxPokemon.UrlImagen = (string)lector["UrlImagen"];



                    auxPokemon.Tipo = new Elemento();
                    auxPokemon.Tipo.Descripcion = (string)lector["Tipo"];
                    auxPokemon.Debilidad = new Elemento();
                    auxPokemon.Debilidad.Descripcion = (string)lector["Debilidad"];

                    
                    

                    listaPokemones.Add(auxPokemon);
                }

                return listaPokemones;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                conexion.Close();
            }







        }

        //Para AGREGAR...
        public void Add(Pokemon newPokemon)
        {
            //Aca debemos insertar registros.
            AccesoDatos datos = new AccesoDatos();
            

            try
            {
                datos.setearConsulta("insert into POKEMONS (Numero, Nombre, Descripcion, Activo, IdTipo, IdDEbilidad, urlimagen) values ("+ newPokemon.Numero +", '"+newPokemon.Nombre +"', '"+newPokemon.Descripcion +"', 1, @idTipo, @idDebilidad, @urlimagen)");
                datos.setearParametro("@idTipo", newPokemon.Tipo.Id);
                datos.setearParametro("@idDebilidad", newPokemon.Debilidad.Id);
                datos.setearParametro("@urlimagen", newPokemon.UrlImagen);
                datos.ejecutarAccion();
            }

            catch (Exception)
            {

                throw;
            }

            finally
            {
                datos.cerrarConexion();
            }
        }

        //Para MODIFICAR...
        public void Update(Pokemon updatePokemon)
        {

        }

        //Para ELIMINAR...
        public void Delete(Pokemon deletePokemon)
        {

        }


    }
}