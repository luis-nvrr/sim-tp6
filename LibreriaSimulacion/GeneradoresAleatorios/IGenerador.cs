using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numeros_aleatorios.LibreriaSimulacion.GeneradoresAleatorios
{
    interface IGenerador
    {

        double siguienteAleatorio();
        DataTable generarSerie(int cantidadAleatorios);

        DataTable generarSerie(int cantidadAleatorios, ContadorFrecuenciaObservada frecuenciaObservada);


    }
}
