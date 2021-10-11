using System;
using System.Collections.Generic;
using System.Text;

namespace CoreERP.Model
{
    public class PurchaseOrder
    {
        public int id_orden_compra { get; set; }
        public DateTime fecha { get; set; }
        public int id_funcionario { get; set; }
        public string estado { get; set; }

        public int id_compra_general { get; set; }
        public int id_compra { get; set; }

        public string tipo_compra { get; set; }

        public string aprobado_por { get; set; }

        public DateTime fecha_aprobacion { get; set; }

        public string propietario { get; set; }
        public string aprobador { get; set; }


    }
}
