using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Numeros_aleatorios.LibreriaSimulacion.GeneradoresAleatorios
{
    class GeneradorNormalConvolucion : IGenerador
    {
        private Truncador truncador;
        private GeneradorUniformeLenguaje generadorLenguaje;
        private DataTable dataTable;
        private DataRow dataRow;

        private double[] aleatorios;
        private double aleatorio01;

        private double acumulado;
        private double aleatorio;

        // parametros
        private double desviacion;
        private double media;


        private double menor;
        private double mayor;

        public GeneradorNormalConvolucion(DataTable tabla, GeneradorUniformeLenguaje generadorLenguaje, Truncador truncador, double desviacion, double media)
        {
            this.truncador = truncador;
            this.desviacion = desviacion;
            this.media = media;

            this.acumulado = 0;
            this.aleatorios = new double[12];
            this.generadorLenguaje = generadorLenguaje;
            this.dataTable = tabla; 
        }

        // retorna un aleatorio
        public double siguienteAleatorio()
        {
            acumulado = 0;
            for (int i = 0; i < aleatorios.Length; i++)
            {
                aleatorio01 = generadorLenguaje.siguienteAleatorio();
                aleatorios[i] = aleatorio01;
                acumulado += aleatorio01;
            }
            return truncador.truncar((acumulado - 6)*desviacion + media);
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

                if (i == 0) { inicializarMenorMayor(aleatorio); }
                actualizarMayor(aleatorio);
                actualizarMenor(aleatorio);
                //if(menor < 0) { MessageBox.Show(menor.ToString()); }

                dataRow = dataTable.NewRow();
                dataRow[0] = i+1;
                dataRow[1] = aleatorio;
                dataTable.Rows.Add(dataRow);

                if (frecuenciaObservada != null) { frecuenciaObservada.contarNumero(aleatorio); }
            }
            return dataTable;
        }

        private void inicializarMenorMayor(double numero)
        {
            menor = truncador.truncar(Math.Floor(numero));
            mayor = truncador.truncar(Math.Ceiling(numero));
        }

        private void actualizarMenor(double numero)
        {
            if (numero < menor)
            {
                menor = truncador.truncar(Math.Floor(numero));
            }
        }

        private void actualizarMayor(double numero)
        {
            if (numero > mayor)
            {
                mayor = truncador.truncar(Math.Ceiling(numero));
            }
        }

        public double getMayor()
        {
            return mayor;
        }

        public double getMenor()
        {
            return menor;
        }
    }
}
