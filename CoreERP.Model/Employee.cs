using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreERP.Model
{
    public class Employee
    {
        public int id_funcionario { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Estado debe tener un valor.")]
        public String usuario { get; set; }
        public String nombres { get; set; }
        public String apellidos { get; set; }
        public int id_cargo { get; set; }

        public string correo { get; set; }

        public string cargo { get; set; }

        //ROL

        public string rol { get; set; }
        public bool can_edit { get; set; }
        public bool can_insert { get; set; }
        public bool can_update { get; set; }
        public bool can_delete { get; set; }
        public bool can_approve { get; set; }
    }
}
