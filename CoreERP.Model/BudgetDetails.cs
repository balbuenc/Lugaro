using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreERP.Model
{
    public class BudgetDetails
    {
        public int id_presupuesto_detalle { get; set; }
        public int id_presupuesto { get; set; }
        public int id_producto { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo descuento debe tener un valor.")]
        public int id_descuento { get; set; }
        public decimal cantidad { get; set; }
        public decimal costo { get; set; }
        public decimal precio { get; set; }
        public decimal precio_unitario { get; set; }

        public decimal total { get; set; }

        public string codigo { get; set; }
        public string producto { get; set; }
        public string descuento { get; set; }
        public decimal porcentaje { get; set; }

        public DateTime fecha { get; set; }

        public string cliente { get; set; }

        public string direccion { get; set; }

        public string telefono { get; set; }
        public string imagen { get; set; }

        public string observaciones { get; set; }

        public string plazo_entrega { get; set; }

        public string forma_pago { get; set; }

        public string vendedor { get; set; }
        public string moneda { get; set; }
        public decimal cotizacion { get; set; }

        public string ruc { get; set; }

        public string contacto { get; set; }

        public string direccion_entrega { get; set; }

        public string condicion { get; set; }

        public string monto_total { get; set; }

        public string obra { get; set; }

        public string motivo { get; set; }

        public string cliente_exento { get; set; }

    }
}
