using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TaskDB
{
    public partial class FrmAgregarTarea : Form
    {
        public FrmAgregarTarea()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                MessageBox.Show(
                    "El título de la tarea es obligatorio.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtTitulo.Focus();
                return;
            }

            try
            {
                using (SqlConnection conexion = DatabaseConnection.GetConnection())
                {
                    conexion.Open();

                    string consulta = @"INSERT INTO Tareas
                                      (Titulo, Descripcion, Estado, FechaCreacion)
                                      VALUES
                                      (@Titulo, @Descripcion, @Estado, @FechaCreacion)";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@Titulo", txtTitulo.Text.Trim());
                        comando.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text.Trim());
                        comando.Parameters.AddWithValue("@Estado", "Pendiente");
                        comando.Parameters.AddWithValue("@FechaCreacion", DateTime.Now);

                        comando.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Tarea guardada correctamente.",
                    "TaskDB",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al guardar la tarea:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtTitulo.Clear();
            txtDescripcion.Clear();
            txtTitulo.Focus();
        }
    }
}