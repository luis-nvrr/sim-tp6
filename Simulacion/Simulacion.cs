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
        double[] probabilidadesEstadosAcum = new double[] { 1, 0 };
        string[] estadosFactura = new string[] { "vencida", "al dia" };

        double[] probabilidadesConoceProcedimientoAcum = new double[] { 0, 1 };
        string[] conoceProcedimiento = new string[] { "si", "no" };

        double[] probabilidadesInestable = new double[] { 0.2, 0.5, 1 };
        double[] tiempos;

        DataTable resultados;
        private int cantidadClientes;
        private int indice;
        private Linea lineaActual;
        private double alfa;

        private List<List<String>> lineas;
        
        public Simulacion(double alfa)
        {
            this.alfa = alfa;
            resultados = new DataTable();
            crearTabla(resultados);
            this.lineas = new List<List<string>>(100);
        }

        private void crearTabla(DataTable tabla)
        {
            tabla.Columns.Add("i", typeof(string));
            tabla.Columns.Add("cant llegadas", typeof(string));
            tabla.Columns.Add("evento", typeof(string));
            tabla.Columns.Add("reloj", typeof(string));
            tabla.Columns.Add("tiempo para llegada", typeof(string));
            tabla.Columns.Add("llegada cliente", typeof(string));
            tabla.Columns.Add("RND estado factura", typeof(string));
            tabla.Columns.Add("estado factura", typeof(string));
            tabla.Columns.Add("RND conoce", typeof(string));
            tabla.Columns.Add("conoce procedimiento", typeof(string));
            tabla.Columns.Add("RND inestable", typeof(string));
            tabla.Columns.Add("tiempo p/ inestable", typeof(string));
            tabla.Columns.Add("inestable", typeof(string));
            tabla.Columns.Add("tiempo de purga", typeof(string));
            tabla.Columns.Add("fin de purga", typeof(string));
            tabla.Columns.Add("RND fin de informe", typeof(string));
            tabla.Columns.Add("tiempo de informe", typeof(string));
            tabla.Columns.Add("fin de informe", typeof(string));
            tabla.Columns.Add("estado informes", typeof(string));
            tabla.Columns.Add("cola informes", typeof(string));
            tabla.Columns.Add("tiempo restante", typeof(string));
            tabla.Columns.Add("fin actualizacion", typeof(string));
            tabla.Columns.Add("RND Fin Cobro", typeof(string));
            tabla.Columns.Add("Tiempo Fin Cobro", typeof(string));
            tabla.Columns.Add("fin caja 1",typeof(string));
            tabla.Columns.Add("fin caja 2", typeof(string));
            tabla.Columns.Add("fin caja 3", typeof(string));
            tabla.Columns.Add("fin caja 4", typeof(string));
            tabla.Columns.Add("fin caja 5", typeof(string));
            tabla.Columns.Add("estado actualizacion",typeof(string));
            tabla.Columns.Add("cola actualizacion",typeof(string));
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


        public void simular(int filaDesde, int filaHasta, int cantSimulaciones, 
            int TiempoLlegada, double TiempoFinInforme, double TiempoFinActualizacion, 
            double reloj50, double reloj70, double reloj100, double UniformeA, double UniformeB,
            double pasoDescarga)
        {
            tiempos = new double[]{reloj50, reloj70, reloj100 };

            Linea lineaAnterior = new Linea(5,TiempoLlegada,TiempoFinInforme,UniformeA, UniformeB);
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
                lineaActual.calcularFinPurga(alfa, pasoDescarga);
                lineaActual.calcularFinCobro();
                lineaActual.calcularColumnaFinActualizacion(TiempoFinActualizacion);
                lineaActual.calcularFinInforme();
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

        public void agregarColumna()
        {
            cantidadClientes++;
            this.resultados.Columns.Add("estado " + cantidadClientes, typeof(string));
            this.resultados.Columns.Add("hora espera " + cantidadClientes, typeof(string));
        }

        private void agregarLinea(Linea linea, int i)
        {
            DataRow row = resultados.NewRow();
            row[0] = i.ToString();
            row[1] = linea.contadorLlegadas.ToString();
            row[2] = linea.evento.ToString();
            row[3] = linea.reloj.ToString();
            row[4] = linea.tiempoParaLlegada.ToString() != "-1" ? linea.tiempoParaLlegada.ToString() : "".ToString();
            row[5] = linea.llegadaCliente.ToString();
            row[6] = linea.rndEstadoFactura.ToString() != "-1" ? linea.rndEstadoFactura.ToString() : "".ToString();
            row[7] = linea.estadoFactura.ToString();
            row[8] = linea.rndConoceProcedimiento.ToString() != "-1" ? linea.rndConoceProcedimiento.ToString() : "".ToString();
            row[9] = linea.conoceProcedimiento.ToString();
            row[10] = linea.rndInestable.ToString() != "-1" ? linea.rndInestable.ToString() : "".ToString();
            row[11] = linea.tiempoInestable.ToString() != "-1" ? linea.tiempoInestable.ToString() : "".ToString();
            row[12] = linea.finInestable.ToString() != "-1" ? linea.finInestable.ToString() : "".ToString();
            row[13] = linea.tiempoPurga.ToString() != "-1" ? linea.tiempoPurga.ToString() : "".ToString();
            row[14] = linea.finPurga.ToString() != "-1" ? linea.finPurga.ToString() : "".ToString();
            row[15] = linea.rndFinInforme.ToString() != "-1" ? linea.rndFinInforme.ToString() : "".ToString();
            row[16] = linea.tiempoFinInforme.ToString() != "-1" ? linea.tiempoFinInforme.ToString() : "".ToString();
            row[17] = linea.ventanillaInforme.finInforme.ToString() != "-1" ? linea.ventanillaInforme.finInforme.ToString() : "".ToString();
            row[18] = linea.ventanillaInforme.estado;
            row[19] = linea.ventanillaInforme.cola.Count.ToString();
            row[20] = linea.ventanillaInforme.tiempoRestanteAtencion.ToString() != "-1" ? linea.ventanillaInforme.tiempoRestanteAtencion.ToString() : "".ToString();
            row[21] = linea.ventanillaActualizacion.finActualizacion.ToString() != "-1" ? linea.ventanillaActualizacion.finActualizacion.ToString() : "".ToString();
            row[22] = linea.rndFinCobro.ToString() != "-1" ? linea.rndFinCobro.ToString() : "".ToString();
            row[23] = linea.tiempoFinCobro.ToString() != "-1" ? linea.tiempoFinCobro.ToString() : "".ToString();
            row[24] = linea.cajas[0].finCobro.ToString() != "-1" ? linea.cajas[0].finCobro.ToString() : "".ToString() ;
            row[25] = linea.cajas[1].finCobro.ToString() != "-1" ? linea.cajas[1].finCobro.ToString() : "".ToString();
            row[26] = linea.cajas[2].finCobro.ToString() != "-1" ? linea.cajas[2].finCobro.ToString() : "".ToString();
            row[27] = linea.cajas[3].finCobro.ToString() != "-1" ? linea.cajas[3].finCobro.ToString() : "".ToString();
            row[28] = linea.cajas[4].finCobro.ToString() != "-1" ? linea.cajas[4].finCobro.ToString() : "".ToString();
            row[29] = linea.ventanillaActualizacion.estado;
            row[30] = linea.ventanillaActualizacion.cola.Count.ToString();  
            row[31]  = linea.cajas[0].estado.ToString();
            row[32] = linea.cajas[1].estado.ToString();
            row[33] = linea.cajas[2].estado.ToString();
            row[34] = linea.cajas[3].estado.ToString();
            row[35] = linea.cajas[4].estado.ToString();
            row[36] = Caja.cola.Count.ToString();
            row[37] = linea.acumuladorTiemposEsperaEnCaja.ToString();
            row[38] = linea.cantidadClientesEsperan.ToString();
            row[39] = linea.acumuladorTiempoOcupacionVentanillaInformes.ToString();
            row[40] = linea.acumuladorTiempoOciosoVentanillaActualizacion.ToString();
            row[41] = linea.tiempoMaximoEsperaEnCola.ToString();

            indice = 41;
                for (int j = 0; j < cantidadClientes; j++)
                {
                    indice += 1;
                    row[indice] = linea.clientes[j].estado.ToString();
                    indice += 1;
                    row[indice] = linea.clientes[j].horaLLegadaACaja.ToString() != "-1" ? linea.clientes[j].horaLLegadaACaja.ToString() : "".ToString(); ;
                }
            resultados.Rows.Add(row);
        }

    }
}
