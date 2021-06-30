using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using Numeros_aleatorios.LibreriaSimulacion;

namespace Numeros_aleatorios.Colas
{
	class RungeKutta
	{
		public double limite;
		public double h;
		public double to;
		public double eo;
		public double a;
		public DataTable tabla;
		public double k1;
		public double k2;
		public double k3;
		public double k4;
		public double ti;
		public double ei;

		public double tiempo50;
		public double tiempo70;

		public double diferencia;

		public RungeKutta()
		{

		}

		public void crearTabla()
		{
			tabla = new DataTable();
			tabla.Columns.Add("to");
			tabla.Columns.Add("eo");
			tabla.Columns.Add("k1");
			tabla.Columns.Add("k2");
			tabla.Columns.Add("k3");
			tabla.Columns.Add("k4");
			tabla.Columns.Add("ti+1");
			tabla.Columns.Add("ei+1");
        }

		public void crearTablaInestable()
		{
			tabla = new DataTable();
			tabla.Columns.Add("i");
			tabla.Columns.Add("to");
			tabla.Columns.Add("eo");
			tabla.Columns.Add("k1");
			tabla.Columns.Add("k2");
			tabla.Columns.Add("k3");
			tabla.Columns.Add("k4");
			tabla.Columns.Add("ti+1");
			tabla.Columns.Add("ei+1");
		}

		public double calcularRungeKuttaTiemposInestable(double h, double a, double limite1, double limite2)
		{
			this.h = h;
			this.to = 0;
			this.eo = 15;
			this.a = a;
			Boolean flag = false;
			Truncador truncador = new Truncador(6);

			int i = 0;

			crearTablaInestable();
			DataRow row;
			do
			{
				row = tabla.NewRow();
				row[0] = i;
				row[1] = truncador.truncar(to);
				row[2] = truncador.truncar(eo);
				k1 = h * (a * eo);
				row[3] = truncador.truncar(k1);
				k2 = h * (a * (eo + (k1 / 2)));
				row[4] = truncador.truncar(k2);
				k3 = h * (a * (eo + (k2 / 2)));
				row[5] = k3;
				k4 = h * truncador.truncar((a * (eo + k3)));
				row[6] = k4;
				ti = to + h;
				row[7] = truncador.truncar(ti);
				ei = eo + ((1.0 / 6.0)) * (k1 + 2 * k2 + 2 * k3 + k4);
				row[8] = truncador.truncar(ei);
				to = ti;
				eo = ei;

				if(i <= 100) { tabla.Rows.Add(row); }

				if(!flag && eo > limite1)
                {
					tiempo50 = to;
					flag = true;
					tabla.Rows.Add(row);
				}

				i++;
			}
			while (eo <= limite2);
			tabla.Rows.Add(row);

			tiempo70 = to;

			return to;
		}



		public double calcularRungeKuttaTiemposPurga(double h, double b, int cantidadPersonas)
		{
			this.h = h;
			this.to = 0;
			this.eo = cantidadPersonas;
			this.a = b;

			Truncador truncador = new Truncador(6);

			crearTabla();
			DataRow row;
			do
			{
				row = tabla.NewRow();
				row[0] = truncador.truncar(to);
				row[1] = truncador.truncar(eo);
				k1 = h * (-a * eo * (double)0.5);
				row[2] = truncador.truncar(k1);
				k2 = h * (-a * (eo + (k1 / 2)) * (double)0.5);
				row[3] = truncador.truncar(k2);
				k3 = h * (-a * (eo + (k2 / 2)) * (double)0.5);
				row[4] = truncador.truncar(k3);
				k4 = h * (-a * (eo + k3) * (double)0.5);
				row[5] = truncador.truncar(k4);
				ti = to + h;
				row[6] = truncador.truncar(ti);
				ei = eo + ((double)(1.0 / 6.0)) * (k1 + 2 * k2 + 2 * k3 + k4);
				row[7] = truncador.truncar(ei);
				to = ti;
				tabla.Rows.Add(row);

				diferencia = eo - ei;
				if (diferencia < (double)0.02){
					break;
                }

				eo = ei;
			}
			while (true);
			return to;
		}


		public double getTi() 
		{
			return ti; 
		}
	}

}

