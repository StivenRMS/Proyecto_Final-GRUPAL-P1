using MySql.Data.MySqlClient;
using ProyectoFinal.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoFinal
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

       

        private void BtGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String dpi = TbDPI.Text;
                String nombre = Tbnombre.Text;
                String pedidos = tbpedido.Text;
                double preciopub = double.Parse(tbprecioP.Text);
               
                if (dpi != "" && nombre != "" && pedidos != ""
               && preciopub > 0)
                {
                    string sql = "INSERT INTO facturacion (NIT, NOMBRE, Descripcion, PRECIO_COMPRA) VALUES('" + dpi + "','" + nombre + "', '" + pedidos  + "', '" + preciopub + "')";
                    MySqlConnection conexionBD = Conexion.conexion();
                    conexionBD.Open();
                    try
                    {
                        MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Registro Guardado");
                        
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error al guardar" +
                       ex.Message);
                    }
                    finally
                    {
                        conexionBD.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Debe completar todos los campos");
              }
            }
            catch (FormatException fex)
            {
                MessageBox.Show("Datos incorrectos" + fex.Message);
            }





        }

        private void Btbuscar_Click(object sender, RoutedEventArgs e)
        {

            String dpi = TbDPI.Text;
            MySqlDataReader reader = null;
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            string sql = "Select Id, NIT, Nombre, Descripcion, Precio_Compra FROM facturacion WHERE NIT LIKE '" + dpi + "' LIMIT 1"; 
            

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read()) ;
                    {
                        TbDPI.Text = reader.GetString(0);
                        TbDPI.Text = reader.GetString(1);
                        Tbnombre.Text = reader.GetString(2);
                        tbpedido.Text = reader.GetString(3);
                        tbprecioP.Text = reader.GetString(4);
                       
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros");
                }


            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al buscar" + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }


        }

        private void BtActuaizar_Click(object sender, RoutedEventArgs e)
        {
            String dpi = TbDPI.Text;
            String dpi1 = TbDPI.Text;
            String nombre = Tbnombre.Text;
            String pedido = tbpedido.Text;
            double preciopub = double.Parse(tbprecioP.Text);
            

            string sql = "UPDATE facturacion SET NIT= '" + dpi1 + "', Nombre='" + nombre + "', Descripcion='" + pedido + "', Precio_Compra='" + preciopub + "' Where NIT= '" + dpi + "'";

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro Modificado");
                
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al Modificar" + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void BtEliminar_Click(object sender, RoutedEventArgs e)
        {
            String ID = TbDPI.Text;
            string sql = "DELETE FROM facturacion Where NIT = '" + ID + "'";
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql,
               conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro Eliminado");
                limpiar();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al Eliminar" + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void BtLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }
        private void limpiar()
        {
            TbDPI.Text = "";
            Tbnombre.Text = "";
            tbpedido.Text = "";
            tbprecioP.Text = "";
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void BtRegistro_Click(object sender, RoutedEventArgs e)
        {
            GBdatos.Visibility = Visibility;
            
        }

        private DataTable descarga = new DataTable();

        private void BTdescCSV_Click(object sender, RoutedEventArgs e)
        {
            string d1 = TbDPI.Text;
            string d2 = Tbnombre.Text;
            string d3 = tbpedido.Text;
            string d4 = tbprecioP.Text;
            CSLcsv des = new CSLcsv();
            des.imprimirConsulta(descarga, d1,d2,d3,d4);
            MessageBox.Show("Documento Creado en Carpeta del Proyecto");

        }
    }

}
    

