using System;
using System.Collections.Generic;
using System.Text;

namespace CoreERP.Model
{
    public class GeneralPurchase
	{
        public int id_compra_general { get; set; }
        public DateTime fecha { get; set; }
        public int id_funcionario { get; set; }
        public int id_proveedor { get; set; }

        public int id_moneda { get; set; }
        public string estado { get; set; }
        public string nro_orden_compra { get; set; }

        public int id_deposito { get; set; }

        public string proveedor { get; set; }
        public string funcionario { get; set; }
        public string deposito { get; set; }

        public string moneda { get; set; }
    }

}
