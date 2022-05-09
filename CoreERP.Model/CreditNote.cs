﻿using System;
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


        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Motivo debe tener un valor.")]
        public string motivo { get; set; }


        public int id_funcionario { get; set; }


        public int id_presupuesto { get; set; }
    }
}