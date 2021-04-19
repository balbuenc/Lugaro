﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CoreERP.Model
{
    public class Budget
    {
        public int id_presupuesto { get; set; }
        public int? id_cliente { get; set; }
        public int id_funcionario { get; set; }
        public DateTime fecha { get; set; }
        public String estado { get; set; }

        public Int32 nro_presupuesto { get; set; }
        public Int32  id_moneda { get; set; }
        public decimal cotizacion { get; set; }

        public string vendedor { get; set; }
        public string cliente { get; set; }

        public string moneda { get; set; }
    }
}
