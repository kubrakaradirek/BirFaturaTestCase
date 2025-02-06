using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class SoldProductDto
    {
        public int UrunID { get; set; }
        public string UrunAdi { get; set; }
        public string StokKodu { get; set; }
        public string SatisAdeti { get; set; }
        public string KDVOrani { get; set; }
        public string KDVDahilBirimFiyati { get; set; }
    }
}
