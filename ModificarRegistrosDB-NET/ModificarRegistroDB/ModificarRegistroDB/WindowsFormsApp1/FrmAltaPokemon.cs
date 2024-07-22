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
        public FrmAltaPokemon()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pokemon poke = new Pokemon();
            PokemonNegocio negocio = new PokemonNegocio();
            try
            {
                // cuando debemos declarar la variable se dice CASTEAR/
                poke.Numero = int.Parse(txtNumero.Text);
                poke.Nombre = txtNombre.Text;
                poke.Descripcion = txtDescripcion.Text;
                poke.UrlImagen = txtUrlImagen.Text;
                // con esta funcion, traigo el dato que tengo en un comboBox, que era un objeto del tipo elemento.
                poke.Tipo = (Elemento) comboBoxTipo.SelectedItem;
                poke.Debilidad = (Elemento)comboBoxDebilidad.SelectedItem;

                negocio.Add(poke);
                MessageBox.Show("Agregado exitosamente");
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
                comboBoxDebilidad.DataSource = elemento.Listar();
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
