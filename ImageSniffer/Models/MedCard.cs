using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSniffer.Models
{
    public class MedCard
    {
        public int Id { get; set; }
        public string QrCode { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
