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
    public partial class PantallaResultados : Form
    {
        private int paginaActual;
        private ColasMunicipalidad colas;
        private int filaSeleccionada;
        int cantSimulaciones;
        int desde;
        int hasta;
        int tiempoLlegada;
        int tiempoFinInforme;
        int tiempoFinActualizacion;
        int tiempoFinCobro;

        public PantallaResultados()
        {
            InitializeComponent();
            paginaActual = 1;
        }

        private void PantallaResultados_Load(object sender, EventArgs e)
        {
            txtCantSimulaciones.Text = "1000";
            txtTiempoPromedioLlegadas.Text = "60";
            txtTiempoPromedioFinInforme.Text = "20";
            txtTiempoPromedioFinActualizacion.Text = "40";
            txtTiempoPromedioFinCobro.Text = "30";
            txtDesde.Text = "0";
            txtHasta.Text = "500";
         
        }

        private void limpiarCampos()
        {
            txtCantSimulaciones.Text = "";
            txtTiempoPromedioLlegadas.Text = "";
            txtTiempoPromedioFinInforme.Text = "";
            txtTiempoPromedioFinActualizacion.Text = "";
            txtTiempoPromedioFinCobro.Text = "";
            txtDesde.Text = "";
            txtHasta.Text = "";
        }

        public void mostrarEstadisticas(double tiempoPromedioEsperaEnCaja, 
            double tiempoOcupacionInformes, 
            double tiempoOciosoActualizacion, 
            double tiempoMaximoEsperaEnCaja
            )
        {
            txtPromedioEsperaCajas.Text = tiempoPromedioEsperaEnCaja.ToString();
            txtOcupacionInformes.Text = tiempoOcupacionInformes.ToString();
            txtOciosoActualizacion.Text = tiempoOciosoActualizacion.ToString();
            txtMaximaEsperaCajas.Text = tiempoMaximoEsperaEnCaja.ToString();

        }

        public void mostrarResultados(DataTable resultados)
        {
            this.grdRangoResultados.DoubleBuffered(true);
            grdRangoResultados.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdRangoResultados.DataSource = resultados;
        }

        private void grdRangoResultados_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.FillWeight = 1;
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            filaSeleccionada = grdRangoResultados.CurrentCell.RowIndex; 
            paginaActual++;
            colas.mostrarPagina(paginaActual);
            grdRangoResultados.CurrentCell = grdRangoResultados.Rows[filaSeleccionada].Cells[0];
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            filaSeleccionada = grdRangoResultados.CurrentCell.RowIndex;
            paginaActual--;
            colas.mostrarPagina(paginaActual);
            grdRangoResultados.CurrentCell = grdRangoResultados.Rows[filaSeleccionada].Cells[0];
        }

        private void grdRangoResultados_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            grdRangoResultados.ClearSelection();
        }

        private void btnSimular_Click(object sender, EventArgs e)
        {
            paginaActual = 1;
            grdRangoResultados.DataSource = null;
            cantSimulaciones = int.Parse(txtCantSimulaciones.Text);
            desde = int.Parse(txtDesde.Text);
            hasta = int.Parse(txtHasta.Text);
            tiempoLlegada = int.Parse(txtTiempoPromedioLlegadas.Text);
            tiempoFinInforme = int.Parse(txtTiempoPromedioFinInforme.Text);
            tiempoFinActualizacion = int.Parse(txtTiempoPromedioFinActualizacion.Text);
            tiempoFinCobro = int.Parse(txtTiempoPromedioFinCobro.Text);

         
            colas = new ColasMunicipalidad(this);
            if (hasta - desde <= 500) 
            { 
                colas.simular(desde, hasta, cantSimulaciones, tiempoLlegada, tiempoFinInforme, tiempoFinActualizacion, tiempoFinCobro); // 0 es la fila 1, la 0 es inicializacion
                //colas.mostrarPagina(paginaActual);
                colas.calcularEstadisticas();

            }
            else
            {
                MessageBox.Show("El rango puede ser hasta 500 filas", "Error", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }
    }
}
