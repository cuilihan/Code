using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.BF.DataSync
{
    public class OctEntity
    {
        public Guid UserID { get; set; }
        public string UserAccount { get; set; }
        public string Contact { get; set; }
    }

    public class OctAreaEntity
    {

        public Guid AreaID { get; set; }
        public Guid PAreaID { get; set; }
        public string AreaName { get; set; }
        public int DesType { get; set; }
    }
}
