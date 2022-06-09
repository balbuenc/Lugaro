using System;
using System.Collections.Generic;
using System.Text;

namespace CoreERP.Model
{
    public class Sale
    {
        public int id_venta { get; set; }
        public int id_presupuesto { get; set; }
        public String factura { get; set; }
        public DateTime fecha { get; set; }
        public String condicion { get; set; }
        public decimal importe { get; set; }
        public string estado { get; set; }

        public string cliente { get; set; }

        public string vendedor { get; set; }
        public DateTime fecha_presupuesto { get; set; }
        public string moneda { get; set; }

        public string motivo_anulacion { get; set; }
    }
}
