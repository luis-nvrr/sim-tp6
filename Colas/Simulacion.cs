using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Numeros_aleatorios.Colas
{
    class Simulacion
    {
        double[] probabilidadesEstadosAcum = new double[] { 0.4, 1 };
        string[] estadosFactura = new string[] { "vencida", "al dia" };

        double[] probabilidadesConoceProcedimientoAcum = new double[] { 0.8, 1 };
        string[] conoceProcedimiento = new string[] { "si", "no" };

        double[] probabilidadesInestable = new double[] { 0.2, 0.5, 1 };
        double[] tiempos;

        DataTable resultados;
        DataTable temp;
        private int cantidadClientes;
        private int indice;
        private int cantidadPaginas;
        private List<DataTable> paginas;
        private Linea lineaActual;
        private decimal alfa;
        
        public Simulacion(decimal alfa)
        {
            this.alfa = alfa;
            resultados = new DataTable();
            crearTabla(resultados);
            this.paginas = new List<DataTable>();
        }

        private void crearTabla(DataTable tabla)
        {
            tabla.Columns.Add("i");
            tabla.Columns.Add("cant llegadas");
            tabla.Columns.Add("evento");
            tabla.Columns.Add("reloj");
            tabla.Columns.Add("tiempo para llegada");
            tabla.Columns.Add("llegada cliente");
            tabla.Columns.Add("RND estado factura.");
            tabla.Columns.Add("estado factura");
            tabla.Columns.Add("RND conoce");
            tabla.Columns.Add("conoce procedimiento");
            tabla.Columns.Add("RND inestable");
            tabla.Columns.Add("tiempo p/ inestable");
            tabla.Columns.Add("fin de inestable");
            tabla.Columns.Add("tiempo de purga");
            tabla.Columns.Add("fin de purga");
            tabla.Columns.Add("RND fin de informe");
            tabla.Columns.Add("tiempo de informe");
            tabla.Columns.Add("fin informacion");
            tabla.Columns.Add("fin actualizacion");
            tabla.Columns.Add("RND Fin Cobro");
            tabla.Columns.Add("Tiempo Fin Cobro");
            tabla.Columns.Add("fin caja 1");
            tabla.Columns.Add("fin caja 2");
            tabla.Columns.Add("fin caja 3");
            tabla.Columns.Add("fin caja 4");
            tabla.Columns.Add("fin caja 5");
            tabla.Columns.Add("estado informes");
            tabla.Columns.Add("cola informes");
            tabla.Columns.Add("tiempo restante");
            tabla.Columns.Add("estado actualizacion");
            tabla.Columns.Add("cola actualizacion");
            tabla.Columns.Add("estado caja 1");
            tabla.Columns.Add("estado caja 2");
            tabla.Columns.Add("estado caja 3");
            tabla.Columns.Add("estado caja 4");
            tabla.Columns.Add("estado caja 5");
            tabla.Columns.Add("cola caja");
            tabla.Columns.Add("acumulado tiempo espera en caja"); //29
            tabla.Columns.Add("cantidad que espera");
            tabla.Columns.Add("tiempo ocupacion informes");
            tabla.Columns.Add("tiempo ocioso actualizacion");
            tabla.Columns.Add("maxima espera en caja");
        }


        public void simular(int filaDesde, int filaHasta, int cantSimulaciones, 
            int TiempoLlegada, int TiempoFinInforme, int TiempoFinActualizacion, int TiempoFinCobro, 
            double reloj50, double reloj70, double reloj100 )
        {
            tiempos = new double[]{reloj50, reloj70, reloj100 };

            Linea lineaAnterior = new Linea(5,TiempoLlegada,TiempoFinInforme,20, 50);
            lineaAnterior.calcularFinInestable(probabilidadesInestable, tiempos);
            int i;

            agregarLinea(lineaAnterior, 0);

            for (i = 1; i <= cantSimulaciones; i++)
            {
                lineaActual = new Linea(lineaAnterior, this, filaDesde, filaHasta, i);
                lineaActual.calcularEvento();
                lineaActual.calcularSiguienteLlegada();
                lineaActual.calcularEstadoFactura(probabilidadesEstadosAcum, estadosFactura);
                lineaActual.calcularConoceProcedimiento(probabilidadesConoceProcedimientoAcum, conoceProcedimiento);
                lineaActual.calcularFinInestable(probabilidadesInestable, tiempos);
                lineaActual.calcularFinPurga(alfa);
                lineaActual.calcularFinInforme();
                lineaActual.calcularColumnaFinActualizacion(TiempoFinActualizacion);
                lineaActual.calcularFinCobro();
                lineaAnterior = lineaActual;

                if (i >= filaDesde && i <= filaHasta)
                {
                    agregarLinea(lineaActual, i);
                }
            }

            agregarLinea(lineaActual, lineaActual.idFila);
        }

        public DataTable getResultados()
        {
            return this.resultados;
        }

        public void calcularEstadisticas(GestorSimulacion gestor)
        {
            int tamañoTabla = resultados.Rows.Count-1;
            string tiempoEspera = resultados.Rows[tamañoTabla][37].ToString();
            string cantidadEspera = resultados.Rows[tamañoTabla][38].ToString();
            double tiempoPromedioEsperaEnCajas = cantidadEspera != "0" ?
                                                    double.Parse(tiempoEspera) / double.Parse(cantidadEspera) : 0;


            string ocupacionInformes = resultados.Rows[tamañoTabla][39].ToString();
            double tiempoOcupacionInformes = double.Parse(ocupacionInformes);

            string ociosoActualizacion = resultados.Rows[tamañoTabla][40].ToString();
            double tiempoOciosoActualizacion = double.Parse(ociosoActualizacion);

            string maximaEsperaCaja = resultados.Rows[tamañoTabla][41].ToString();
            double tiempoMaximoEsperaCaja = double.Parse(maximaEsperaCaja);

            gestor.mostrarEstadisticas(tiempoPromedioEsperaEnCajas, tiempoOcupacionInformes, tiempoOciosoActualizacion, tiempoMaximoEsperaCaja);
        }

        public double getReloj()
        {
            return this.lineaActual.reloj;
        }

        public void agregarColumna()
        {
            cantidadClientes++;
            this.resultados.Columns.Add("estado " + cantidadClientes);
            this.resultados.Columns.Add("hora espera " + cantidadClientes);
        }

        private void agregarLinea(Linea linea, int i)
        {
            DataRow row = resultados.NewRow();
            row[0] = i;
            row[1] = linea.contadorLlegadas;
            row[2] = linea.evento;
            row[3] = linea.reloj;
            row[4] = linea.tiempoParaLlegada;
            row[5] = linea.llegadaCliente;
            row[6] = linea.rndEstadoFactura.ToString() != "-1" ? linea.rndEstadoFactura.ToString() : "";
            row[7] = linea.estadoFactura;
            row[8] = linea.rndConoceProcedimiento.ToString() != "-1" ? linea.rndConoceProcedimiento.ToString() : "";
            row[9] = linea.conoceProcedimiento;
            row[10] = linea.rndInestable.ToString() != "-1" ? linea.rndInestable.ToString() : "";
            row[11] = linea.tiempoInestable.ToString() != "-1" ? linea.tiempoInestable.ToString() : "";
            row[12] = linea.finInestable.ToString() != "-1" ? linea.finInestable.ToString() : "";
            row[13] = linea.tiempoPurga.ToString() != "-1" ? linea.tiempoPurga.ToString() : "";
            row[14] = linea.finPurga.ToString() != "-1" ? linea.finPurga.ToString() : "";
            row[15] = linea.rndFinInforme.ToString() != "-1"? linea.rndFinInforme.ToString(): "";
            row[16] = linea.tiempoFinInforme.ToString() != "-1" ? linea.tiempoFinInforme.ToString() : "";
            row[17] = linea.ventanillaInforme.finInforme.ToString() != "-1" ? linea.ventanillaInforme.finInforme.ToString() : "";
            row[18] = linea.ventanillaActualizacion.finActualizacion.ToString() != "-1" ? linea.ventanillaActualizacion.finActualizacion.ToString() : "";
            row[19] = linea.rndFinCobro.ToString() != "-1" ? linea.rndFinCobro.ToString( ): "";
            row[20] = linea.tiempoFinCobro.ToString() != "-1"? linea.tiempoFinCobro.ToString() : "";
            row[21] = linea.cajas[0].finCobro.ToString() != "-1" ? linea.cajas[0].finCobro.ToString() : "" ;
            row[22] = linea.cajas[1].finCobro.ToString() != "-1" ? linea.cajas[1].finCobro.ToString() : "";
            row[23] = linea.cajas[2].finCobro.ToString() != "-1" ? linea.cajas[2].finCobro.ToString() : "";
            row[24] = linea.cajas[3].finCobro.ToString() != "-1" ? linea.cajas[3].finCobro.ToString() : "";
            row[25] = linea.cajas[4].finCobro.ToString() != "-1" ? linea.cajas[4].finCobro.ToString() : "";
            row[26] = linea.ventanillaInforme.estado;
            row[27] = linea.ventanillaInforme.cola.Count;
            row[28] = linea.ventanillaInforme.tiempoRestanteAtencion.ToString() != "-1" ? linea.ventanillaInforme.tiempoRestanteAtencion.ToString() : "";
            row[29] = linea.ventanillaActualizacion.estado;
            row[30] = linea.ventanillaActualizacion.cola.Count;
            row[31]  = linea.cajas[0].estado;
            row[32] = linea.cajas[1].estado;
            row[33] = linea.cajas[2].estado;
            row[34] = linea.cajas[3].estado;
            row[35] = linea.cajas[4].estado;
            row[36] = linea.colaCaja;
            row[37] = linea.acumuladorTiemposEsperaEnCaja;
            row[38] = linea.cantidadClientesEsperan;
            row[39] = linea.acumuladorTiempoOcupacionVentanillaInformes;
            row[40] = linea.acumuladorTiempoOciosoVentanillaActualizacion;
            row[41] = linea.tiempoMaximoEsperaEnCola;

            indice = 41;
                for (int j = 0; j < cantidadClientes; j++)
                {
                    indice += 1;
                    row[indice] = linea.clientes[j].estado;
                    indice += 1;
                    row[indice] = linea.clientes[j].horaLLegadaACaja.ToString() != "-1" ? linea.clientes[j].horaLLegadaACaja.ToString() : ""; ;
                }
            resultados.Rows.Add(row);

         
        }

    }
}
