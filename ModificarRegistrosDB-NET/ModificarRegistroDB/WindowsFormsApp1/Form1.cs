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
                dataGridView1.Columns["Id"].Visible = false;

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
            // aca llamo al constructor vacio, FrmAltaPokemon alta = new FrmAltaPokemon()
            FrmAltaPokemon alta = new FrmAltaPokemon();
            //El showdialog, no me permite salir a otra ventana, hasta que no cierre esa.
            alta.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //1 paso
            // para MODIFICAR EL POQUEMON SELECCIONANDO EN LA GRILLA, LO QUE DESEO MODIFICAR.
            // aca llamo al constructor con un parametro, que acabo de creaar,
            // FrmAltaPokemon alta = new FrmAltaPokemon(), como sobrecarga de constructores.
            Pokemon seleccionado = (Pokemon)dataGridView1.CurrentRow.DataBoundItem;

            //3 paso
            FrmAltaPokemon modificar = new FrmAltaPokemon(seleccionado);

            modificar.ShowDialog();
            cargar();
            
        }


        //ELIMINAR REGISTRO TANTO DE LA GRILLA COMO DE LA DB.
        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void btnEliminacionLogica_Click(object sender, EventArgs e)
        {

            eliminar(true);// aca pregunta si la eliminacion es logica o no , true si, no false
        }

        private void eliminar (bool logico = false)// escribirlo asi me da la posibilidad que sea opciones
                                                   // el metodo (bool logico = false), sino me tiraria error
        {
            PokemonNegocio negocio = new PokemonNegocio();
            Pokemon pokemonDelete = (Pokemon)dataGridView1.CurrentRow.DataBoundItem;

            try
            {
                //PARA PREGUNTARLE SI ESTA SEGURO DE ELIMINAR ESE REGISTRO, importante!!!!!!!!!!!!!!

                DialogResult respuesta = MessageBox.Show("Esta seguro de Eliminar este Registro?.", "Eliminando...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)

                    if (logico)
                        negocio.deleteLogic(pokemonDelete.Id);
                    else
                        negocio.Delete(pokemonDelete.Id);

                    cargar();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
//EditMode >> para que no se pueda modificar la celda.
//selectionMode >> para poder seleccionar toda la fila completa.
//Deshabilitar el multiSeleccion