using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreERP.Model
{
    public class Collection
    {
        public int id_cobranza  { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo fecha debe tener un valor.")]
        public DateTime fecha { get; set; }
        public int id_cuota { get; set; }
        public int id_funcionario { get; set; }

        public decimal monto_cobrado { get; set; }
        public int? id_medio_pago { get; set; }
        public string nro_recibo { get; set; }

        public string nro_comprobante { get; set; }

        public string nro_transaccion { get; set; }

        public string funcionario { get; set; }

        public string cuota { get; set; }

        public string cliente { get; set; }

        public string medio_pago { get; set; }

        public string ruc { get; set; }

        public string factura { get; set; }


    }
}
