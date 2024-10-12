using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace _3NLIDTS_JoseMatuz_04
{
    public partial class Formulario : Form
    {
        string SqlConnection = "Server=localhost; Port= 3306; Database=programacionavanzada;Uid=root;Pwd=Timeshirt#21";
        public Formulario()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void insertarRegistro(string nombres, string apellidos, int edad, decimal estatura, string telefono, string genero)
        {
            using (MySqlConnection connection = new MySqlConnection(SqlConnection))
            {
                connection.Open();
                string insertQuery = "INSERT INTO registros (nombre, apellidos, telefono, estatura, edad, genero)" +
                "VALUES (@nombre, @apellidos, @telefono, @estatura, @edad, @genero)";
                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    //command.Parameters.AddWithValue("@",);
                    command.Parameters.AddWithValue("@Nombre", nombres);
                    command.Parameters.AddWithValue("@Apellidos", apellidos);
                    command.Parameters.AddWithValue("@Edad", edad);
                    command.Parameters.AddWithValue("@Telefono", telefono);
                    command.Parameters.AddWithValue("@Estatura", estatura);
                    command.Parameters.AddWithValue("@Genero", genero);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }   

        private void btnsave_Click(object sender, EventArgs e)
        {
            string nombres = tbnombre.Text;
            string apellidos = tbapellido.Text;
            string edad = tbedad.Text; 
            string estatura = tbestatura.Text;
            string telefono = tbtel.Text;
            string genero = "";
            if (rbhombre.Checked)
            {
                genero = "Hombre";
            }
            else if (rbmujer.Checked) 
            {

                genero = "mujer";
            }

            if (string.IsNullOrWhiteSpace(nombres))
            {
                MessageBox.Show("Debes ingresar un nombre", "Nombre", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(apellidos))
            {
                MessageBox.Show("Debes ingresar un apellido", "Apellidos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!int.TryParse(edad, out int numero))
            {
                MessageBox.Show("Debes ingresar tu edad", "Edad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!float.TryParse(estatura, out float number))
            {
                MessageBox.Show("Debes ingresar tu estatura", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!long.TryParse(telefono, out long num))
            {
                MessageBox.Show("Debes ingresar un número celular valido", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (telefono.Length != 10)
            {
                MessageBox.Show("Debes ingresar un número celular completo", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!rbhombre.Checked && !rbmujer.Checked)
            {
                MessageBox.Show("Debes seleccionar un género", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string datos = $"Nombre: {nombres}\r\nApellidos: {apellidos}\r\nTelefono: {telefono}\r\nEstatura: {estatura}\r\nEdad: {edad}\r\nGenero: {genero}\r\n";

                string RutaArchivo = "S:/TercerSemestreCodes/Datos.txt";

                bool archivoexiste = File.Exists(RutaArchivo);
                using (StreamWriter writer = new StreamWriter(RutaArchivo, true))
                {
                    if (archivoexiste)
                    {
                        writer.WriteLine(datos);
                        insertarRegistro(nombres, apellidos, int.Parse(edad), decimal.Parse(estatura), telefono, genero);
                    }
                }

                MessageBox.Show("Datos Guardados con Éxito\n\n" + datos, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


             
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            tbnombre.Clear();
            tbapellido.Clear();
            tbtel.Clear();
            tbedad.Clear();
            tbestatura.Clear();
            rbhombre.Checked = false;
            rbmujer.Checked = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
