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
    }
}
