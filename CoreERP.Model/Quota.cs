using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreERP.Model
{
    public class Quota
    {
        public int id_cuota { get; set; }
        public int? id_credito { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Cuota debe tener un valor.")]
        public int cuota { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Monto Capital debe tener un valor.")]
        public decimal monto_capital { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Monto Interes debe tener un valor.")]
        public decimal monto_interes { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Vencimiento debe tener un valor.")]
        public DateTime vencimiento { get; set; }
        public DateTime? fecha_pago { get; set; }
        public decimal multa { get; set; }
        public decimal mora { get; set; }

        public int? id_venta { get; set; }

        public int id_moneda { get; set; }

        public string moneda { get; set; }

        public string estado { get; set; }

        public decimal total  { get; set; }

        public string  cliente { get; set; }

        public string ruc { get; set; }

        public string cuota_str { get; set; }

        public int? id_metodo_pago { get; set; }

        public decimal saldo { get; set; }

        public string factura { get; set; }
    }
}
