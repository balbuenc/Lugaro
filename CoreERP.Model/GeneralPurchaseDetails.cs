using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreERP.Model
{
    public class GeneralPurchaseDetails
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo ID Compra debe tener un valor.")]
        public int id_compra_general_detalle { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo ID debe tener un valor.")]
        public int id_compra_general { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Cantidad debe tener un valor.")]
        public decimal cantidad { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Descripcion debe tener un valor.")]
        public string descripcion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Monto debe tener un valor.")]
        public decimal monto { get; set; }

        public decimal total { get; set; }



    }
}
