using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numeros_aleatorios.Colas
{
    class Cliente
    {
        String ESPERANDO_CAJA = "EAC";
        String ESPERANDO_INFORME = "EAI";
        String ESPERANDO_ACTUALIZACION = "EAA";
        String SIENDO_ATENDIDO_CAJA = "SAC";
        String SIENDO_ATENDID_ACTUALIZACION = "SAA";
        String SIENDO_ATENDIDO_INFORME = "SAI";
        String CON_ATENCION_SUSPENDIDA_INFORME = "ASI";

        public String estado;
        public double horaLLegadaACaja;
        public double tiempoEsperaEnCaja;


        public Cliente()
        {
            this.estado = "";
            this.horaLLegadaACaja = 0;
            this.tiempoEsperaEnCaja = 0;
        }

        public void esperarInforme()
        {
            this.estado = ESPERANDO_INFORME;
        }

        public void limpiar()
        {
            estado = "";
            horaLLegadaACaja = -1;
            tiempoEsperaEnCaja = 0;
        }

        public Boolean estaLibre()
        {
            return this.estado == "";
        }

        public void esperarActualizacion()
        {
            this.estado = ESPERANDO_ACTUALIZACION;
        }

        public void atenderActualizacion()
        {
            this.estado = SIENDO_ATENDID_ACTUALIZACION;
        }

        public void atenderInforme()
        {
            this.estado = SIENDO_ATENDIDO_INFORME;
        }

        public void atenderCaja(int numero, double reloj)
        {
            if (estado.Equals(ESPERANDO_CAJA))
            {
                tiempoEsperaEnCaja = reloj - horaLLegadaACaja;
                horaLLegadaACaja = -1;
            }
           
            this.estado = SIENDO_ATENDIDO_CAJA + " " + numero;
        }

        public void esperarCaja()
        {
            this.estado = ESPERANDO_CAJA;
        }

        public void suspender()
        {
            this.estado = CON_ATENCION_SUSPENDIDA_INFORME;
        }
    }
}
