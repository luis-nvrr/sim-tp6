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
    public partial class PantallaLlegadas : Form
    {
        public PantallaLlegadas()
        {
            InitializeComponent();
        }
        public void mostrarResultados(DataTable resultados)
        {
            this.grdRangoResultados.DoubleBuffered(true);
            grdRangoResultados.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdRangoResultados.DataSource = resultados;
            this.Show();
        }
    }
}
