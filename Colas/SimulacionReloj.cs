using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Numeros_aleatorios.Colas
{
    class SimulacionReloj
    {
        double[] probabilidadesEstadosAcum = new double[] { 1, 0 };
        string[] estadosFactura = new string[] { "vencida", "al dia" };

        double[] probabilidadesConoceProcedimientoAcum = new double[] { 0, 1 };
        string[] conoceProcedimiento = new string[] { "si", "no" };

        DataTable resultados;
        private int cantidadClientes;
        private int indice;
        private LineaReloj LineaRelojActual;


        public SimulacionReloj()
        {
            resultados = new DataTable();
            crearTabla(resultados);
        }

        private void crearTabla(DataTable tabla)
        {
            tabla.Columns.Add("i", typeof(string));
            tabla.Columns.Add("cant llegadas", typeof(string));
            tabla.Columns.Add("evento", typeof(string));
            tabla.Columns.Add("reloj", typeof(string));
            tabla.Columns.Add("tiempo para llegada", typeof(string));
            tabla.Columns.Add("llegada cliente", typeof(string));
            tabla.Columns.Add("RND estado factura.", typeof(string));
            tabla.Columns.Add("estado factura", typeof(string));
            tabla.Columns.Add("RND conoce", typeof(string));
            tabla.Columns.Add("conoce procedimiento", typeof(string));
            tabla.Columns.Add("RND fin de informe", typeof(string));
            tabla.Columns.Add("tiempo de informe", typeof(string));
            tabla.Columns.Add("fin informacion", typeof(string));
            tabla.Columns.Add("fin actualizacion", typeof(string));
            tabla.Columns.Add("RND Fin Cobro", typeof(string));
            tabla.Columns.Add("Tiempo Fin Cobro", typeof(string));
            tabla.Columns.Add("fin caja 1", typeof(string));
            tabla.Columns.Add("fin caja 2", typeof(string));
            tabla.Columns.Add("fin caja 3", typeof(string));
            tabla.Columns.Add("fin caja 4", typeof(string));
            tabla.Columns.Add("fin caja 5", typeof(string));
            tabla.Columns.Add("estado informes", typeof(string));
            tabla.Columns.Add("cola informes", typeof(string));
            tabla.Columns.Add("estado actualizacion", typeof(string));
            tabla.Columns.Add("cola actualizacion", typeof(string));
            tabla.Columns.Add("estado caja 1", typeof(string));
            tabla.Columns.Add("estado caja 2", typeof(string));
            tabla.Columns.Add("estado caja 3", typeof(string));
            tabla.Columns.Add("estado caja 4", typeof(string));
            tabla.Columns.Add("estado caja 5", typeof(string));
            tabla.Columns.Add("cola caja", typeof(string));
            tabla.Columns.Add("acumulado tiempo espera en caja", typeof(string));
            tabla.Columns.Add("cantidad que espera", typeof(string));
            tabla.Columns.Add("tiempo ocupacion informes", typeof(string));
            tabla.Columns.Add("tiempo ocioso actualizacion", typeof(string));
            tabla.Columns.Add("maxima espera en caja", typeof(string));
        }

        public DataTable getResultados()
        {
            return this.resultados;
        }


        public void calcularPrimerasLlegadas(int TiempoLlegada, double TiempoFinInforme, double TiempoFinActualizacion, double uniformeA,
            double uniformeB)
        {
            LineaReloj LineaRelojAnterior = new LineaReloj(5, TiempoLlegada, TiempoFinInforme, uniformeA, uniformeB);

            agregarLineaReloj(LineaRelojAnterior, 0);
            int i = 0;
            do
            {
                LineaRelojActual = new LineaReloj(LineaRelojAnterior, this, 0, 100000000, i);
                LineaRelojActual.calcularEvento();
                LineaRelojActual.calcularSiguienteLlegada();
                LineaRelojActual.calcularEstadoFactura(probabilidadesEstadosAcum, estadosFactura);
                LineaRelojActual.calcularConoceProcedimiento(probabilidadesConoceProcedimientoAcum, conoceProcedimiento);
                LineaRelojActual.calcularFinCobro();
                LineaRelojActual.calcularColumnaFinActualizacion(TiempoFinActualizacion);
                LineaRelojActual.calcularFinInforme();
                LineaRelojAnterior = LineaRelojActual;
                agregarLineaReloj(LineaRelojActual, i);
                i++;
            }
            while (!LineaRelojActual.tieneLlegadasCumplidas());

        }

        public double getReloj()
        {
            return this.LineaRelojActual.reloj;
        }

        public void agregarColumna()
        {
            cantidadClientes++;
            this.resultados.Columns.Add("estado " + cantidadClientes);
            this.resultados.Columns.Add("hora espera " + cantidadClientes);
        }

        private void agregarLineaReloj(LineaReloj LineaReloj, int i)
        {
            DataRow row = resultados.NewRow();
            row[0] = i;
            row[1] = LineaReloj.contadorLlegadas;
            row[2] = LineaReloj.evento;
            row[3] = LineaReloj.reloj;
            row[4] = LineaReloj.tiempoParaLlegada.ToString() != "-1" ? LineaReloj.tiempoParaLlegada.ToString() : "";
            row[5] = LineaReloj.llegadaCliente;
            row[6] = LineaReloj.rndEstadoFactura.ToString() != "-1" ? LineaReloj.rndEstadoFactura.ToString() : "";
            row[7] = LineaReloj.estadoFactura;
            row[8] = LineaReloj.rndConoceProcedimiento.ToString() != "-1" ? LineaReloj.rndConoceProcedimiento.ToString() : "";
            row[9] = LineaReloj.conoceProcedimiento;
            row[10] = LineaReloj.rndFinInforme.ToString() != "-1"? LineaReloj.rndFinInforme.ToString(): "";
            row[11] = LineaReloj.tiempoFinInforme.ToString() != "-1" ? LineaReloj.tiempoFinInforme.ToString() : "";
            row[12] = LineaReloj.ventanillaInforme.finInforme.ToString() != "-1" ? LineaReloj.ventanillaInforme.finInforme.ToString() : "";
            row[13] = LineaReloj.ventanillaActualizacion.finActualizacion.ToString() != "-1" ? LineaReloj.ventanillaActualizacion.finActualizacion.ToString() : "";
            row[14] = LineaReloj.rndFinCobro.ToString() != "-1" ? LineaReloj.rndFinCobro.ToString( ): "";
            row[15] = LineaReloj.tiempoFinCobro.ToString() != "-1"? LineaReloj.tiempoFinCobro.ToString() : "";
            row[16] = LineaReloj.cajas[0].finCobro.ToString() != "-1" ? LineaReloj.cajas[0].finCobro.ToString() : "" ;
            row[17] = LineaReloj.cajas[1].finCobro.ToString() != "-1" ? LineaReloj.cajas[1].finCobro.ToString() : "";
            row[18] = LineaReloj.cajas[2].finCobro.ToString() != "-1" ? LineaReloj.cajas[2].finCobro.ToString() : "";
            row[19] = LineaReloj.cajas[3].finCobro.ToString() != "-1" ? LineaReloj.cajas[3].finCobro.ToString() : "";
            row[20] = LineaReloj.cajas[4].finCobro.ToString() != "-1" ? LineaReloj.cajas[4].finCobro.ToString() : "";
            row[21] = LineaReloj.ventanillaInforme.estado;
            row[22] = LineaReloj.ventanillaInforme.cola.Count;
            row[23] = LineaReloj.ventanillaActualizacion.estado;
            row[24] = LineaReloj.ventanillaActualizacion.cola.Count;
            row[25]  = LineaReloj.cajas[0].estado;
            row[26] = LineaReloj.cajas[1].estado;
            row[27] = LineaReloj.cajas[2].estado;
            row[28] = LineaReloj.cajas[3].estado;
            row[29] = LineaReloj.cajas[4].estado;
            row[30] = Caja.cola.Count;
            row[31] = LineaReloj.acumuladorTiemposEsperaEnCaja;
            row[32] = LineaReloj.cantidadClientesEsperan;
            row[33] = LineaReloj.acumuladorTiempoOcupacionVentanillaInformes;
            row[34] = LineaReloj.acumuladorTiempoOciosoVentanillaActualizacion;
            row[35] = LineaReloj.tiempoMaximoEsperaEnCola;

            indice = 35;
                for (int j = 0; j < cantidadClientes; j++)
                {
                    indice += 1;
                    row[indice] = LineaReloj.clientes[j].estado;
                    indice += 1;
                    row[indice] = LineaReloj.clientes[j].horaLLegadaACaja.ToString() != "-1" ? LineaReloj.clientes[j].horaLLegadaACaja.ToString() : ""; ;
                }
            resultados.Rows.Add(row);

        }
    }
}
