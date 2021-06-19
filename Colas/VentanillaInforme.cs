using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numeros_aleatorios.Colas
{
    class VentanillaInforme : ICloneable
    {
        string LIBRE = "libre";
        string OCUPADO = "ocupado";

        public string estado { get; set; }
        public double finInforme { get; set; }

        public Queue<Cliente> cola;

        public Cliente clienteActual;

        public VentanillaInforme()
        {
            this.estado = LIBRE;
            this.finInforme = -1;
            this.cola = new Queue<Cliente>();
        }

        public void agregarFinInforme(double fin)
        {
            this.estado = OCUPADO;
            finInforme = fin;

            return;
        }


        public Boolean estaOcupada()
        {
            return this.estado.Equals(OCUPADO);
        }

        public Boolean tieneCola()
        {
            return cola.Count > 0;
        }

        public Boolean tieneFinInforme()
        {
            return finInforme != -1;
        }

        public void liberar()
        {
            this.estado = LIBRE;
            this.finInforme = -1;
        }

        public object Clone()
        {
            VentanillaInforme res = new VentanillaInforme();
            res.estado = this.estado;
            res.finInforme = this.finInforme;
            res.clienteActual = this.clienteActual;
            Cliente[] temp = new Cliente[cola.Count];
            cola.CopyTo(temp, 0);
            res.cola = new Queue<Cliente>(temp);

            return res;
        }

        public Cliente siguienteCliente()
        {
            return this.cola.Dequeue();
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
