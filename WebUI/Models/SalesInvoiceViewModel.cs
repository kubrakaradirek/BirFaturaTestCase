namespace WebUI.Models
{
    //Fatura Modeli
    public class SalesInvoiceViewModel
    {
        public int FaturaID { get; set; }
        public string MusteriAdi { get; set; }
        public string MusteriAdresi { get; set; }
        public string MusteriTel { get; set; }
        public string MusteriSehir { get; set; }
        public string MusteriTCVKN { get; set; }
        public string MusteriVergiDairesi { get; set; }
        public List<SoldProductViewModel> SatilanUrunler { get; set; }
    }
}
