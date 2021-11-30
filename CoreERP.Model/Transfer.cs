using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreERP.Model
{
    public class Transfer
    {


        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo ID debe tener un valor.")]
        public int id_transferencia { get; set; }
        public DateTime fecha { get; set; }

        public int id_producto { get; set; }

        public int id_deposito_origen { get; set; }
        public int id_deposito_destino { get; set; }

        public int id_funcionario { get; set; }

        public decimal cantidad { get; set; }

        public string retirado_por { get; set; }

        public int nro_transferencia { get; set; }

        public string origen { get; set; }

        public string destino { get; set; }

        public string usuario { get; set; }

        public string observaciones { get; set; }

    }
}
