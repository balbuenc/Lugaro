using System;
using System.Collections.Generic;
using System.Text;

namespace CoreERP.Model
{
    public class LittleBox
	{

        public int id_caja_chica { get; set; }
        public DateTime fecha_apertura { get; set; }

        public DateTime fecha_cierre { get; set; }
        public int id_funcionario { get; set; }
        public decimal monto_apertura { get; set; }
        public string estado { get; set; }

        public string usuario { get; set; }

        public string nro_comprobante { get; set; }


    }
}
