using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CoreERP.Model
{
    public class DailyBook
    {
        public Int32 id { get; set; }
        public Int32 asiento_numero { get; set; }
        public DateTime fecha { get; set; }
        public Int32 id_plan_cuenta { get; set; }

        public decimal debe { get; set; }
        public decimal haber { get; set; }

        public string cuenta { get; set; }
        public string descripcion { get; set; }



    }
}
