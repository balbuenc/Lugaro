using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreERP.Model
{
    public class CreditNote
    {
        public int id_nota_credito { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo fecha debe tener un valor.")]
        public DateTime fecha { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nro. Nota debe tener un valor.")]
        public string nro_nota { get; set; }


        public string factura { get; set; }
        public DateTime fecha_factura { get; set; }
        public string facturacion { get; set; }
        public decimal importe_factura { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Motivo debe tener un valor.")]
        public string motivo { get; set; }


        public int id_funcionario { get; set; }


        public int id_presupuesto { get; set; }
        public DateTime fecha_prespuesto { get; set; }
        public string estado_presupuesto { get; set; }

        public Int32 id_cliente { get; set; }

        public string ruc { get; set; }
        public string razon_social { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }

        public decimal total { get; set; }

    }
}
