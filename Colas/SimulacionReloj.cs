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
        double[] probabilidadesEstadosAcum = new double[] { 0.4, 1 };
        string[] estadosFactura = new string[] { "vencida", "al dia" };

        double[] probabilidadesConoceProcedimientoAcum = new double[] { 0.8, 1 };
        string[] conoceProcedimiento = new string[] { "si", "no" };

        DataTable resultados;
        DataTable temp;
        private int cantidadClientes;
        private int indice;
        private int cantidadPaginas;
        private List<DataTable> paginas;
        private LineaReloj LineaRelojActual;


        public SimulacionReloj()
        {
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


        public void simular(int filaDesde, int filaHasta, int cantSimulacionRelojes, 
            int TiempoLlegada, int TiempoFinInforme, int TiempoFinActualizacion, int TiempoFinCobro, 
            double reloj50, double reloj70, double reloj100 )
        {
            LineaReloj LineaRelojAnterior = new LineaReloj(5,TiempoLlegada, TiempoFinInforme,20, 50);
            int i;

            agregarLineaReloj(LineaRelojAnterior, 0);

            for (i = 1; i <= cantSimulacionRelojes; i++)
            {
                LineaRelojActual = new LineaReloj(LineaRelojAnterior, this, filaDesde, filaHasta, i);
                LineaRelojActual.calcularEvento();
                LineaRelojActual.calcularSiguienteLlegada();
                LineaRelojActual.calcularEstadoFactura(probabilidadesEstadosAcum, estadosFactura);
                LineaRelojActual.calcularConoceProcedimiento(probabilidadesConoceProcedimientoAcum, conoceProcedimiento);
                LineaRelojActual.calcularFinInforme();
                LineaRelojActual.calcularColumnaFinActualizacion(TiempoFinActualizacion);
                LineaRelojActual.calcularFinCobro();
                LineaRelojAnterior = LineaRelojActual;

                if (i >= filaDesde && i <= filaHasta)
                {
                    agregarLineaReloj(LineaRelojActual, i);
                }
            }

            agregarLineaReloj(LineaRelojActual, LineaRelojActual.idFila);
        }

        public DataTable getResultados()
        {
            return this.resultados;
        }


        public void calcularPrimerasLlegadas(int TiempoLlegada, int TiempoFinInforme, int TiempoFinActualizacion)
        {
            LineaReloj LineaRelojAnterior = new LineaReloj(5, TiempoLlegada, TiempoFinInforme, 20, 50);

            agregarLineaReloj(LineaRelojAnterior, 0);
            int i = 0;
            do
            {
                LineaRelojActual = new LineaReloj(LineaRelojAnterior, this, 0, 100000000, i);
                LineaRelojActual.calcularEvento();
                LineaRelojActual.calcularSiguienteLlegada();
                LineaRelojActual.calcularEstadoFactura(probabilidadesEstadosAcum, estadosFactura);
                LineaRelojActual.calcularConoceProcedimiento(probabilidadesConoceProcedimientoAcum, conoceProcedimiento);
                LineaRelojActual.calcularFinInforme();
                LineaRelojActual.calcularColumnaFinActualizacion(TiempoFinActualizacion);
                LineaRelojActual.calcularFinCobro();
                LineaRelojAnterior = LineaRelojActual;
                agregarLineaReloj(LineaRelojActual, i);
                i++;
            }
            while (!LineaRelojActual.tieneLlegadasCumplidas());
        }

        //private void construirPaginas()
        //{
        //    int columnasPorPagina = 8;
        //    cantidadPaginas = (int)Math.Ceiling((double)(resultados.Columns.Count-1) / (double)columnasPorPagina);

        //    for (int i = 1; i <= cantidadPaginas; i++)
        //    {
        //        int columnaDesde = i * columnasPorPagina - columnasPorPagina + 1;
        //        int columnaHasta = i * columnasPorPagina + 1;
        //        construirTablaEntre(columnaDesde, columnaHasta);
        //        paginas.Add(temp);
        //    }
        //}

        //public void mostrarPagina(int pagina)
        //{
        //    if(pagina >=1 && pagina <= cantidadPaginas)
        //    {
        //        pantallaResultados.mostrarResultados(paginas[pagina - 1]);
        //    } 
        //}

        public void calcularEstadisticas(GestorSimulacion gestor)
        {
            int tamañoTabla = resultados.Rows.Count-1;
            string tiempoEspera = resultados.Rows[tamañoTabla][31].ToString();
            string cantidadEspera = resultados.Rows[tamañoTabla][32].ToString();
            double tiempoPromedioEsperaEnCajas = cantidadEspera != "0" ?
                                                    double.Parse(tiempoEspera) / double.Parse(cantidadEspera) : 0;


            string ocupacionInformes = resultados.Rows[tamañoTabla][33].ToString();
            double tiempoOcupacionInformes = double.Parse(ocupacionInformes);

            string ociosoActualizacion = resultados.Rows[tamañoTabla][34].ToString();
            double tiempoOciosoActualizacion = double.Parse(ociosoActualizacion);

            string maximaEsperaCaja = resultados.Rows[tamañoTabla][35].ToString();
            double tiempoMaximoEsperaCaja = double.Parse(maximaEsperaCaja);

            gestor.mostrarEstadisticas(tiempoPromedioEsperaEnCajas, tiempoOcupacionInformes, tiempoOciosoActualizacion, tiempoMaximoEsperaCaja);
        }



        private void construirTablaEntre(int desde, int hasta)
        {
            if(hasta > resultados.Columns.Count)
            {
                hasta = resultados.Columns.Count;
            }

            temp = new DataTable();

            temp.Columns.Add(resultados.Columns[0].ColumnName);
            for (int i = desde; i < hasta; i++)
            {
                temp.Columns.Add(resultados.Columns[i].ColumnName);
            }

            foreach(DataRow row in resultados.Rows)
            {
                var r = temp.Rows.Add();
                r[0] = row[0];
                for (int j = desde; j < hasta; j++)
                {
                    var column = resultados.Columns[j].ColumnName;
                    r[column] = row[column];
                }
            }

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
            row[30] = LineaReloj.colaCaja;
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
