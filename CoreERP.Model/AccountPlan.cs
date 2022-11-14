using System;
using System.Collections.Generic;
using System.Text;

namespace CoreERP.Model
{
    public class AccountPlan
    {
        public int id_plan_cuenta { get; set; }
        public string cuenta { get; set; }
        public string descripcion { get; set; }
     
     
        public string denominacion { get; set; }

        public bool imputable { get; set; }

        public bool impositivo { get; set; }

        public bool con_costo { get; set; }

        public int nivel { get; set; }
    }
}
