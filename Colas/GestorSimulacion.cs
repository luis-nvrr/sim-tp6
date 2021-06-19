using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numeros_aleatorios.Colas
{
    class GestorSimulacion
    {
        PantallaResultados pantalla;
        ColasMunicipalidad simulacion;
        ColasMunicipalidad simulacionLlegadas;
        private double alfa;
        private double relojPrimerasLlegadas;

        public GestorSimulacion(PantallaResultados pantalla)
        {
            this.pantalla = pantalla;
        }

        public void simular(int filaDesde, int filaHasta, int cantSimulaciones, int TiempoLlegada, int TiempoFinInforme, int TiempoFinActualizacion)
        {


            simulacion = new ColasMunicipalidad();
            simulacion.simular(filaDesde, filaHasta, cantSimulaciones, TiempoLlegada, TiempoFinInforme, TiempoFinActualizacion, 0); // cambiar 0 por los parametros de la unfiorme del cobro
            pantalla.mostrarResultados(simulacion.getResultados());
        }

        public void calcularPrimerasLlegadas(int TiempoLlegada, int TiempoFinInforme, int TiempoFinActualizacion)
        {
            PantallaLlegadas pantallaLlegadas = new PantallaLlegadas();
            simulacionLlegadas= new ColasMunicipalidad();
            simulacionLlegadas.calcularPrimerasLlegadas(TiempoLlegada, TiempoFinInforme, TiempoFinActualizacion);
            pantallaLlegadas.mostrarResultados(simulacionLlegadas.getResultados());
            this.relojPrimerasLlegadas = simulacionLlegadas.getReloj();
            this.alfa = Math.Log(100 / 5) / relojPrimerasLlegadas;
        }

        public double getAlfa()
        {
            return alfa;
        }

        public void calcularEstadisticas()
        {
            simulacion.calcularEstadisticas(this);
        }

        public void mostrarEstadisticas(Double tiempoPromedioEsperaEnCajas,Double tiempoOcupacionInformes, Double tiempoOciosoActualizacion, Double tiempoMaximoEsperaCaja)
        {
            pantalla.mostrarEstadisticas(tiempoPromedioEsperaEnCajas, tiempoOcupacionInformes, tiempoOciosoActualizacion, tiempoMaximoEsperaCaja);
        }

    }
}
