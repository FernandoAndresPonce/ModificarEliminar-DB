using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace WindowsFormsApp1
{
    public partial class FrmAltaPokemon : Form
    {
        //ahora debo crear un atributo privado para poder modificar.
        //4 paso
        private Pokemon pokemon = null;
        public FrmAltaPokemon()
        {
            InitializeComponent();
        }

        //2 paso modificar
        // PARA MODIFICAR EL POKEMON, SEGUIMOS POR ACA.
        public FrmAltaPokemon(Pokemon pokemon)
        {
            InitializeComponent();
            //paso 5m odificar
            //this pokemons nos referimos al atributo privado, y el que se asigna un valor es del constructor.
            this.pokemon = pokemon;
            //paso 9 modificar
            Text = "Modificar Pokemon";
        }
        // ARRIBA

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            PokemonNegocio negocio = new PokemonNegocio();
            try
            {
                // paso 10 modificar, useamos el atributo privado que creamos

                // cuando debemos declarar la variable se dice CASTEAR/
                // escribir todos los datos, se dice MAPEAR.

                // paso 11 modificar
                if (pokemon == null)
                    pokemon = new Pokemon();

                pokemon.Numero = int.Parse(txtNumero.Text);
                pokemon.Nombre = txtNombre.Text;
                pokemon.Descripcion = txtDescripcion.Text;
                pokemon.UrlImagen = txtUrlImagen.Text;
                // con esta funcion, traigo el dato que tengo en un comboBox, que era un objeto del tipo elemento.
                pokemon.Tipo = (Elemento) comboBoxTipo.SelectedItem;
                pokemon.Debilidad = (Elemento)comboBoxDebilidad.SelectedItem;

                //paso 12 modificar
                if (pokemon.Id != 0)
                {
                    negocio.Update(pokemon);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.Add(pokemon);
                    MessageBox.Show("Agregado exitosamente");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally 
            {
                Close();// puedo cerrarla comom no.
            }

        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 59) && e.KeyChar != 8)

                e.Handled = true;
        }

        private void FrmAltaPokemon_Load(object sender, EventArgs e)
        {
            //ACA voy a poner en los desplegables (combo box), los datos traidos de la base de dato.

            ElementoNegocio elemento = new ElementoNegocio();

            try
            {
                comboBoxTipo.DataSource = elemento.Listar();
                //6 paso modificar
                //ValueMember , valor clave,
                comboBoxTipo.ValueMember = "Id";
                //DisplayMember, lo que voy a mostrar,
                comboBoxTipo.DisplayMember = "Descripcion";

                comboBoxDebilidad.DataSource = elemento.Listar();
                comboBoxDebilidad.ValueMember = "Id";
                comboBoxDebilidad.DisplayMember = "Descripcion";

                // 5 paso modificar.
                if (pokemon != null)
                {
                    txtNumero.Text = pokemon.Numero.ToString();
                    txtNombre.Text = pokemon.Nombre;
                    txtDescripcion.Text = pokemon.Descripcion;
                    txtUrlImagen.Text = pokemon.UrlImagen;
                    if (pokemon.UrlImagen != null)
                        
                        try
                        {
                            pictureBoxPokemonAlta.Load(txtUrlImagen.Text);
                        }
                        catch (Exception ex)
                        {
                            pictureBoxPokemonAlta.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSkq9bHJ3gt0lMcFAlhsCbumDb0fYgvpP0HNQ&s");


                        }
                    // 7 paso modificar, Y ADEMAS
                    comboBoxTipo.SelectedValue = pokemon.Tipo.Id;
                    comboBoxDebilidad.SelectedValue = pokemon.Debilidad.Id;
                    // PASO 8 AGREGAR EL ID.TIPO Y ID.DEBILIDAD EN LA DB DE POKEMON, PORQUE EL
                    // ID FIGURA EN 0, PORQUE NUNCA SE TRAJIERON ESOS DATOS A LA TABLA, PARA RELACIONARSE.
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtDescripcion_Enter(object sender, EventArgs e)
        {
            try
            {
                pictureBoxPokemonAlta.Load(txtUrlImagen.Text);
            }
            catch (Exception ex)
            {
                pictureBoxPokemonAlta.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSkq9bHJ3gt0lMcFAlhsCbumDb0fYgvpP0HNQ&s");


            }

        }
    }
}
