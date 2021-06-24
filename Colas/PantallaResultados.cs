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
        private GestorSimulacion gestor;
        private int filaSeleccionada;
        int cantSimulaciones;
        int desde;
        int hasta;
        int tiempoLlegada;
        int tiempoFinInforme;
        int tiempoFinActualizacion;
        double uniformeA;
        double uniformeB;
        double pasoInestabilidad;
        double pasoDescarga;

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
            txtFinCobroA.Text = "20";
            txtFinCobroB.Text = "50";
            txtDesde.Text = "0";
            txtHasta.Text = "100";
            txtPasoInestabilidad.Text = "0.01";
            txtPasoDescarga.Text = "1";
        }

        private void limpiarCampos()
        {
            txtCantSimulaciones.Text = "";
            txtTiempoPromedioLlegadas.Text = "";
            txtTiempoPromedioFinInforme.Text = "";
            txtTiempoPromedioFinActualizacion.Text = "";
            txtFinCobroA.Text = "";
            txtFinCobroB.Text = "";
            txtDesde.Text = "";
            txtHasta.Text = "";
            txtPasoInestabilidad.Text = "";
            txtPasoDescarga.Text = "";
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
            //filaSeleccionada = grdRangoResultados.CurrentCell.RowIndex; 
            //paginaActual++;
            //gestor.mostrarPagina(paginaActual);
            //grdRangoResultados.CurrentCell = grdRangoResultados.Rows[filaSeleccionada].Cells[0];
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            //filaSeleccionada = grdRangoResultados.CurrentCell.RowIndex;
            //paginaActual--;
            //gestor.mostrarPagina(paginaActual);
            //grdRangoResultados.CurrentCell = grdRangoResultados.Rows[filaSeleccionada].Cells[0];
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
            uniformeA = double.Parse(txtFinCobroA.Text);
            uniformeB = double.Parse(txtFinCobroB.Text);
            pasoInestabilidad = double.Parse(txtPasoInestabilidad.Text);
            pasoDescarga = double.Parse(txtPasoDescarga.Text);

            gestor = new GestorSimulacion(this);
            if (hasta - desde <= 500) 
            {
                gestor.simular(desde, hasta, cantSimulaciones, tiempoLlegada, 
                    tiempoFinInforme, tiempoFinActualizacion, uniformeA, uniformeB, pasoInestabilidad, pasoDescarga);
            }
            else
            {
                MessageBox.Show("El rango puede ser hasta 500 filas", "Error", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }
    }
}
