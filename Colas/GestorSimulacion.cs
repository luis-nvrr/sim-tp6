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
        Simulacion simulacion;
        SimulacionReloj simulacionLlegadas;
        private decimal alfa;
        private double reloj100;
        private double reloj50;
        private double reloj70;


        public GestorSimulacion(PantallaResultados pantalla)
        {
            this.pantalla = pantalla;
        }

        public void simular(int filaDesde, int filaHasta, int cantSimulaciones, int TiempoLlegada, int TiempoFinInforme, int TiempoFinActualizacion)
        {
            calcularPrimerasLlegadas(TiempoLlegada, TiempoFinInforme, TiempoFinActualizacion);
            calcularTiempos();
            ejecutar(filaDesde, filaHasta, cantSimulaciones, TiempoLlegada, TiempoFinInforme, TiempoFinActualizacion, reloj50, reloj70, reloj100) ;
            calcularEstadisticas();
        }

        private void calcularTiempos()
        {
            RungeKutta rungeKutta = new RungeKutta((decimal)0.01, alfa, 50);
            reloj50 = (double)rungeKutta.calcularRungeKutta();

            RungeKuttaResultados rungeKutta50 = new RungeKuttaResultados();
            rungeKutta50.mostrarResultados(rungeKutta.tabla);

            rungeKutta = new RungeKutta((decimal)0.01, alfa, 70);
            reloj70 = (double)rungeKutta.calcularRungeKutta();

            RungeKuttaResultados rungeKutta70 = new RungeKuttaResultados();
            rungeKutta70.mostrarResultados(rungeKutta.tabla);
        }

        private void ejecutar(int filaDesde, int filaHasta, int cantSimulaciones, 
            int TiempoLlegada, int TiempoFinInforme, int TiempoFinActualizacion,
            double reloj50, double  reloj70, double reloj100)
        {
            simulacion = new Simulacion();
            simulacion.simular(filaDesde, filaHasta, cantSimulaciones, TiempoLlegada, 
                TiempoFinInforme, TiempoFinActualizacion, 0, reloj50, reloj70, reloj100); // cambiar 0 por los parametros de la unfiorme del cobro
            pantalla.mostrarResultados(simulacion.getResultados());
        }

        private void calcularPrimerasLlegadas(int TiempoLlegada, int TiempoFinInforme, int TiempoFinActualizacion)
        {
            PantallaLlegadas pantallaLlegadas = new PantallaLlegadas();
            simulacionLlegadas= new SimulacionReloj();
            simulacionLlegadas.calcularPrimerasLlegadas(TiempoLlegada, TiempoFinInforme, TiempoFinActualizacion);
            pantallaLlegadas.mostrarResultados(simulacionLlegadas.getResultados());
            calcularAlfa();
        }

        private void calcularAlfa()
        {
            this.reloj100 = simulacionLlegadas.getReloj();
            this.alfa = (decimal)(Math.Log(100 / 5) / reloj100);
        }

        public decimal getAlfa()
        {
            return alfa;
        }

        private void calcularEstadisticas()
        {
            simulacion.calcularEstadisticas(this);
        }

        public void mostrarEstadisticas(Double tiempoPromedioEsperaEnCajas,Double tiempoOcupacionInformes, Double tiempoOciosoActualizacion, Double tiempoMaximoEsperaCaja)
        {
            pantalla.mostrarEstadisticas(tiempoPromedioEsperaEnCajas, tiempoOcupacionInformes, tiempoOciosoActualizacion, tiempoMaximoEsperaCaja);
        }

    }
}
