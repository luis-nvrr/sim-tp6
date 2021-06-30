using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;


namespace Numeros_aleatorios.Colas
{
    public partial class RungeKuttaResultados : Form
    {
        public RungeKuttaResultados()
        {
            InitializeComponent();
        }

        private void RungeKuttaResultados_Load(object sender, EventArgs e)
        {
  
        }

        public void mostrarResultados(DataTable resultados, String texto)
        {
           this.grdResultados.DoubleBuffered(true);
           grdResultados.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdResultados.DataSource = resultados;
            this.lblTexto.Text = texto;
            this.Show();
        }

    }
}
