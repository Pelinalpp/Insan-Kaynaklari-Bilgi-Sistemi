using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InsanKaynaklariBilgiSistemi
{
    public partial class frmInsanKaynaklariBilgiSistemi : Form
    {
        public frmInsanKaynaklariBilgiSistemi()
        {
            InitializeComponent();
        }

        HashTable ht = new HashTable();

        KisiAgaci ka = new KisiAgaci();
        Kisi k1 = new Kisi();
        IkiliAramaAgacDugumu dugum = new IkiliAramaAgacDugumu();

        LinkedListEgitim listEgitim = new LinkedListEgitim();
        Egitim egitim = new Egitim();

        LinkedListIsDeneyimi listIsDeneyimi = new LinkedListIsDeneyimi();
        IsDeneyimi isDeneyimi = new IsDeneyimi();

        Sirket sirket = new Sirket();
        List<Sirket> listeSirket = new List<Sirket>();
        int ilanID;
        Boolean ilanBulundu = false;


        private void frmInsanKaynaklariBilgiSistemi_Load(object sender, EventArgs e)
        {
            //ilanID'yi ilk kayıtta kullanmak için değer atadık
            //Burada henüz hiç kayıt olmadığından sıfır değeri atandı.
            ilanID = lbIlanlar.Items.Count;
            //Sirket.txt dosyasından şirket bilgileri okundu.
            StreamReader oku;
            oku = File.OpenText(@"C:\Users\Pelin\Desktop\Veri Yapıları Projesi\InsanKaynaklariBilgiSistemiSon\InsanKaynaklariBilgiSistemi\bin\Debug\sirket.txt");
            string yazi;
            while ((yazi = oku.ReadLine()) != null)
            {
                //Şirket bilgileri dolduruldu.
                sirket.Ad = yazi;
                yazi = oku.ReadLine();
                sirket.Adresi = yazi;
                yazi = oku.ReadLine();
                sirket.Telefon = yazi;
                yazi = oku.ReadLine();
                sirket.Eposta = yazi;
                yazi = oku.ReadLine();
                sirket.Faks = yazi;
                //Şirketin ilk ilan bilgisi dolduruldu.
                Ilan ilan = new Ilan();
                ilan.sirket = sirket;
                yazi = oku.ReadLine();
                ilan.IlanId = Convert.ToInt32(yazi);
                ilanID++;
                yazi = oku.ReadLine();
                ilan.IsTanimi = yazi;
                yazi = oku.ReadLine();
                ilan.ElemanOzellik = yazi;

                //Şirkete ilan eklendi
                sirket.Ilanlar.InsertFirst(ilan);
                //İlana şirket eklendi
                listeSirket.Add(sirket);
                lbIlanlar.Items.Add(ilanID + ". " + ilan.IsTanimi);

                //Hash Table oluşturuldu.
                ht.IlanEkle(ilan.IlanId, ilan.heapBasvuru);
            }
            oku.Close();

            //Eleman.txt dosyasından ilk elemanların bilgilerini okuma işlemleri gerçekleştirildi.
            //Bilgiler okunduktan sonra ikili arama ağacına ada göre eklendi.
            StreamReader okuEleman;
            okuEleman = File.OpenText(@"C:\Users\Pelin\Desktop\Veri Yapıları Projesi\InsanKaynaklariBilgiSistemiSon\InsanKaynaklariBilgiSistemi\bin\Debug\eleman.txt");
            string yaziEleman;
            while ((yaziEleman = okuEleman.ReadLine()) != null)
            {
                k1 = new Kisi();
                k1.Ad = yaziEleman;
                yaziEleman = okuEleman.ReadLine();
                k1.Adres = yaziEleman;
                yaziEleman = okuEleman.ReadLine();
                k1.Telefon = yaziEleman;
                yaziEleman = okuEleman.ReadLine();
                k1.Eposta = yaziEleman;
                yaziEleman = okuEleman.ReadLine();
                k1.Referans = yaziEleman;
                yaziEleman = okuEleman.ReadLine();
                k1.DogumTarihi = Convert.ToDateTime(yaziEleman);
                yaziEleman = okuEleman.ReadLine();
                k1.DogumYeri = yaziEleman;
                yaziEleman = okuEleman.ReadLine();
                k1.IlgiAlanlari = yaziEleman;

                yaziEleman = okuEleman.ReadLine();
                if (yaziEleman == "Ingilizce")
                    k1.YabanciDil.Add("İngilizce");
                else
                    k1.YabanciDil.Add("Diğer");

                yaziEleman = okuEleman.ReadLine();
                MedeniDurum m = new MedeniDurum();
                if (yaziEleman == "Evli")
                    m = MedeniDurum.Evli;
                else if (yaziEleman == "Bekar")
                    m = MedeniDurum.Bekar;
                k1.medeniDurum = m;

                yaziEleman = okuEleman.ReadLine();
                Uyruk u = new Uyruk();
                if (yaziEleman == "T.C.")
                    u = Uyruk.TC;
                else if (yaziEleman == "K.K.T.C.")
                    u = Uyruk.KKTC;
                else
                    u = Uyruk.Yabanci;
                k1.uyruk = u;

                yaziEleman = okuEleman.ReadLine();
                egitim.OkulAdi = yaziEleman;
                yaziEleman = okuEleman.ReadLine();
                egitim.Bolum = yaziEleman;
                yaziEleman = okuEleman.ReadLine();
                egitim.BaslangicTarih = Convert.ToInt32(yaziEleman);
                yaziEleman = okuEleman.ReadLine();
                egitim.BitisTarih = Convert.ToInt32(yaziEleman);
                yaziEleman = okuEleman.ReadLine();
                egitim.NotOrtalamasi = Convert.ToDouble(yaziEleman);

                listEgitim.InsertFirst(egitim);
                k1.EgitimBilgisi = listEgitim;
                egitim = new Egitim();

                yaziEleman = okuEleman.ReadLine();
                isDeneyimi.IsAd = yaziEleman;
                yaziEleman = okuEleman.ReadLine();
                isDeneyimi.IsAdres = yaziEleman;
                yaziEleman = okuEleman.ReadLine();
                isDeneyimi.Gorev = yaziEleman;

                listIsDeneyimi.InsertFirst(isDeneyimi);
                k1.Deneyimler = listIsDeneyimi;
                isDeneyimi = new IsDeneyimi();

                ka.Ekle(k1);
                listEgitim = new LinkedListEgitim();
                listIsDeneyimi = new LinkedListIsDeneyimi();
            }
            okuEleman.Close();
        }


        private void btnYeniOkulEkle_Click(object sender, EventArgs e)
        {
            if (txtOkulAdi.Text == "")
                MessageBox.Show("Eğitim bilgilerini girin.");
            else
            {
                //yeni egitim bilgileri dolduruldu
                egitim.OkulAdi = txtOkulAdi.Text;
                egitim.Bolum = txtBolum.Text;
                egitim.BaslangicTarih = Convert.ToInt32(txtBasTarih.Text);
                egitim.BitisTarih = Convert.ToInt32(txtBitTarih.Text);
                egitim.NotOrtalamasi = Convert.ToDouble(txtNotOrtalamasi.Text);

                //doldurulan eğitim bilgileri kaydedildi
                listEgitim.InsertFirst(egitim);

                MessageBox.Show("Eğitim bilgileri eklendi.");
                txtOkulAdi.Text = txtBolum.Text = txtBasTarih.Text = txtBitTarih.Text = txtNotOrtalamasi.Text = "";
                egitim = new Egitim();
            }
        }

        private void btnYeniIsEkle_Click(object sender, EventArgs e)
        {
            if (txtIsyeriAdi.Text == "")
                MessageBox.Show("Eğitim bilgilerini girin.");
            else
            {
                //yeni iş bilgileri dolduruldu
                isDeneyimi.IsAd = txtIsyeriAdi.Text;
                isDeneyimi.IsAdres = txtIsyeriAdres.Text;
                isDeneyimi.Gorev = txtPozisyon.Text;

                //yeni iş bilgileri kaydedildi.
                listIsDeneyimi.InsertFirst(isDeneyimi);
                MessageBox.Show("İşyeri bilgileri eklendi.");
                txtIsyeriAdi.Text = txtIsyeriAdres.Text = txtPozisyon.Text = "";
                isDeneyimi = new IsDeneyimi();
            }
        }

        private void btnTemelBilgiKaydet_Click(object sender, EventArgs e)
        {
            if (txtAd.Text == "")
                MessageBox.Show("Kişi bilgilerini girin.");
            else
            {
                //Kişi bilgileri dolduruldu
                k1 = new Kisi();
                k1.Ad = txtAd.Text;
                k1.Adres = txtAdres.Text;
                k1.Telefon = txtTelefon.Text;
                k1.Eposta = txtEposta.Text;
                k1.Referans = txtReferans.Text;
                k1.DogumTarihi = Convert.ToDateTime(dtDogumTarihi.Value.ToString());
                k1.DogumYeri = txtDogumYeri.Text;
                k1.IlgiAlanlari = txtIlgiAlani.Text;

                if (cbIngilizce.Checked == true)
                    k1.YabanciDil.Add(cbIngilizce.Text);
                if (cbDiger.Checked == true)
                    k1.YabanciDil.Add(cbDiger.Text);
                
                MedeniDurum m = new MedeniDurum();
                if (rbEvli.Checked == true)
                    m = MedeniDurum.Evli;
                else if (rbBekar.Checked == true)
                    m = MedeniDurum.Bekar;
                k1.medeniDurum = m;

                Uyruk u = new Uyruk();
                if (rbUyrukTC.Checked == true)
                    u = Uyruk.TC;
                else if (rbUyrukKKTC.Checked == true)
                    u = Uyruk.KKTC;
                else if (rbUyrukYabanci.Checked == true)
                    u = Uyruk.Yabanci;
                k1.uyruk = u;

                //kişinin eklenen eğitim bilgileri kişi ile ilişkilendirildi
                k1.EgitimBilgisi = listEgitim;
                //kişinin eklenen iş bilgileri kişi ile ilişkilendirildi
                k1.Deneyimler = listIsDeneyimi;

                //doldurulan kişi bilgileri kişi ağacına eklendi
                ka.Ekle(k1);

                MessageBox.Show("Kişi başarıyla eklendi.");

                txtAd.Text = txtAdres.Text = txtTelefon.Text = txtEposta.Text = txtIlgiAlani.Text = txtDogumYeri.Text = txtReferans.Text = "";
                rbBekar.Checked = rbEvli.Checked = rbUyrukTC.Checked = rbUyrukKKTC.Checked = rbUyrukYabanci.Checked = false;
                cbIngilizce.Checked = cbDiger.Checked = false;

                //bir sonra ki kişi için eğitim ve iş deneyimi listeleri baştan oluşturuldu
                listEgitim = new LinkedListEgitim();
                listIsDeneyimi = new LinkedListIsDeneyimi();
            }
        }
        //**********************************************************************
        private void btnAra_Click_1(object sender, EventArgs e)
        {
            if (txtAra.Text == "")
                MessageBox.Show("Arama yapmak için önce aranacak kişi ismini girin.");
            else
            {
                //Eski arama bilgilerinde eski arana kişini eğitim bilgileri listbox'dan silindi.
                lbEgitim.Items.Clear();
                lbDeneyim.Items.Clear();

                //Ara methodu ile kişi ağacı üzerinde kişi adına göre arama işlemi gerçekleştirildi
                dugum = ka.Ara(txtAra.Text);
                //kişi ikili arama ağacında bulunamazsa null değer döner
                if (dugum == null)
                {
                    MessageBox.Show("Aradığınız kişi bulunamadı.");
                    txtAra.Text = "";
                }
                else
                {
                    //ikili arama ağacı üzerinde kişi bulunursa bilgileri gösterildi
                    k1 = ((Kisi)dugum.veri);
                    txtGunAd.Text = ((Kisi)dugum.veri).Ad;
                    txtGunAdres.Text = ((Kisi)dugum.veri).Adres;
                    txtGunTelefon.Text = ((Kisi)dugum.veri).Telefon;
                    txtGunEposta.Text = ((Kisi)dugum.veri).Eposta;
                    txtGunReferans.Text = ((Kisi)dugum.veri).Referans;
                    txtGunDogumYeri.Text = ((Kisi)dugum.veri).DogumYeri;
                    txtGunIlgiAlanlari.Text = ((Kisi)dugum.veri).IlgiAlanlari;
                    dtGunDogumTarihi.Value =(Convert.ToDateTime((k1.DogumTarihi)));

                    string ingilizce = ((Kisi)dugum.veri).YabanciDil.Find(stringX => stringX == cbGunIngilizce.Text);
                    string diger = ((Kisi)dugum.veri).YabanciDil.Find(stringX => stringX == cbGunDiger.Text);
                    if (ingilizce == cbGunIngilizce.Text)
                        cbGunIngilizce.Checked = true;
                    else
                        cbGunIngilizce.Checked = false;
                    if (diger == cbGunDiger.Text)
                        cbGunDiger.Checked = true;
                    else
                        cbGunDiger.Checked = false;

                    MedeniDurum m = new MedeniDurum();
                    m = ((Kisi)dugum.veri).medeniDurum;
                    if (m == MedeniDurum.Evli)
                        rbGunEvli.Checked = true;
                    else if (m == MedeniDurum.Bekar)
                        rbGunBekar.Checked = true;
                    Uyruk u = new Uyruk();
                    u = ((Kisi)dugum.veri).uyruk;
                    if (u == Uyruk.TC)
                        rbGunUyrukTC.Checked = true;
                    else if (u == Uyruk.KKTC)
                        rbGunUyrukKKTC.Checked = true;
                    else if (u == Uyruk.Yabanci)
                        rbGunUyrukYabanci.Checked = true;

                    //Kişinin kayıtlı eğitim bilgileri eğitim bilgisi listesi null olana kadar listelendi
                    Node nodeEgitim = new Node();
                    nodeEgitim = ((Kisi)dugum.veri).EgitimBilgisi.Head;
                    while (nodeEgitim != null)
                    {
                        lbEgitim.Items.Add(((Egitim)nodeEgitim.Data).OkulAdi.ToString());
                        nodeEgitim = nodeEgitim.Next;
                    }

                    //Kişinin kayıtlı deneyim bilgileri deneyim bilgisi listesi null olana kadar listelendi
                    Node nodeDeneyim = new Node();
                    nodeDeneyim = ((Kisi)dugum.veri).Deneyimler.Head;
                    while (nodeDeneyim != null)
                    {
                        lbDeneyim.Items.Add(((IsDeneyimi)nodeDeneyim.Data).IsAd.ToString());
                        nodeDeneyim = nodeDeneyim.Next;
                    }
                }
            }
        } 

        private void btnSil_Click_1(object sender, EventArgs e)
        {
            if (txtAra.Text == "")
                MessageBox.Show("Önce silmek istediğiniz kişinin adını girin.");
            else
            {
                //Kişi ağacında aranmış ve bulunmuş olan kişi, kişi ağacından silindi.
                bool sil = ka.Sil(k1.Ad);
                if (sil)
                {
                    MessageBox.Show("Silme işlemi başarılı.");
                    txtGunOkulAdi.Text = txtGunBolum.Text = txtGunBasTarih.Text = txtGunBitTarih.Text = txtGunIsyeriAdi.Text = txtGunIsyeriAdres.Text = txtGunPozisyon.Text = txtGunNotOrtalamasi.Text = txtAra.Text = txtGunAd.Text = txtGunAdres.Text = txtGunTelefon.Text = txtGunEposta.Text = txtGunReferans.Text = txtGunDogumYeri.Text = txtGunIlgiAlanlari.Text = "";
                    rbGunEvli.Checked = rbGunBekar.Checked = rbGunUyrukTC.Checked = rbGunUyrukKKTC.Checked = rbGunUyrukYabanci.Checked = false;
                    cbGunIngilizce.Checked = cbGunDiger.Checked = false;
                    lbEgitim.Items.Clear();
                    lbDeneyim.Items.Clear();
                }
                else
                    MessageBox.Show("Kişi bulunamadı.");
            }
        }
        //**********************************
        private void btnEgitimBilGoster_Click_1(object sender, EventArgs e)
        {
            //Listbox'dan seçilmiş olan eğitim bilgisi güncelleme ekranında textboxlarda gösterildi
            if (lbEgitim.SelectedItem == null)
                MessageBox.Show("Önce güncellenecek eğitim bilgisini seçin.");
            else
            {
                Node egitim = new Node();
                egitim = ((Kisi)dugum.veri).EgitimBilgisi.Head;

                //Listbox'da seçilmiş olan eğitim bilgisi okul adına göre bulundu
                while (((Egitim)egitim.Data).OkulAdi != lbEgitim.SelectedItem.ToString())
                    egitim = egitim.Next;
                //bulunan eğitim bilgileri listelendi
                txtGunOkulAdi.Text = ((Egitim)egitim.Data).OkulAdi;
                txtGunBolum.Text = ((Egitim)egitim.Data).Bolum;
                txtGunBasTarih.Text = ((Egitim)egitim.Data).BaslangicTarih.ToString();
                txtGunBitTarih.Text = ((Egitim)egitim.Data).BitisTarih.ToString();
                txtGunNotOrtalamasi.Text = ((Egitim)egitim.Data).NotOrtalamasi.ToString();
            }
        }

        private void btnGunEgitimEkle_Click(object sender, EventArgs e)
        {
            //Sistemde kayıtlı kişiye kayıt işleminde eklediği eğitim bilgileri dışında yeni eğitim bilgisi eklemek istersek
            if (txtAra.Text == "" || ka.Ara(txtAra.Text) == null) //ilk olarak kişi bulundu.
                MessageBox.Show("Güncelleme kısmından eğitim bilgisi eklemek için önce güncellenecek kişiyi bulun.");
            else
            {
                if (txtGunOkulAdi.Text == "")
                    MessageBox.Show("Eğitim bilgilerini girin.");
                else
                {
                    //yeni eğitim bilgileri dolduruldu
                    egitim.OkulAdi = txtGunOkulAdi.Text;
                    egitim.Bolum = txtGunBolum.Text;
                    egitim.BaslangicTarih = Convert.ToInt32(txtGunBasTarih.Text);
                    egitim.BitisTarih = Convert.ToInt32(txtGunBitTarih.Text);
                    egitim.NotOrtalamasi = Convert.ToDouble(txtGunNotOrtalamasi.Text);

                    //doldurulan eğitim bilgileri bulunan kişiye eklendi
                    ((Kisi)dugum.veri).EgitimBilgisi.InsertFirst(egitim);
                    MessageBox.Show("Eğitim bilgileri eklendi.");
                    txtGunOkulAdi.Text = txtGunBolum.Text = txtGunBasTarih.Text = txtGunBitTarih.Text = txtGunNotOrtalamasi.Text = "";
                    egitim = new Egitim(); //yeni eğitim bilgisi ekleme için eğitim değişkeni new'lendi
                }
            }
        }

        private void btnGunEgitimIleri_Click_1(object sender, EventArgs e)
        {
            if (lbEgitim.SelectedItem == null)
                MessageBox.Show("Önce güncellenecek eğitim bilgisini seçin.");
            else
            {
                //listbox'da şeçilmiş eğitim bilgisi kişinin eğitim bilgileri arasından(linked list) bulundu
                Node egitimBilgisi = new Node();
                egitimBilgisi = ((Kisi)dugum.veri).EgitimBilgisi.Head;

                while (true)
                {
                    if (((Egitim)egitimBilgisi.Data).OkulAdi == lbEgitim.SelectedItem.ToString())
                    {
                        //bulunan eğitim bilgisine ilgili textboxlardaki bilgiler gönderildi ve güncelleme gerçekleştirildi.
                        ((Egitim)egitimBilgisi.Data).OkulAdi = txtGunOkulAdi.Text;
                        ((Egitim)egitimBilgisi.Data).Bolum = txtGunBolum.Text;
                        ((Egitim)egitimBilgisi.Data).BaslangicTarih = Convert.ToInt32(txtGunBasTarih.Text);
                        ((Egitim)egitimBilgisi.Data).BitisTarih = Convert.ToInt32(txtGunBitTarih.Text);
                        ((Egitim)egitimBilgisi.Data).NotOrtalamasi = Convert.ToDouble(txtGunNotOrtalamasi.Text);
                        MessageBox.Show("Eğitim bilgisi güncellendi.");
                        break;
                    }
                    else//Eğitim bilgisi bulunamazsa diğer düğümlerde ara
                        egitimBilgisi = egitimBilgisi.Next;
                }
            }
        }

        //**********************************************

        private void btnIsBilGoster_Click_1(object sender, EventArgs e)
        {
            //Listbox'dan seçilmiş olan iş bilgisi güncelleme ekranında textboxlarda gösterildi
            if (lbDeneyim.SelectedItem == null)
                MessageBox.Show("Önce güncellenecek geçmiş iş bilgisini seçin.");
            else
            {
                Node isDeneyimi = new Node();
                isDeneyimi = ((Kisi)dugum.veri).Deneyimler.Head;

                //Listbox'da seçilmiş olan iş bilgisi iş adına göre bulundu
                while (((IsDeneyimi)isDeneyimi.Data).IsAd != lbDeneyim.SelectedItem.ToString())
                    isDeneyimi = isDeneyimi.Next;

                //bulunan iş bilgileri listelendi
                txtGunIsyeriAdi.Text = ((IsDeneyimi)isDeneyimi.Data).IsAd;
                txtGunIsyeriAdres.Text = ((IsDeneyimi)isDeneyimi.Data).IsAdres;
                txtGunPozisyon.Text = ((IsDeneyimi)isDeneyimi.Data).Gorev;
            }
        }
        
        private void btnGunIsBilEkle_Click(object sender, EventArgs e)
        {
            //Sistemde kayıtlı kişiye kayıt işleminde eklediği deneyim bilgileri dışında yeni deneyim bilgisi eklemek istersek
            if (txtAra.Text == "" || ka.Ara(txtAra.Text) == null)
                MessageBox.Show("Güncelleme kısmından eğitim bilgisi eklemek için önce güncellenecek kişiyi bulun.");
            else
            {
                if (txtGunIsyeriAdi.Text == "")
                    MessageBox.Show("İşyeri bilgilerini girin.");
                else
                {
                    //yeni deneyim bilgileri dolduruldu
                    isDeneyimi.IsAd = txtGunIsyeriAdi.Text;
                    isDeneyimi.IsAdres = txtGunIsyeriAdres.Text;
                    isDeneyimi.Gorev = txtGunPozisyon.Text;

                    //doldurulan deneyim bilgileri bulunan kişiye eklendi
                    ((Kisi)dugum.veri).Deneyimler.InsertFirst(isDeneyimi);
                    MessageBox.Show("İşyeri bilgileri eklendi.");
                    txtGunIsyeriAdi.Text = txtGunIsyeriAdres.Text = txtGunPozisyon.Text = "";
                    isDeneyimi = new IsDeneyimi(); //yeni deneyim bilgisi ekleme için işDeneyimi değişkeni new'lendi
                }
            }
        }

        private void btnGunIsBilgisi_Click_1(object sender, EventArgs e)
        {
            if (lbDeneyim.SelectedItem == null)
                MessageBox.Show("Önce güncellenecek geçmiş iş bilgisini seçin.");
            else
            {
                //listbox'da şeçilmiş deneyim bilgisi kişinin deneyim bilgileri arasından(linked list) bulundu
                Node isDeneyimi = new Node();
                isDeneyimi = ((Kisi)dugum.veri).Deneyimler.Head;

                while (true)
                {
                    //bulunan deneyim bilgisine ilgili textboxlardaki bilgiler gönderildi ve güncelleme gerçekleştirildi.
                    if (((IsDeneyimi)isDeneyimi.Data).IsAd == lbDeneyim.SelectedItem.ToString())
                    {
                        ((IsDeneyimi)isDeneyimi.Data).IsAd = txtGunIsyeriAdi.Text;
                        ((IsDeneyimi)isDeneyimi.Data).IsAdres = txtGunIsyeriAdres.Text;
                        ((IsDeneyimi)isDeneyimi.Data).Gorev = txtGunPozisyon.Text;
                        MessageBox.Show("İş bilgisi güncellendi.");
                        break;
                    }
                    else//Deneyim bilgisi bulunamazsa diğer düğümlerde ara
                        isDeneyimi = isDeneyimi.Next;
                }
            }
        }

        //*****************************************

        private void btnGuncelle_Click_1(object sender, EventArgs e)
        {
            if (txtGunAd.Text == "")
                MessageBox.Show("Önce güncellenecek kişiyi bulun.");
            else
            {
                //Güncel kişi bilgileri(Guncelleme işlemindeki textboxlardan) kişi ağacından bulunan kişinin bilgilerine gönderildi
                k1.Ad = txtGunAd.Text;
                k1.Adres = txtGunAdres.Text;
                k1.Telefon = txtGunTelefon.Text;
                k1.Eposta = txtGunEposta.Text;
                k1.Referans = txtGunReferans.Text;
                k1.DogumTarihi = Convert.ToDateTime(dtGunDogumTarihi.Value.ToString());
                k1.DogumYeri = txtGunDogumYeri.Text;
                k1.IlgiAlanlari = txtGunIlgiAlanlari.Text;

                k1.YabanciDil = new List<string>();
                if (cbGunIngilizce.Checked == true)
                    k1.YabanciDil.Add(cbGunIngilizce.Text);
                if (cbGunDiger.Checked == true)
                    k1.YabanciDil.Add(cbGunDiger.Text);

                MedeniDurum m = new MedeniDurum();
                if (rbGunEvli.Checked == true)
                    m = MedeniDurum.Evli;
                else if (rbGunBekar.Checked == true)
                    m = MedeniDurum.Bekar;
                k1.medeniDurum = m;

                Uyruk u = new Uyruk();
                if (rbGunUyrukTC.Checked == true)
                    u = Uyruk.TC;
                else if (rbGunUyrukKKTC.Checked == true)
                    u = Uyruk.KKTC;
                else if (rbGunUyrukYabanci.Checked == true)
                    u = Uyruk.Yabanci;
                k1.uyruk = u;

                MessageBox.Show("Güncelleme işlemi başarılı.");

                //Güncelleme işlemi tamamlandıktan sonra yeni güncelleme işlemine hazırlamak için textboxlar ve listboxlar temizlendi
                txtGunOkulAdi.Text = txtGunBolum.Text = txtGunBasTarih.Text = txtGunBitTarih.Text = txtGunIsyeriAdi.Text = txtGunIsyeriAdres.Text = txtGunPozisyon.Text = txtGunNotOrtalamasi.Text = txtAra.Text = txtGunAd.Text = txtGunAdres.Text = txtGunTelefon.Text = txtGunEposta.Text = txtGunReferans.Text = txtGunDogumYeri.Text = txtGunIlgiAlanlari.Text = "";
                rbGunEvli.Checked = rbGunBekar.Checked = rbGunUyrukTC.Checked = rbGunUyrukKKTC.Checked = rbGunUyrukYabanci.Checked = false;
                cbGunIngilizce.Checked = cbGunDiger.Checked = false;

                lbEgitim.Items.Clear();
                lbDeneyim.Items.Clear();
                k1 = new Kisi(); // yeni güncelleme işlemi için kişi bilgisi new'lendi
            }
        }

        //*************************************************

        public Node IlanBul() //İlana Başvuran kişileri bulma(heapler), ilana uygun adayı bulma(uygun adayı bulmadan önce istenilen ilan bulunmalıdır.), ilana başvuru yapma(başvuru yapmadan önce başvuru yapmak istediğimiz ilan bulunmalı) işlemlerinde IlanBul() methodu kullanıldı
        {
            Node temp = new Node();
            ilanBulundu = false;

            foreach (Sirket i in listeSirket)
            {
                temp = i.Ilanlar.Head;
                while (temp != null)//Head'den itibaren ilanlarda ilerlendi
                {
                    if (((Ilan)temp.Data).IlanId == lbIlanlar.SelectedIndex) //Id'ler eşitse ilan bulundu
                    {
                        ilanBulundu = true;
                        break;
                    }
                    temp = temp.Next;//Id'lerin eşit olmaması durumunda yeni düğüme geçildi
                }
                if (ilanBulundu)//ilan bulunduysa diğer ilanları kontrol etmeye gerek yok
                    break;
            }
            return temp;
        }

        public IkiliAramaAgacDugumu KisiBul()//Başvuru işleminde KisiBul() methodu kullanıldı
        {
            IkiliAramaAgacDugumu k = new IkiliAramaAgacDugumu();
            k = ka.Ara(txtBasvurKisiAd.Text);//kisi agacında arama işlemi gerçekleştirildi
            return k;//kişi bulunursa bulunan kişiyi, bulunamazsa null döndürür.
        } 

        private void btnBasvur_Click(object sender, EventArgs e)//Başvurmak istediğimiz ilanı bulup ilan ayrıntılarını (ilanın adı, eleman özellikleri gibi ve ilana başvuran kişileri listeleme vb) göstermek için bu buton kullanıldı
        {
            if (lbIlanlar.SelectedItem != null)
            {
                Node temp = IlanBul();//listbox'da seçilen ilan bulundu
                txtIlanGoster.Text = "İş tanımı :" + Environment.NewLine + ((Ilan)temp.Data).IsTanimi + Environment.NewLine + Environment.NewLine + "Eleman özellik :" + Environment.NewLine + ((Ilan)temp.Data).ElemanOzellik;
                HeapBasvuru tekHeapBasvurusu = ((Ilan)temp.Data).heapBasvuru;
                txtIseBasvuranlar.Text = tekHeapBasvurusu.BasvuruListele(tekHeapBasvurusu);//Bulunan ilanın heap'i (başvuran kişiler listelendi)
            }
            else
                MessageBox.Show("Lütfen önce başvurmak istediğiniz ilanı seçin!");
        }

        private void btnIseAlinanAdayiBul_Click(object sender, EventArgs e)
        {
            if (lbIlanlar.SelectedItem == null)
                MessageBox.Show("Önce ilanı seçin.");
            else
            {
                Node temp = IlanBul();//Seçilen ilana uygun aday(uygunluk puanı, not ortalaması ve ingilizce bilgisine göre karar veriliyor) arandı. Bulunursa adı gösterildi.
                HeapDugumu uygunAday = ((Ilan)temp.Data).heapBasvuru.UygunAdayBul();
                if (uygunAday == null)
                    MessageBox.Show("Henüz işe uygun aday başvurmadı");
                else
                    txtIseAlinanAday.Text = ((Kisi)uygunAday.Deger).Ad;
            }
        }

        private void btnBasvuruYap_Click(object sender, EventArgs e)
        {
            if (lbIlanlar.SelectedItem == null)
                MessageBox.Show("Başvuru yapmak için önce başvurmak istediğiniz işyerini seçin.");
            else
            {
                //Listbox'da seçilen ilan ve kişi ağacında kayıtlı olan kişi bulunup ilana başvuru işlemi gerçekleştirildi.
                if (txtBasvurKisiAd.Text == "")
                    MessageBox.Show("Başvuracak kişinin ismini girin.");
                else
                {
                    if (KisiBul() == null) //Ağaçta başvuru yapmak istenen var mı kontrolü
                        MessageBox.Show("Başvuru yapmak istediğiniz kişi sistemde kayıtlı değil!" + Environment.NewLine + "Lütfen önce kişiyi sisteme kaydedin.");
                    else
                    {
                        Node temp = IlanBul();//Seçilen ilan bulundu
                        Kisi k = ((Kisi)KisiBul().veri);
                        if (((Ilan)temp.Data).heapBasvuru.Ara(((Ilan)temp.Data).heapBasvuru, k) == true)
                            MessageBox.Show("Bu işe başvurunuz zaten var." + Environment.NewLine + "Lütfen başka bir işe başvuru yapın");//Kişi ilana daha önce başvuru yaptıysa tekrar başvuru yapılması önlendi.
                        else
                        {
                            if (txtUygunlukPuani.Text != "") //Uygunluk puanı kullanıcı tarafından verilirse
                                k.UygunlukPuani = Convert.ToDouble(txtUygunlukPuani.Text);
                            else
                            {
                                //Uygunluk puanı random atandı
                                Random r = new Random();
                                k.UygunlukPuani = r.Next(0, 10);
                            }
                            ((Ilan)temp.Data).heapBasvuru.Insert(k); //Başvuru eklendi
                            MessageBox.Show("Başvuru başarıyla gerçekleştirildi.");
                            txtUygunlukPuani.Text = txtBasvurKisiAd.Text = "";
                        }
                    }
                }
            }
        }

        //**************************************

        private void btnSirketEkle_Click(object sender, EventArgs e)
        {
            if (txtSirketAd.Text == "")
                MessageBox.Show("Şirket bilgilerini girin.");
            else
            {
                //Yeni şirket bilgileri sirket nesnesinde tutuldu.
                sirket = new Sirket();
                sirket.Ad = txtSirketAd.Text;
                sirket.Adresi = txtSirketAdres.Text;
                sirket.Eposta = txtSirketEposta.Text;
                sirket.Telefon = txtSirketTelefon.Text;
                sirket.Faks = txtSirketFaks.Text;

                //Tutulan bilgiler şirket listesinde tutuldu.
                listeSirket.Add(sirket);
                MessageBox.Show("Şirket sisteme başarıyla kaydedildi.");
                txtSirketAd.Text = txtSirketAdres.Text = txtSirketEposta.Text = txtSirketTelefon.Text = txtSirketFaks.Text = "";
            }
        }

        //*****************************************

        private void btnGunSirketAra_Click_1(object sender, EventArgs e)
        {
            if (txtGunSirketAra.Text == "")
                MessageBox.Show("Güncellemek istediğiniz şirketin adını yazın.");
            else
            {
                //İşyeri adına göre listede kayıtlı işyeri bulundu.
                sirket = listeSirket.Find(x => x.Ad == txtGunSirketAra.Text);
                if (sirket == null)
                    MessageBox.Show("Aradığınız şirket sistemde kayıtlı değil!");
                else
                {
                    //Bulunan işyeri bilgileri ekranda gösterildi.
                    txtGunSirketAdi.Text = sirket.Ad;
                    txtGunSirketAdres.Text = sirket.Adresi;
                    txtGunSirketEposta.Text = sirket.Eposta;
                    txtGunSirketTelefon.Text = sirket.Telefon;
                    txtGunSirketFaks.Text = sirket.Faks;
                }
            }
        }
        
        private void btnGunSirketKaydet_Click_1(object sender, EventArgs e)
        {
            if (txtGunSirketAdi.Text == "")
                MessageBox.Show("Güncelleme yapmak için önce güncellenecek şirketi arayın.");
            else
            {
                //Güncellenen bilgiler sirket nesnesine kaydedildi.
                sirket.Ad = txtGunSirketAdi.Text;
                sirket.Adresi = txtGunSirketAdres.Text;
                sirket.Eposta = txtGunSirketEposta.Text;
                sirket.Telefon = txtGunSirketTelefon.Text;
                sirket.Faks = txtGunSirketFaks.Text;
                MessageBox.Show("Güncelleme işlemi başarıyla gerçekleştirildi.");
                txtGunSirketAdi.Text = txtGunSirketAdres.Text = txtGunSirketEposta.Text = txtGunSirketTelefon.Text = txtGunSirketFaks.Text = "";
            }
        }

        //********************************************************

        private void btnYeniIlanEkle_Click_1(object sender, EventArgs e)
        {
            //İşyeri adına göre listede kayıtlı işyeri bulundu.
            sirket = listeSirket.Find(x => x.Ad == txtIlanIsyeriAdi.Text);

            if (sirket == null)
                MessageBox.Show("Şirket bilgisi bulunamadı!" + Environment.NewLine + "İlan vermek için önce şiketi sisteme kaydedin.");
            else
            {
                if (txtIlanIsTanimi.Text == "")
                    MessageBox.Show("İlan verebilmek için ilan bilgilerini doldurun.");
                else
                {
                    //Yeni ilan bilgileri alındı.
                    Ilan ilan = new Ilan();
                    ilan.IlanId = ilanID++;
                    ilan.IsTanimi = txtIlanIsTanimi.Text;
                    ilan.ElemanOzellik = txtIlanElemanOzellik.Text;

                    //İlan sınıfının içerisindeki şirket bilgisi dolduruldu.
                    ilan.sirket = sirket;
                    //Şirket sınıfı içerisindeki Ilanlar listesine yeni ilan eklendi.
                    sirket.Ilanlar.InsertFirst(ilan);
                    MessageBox.Show("İlan ekleme işleminiz başarıyla gerçekleştirildi.");
                    lbIlanlar.Items.Add(ilanID + ". " + ilan.IsTanimi);
                    ht.IlanEkle(ilan.IlanId, ilan.heapBasvuru);

                    txtIlanElemanOzellik.Text = txtIlanIsTanimi.Text = "";
                }
            }
        }

        //***************************************************
                
        private void btnPreOrder_Click(object sender, EventArgs e)
        {
            ka.PreOrder();
            txtPreOrderListe.Text = ka.DugumleriYazdir();
        }

        private void btnInOrder_Click(object sender, EventArgs e)
        {
            ka.InOrder();
            txtInOrderListe.Text = ka.DugumleriYazdir();
        }

        private void btnPostOrder_Click(object sender, EventArgs e)
        {
            ka.PostOrder();
            txtPostOrderListe.Text = ka.DugumleriYazdir();
        }

        private void btnDoksanUzeriNot_Click(object sender, EventArgs e)
        {
            ka.DoksanUzeriNotAra(); //Kişinin not bilgisine göre ağaçta 90 üzeri notlar listelendi.
            txtListe.Text = ka.DugumleriYazdir();
        }

        private void btnIngilizceBilenler_Click(object sender, EventArgs e)
        {
            ka.DilKontrolAra(); //Kişinin yabancı dil bilgisine göre ingilizce bilenler listelendi.
            txtListe.Text = ka.DugumleriYazdir();
        }

        private void btnDerinlikBul_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Derinlik : " + ka.DerinlikBul().ToString());
        }

        private void btnElemanSayisiBul_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Eleman Sayısı : " + ka.ElemanSayisi().ToString());
        }
    }
}
