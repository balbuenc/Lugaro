using System;
using System.Collections.Generic;
using System.Text;

namespace CoreERP.Model
{
    public class Enterprise
    {
        public int cod_empresa { get; set; }
        public string empresa { get; set; }
        public string ruc { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
    }
}
