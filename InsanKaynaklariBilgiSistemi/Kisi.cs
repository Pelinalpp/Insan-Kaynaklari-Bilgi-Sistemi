using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsanKaynaklariBilgiSistemi
{
    public class Kisi
    {
        public Kisi()
        {
            this.Deneyimler = new LinkedListIsDeneyimi();
            this.EgitimBilgisi = new LinkedListEgitim();
            this.YabanciDil = new List<string>();
        }
        public string Ad { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string Eposta { get; set; }
        public Uyruk uyruk { get; set; }
        public DateTime DogumTarihi { get; set; }
        public MedeniDurum medeniDurum { get; set; }
        public string DogumYeri { get; set; }
        public List<string> YabanciDil { get; set; }
        public string IlgiAlanlari { get; set; }
        public string Referans { get; set; }
        public double UygunlukPuani { get; set; }

        public LinkedListIsDeneyimi Deneyimler;
        public LinkedListEgitim EgitimBilgisi;

        }
}
