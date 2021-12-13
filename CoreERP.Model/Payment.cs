using System;
using System.Collections.Generic;
using System.Text;

namespace CoreERP.Model
{
    public class Payment
	{

        public int id_pago { get; set; }

        public int nro_orden_pago { get; set; }

        public DateTime fecha_orden { get; set; }
        public int id_compra { get; set; }

        public int id_compra_general { get; set; }
        public int id_funcionario { get; set; }

        public int aprobado_por { get; set; }

        public DateTime fecha_pago { get; set; }

        public decimal monto_pagado { get; set; }

        public string nro_comprobante { get; set; }
        public string estado { get; set; }

        public decimal total_orden { get; set; }

        public int id_medio_pago { get; set; }

        public int id_moneda { get; set; }

        public string aprobador { get; set; }
        public string responsable { get; set; }
        public string medio_pago { get; set; }
        public string moneda { get; set; }

    }
}
