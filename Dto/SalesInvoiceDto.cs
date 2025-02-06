using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class SalesInvoiceDto
    {
        public int FaturaID { get; set; }
        public string MusteriAdi { get; set; }
        public string MusteriAdresi { get; set; }
        public string MusteriTel { get; set; }
        public string MusteriSehir { get; set; }
        public string MusteriTCVKN { get; set; }
        public string MusteriVergiDairesi { get; set; }
        public List<SoldProductDto> SatilanUrunler { get; set; }
    }
}
