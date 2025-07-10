using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Estudiantes2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Conexion.Conectar();
            MessageBox.Show("conexion exitosa");
            dataGridView1.DataSource = llenar_grid();
        }

        public DataTable llenar_grid()
        {
            Conexion.Conectar();
            DataTable dt = new DataTable();
            string consulta = "select * from alumno";
            SqlCommand cmd = new SqlCommand(consulta, Conexion.Conectar());


            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            return dt;

        }

        private void BuscarAlumno()
        {
            Conexion.Conectar();
            string query = "SELECT Nombres, Apellidos, Direccion FROM alumno WHERE Codigo = @Codigo";
            SqlCommand cmd = new SqlCommand(query, Conexion.Conectar());
            cmd.Parameters.AddWithValue("@Codigo", txtcodigo.Text);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                txtnombres.Text = reader["Nombres"].ToString();
                txtapellidos.Text = reader["Apellidos"].ToString();
                txtdireccion.Text = reader["Direccion"].ToString();
            }
            else
            {
                MessageBox.Show("No se encontró un alumno con ese Código");
                txtnombres.Text = "";
                txtapellidos.Text = "";
                txtdireccion.Text = "";
            }

            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string insertar = "insert into alumno (Codigo,Nombres,Apellidos,Direccion)values(@Codigo,@Nombres,@Apellidos,@Direccion)";
            SqlCommand cmd1 = new SqlCommand(insertar, Conexion.Conectar());
            cmd1.Parameters.AddWithValue("@Codigo", txtcodigo.Text);
            cmd1.Parameters.AddWithValue("@Nombres", txtnombres.Text);
            cmd1.Parameters.AddWithValue("@Apellidos", txtapellidos.Text);
            cmd1.Parameters.AddWithValue("@Direccion", txtdireccion.Text);

            cmd1.ExecuteNonQuery();

            MessageBox.Show("Los datos fueron agregados con exito");

            dataGridView1.DataSource = llenar_grid();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try {

                txtcodigo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtnombres.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtapellidos.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtdireccion.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            }

            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string actualizar = "UPDATE alumno SET Nombres=@Nombres, Apellidos=@Apellidos, Direccion=@Direccion WHERE Codigo=@Codigo";

            SqlCommand cmd2 = new SqlCommand(actualizar, Conexion.Conectar());

            cmd2.Parameters.AddWithValue("@Codigo", txtcodigo.Text);
            cmd2.Parameters.AddWithValue("@Nombres", txtnombres.Text);
            cmd2.Parameters.AddWithValue("@Apellidos", txtapellidos.Text);
            cmd2.Parameters.AddWithValue("@Direccion", txtdireccion.Text);

            cmd2.ExecuteNonQuery();
            MessageBox.Show("Los datos fueron actualizados con éxito");

            dataGridView1.DataSource = llenar_grid();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string eliminar = "Delete from alumno where Codigo =@Codigo";
            SqlCommand cmd3 = new SqlCommand(eliminar, Conexion.Conectar());
            cmd3.Parameters.AddWithValue("@Codigo", txtcodigo.Text);
            cmd3.ExecuteNonQuery();
            MessageBox.Show("Los datos fueron eliminados con exito");
            dataGridView1.DataSource = llenar_grid();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtcodigo.Clear();
            txtnombres.Clear();
            txtapellidos.Clear();
            txtdireccion.Clear();
            txtcodigo.Focus();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarAlumno();
        }

       private void txtcodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BuscarAlumno();
                e.Handled = true; // evita beep o salto de línea
            }
        }
    }
}
