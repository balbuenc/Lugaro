using System;
using System.Collections.Generic;
using System.Text;

namespace CoreERP.Model
{
    public class Budget
    {
        public int id_presupuesto { get; set; }
        public int? id_cliente { get; set; }
        public int id_funcionario { get; set; }
        public DateTime fecha { get; set; }
        public String estado { get; set; }

        public string nro_presupuesto { get; set; }
        public Int32 id_moneda { get; set; }
        public decimal cotizacion { get; set; }

        public Int32 id_condicion_venta { get; set; }

        public string vendedor { get; set; }
        public string cliente { get; set; }

        public string moneda { get; set; }

        public string forma_pago { get; set; }
        public string plazo_entrega { get; set; }
        public string observaciones { get; set; }

        public string direccion_entrega { get; set; }
        public string contacto { get; set; }

        public String condicion { get; set; }

        public string obra { get; set; }
        public string motivo { get; set; }

        public string cliente_exento { get; set; }

        public string estado_venta { get; set; }

        public DateTime fecha_factura { get; set; }

        public decimal Total { get; set; }

        public int cantidad { get; set; }

        public int dias_devolucion { get; set; }

        public DateTime fecha_devolucion { get; set; }

        public bool marcado { get; set; }
    }
}
