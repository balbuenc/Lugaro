using System;
using System.Collections.Generic;
using System.Text;

namespace CoreERP.Model
{
    public class AuthorizationMatrix
    {
        public bool CanView { get; set; }
        public bool CanViewOnlyOwned { get; set; }
        public bool CanEdit { get; set; }
        public bool CanCreate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanAuthorize { get; set; }

        public AuthorizationMatrix()
        {
            CanView = false;
            CanViewOnlyOwned = true;
            CanEdit = false;
            CanCreate = false;
            CanDelete = false;
            CanAuthorize = false;
        }
    }
}
