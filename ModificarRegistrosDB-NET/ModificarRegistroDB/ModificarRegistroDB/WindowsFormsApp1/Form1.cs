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
    public partial class Form1 : Form
    {
        private List<Pokemon> listaPokemones;
        private List<Elemento> listaElementos;
        public Form1()

        {

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            cargar();

            

        }

        private void cargar()
        {
            PokemonNegocio negocioLista = new PokemonNegocio();

            try
            {
                listaPokemones = negocioLista.Listar();
                dataGridView1.DataSource = listaPokemones;
                dataGridView1.Columns["UrlImagen"].Visible = false;

                //pictureBox1.Load(listaPokemones[0].UrlImagen);



                ElementoNegocio negocioElemento = new ElementoNegocio();
                listaElementos = negocioElemento.Listar();
                dataGridView2.DataSource = listaElementos;
                dataGridView2.Columns["Id"].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Pokemon pokemonSeleccionado = (Pokemon)dataGridView1.CurrentRow.DataBoundItem;

            CargarImagen(pokemonSeleccionado.UrlImagen);
        }

        private void CargarImagen(string imagen)
        {
            try
            {
                pictureBox1.Load(imagen);
            }
            catch (Exception)
            {
                pictureBox1.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSkq9bHJ3gt0lMcFAlhsCbumDb0fYgvpP0HNQ&s");
            }


        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmAltaPokemon alta = new FrmAltaPokemon();
            //El showdialog, no me permite salir a otra ventana, hasta que no cierre esa.
            alta.ShowDialog();
            cargar();
        }
    }
}
//EditMode >> para que no se pueda modificar la celda.
//selectionMode >> para poder seleccionar toda la fila completa.
//Deshabilitar el multiSeleccion