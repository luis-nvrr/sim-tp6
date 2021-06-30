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
        int cantSimulaciones;
        int desde;
        int hasta;
        int tiempoLlegada;
        double tiempoFinInforme;
        double tiempoFinActualizacion;
        double uniformeA;
        double uniformeB;
        double pasoInestabilidad;
        double pasoDescarga;

        public PantallaResultados()
        {
            InitializeComponent();
        }

        private void PantallaResultados_Load(object sender, EventArgs e)
        {
            txtCantSimulaciones.Text = "200";
            txtTiempoPromedioLlegadas.Text = "20";
            txtTiempoPromedioFinInforme.Text = "200";
            txtTiempoPromedioFinActualizacion.Text = "40";
            txtFinCobroA.Text = "40";
            txtFinCobroB.Text = "60";
            txtDesde.Text = "0";
            txtHasta.Text = "200";
            txtPasoInestabilidad.Text = "0.01";
            txtPasoDescarga.Text = "1";
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
            grdRangoResultados.Columns[3].DefaultCellStyle.BackColor = Color.LightGreen;
            grdRangoResultados.Columns[11].DefaultCellStyle.BackColor = Color.LightGreen;
            grdRangoResultados.Columns[12].DefaultCellStyle.BackColor = Color.LightGreen;
            grdRangoResultados.Columns[13].DefaultCellStyle.BackColor = Color.LightGreen;
            grdRangoResultados.Columns[14].DefaultCellStyle.BackColor = Color.LightGreen;
            grdRangoResultados.Columns[16].DefaultCellStyle.BackColor = Color.LightGreen;
            grdRangoResultados.Columns[17].DefaultCellStyle.BackColor = Color.LightGreen;
            grdRangoResultados.Columns[18].DefaultCellStyle.BackColor = Color.LightGreen;
            grdRangoResultados.Columns[19].DefaultCellStyle.BackColor = Color.LightGreen;
            grdRangoResultados.Columns[20].DefaultCellStyle.BackColor = Color.LightGreen;

        }

        private void grdRangoResultados_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.FillWeight = 1;
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
        }

        private void grdRangoResultados_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            grdRangoResultados.ClearSelection();
        }

        private void cerrarVentanas()
        {
            List<Form> openForms = new List<Form>();

            foreach (Form f in Application.OpenForms)
                openForms.Add(f);

            foreach (Form f in openForms)
            {
                if (f.Name != "PantallaResultados")
                    f.Close();
            }
        }

        private void btnSimular_Click(object sender, EventArgs e)
        {
            cerrarVentanas();

            grdRangoResultados.DataSource = null;
            cantSimulaciones = int.Parse(txtCantSimulaciones.Text);
            desde = int.Parse(txtDesde.Text);
            hasta = int.Parse(txtHasta.Text);
            tiempoLlegada = int.Parse(txtTiempoPromedioLlegadas.Text);
            tiempoFinInforme = double.Parse(txtTiempoPromedioFinInforme.Text);
            tiempoFinActualizacion = double.Parse(txtTiempoPromedioFinActualizacion.Text);
            uniformeA = double.Parse(txtFinCobroA.Text);
            uniformeB = double.Parse(txtFinCobroB.Text);
            pasoInestabilidad = double.Parse(txtPasoInestabilidad.Text);
            pasoDescarga = double.Parse(txtPasoDescarga.Text);


            GestorSimulacion gestor = new GestorSimulacion(this);
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
