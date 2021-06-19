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
    class Linea
    {
        public string LLEGADA_PERSONA = "llegada de persona";
        public string FIN_INFORME = "fin de informe";
        public string FIN_ACTUALIZACION = "fin de actualizacion";
        public string FIN_COBRO = "fin de cobro";

       public IGenerador aleatorios;
       public Truncador truncador;
       public IGenerador poisson;
       public IGenerador uniforme;
       public IGenerador exponencial; 
       public string evento { get; set; }
       public double reloj { get; set; }
       public double tiempoParaLlegada { get; set; }
       public double tiempoFinInforme { get; set; }
       public double llegadaCliente { get; set; }
       public double rndEstadoFactura { get; set; }
       public string estadoFactura { get; set; }
       public double rndConoceProcedimiento { get; set; }
       public string conoceProcedimiento { get; set; }
       public double rndFinCobro { get; set; }
       public double tiempoFinCobro { get; set; }
        public double rndFinInforme { get; set;  }
        public List<Caja> cajas { get; set; }
       public VentanillaActualizacion ventanillaActualizacion { get; set; }
       public VentanillaInforme ventanillaInforme { get; set; }
       public Linea lineaAnterior { get; set; }

        public Caja cajaFinCobro;

        public long colaCaja;

        public List<Cliente> clientes;

        public ColasMunicipalidad colas;
        public Queue<Cliente> clientesLibre;

        public double acumuladorTiemposEsperaEnCaja;
        public long cantidadClientesEsperan;
        public double acumuladorTiempoOcupacionVentanillaInformes;
        public double acumuladorTiempoOciosoVentanillaActualizacion;
        public double tiempoMaximoEsperaEnCola;

        public int idFila;
        private int filaDesde;
        private int filaHasta;

        public Linea(int cantidadCajas, int mediaLLegada, int mediaFinInforme, double a, double b)
        {
            this.llegadaCliente = mediaLLegada;
            this.truncador = new Truncador(4);
            this.aleatorios = new GeneradorUniformeLenguaje(truncador);
            this.poisson = new GeneradorPoisson((GeneradorUniformeLenguaje)aleatorios, truncador, mediaLLegada);
            this.ventanillaInforme = new VentanillaInforme();
            this.ventanillaActualizacion = new VentanillaActualizacion();
            this.uniforme = new GeneradorUniformeAB((GeneradorUniformeLenguaje)aleatorios, truncador, a, b);
            this.cajas = new List<Caja>();
            cargarCajas(cantidadCajas);
            this.clientes = new List<Cliente>();
            this.colaCaja = 0;
            this.clientesLibre = new Queue<Cliente>();
            this.rndFinInforme = -1;
            this.rndFinCobro = -1;
            this.rndConoceProcedimiento = -1;
            this.rndEstadoFactura = -1;
            this.tiempoFinInforme = -1;
            this.tiempoParaLlegada = 60;
            this.tiempoFinCobro = -1;
            this.exponencial = new GeneradorExponencialNegativa((GeneradorUniformeLenguaje)aleatorios, truncador, mediaFinInforme);
        }

        public Linea(Linea anterior, ColasMunicipalidad colas, int filaDesde, int filaHasta, int idFila)
        {
            this.lineaAnterior = anterior;
            this.truncador = anterior.truncador;
            this.aleatorios = anterior.aleatorios;
            this.poisson = anterior.poisson;
            this.uniforme = anterior.uniforme;
            this.exponencial = anterior.exponencial;

            this.ventanillaInforme = anterior.obtenerVentanillaInforme();
            this.ventanillaActualizacion = anterior.obtenerVentanillaActualizacion();

            Caja[] temp = new Caja[anterior.cajas.Count];
            anterior.cajas.CopyTo(temp);

            this.cajas = new List<Caja>(temp);
            this.clientes = anterior.clientes;

            colaCaja = anterior.colaCaja;
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

            acumuladorTiemposEsperaEnCaja = anterior.acumuladorTiemposEsperaEnCaja;
            cantidadClientesEsperan = anterior.cantidadClientesEsperan;
            acumuladorTiempoOcupacionVentanillaInformes = anterior.acumuladorTiempoOcupacionVentanillaInformes;
            acumuladorTiempoOciosoVentanillaActualizacion = anterior.acumuladorTiempoOciosoVentanillaActualizacion;
            tiempoMaximoEsperaEnCola = anterior.tiempoMaximoEsperaEnCola;
        }


        public VentanillaInforme obtenerVentanillaInforme()
        {
            return (VentanillaInforme)this.ventanillaInforme.Clone();
        }

        public VentanillaActualizacion obtenerVentanillaActualizacion()
        {
            return (VentanillaActualizacion) this.ventanillaActualizacion.Clone();
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

            this.reloj = lineaAnterior.llegadaCliente;
            this.evento = LLEGADA_PERSONA;
            cajaFinCobro = null;

            if (lineaAnterior.ventanillaInforme.finInforme >0 && lineaAnterior.ventanillaInforme.finInforme < reloj) {
                reloj = lineaAnterior.ventanillaInforme.finInforme;
                evento = FIN_INFORME;
            }

            if (lineaAnterior.ventanillaActualizacion.finActualizacion > 0 && lineaAnterior.ventanillaActualizacion.finActualizacion < reloj)
            {
                reloj = lineaAnterior.ventanillaActualizacion.finActualizacion;
                evento = FIN_ACTUALIZACION;
            }

            foreach (var caja in lineaAnterior.cajas)
            {
                if (caja.finCobro < reloj && caja.finCobro > 0) {
                    reloj = caja.finCobro;
                    cajaFinCobro = (Caja)this.cajas.Where(x => x.id == caja.id).FirstOrDefault();
                    evento = FIN_COBRO;
                }
            }

        }

        public void calcularSiguienteLlegada() {
            if (this.evento.Equals(LLEGADA_PERSONA))
            {
                this.tiempoParaLlegada = poisson.siguienteAleatorio();
                this.llegadaCliente = reloj + tiempoParaLlegada;
                return;
            }
            this.llegadaCliente = lineaAnterior.llegadaCliente;
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
            if (lineaAnterior.ventanillaInforme.estaOcupada())
            {
                this.acumuladorTiempoOcupacionVentanillaInformes += (reloj - lineaAnterior.reloj);
            }

        }

        private void calcularFinInformeEventoFinInforme()
        {

            if (this.evento.Equals(FIN_INFORME))
            {
                if (lineaAnterior.tieneColaInforme())
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
            ventanillaInforme.agregarFinInforme(lineaAnterior.obtenerFinInforme());
            ventanillaInforme.agregarACola(clienteActual);
            clienteActual.esperarInforme();
        }
        private void esperarActualizacion(Cliente clienteActual)
        {
            ventanillaActualizacion.agregarFinActualizacion(lineaAnterior.obtenerFinActualizacion());
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
            if (this.evento.Equals(FIN_ACTUALIZACION) && lineaAnterior.tieneFinInforme())
            {
                ventanillaInforme.agregarFinInforme(lineaAnterior.obtenerFinInforme());
            }
        }

        private Cliente buscarClienteLibre()
        {
            Cliente libre;
            Boolean correcto = clientesLibre.TryDequeue(out libre);
            if (correcto)
            {
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
            if (!lineaAnterior.ventanillaActualizacion.estaOcupada())
            {
                this.acumuladorTiempoOciosoVentanillaActualizacion += (reloj - lineaAnterior.reloj);
            }
        }

        private void calcularFinActualizacionEventoLlegadaCliente(double tiempo)
        {
            if ((this.conoceProcedimiento.Equals("si")))
            {
                Cliente clienteActual = buscarClienteLibre();
                if (lineaAnterior.tieneVentanillaActualizacionOcupada())
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
                if (lineaAnterior.tieneVentanillaInformeOcupada())
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
                Cliente clienteActual = lineaAnterior.ventanillaInforme.getClienteActual();

                if (lineaAnterior.tieneVentanillaActualizacionOcupada())
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
                if (this.lineaAnterior.tieneColaActualizacion())
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
            this.colaCaja++;
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
                clientesLibre.Enqueue(clienteViejo);
                cajaFinCobro.liberar();

                if (lineaAnterior.tieneColaCobro())
                {
                    Cliente clienteActual = Caja.siguienteCliente();
                    colaCaja--;
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
                Cliente clienteActual = lineaAnterior.ventanillaActualizacion.getClienteActual();
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
