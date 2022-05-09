using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreERP.Model
{
    public class CreditNoteDetails
    {
        public int id_nota_credito_detalle { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Factura debe tener un valor.")]
        public int id_venta { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo detalle debe tener un valor.")]
        public int id_presupuesto_detalle { get; set; }

        public decimal monto { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nota de credito debe tener un valor.")]
        public int id_nota_credito { get; set; }

        public string nro_factura { get; set; }
        public string producto { get; set; }

        public decimal total { get; set; }

        

    }
}
