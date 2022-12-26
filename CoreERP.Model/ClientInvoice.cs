using System;
using System.Collections.Generic;
using System.Text;

namespace CoreERP.Model
{
    public class ClientInvoice
    {
        public string factura { get; set; }
        public DateTime fecha { get; set; }

        public string condicion { get; set; }

        public decimal importe { get; set; }

        public string facturacion { get; set; }
        public string motivo_anulacion { get; set; }
        public int cuota { get; set; }
        public decimal monto_capital { get; set; }
        public decimal monto_interes { get; set; }
        public string moneda { get; set; }
        public DateTime vencimiento { get; set; }
        public string estado { get; set; }
        public DateTime fecha_pago { get; set; }
        public Int32 id_cliente { get; set; }
        public string ruc { get; set; }
        public string razon_social { get; set; }
      
    }
}
