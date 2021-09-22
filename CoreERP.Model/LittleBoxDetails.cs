using System;
using System.Collections.Generic;
using System.Text;

namespace CoreERP.Model
{
    public class LittleBoxDetails
	{

        public int id_caja_chica { get; set; }
        public int id_caja_chica_detalle { get; set; }

        public string nro_comprobante { get; set; }

        public DateTime fecha { get; set; }

        public string beneficiario { get; set; }
        public string concepto { get; set; }

        public decimal monto { get; set; }

        public decimal total_gasto { get; set; }
        public decimal saldo { get; set; }
    }
}
