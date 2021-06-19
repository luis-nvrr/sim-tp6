using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numeros_aleatorios.Colas
{
    class Caja
    {
        string LIBRE = "libre";
        string OCUPADO = "ocupado";

        public string estado { get; set; }
        public double finCobro { get; set; }

        public int id;

        public static Queue<Cliente> cola;

        public Cliente clienteActual;

        public Caja(int id)
        {
            this.estado = LIBRE;
            this.id = id;
            cola = new Queue<Cliente>();
            this.finCobro = -1;
        }

        public Boolean estaLibre()
        {
            return this.estado.Equals(LIBRE);
        }

        public void agregarFinCobro(double fin)
        {
            this.finCobro = fin;
            this.estado = OCUPADO;
        }

        public void liberar()
        {
            this.finCobro = -1;
            this.estado = LIBRE;
            this.clienteActual = null; 
        }


        public Cliente getClienteActual()
        {
            return this.clienteActual;
        }

        public static Boolean tieneCola()
        {
            return cola.Count > 0;
        }

        public static void agregarACola(Cliente cliente)
        {
            cola.Enqueue(cliente);
        }

        public static Cliente siguienteCliente()
        {
            return cola.Dequeue();
        }
    }
}
