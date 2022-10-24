using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreERP.Model
{
    public class Client
    {
        public int id_cliente { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre debe tener un valor.")]
        public String nombres { get; set; }
        public String apellidos { get; set; }
        public String sexo { get; set; }
        public DateTime fecha_nacimiento { get; set; }

        
        public String ci { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo RUC debe tener un valor.")]
        public String ruc { get; set; }
        public String direccion { get; set; }
        public String telefono { get; set; }
        public String email { get; set; }
        public String observaciones { get; set; }
        public DateTime fecha_alta { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Razón social debe tener un valor.")]
        public String razon_social { get; set; }
        public String codigo { get; set; }
        public String es_cliente_fiel { get; set; }

        
        public int? id_estado_civil { get; set; }
        public String tipo_vivienda { get; set; }

        
        public int? id_nacionalidad { get; set; }
        public String direccion_envio { get; set; }
        
        
        public int? id_barrio { get; set; }

        
        public int? id_tipo_cliente { get; set; }

        public string barrio { get; set; }
        public string estado_civil { get; set; }
        public string nacionalidad { get; set; }
        public string tipo { get; set; }

        public int id_funcionario { get; set; }

        public string vendedor { get; set; }

        public string cliente_exento { get; set; }

        public int? id_plan_cuenta { get; set; }
        public string cuenta_contable { get; set; }

        //public virtual Neighborhood Neighborhood { get; set; }
        //public virtual CivilStatus CivilStatus { get; set; }
        //public  virtual Nationality Nationality { get; set; }
        //public virtual ClientType ClientType { get; set; }

    }
}
