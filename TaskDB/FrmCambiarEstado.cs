using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TaskDB
{
    public partial class FrmCambiarEstado : Form
    {
        public FrmCambiarEstado()
        {
            InitializeComponent();

            cmbEstado.Items.Add("Pendiente");
            cmbEstado.Items.Add("En Proceso");
            cmbEstado.Items.Add("Completada");

            cmbEstado.SelectedIndex = 0;
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show(
                    "Debe ingresar el ID de la tarea.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtId.Focus();
                return;
            }

            int id;

            if (!int.TryParse(txtId.Text.Trim(), out id))
            {
                MessageBox.Show(
                    "El ID debe ser un número válido.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtId.Focus();
                return;
            }

            if (cmbEstado.SelectedItem == null)
            {
                MessageBox.Show(
                    "Debe seleccionar un estado.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            string nuevoEstado = cmbEstado.SelectedItem.ToString();

            try
            {
                using (SqlConnection conexion = DatabaseConnection.GetConnection())
                {
                    string consulta = @"
                        UPDATE Tareas
                        SET Estado = @Estado
                        WHERE Id = @Id";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@Estado", nuevoEstado);
                        comando.Parameters.AddWithValue("@Id", id);

                        conexion.Open();

                        int filasAfectadas = comando.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show(
                                "Estado actualizado correctamente.",
                                "TaskDB",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );

                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show(
                                "No se encontró una tarea con el ID ingresado.",
                                "TaskDB",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al actualizar el estado:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            cmbEstado.SelectedIndex = 0;
            txtId.Focus();
        }
    }
}