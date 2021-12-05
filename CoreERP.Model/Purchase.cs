using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreERP.Model
{
    public class Purchase
    {
        public int id_compra { get; set; }

      
        public String factura { get; set; }
        public int id_proveedor { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Importe debe tener un valor.")]
        public decimal importe { get; set; }
        public int id_moneda { get; set; }

        public int id_deposito { get; set; }
        public string estado { get; set; }

        public int id_condicion_venta { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nro. OC debe tener un valor.")]
        public string nro_orden_compra { get; set; }

        public DateTime fecha { get; set; }

        public int id_funcionario { get; set; }


        public string proveedor { get; set; }
        public string moneda { get; set; }
        public string deposito { get; set; }
        public string condicion { get; set; }
        public string funcionario { get; set; }
    }
}
