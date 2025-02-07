# BİR FATURA TEST CASE PROJESİ
Bu proje, bir Web API ve bir Web UI içeren iki ana bileşenden oluşmaktadır. Proje, satış verilerini almak ve bu verileri kullanıcıya sunmak amacıyla geliştirilmiştir. Web API, satış verileriyle ilgili işlem yaparken, Web UI bu verileri görsel olarak kullanıcıya sunmakta ve ayrıca bir PDF formatında yazdırma işlemi gerçekleştirmektedir.
###  Web API 

SalesController Web API projesinde Controllers klasöründe yer alan Salesontroller, satış verilerini almayı sağlayan API endpoini içerir. Services klasörünün TokenService kullanılarak dış bir API'den satış verileri çekilir.
Token alımı için TokenService kullanılarak Bearer Token ile yetkilendirme sağlanır.
Dtos klasöründe bulunan SalesInvoiceDto ve SoldProductDto sınıfları, satış verilerini taşımak için kullanılan dtolardır.
Token almak için bir post  isteği gönderilir ve alınan token, sonraki API isteklerinde yetkilendirme başlığı olarak kullanılır.
###  Web UI
Web UI projesindeki SalesController, satış verilerini almak için Web API'ye GET isteği gönderir. Alınan veriler, View üzerinden kullanıcıya sunulur.
Veriler, SalesInvoiceViewModel modeline dönüştürülerek View'de görüntülenir.
Web UI, Index view'ında her bir fatura için bilgileri kullanıcıya gösterir.
Fatura verileri, kullanıcıya Bootstrap kullanılarak düzenli bir şekilde sunulur.
Her bir fatura için PDF çıktısı almak amacıyla bir "Faturalandır" butonu yerleştirilmiştir. Bu buton, html2pdf kütüphanesi kullanılarak PDF'ye dönüştürülür.


https://github.com/user-attachments/assets/f860906e-587b-4d47-b0e9-f16725959335

