using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session02.MoveTracking.Infrastructure
{
    public class Person
    {
        public int PersonCode { get; set; }
        public string PersonRole { get; set; }
        public int LastSecurityPointNumber { get; set; }
        public string LastSecurityPointDirection { get; set; }
        public string LastSecurityPointTime { get; set; }
    }
}
