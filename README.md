# REHBER UYGULAMASI

Rehber Uygulaması, bir rehbere kişiler ve iletişim bilgilerinin kaydedilebildiği ve bu kayıtlarla alakalı bir rapor alınabildiği bir uygulamadır.

Uygulamada 2 menü bulunmaktadır:

	* Kişiler
	* Raporlar

Kişiler menüsünden bir kişinin isim, soyisim, firma, telefon, email ve konum gibi bilgileri listelenmektedir. 
Aynı menüden yeni kişi ve bilgileri ekleme, mevcut kişinin bilgilerini düzenleme ve kişi ve iletişim bilgilerini silme işlemleri yapılabilmektedir.

Raporlar menüsünden daha önce oluşturulmuş raporların tarih ve durum (hazırlanıyor veya tamamlandı) bilgileri listelenmektedir.
İndir butonuna basıldığında ilgili rapor indirilebilir. Yeni Rapor butonuna tıklandığında da rehberdeki mevcut kayıt durumuyla alakalı
bir rapor oluşturulabilir.


# Teknik Yapı:

Uygulama ön yüzde react, arka yüzde .net core kullanılarak geliştirilmiştir. Arka yüz mimaride 2 adet mikroservis vardır:

	* ContactService
	* ReportService

Contact Service kişi bilgilerinin CRUD işlemlerinin yapıldığı servistir. Ayrıca ön yüz react uygulamasıyla api gateway görevi de
üstlenmektedir.

Report Service raporların oluşturulduğu ve listlendiği servistir. Rapor listeleme, indirme ve üretme talebi geldiğinde Contact Service
Report Service e bağlanarak bu işlemleri gerçekleştirir.


# Kuyruk Sistemi: 

Uygulamada kuyruk sistemi olarak Rabbit mq kullanılmıştır. Kullanıcı yeni bir rapor talebi oluşturduğunda bu talep kuyruğa gönderilir.
Bu kuyruğu dinleyen bir background servis aracılığıyla da sırası gelen rapor oluşturulur.


# Katman Yapısı:

Projede 7 katman bulunmaktadır.

	* Domain:
Projenin ana katmanıdır. Projede kullanılan temel class ve metodları (DBModel, DTO vs.) barındırır.

	* DataAccess
Veri tabanı konfigurasyonunun ve iletişiminin yapıldığı katmandır. Context, Mapping ve Migration class ve metodlarını ihtiva eder.

	* Service
Veri tabanıyla ilgili işlemlerin yapıldığı katmandır. Veri tabanıyla proje arasında bir köprü görevi görür. Veri tabanı manipulasyonunu
gerçekleştiren class ve metodları ihtiva eder.

	* ServiceTest
Servis metodlarının birim testlerinin yapıldığı katmandır.

	* Worker
Projeyle ilgili işlemlerin (kuyruk iletişimi, rapor üretimi vs) yapıldığı katmandır.

	* WorkerTest
Worker metodlarının birim testlerinin yapıldığı katmandır.

	* UI
Projenin ön yüz (UI) katmanıdır. Projeye gelen istekleri karşılar ve gerekli cevapları (response) diğer katmanlarla beraber üretir.


# Kullanılan Teknolojiler:

Projede veritabanı olarak postgresql, sürüm takibi için git ve servisler arası iletişim için http servisleri kullanılmıştır.
