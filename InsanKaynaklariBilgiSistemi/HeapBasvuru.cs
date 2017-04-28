using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsanKaynaklariBilgiSistemi
{
    public class HeapBasvuru
    {
        private HeapDugumu[] heapBasvuru;
        private int maksBoyut;
        private int gecerliBoyut;
        public HeapBasvuru(int maskHeapBoyutu)
        {
            maksBoyut = maskHeapBoyutu;
            gecerliBoyut = 0;
            heapBasvuru = new HeapDugumu[maksBoyut];
        }
        public bool IsEmpty()
        {
            return gecerliBoyut == 0; 
        }
        public bool Insert(Kisi deger)
        {
            //Heap dolu ise ekleme işlemi gerçekleştirilmedi
            if (gecerliBoyut == maksBoyut)
                return false;
            //Başvuru yapan kişi nesnesi heap'in son boş düğümüne eklendi
            HeapDugumu yeniHeapDugumu = new HeapDugumu(deger);
            heapBasvuru[gecerliBoyut] = yeniHeapDugumu;
            //Son düğüme eklenen Kişi nesnesi ad'a göre heap'de yerini alması için MoveToUp() methodu kullanıldı.
            MoveToUp(gecerliBoyut++);
            return true;
        }
        public void MoveToUp(int index)
        {
            int parent = (index - 1) / 2;
            HeapDugumu bottom = heapBasvuru[index];
            //Yeni eklenen kişi adının (ilk harfinin) ascii karşılığı Heap'de o an bulunduğu parentının adının(ilk harfinin) ascii karşılığından büyük olduğu sürece yer değiştirme işlemi gerçekleştirildi
            while (index > 0 && Convert.ToInt32(((Kisi)heapBasvuru[parent].Deger).Ad[0]) < Convert.ToInt32(((Kisi)bottom.Deger).Ad[0]))
            {
                heapBasvuru[index] = heapBasvuru[parent];
                index = parent;
                parent = (parent - 1) / 2;
            }
            heapBasvuru[index] = bottom;
        }
        
        public string BasvuruListele(HeapBasvuru temp)
        {
            int i = 0;
            string liste = "";
            while (((HeapDugumu)temp.heapBasvuru[i]) != null) //Heap'deki (ilana başvurmuş olan kişiler) kişi isimlerinin listeletme işlemleri gerçekleştirildi.
	        {
	            liste += ((Kisi)((HeapDugumu)temp.heapBasvuru[i]).Deger).Ad + Environment.NewLine;
                i++;
	        }
            return liste;
        }

        public bool Ara(HeapBasvuru temp, Kisi k)
        {
            //Bu method daha önce bir ilana başvuran kişinin tekrar başvuru yapmaması için oluşturuldu.
            //ilandaki başvuruların hepsi kontrol edilerek başvurunun ilanda kayıtlı olması durumunda true, aksi halde false döndürülerek kontrol işlemi gerçekleştirildi.
            int i = 0;
            Boolean bulundu = false;
            while (((HeapDugumu)temp.heapBasvuru[i]) != null)
            {
                if (((Kisi)((HeapDugumu)temp.heapBasvuru[i]).Deger) == k)
                {
                    bulundu = true;
                    break;
                }
                i++;
            }
            return bulundu;
        }

        public HeapDugumu UygunAdayBul()
        {
            int i = 0;
            double puan = 0;
            int birinciOncelik = -1; //Önceliğin ilk değeri -1 verildi ve -1 in değişmesi veya değişmemesi durumuna göre uygun adayın bulunup bulunmadığı kontrol edildi. Bulunduysa birinciOncelik değişkeni bunu belirten indis olarak kullanıldı.
            
            while (heapBasvuru[i] != null)
            {
                if (((Kisi)heapBasvuru[i].Deger).UygunlukPuani > puan)//Öncelikteki ilk kriter uygunluk puanı kontol edildi ve uygunluk puanı daha yüksek aday bulundu.
                {
                    birinciOncelik = i;
                    puan = ((Kisi)heapBasvuru[i].Deger).UygunlukPuani;
                } 
                else if (((Kisi)heapBasvuru[i].Deger).UygunlukPuani == puan)//uygunluk puanı eşit olması durumunda adayın ingilizce ve not bilgileri kontrol edildi.
                {
                    if (((Kisi)heapBasvuru[i].Deger).EgitimBilgisi.DoksanUzeriNot() == true && ((Kisi)heapBasvuru[i].Deger).YabanciDil.Find(stringX => stringX == "İngilizce") == "İngilizce")
                        birinciOncelik = i;
                }
                i++;
            }
            if (birinciOncelik == -1)
                return null;
            else
                return heapBasvuru[birinciOncelik];
        }

    }
}
