using Numeros_aleatorios.LibreriaSimulacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Numeros_aleatorios.Colas
{
    class GestorSimulacion
    {
        PantallaResultados pantalla;
        Simulacion simulacion;
        SimulacionReloj simulacionLlegadas;
        private double alfa;
        private double reloj100;
        private double reloj50;
        private double reloj70;


        public GestorSimulacion(PantallaResultados pantalla)
        {
            this.pantalla = pantalla;
        }

        public void simular(int filaDesde, int filaHasta, int cantSimulaciones, int TiempoLlegada, 
            double TiempoFinInforme, double TiempoFinActualizacion, double uniformeA,double uniformeB,
            double hInestabilidad, double hDescarga)
        {
            calcularPrimerasLlegadas(TiempoLlegada, TiempoFinInforme, TiempoFinActualizacion, uniformeA, uniformeB);
            calcularTiempos(hInestabilidad);
            ejecutar(filaDesde, filaHasta, cantSimulaciones, TiempoLlegada, TiempoFinInforme, 
                TiempoFinActualizacion, reloj50, reloj70, reloj100, uniformeA, uniformeB, hDescarga) ;
            calcularEstadisticas();
        }

        private void calcularTiempos(double hInestabilidad)
        {
            RungeKutta rungeKutta = new RungeKutta();
            RungeKuttaResultados pantallaRungeKutta50 = new RungeKuttaResultados();
        
            rungeKutta.calcularRungeKuttaTiemposInestable((double)hInestabilidad, alfa, 50, 70);
            reloj50 = rungeKutta.tiempo50;
            reloj70 = rungeKutta.tiempo70;

            Truncador truncador = new Truncador(4);

            pantallaRungeKutta50.mostrarResultados(rungeKutta.tabla, "Inestabilidad al 50%: " + truncador.truncar(reloj50).ToString() + " seg" + "\n"
                + "Inestabilidad al 70%: " + truncador.truncar(reloj70).ToString() + " seg" + "\n"
                + "Inestabilidad al 100%: " + truncador.truncar(reloj100).ToString() + " seg" + "\n"
                 + "Alfa: " + alfa);
        }

        private void ejecutar(int filaDesde, int filaHasta, int cantSimulaciones, 
            int TiempoLlegada, double TiempoFinInforme, double TiempoFinActualizacion,
            double reloj50, double  reloj70, double reloj100, double uniformeA, double uniformeB,
            double hDescarga)
        {
            simulacion = new Simulacion(alfa);
            simulacion.simular(filaDesde, filaHasta, cantSimulaciones, TiempoLlegada, 
                TiempoFinInforme, TiempoFinActualizacion, reloj50, 
                reloj70, reloj100, uniformeA, uniformeB, hDescarga);
            pantalla.mostrarResultados(simulacion.getResultados());
            pantalla.Show();
        }

        private void calcularPrimerasLlegadas(int TiempoLlegada, double TiempoFinInforme, double TiempoFinActualizacion, double uniformeA, double uniformeB)
        {
            PantallaLlegadas pantallaLlegadas = new PantallaLlegadas();
            simulacionLlegadas= new SimulacionReloj();
            simulacionLlegadas.calcularPrimerasLlegadas(TiempoLlegada, TiempoFinInforme, TiempoFinActualizacion, uniformeA, uniformeB);
            pantallaLlegadas.mostrarResultados(simulacionLlegadas.getResultados());
            calcularAlfa();
        }

        private void calcularAlfa()
        {
            this.reloj100 = simulacionLlegadas.getReloj();
            this.alfa = (double)(Math.Log(100 / 5) / reloj100);
        }

        public double getAlfa()
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
