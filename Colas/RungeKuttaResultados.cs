using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Numeros_aleatorios.Colas
{
    public partial class RungeKuttaResultados : Form
    {
        public RungeKuttaResultados()
        {
            InitializeComponent();
            DataTable table = new DataTable();
        }

        private void RungeKuttaResultados_Load(object sender, EventArgs e)
        {
            RungeKutta rungeKutta = new RungeKutta((decimal)0.1,(decimal)0.008869191,50);
            decimal resultado = rungeKutta.calcularRungeKutta();
            MessageBox.Show(resultado.ToString());
            grdResultados.DataSource = rungeKutta.tabla;
        }
    }
}
