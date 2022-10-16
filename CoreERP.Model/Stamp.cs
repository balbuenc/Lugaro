using System;
using System.Collections.Generic;
using System.Text;

namespace CoreERP.Model
{
    public class Stamp
    {
        public int id_timbrado { get; set; }
        public string timbrado { get; set; }
        public string punto_emision { get; set; }
        public DateTime fecha_emision { get; set; }
        public DateTime fecha_vencimiento { get; set; }
        public Int32 serie_inicio { get; set; }
        public Int32 serie_final { get; set; }
        public Int32 nro_factura { get; set; }
    }
}
