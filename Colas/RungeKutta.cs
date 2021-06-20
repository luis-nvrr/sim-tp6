using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace Numeros_aleatorios.Colas
{
	class RungeKutta
	{
		public decimal limite;
		public decimal h;
		public decimal to;
		public decimal eo;
		public decimal a;
		public DataTable tabla;
		public decimal k1;
		public decimal k2;
		public decimal k3;
		public decimal k4;
		public decimal ti;
		public decimal ei;

		public RungeKutta(decimal h, decimal a, decimal limite)
		{
			this.limite = limite;
			this.h = h;
			this.to = 0;
			this.eo = 15;
			this.a = a;
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

		public decimal calcularRungeKutta()
		{
			crearTabla();
			DataRow row;
			do
			{
				row = tabla.NewRow();
				row[0] = to;
				row[1] = eo;
				k1 = h * (a * eo);
				row[2] = k1;
				k2 = h * (a * (eo + (k1 / 2)));
				row[3] = k2;
				k3 = h * (a * (eo + (k2 / 2)));
				row[4] = k3;
				k4 = h * (a * (eo + k3));
				row[5] = k4;
				ti = to + h;
				row[6] = ti;
				ei = eo + ((decimal) (1.0 / 6.0)) * (k1 + 2 * k2 + 2 * k3 + k4);
				row[7] = ei;
				to = ti;
				eo = ei;
				tabla.Rows.Add(row);
			}
			while (eo <= limite);

			return to;
		}

		public decimal getTi() 
		{
			return ti; 
		}
	}

}

