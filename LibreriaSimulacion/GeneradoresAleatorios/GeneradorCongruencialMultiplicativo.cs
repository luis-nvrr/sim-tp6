using Numeros_aleatorios.LibreriaSimulacion.GeneradoresAleatorios;
using System.Data;

namespace Numeros_aleatorios.LibreriaSimulacion
{
    class GeneradorCongruencialMultiplicativo : IGenerador
    {
        // para la cantidad de decimales de los aleatorios
        private Truncador truncador;
        private DataTable dataTable;
        private DataRow dataRow;

        private long entradaAnterior;
        private long entradaActual;
        private double aleatorioActual;

        private double aleatorio;

        // parametros
        private int a;
        private long m;

        public GeneradorCongruencialMultiplicativo(DataTable tabla, Truncador truncador, long semilla, int a, long m)
        {
            this.entradaAnterior = semilla;
            this.truncador = truncador;
            this.a = a;
            this.m = m;

            this.dataTable = tabla;
        }

        // retorna un aleatorio
        public double siguienteAleatorio()
        {
            entradaActual = (a * entradaAnterior) % (m);
            aleatorioActual = (double)entradaActual / (m); // (m-1) para incluir el 1 
            entradaAnterior = entradaActual;
            return truncador.truncar(aleatorioActual);
        }

        public DataTable generarSerie(int cantidadAleatorios)
        {
            return this.generarSerie(cantidadAleatorios, null);
        }

        public DataTable generarSerie(int cantidadAleatorios, ContadorFrecuenciaObservada frecuenciaObservada)
        {
            dataTable.Rows.Clear();

            for (int i = 0; i < cantidadAleatorios; i++)
            {
                aleatorio = siguienteAleatorio();
                dataRow = dataTable.NewRow();
                dataRow[0] = i+1;
                dataRow[1] = aleatorio;
                dataTable.Rows.Add(dataRow);

                if (frecuenciaObservada != null) { frecuenciaObservada.contarNumero(aleatorio); }
            }
            return dataTable;
        }
    }
}
