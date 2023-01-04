using System;
using System.Collections.Generic;
using System.Text;

namespace CoreERP.Model
{
    public class Vendor
    {
        public int id_proveedor { get; set; }
        public String proveedor { get; set; }
        public String descripcion { get; set; }
        public int id_pais { get; set; }

        public string pais { get; set; }

        public string ruc { get; set; }
        public string direccion { get; set; }
        public int? id_plan_cuenta { get; set; }
        public string cuenta_contable { get; set; }

        public string telefono { get; set; }
        public string email { get; set; }
        public string contacto { get; set; }

        public string razon_social { get; set; }
    }
}
