using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;
using System.Threading;

namespace CoreERP.Model
{
    public class CreditNoteDetails
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo ID debe tener un valor.")]
        public int id_nota_credito_detalle { get; set; }


        public decimal monto { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nota de credito debe tener un valor.")]
        public int id_nota_credito { get; set; }

        public DataColumn fecha { get; set; }

        public string concepto { get; set; }

        public decimal total { get; set; }
        public string impuesto { get; set; }

        public decimal porcentaje_impuesto { get; set; }

        public string nro_nota { get; set; }
        public decimal monto_impuesto { get; set; }



    }
}
