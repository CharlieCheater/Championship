using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Session02.MoveTracking
{
    public class SkudInfo
    {
        public int SkudId { get; set; }
        public Border Border { get; set; }
        public List<HumanPosition> HumanPositions { get; set; }
        public SkudInfo(int skudId, Border border)
        {
            SkudId = skudId;
            Border = border;
            HumanPositions = new List<HumanPosition>();
        }
    }
    public class HumanPosition
    {
        public int Id { get; set; }
        public Ellipse Ellipse { get; set; }
    }
}
