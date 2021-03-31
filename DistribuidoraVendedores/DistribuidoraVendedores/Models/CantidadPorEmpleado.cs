using System;
using System.Collections.Generic;
using System.Text;

namespace DistribuidoraVendedores.Models
{
	public class CantidadPorEmpleado
	{
		public int cantidad { get; set; }
		public int id_tipo_producto { get; set; }
		public int id_vendedor { get; set; }
	}
}
