using Numeros_aleatorios.LibreriaSimulacion;
using Numeros_aleatorios.LibreriaSimulacion.GeneradoresAleatorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Numeros_aleatorios.Colas
{
    class LineaReloj
    {
        public string LLEGADA_PERSONA = "llegada de persona";
        public string FIN_INFORME = "fin de informe";
        public string FIN_ACTUALIZACION = "fin de actualizacion";
        public string FIN_COBRO = "fin de cobro";
        public string INICIALIZACION = "inicializacion";

        public IGenerador aleatorios;
       public Truncador truncador;
       public IGenerador poisson;
       public IGenerador uniforme;
       public IGenerador exponencial;
        public string evento;
        public double reloj;
        public double tiempoParaLlegada;
        public double tiempoFinInforme;
        public double llegadaCliente;
        public double rndEstadoFactura;
        public string estadoFactura;
        public double rndConoceProcedimiento;
        public string conoceProcedimiento;
        public double rndFinCobro;
        public double tiempoFinCobro;
        public double rndFinInforme;
        public List<Caja> cajas;
        public VentanillaActualizacion ventanillaActualizacion;
        public VentanillaInforme ventanillaInforme;
        public LineaReloj LineaRelojAnterior;

        public Caja cajaFinCobro;

        public List<Cliente> clientes;

        public SimulacionReloj colas;
        public List<Cliente> clientesLibre;

        public double acumuladorTiemposEsperaEnCaja;
        public long cantidadClientesEsperan;
        public double acumuladorTiempoOcupacionVentanillaInformes;
        public double acumuladorTiempoOciosoVentanillaActualizacion;
        public double tiempoMaximoEsperaEnCola;

        public int idFila;
        private int filaDesde;
        private int filaHasta;

        public int contadorLlegadas;

        private double reloj50;
        private double reloj70;
        private double reloj100;

        public LineaReloj(int cantidadCajas, double mediaLLegada, double mediaFinInforme, double a, double b)
        {
            this.truncador = new Truncador(4);
            this.aleatorios = new GeneradorUniformeLenguaje(truncador);
            this.poisson = new GeneradorPoisson((GeneradorUniformeLenguaje)aleatorios, truncador, mediaLLegada);
            this.ventanillaInforme = new VentanillaInforme();
            this.ventanillaActualizacion = new VentanillaActualizacion();
            this.uniforme = new GeneradorUniformeAB((GeneradorUniformeLenguaje)aleatorios, truncador, a, b);
            this.cajas = new List<Caja>();
            cargarCajas(cantidadCajas);
            this.clientes = new List<Cliente>();
            this.clientesLibre = new List<Cliente>();
            this.rndFinInforme = -1;
            this.rndFinCobro = -1;
            this.rndConoceProcedimiento = -1;
            this.rndEstadoFactura = -1;
            this.tiempoFinInforme = -1;
            this.tiempoParaLlegada = poisson.siguienteAleatorio();
            this.llegadaCliente = tiempoParaLlegada;
            this.tiempoFinCobro = -1;
            this.exponencial = new GeneradorExponencialNegativa((GeneradorUniformeLenguaje)aleatorios, truncador, (double)(1.0/mediaFinInforme));
            this.evento = INICIALIZACION;
        }

        public LineaReloj(LineaReloj anterior, SimulacionReloj colas, int filaDesde, int filaHasta, int idFila)
        {
            this.LineaRelojAnterior = anterior;
            this.truncador = anterior.truncador;
            this.aleatorios = anterior.aleatorios;
            this.poisson = anterior.poisson;
            this.uniforme = anterior.uniforme;
            this.exponencial = anterior.exponencial;

            this.ventanillaInforme = anterior.ventanillaInforme;
            this.ventanillaActualizacion = anterior.ventanillaActualizacion ;


            this.cajas = anterior.cajas;
            this.clientes = anterior.clientes;

            this.colas = colas;
            this.clientesLibre = anterior.clientesLibre;

            this.filaDesde = filaDesde;
            this.filaHasta = filaHasta;
            this.idFila = idFila;

            this.rndFinInforme = -1;
            this.rndFinCobro = -1;
            this.rndConoceProcedimiento = -1;
            this.rndEstadoFactura = -1;
            this.tiempoFinInforme = -1;
            this.tiempoFinCobro = -1;
            this.tiempoParaLlegada = -1;

            acumuladorTiemposEsperaEnCaja = anterior.acumuladorTiemposEsperaEnCaja;
            cantidadClientesEsperan = anterior.cantidadClientesEsperan;
            acumuladorTiempoOcupacionVentanillaInformes = anterior.acumuladorTiempoOcupacionVentanillaInformes;
            acumuladorTiempoOciosoVentanillaActualizacion = anterior.acumuladorTiempoOciosoVentanillaActualizacion;
            tiempoMaximoEsperaEnCola = anterior.tiempoMaximoEsperaEnCola;
            contadorLlegadas = anterior.contadorLlegadas;

            this.reloj50 = anterior.reloj50;
            this.reloj70 = anterior.reloj70;
            this.reloj100 = anterior.reloj100;
        }


        public void calcularFinInforme()
        {
            calcularTiempoOcupacionInformes();

            calcularFinInformeEventoFinInforme();
            calcularFinInformeEventoLlegadaCliente();
            calcularFinInformeEventoFinActualizacion();
        }

        public void calcularColumnaFinActualizacion(double tiempo)
        {
            sumarTiempoOciosoActualizacion();

            calcularFinActualizacionEventoFinActualizacion(tiempo);
            calcularFinActualizacionEventoFinInforme(tiempo);
            calcularFinActualizacionEventoLlegadaCliente(tiempo);
        }

        private void cargarCajas(int cantidadCajas)
        {
            for (int i = 1; i <= cantidadCajas; i++)
            {
                cajas.Add(new Caja(i));
            }
        }

        public void calcularEvento()
        {

            this.reloj = LineaRelojAnterior.llegadaCliente;
            this.evento = LLEGADA_PERSONA;
            cajaFinCobro = null;

            if (LineaRelojAnterior.ventanillaInforme.finInforme >0 && LineaRelojAnterior.ventanillaInforme.finInforme < reloj) {
                reloj = LineaRelojAnterior.ventanillaInforme.finInforme;
                evento = FIN_INFORME;
            }

            if (LineaRelojAnterior.ventanillaActualizacion.finActualizacion > 0 && LineaRelojAnterior.ventanillaActualizacion.finActualizacion < reloj)
            {
                reloj = LineaRelojAnterior.ventanillaActualizacion.finActualizacion;
                evento = FIN_ACTUALIZACION;
            }

            foreach (var caja in LineaRelojAnterior.cajas)
            {
                if (caja.finCobro < reloj && caja.finCobro > 0) {
                    reloj = caja.finCobro;
                    cajaFinCobro = caja;
                    evento = FIN_COBRO;
                }
            }

            if (this.evento.Equals(LLEGADA_PERSONA)) { contadorLlegadas++; }
        }


        public Boolean tieneLlegadasCumplidas()
        {
            return this.contadorLlegadas >= 60;
        }

        public void calcularSiguienteLlegada() {
            if (this.evento.Equals(LLEGADA_PERSONA))
            {
                this.tiempoParaLlegada = poisson.siguienteAleatorio();
                this.llegadaCliente = reloj + tiempoParaLlegada;
                return;
            }
            this.llegadaCliente = LineaRelojAnterior.llegadaCliente;
        }

        public void calcularEstadoFactura(double[] probabilidades, string[] estadosFactura)
        {
            rndEstadoFactura = -1;
            if (this.evento.Equals(LLEGADA_PERSONA))
            {
                rndEstadoFactura = aleatorios.siguienteAleatorio();
                this.estadoFactura = buscarProbabilidadEnVector(probabilidades, estadosFactura, rndEstadoFactura);
                return;
            }
            this.estadoFactura = "";
        }

        public void calcularConoceProcedimiento(double[] probabilidades, string[] conoceProcedimiento)
        {
            if (this.estadoFactura.Equals("vencida"))
            {
                rndConoceProcedimiento = aleatorios.siguienteAleatorio();
                this.conoceProcedimiento = buscarProbabilidadEnVector(probabilidades, conoceProcedimiento, rndConoceProcedimiento);
                return;
            }
            this.rndConoceProcedimiento = -1;
            this.conoceProcedimiento = "";
        }

        private string buscarProbabilidadEnVector(double[] probAcum, string[] vector, double random)
        {

            for (int i = 0; i < probAcum.Length; i++)
            {
                if (random < probAcum[i])
                {
                    return vector[i];
                }
            }
            return "";
        }

        private void calcularTiempoOcupacionInformes()
        {
            if (LineaRelojAnterior.ventanillaInforme.estaOcupada())
            {
                this.acumuladorTiempoOcupacionVentanillaInformes += (reloj - LineaRelojAnterior.reloj);
            }

        }

        private void calcularFinInformeEventoFinInforme()
        {

            if (this.evento.Equals(FIN_INFORME))
            {
                if (LineaRelojAnterior.tieneColaInforme())
                {
                    this.tiempoFinInforme = exponencial.siguienteAleatorio();
                    this.rndFinInforme = ((GeneradorExponencialNegativa)exponencial).getAleatorio();
                    Cliente clienteActual = ventanillaInforme.siguienteCliente();
                    atenderInforme(clienteActual, tiempoFinInforme);
                }
                else
                {
                    ventanillaInforme.liberar();
                }
            }
        }

        private void esperarInforme(Cliente clienteActual)
        {
            ventanillaInforme.agregarFinInforme(LineaRelojAnterior.obtenerFinInforme());
            ventanillaInforme.agregarACola(clienteActual);
            clienteActual.esperarInforme();
        }
        private void esperarActualizacion(Cliente clienteActual)
        {
            ventanillaActualizacion.agregarFinActualizacion(LineaRelojAnterior.obtenerFinActualizacion());
            ventanillaActualizacion.agregarACola(clienteActual);
            clienteActual.esperarActualizacion();
        }

        private void atenderInforme(Cliente clienteActual, double tiempo)
        {
            clienteActual.atenderInforme();
            ventanillaInforme.agregarFinInforme(this.reloj + tiempo);
            ventanillaInforme.clienteActual = clienteActual;
        }

        private void atenderActualizacion(Cliente clienteActual, double tiempo)
        {
            clienteActual.atenderActualizacion();
            ventanillaActualizacion.agregarFinActualizacion(this.reloj + tiempo);
            ventanillaActualizacion.clienteActual = clienteActual;
        }



        private void calcularFinInformeEventoFinActualizacion()
        {
            if (this.evento.Equals(FIN_ACTUALIZACION) && LineaRelojAnterior.tieneFinInforme())
            {
                ventanillaInforme.agregarFinInforme(LineaRelojAnterior.obtenerFinInforme());
            }
        }

        private Cliente buscarClienteLibre()
        {
            Cliente libre;

            if(clientesLibre.Count > 0) {
                libre = clientesLibre[clientesLibre.Count - 1];
                clientesLibre.RemoveAt(clientesLibre.Count - 1);
                return libre;
            }

            Cliente res = new Cliente();
            this.clientes.Add(res);
            if(idFila <= filaHasta)
            {
                colas.agregarColumna();
            }
         
            return res;
        }

        private Boolean tieneVentanillaInformeOcupada()
        {
            return this.ventanillaInforme.estaOcupada();
        }

        private Boolean tieneColaInforme()
        {
            return this.ventanillaInforme.tieneCola();
        }

        private Boolean tieneFinInforme()
        {
            return this.ventanillaInforme.tieneFinInforme();
        }

        private double obtenerFinInforme()
        {
            return this.ventanillaInforme.finInforme;
        }

        private void sumarTiempoOciosoActualizacion()
        {
            if (!LineaRelojAnterior.ventanillaActualizacion.estaOcupada())
            {
                this.acumuladorTiempoOciosoVentanillaActualizacion += (reloj - LineaRelojAnterior.reloj);
            }
        }

        private void calcularFinActualizacionEventoLlegadaCliente(double tiempo)
        {
            if ((this.conoceProcedimiento.Equals("si")))
            {
                Cliente clienteActual = buscarClienteLibre();
                if (LineaRelojAnterior.tieneVentanillaActualizacionOcupada())
                {
                    esperarActualizacion(clienteActual);
                }
                else
                {
                    atenderActualizacion(clienteActual, tiempo);

                }
            }
        }

        private void calcularFinInformeEventoLlegadaCliente()
        {
            if (this.conoceProcedimiento.Equals("no"))
            {
                Cliente clienteActual = buscarClienteLibre();
                if (LineaRelojAnterior.tieneVentanillaInformeOcupada())
                {
                    esperarInforme(clienteActual);
                }
                else
                {
                    this.tiempoFinInforme = exponencial.siguienteAleatorio();
                    this.rndFinInforme = ((GeneradorExponencialNegativa)exponencial).getAleatorio();
                    atenderInforme(clienteActual, tiempoFinInforme);

                }
            }
        }

        private void calcularFinActualizacionEventoFinInforme(double tiempo)
        {
            if (this.evento.Equals(FIN_INFORME))
            {
                Cliente clienteActual = LineaRelojAnterior.ventanillaInforme.getClienteActual();

                if (LineaRelojAnterior.tieneVentanillaActualizacionOcupada())
                {
                    esperarActualizacion(clienteActual);
                }
                else
                {
                    atenderActualizacion(clienteActual, tiempo);
                }
            }
        }

        private void calcularFinActualizacionEventoFinActualizacion(double tiempo)
        {
            if (this.evento.Equals(FIN_ACTUALIZACION))
            {
                if (this.LineaRelojAnterior.tieneColaActualizacion())
                {
                    Cliente clienteActual = ventanillaActualizacion.siguienteCliente();
                    atenderActualizacion(clienteActual, tiempo);
                }
                else
                {
                    ventanillaActualizacion.liberar();
                }
            }

        }


        private Boolean tieneVentanillaActualizacionOcupada()
        {
            return this.ventanillaActualizacion.estaOcupada();
        }

        private double obtenerFinActualizacion()
        {
            return this.ventanillaActualizacion.obtenerFinActualizacion();
        }

        private Boolean tieneFinActualizacion()
        {
            return this.ventanillaActualizacion.tieneFinActualizacion();
        }

        private Boolean tieneColaActualizacion()
        {
            return this.ventanillaActualizacion.tieneCola();
        }

        private void esperarCaja(Cliente nuevoCliente)
        {
            Caja.agregarACola(nuevoCliente);
            nuevoCliente.esperarCaja();
            nuevoCliente.horaLLegadaACaja = this.reloj;
        }

        private void atenderCaja(Cliente nuevoCliente, Caja cajaLibre, double tiempo)
        {
            cajaLibre.clienteActual = nuevoCliente;
            nuevoCliente.atenderCaja(cajaLibre.id, reloj);
            cajaLibre.agregarFinCobro(reloj + tiempo);
        }

        private void actualizarTiempoMaximoEsperaEnCola(Cliente clienteActual)
        {
            double maxTemp = clienteActual.tiempoEsperaEnCaja;
            this.acumuladorTiemposEsperaEnCaja += maxTemp;
            if (maxTemp > tiempoMaximoEsperaEnCola) { tiempoMaximoEsperaEnCola = maxTemp; }
        }

        public void calcularFinCobro()
        {

            if (this.evento.Equals(FIN_COBRO))
            {
                this.rndFinCobro = ((GeneradorUniformeAB)uniforme).getAleatorio();
                this.tiempoFinCobro = uniforme.siguienteAleatorio();
                Cliente clienteViejo = cajaFinCobro.clienteActual;
                clienteViejo.limpiar();
                clientesLibre.Add(clienteViejo);
                cajaFinCobro.liberar();

                if (LineaRelojAnterior.tieneColaCobro())
                {
                    Cliente clienteActual = Caja.siguienteCliente();
                    atenderCaja(clienteActual, cajaFinCobro, tiempoFinCobro);
                    actualizarTiempoMaximoEsperaEnCola(clienteActual);
                    cantidadClientesEsperan++;
                }
                else
                {
                    
                }
            }

            if (this.estadoFactura.Equals("al dia")) 
            {
                this.rndFinCobro = ((GeneradorUniformeAB)uniforme).getAleatorio();
                this.tiempoFinCobro = uniforme.siguienteAleatorio();
                Cliente nuevoCliente = buscarClienteLibre();
                Caja cajaLibre = buscarCajaLibre();

                if (cajaLibre == null)
                {
                    esperarCaja(nuevoCliente);
                }
                else
                {
                    atenderCaja(nuevoCliente, cajaLibre, tiempoFinCobro);
                }
            } 

            if (this.evento.Equals(FIN_ACTUALIZACION))
            {
                this.rndFinCobro = ((GeneradorUniformeAB)uniforme).getAleatorio();
                this.tiempoFinCobro = uniforme.siguienteAleatorio();
                Cliente clienteActual = LineaRelojAnterior.ventanillaActualizacion.getClienteActual();
                Caja cajaLibre = buscarCajaLibre();

                if (cajaLibre == null)
                {
                    esperarCaja(clienteActual);
                }
                else
                {
                    atenderCaja(clienteActual, cajaLibre, tiempoFinCobro);
                    actualizarTiempoMaximoEsperaEnCola(clienteActual);
                }
            }
        }

        private Boolean tieneColaCobro()
        {
            return Caja.tieneCola();
        }

        private Caja buscarCajaLibre()
        {
            foreach (var caja in cajas)
            {
                if (caja.estaLibre()) return caja;
            }
            return null;
        }
    }
}
