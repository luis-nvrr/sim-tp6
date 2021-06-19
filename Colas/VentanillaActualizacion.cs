using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numeros_aleatorios.Colas
{
    class VentanillaActualizacion : ICloneable
    {
        string LIBRE = "libre";
        string OCUPADO = "ocupado";

        public string estado { get; set; }
        public double finActualizacion { get; set; }

        public Queue<Cliente> cola;

        public Cliente clienteActual;

        public VentanillaActualizacion()
        {
            this.estado = LIBRE;
            this.finActualizacion = -1;
            this.cola = new Queue<Cliente>();
        }

        public Boolean tieneCola()
        {
            return cola.Count > 0;
        }

        public void agregarFinActualizacion(double tiempo)
        {
            this.finActualizacion = tiempo;
            this.estado = OCUPADO;

        }

        public Boolean tieneFinActualizacion()
        {
            return this.finActualizacion > 0;
        }

        public double obtenerFinActualizacion()
        {
            return finActualizacion;
        }

        public void liberar()
        {
            this.estado = LIBRE;
            this.finActualizacion = -1;
        }


        public Boolean estaOcupada()
        {
            return this.estado.Equals(OCUPADO);
        }

        public object Clone()
        {
            VentanillaActualizacion res = new VentanillaActualizacion();
            res.estado = this.estado;
            res.finActualizacion = this.finActualizacion;
            res.clienteActual = this.clienteActual;
            Cliente[] temp = new Cliente[cola.Count];
            cola.CopyTo(temp, 0);
            res.cola = new Queue<Cliente>(temp);



            return res;
        }

        public Cliente siguienteCliente()
        {
            return cola.Dequeue();
        }

        public Cliente getClienteActual()
        {
            return this.clienteActual;
        }

        public void agregarACola(Cliente cliente)
        {
            cola.Enqueue(cliente);
        }
    }
}
