using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsanKaynaklariBilgiSistemi
{
    public class LinkedListSirket:ListADT
    {
        public override void InsertFirst(object value)
        {
            Node tempHead = new Node() { Data = value };

            if (Head == null) //ilk düğüm null ise (yani liste boşsa) heade ekle
            {
                Head = tempHead;
            }
            else
            {
                //head'i head'in next'i yap ve yeni head'i eklenecek düğüm yap
                tempHead.Next = Head;
                Head = tempHead;
            }
            Size++;
        }

        public Node Ara(string sirket)
        {
            Node temp = new Node() { Data = Head };
            while (((Ilan)temp.Data).sirket.Ad != sirket || temp != null) // şirket adı aradığımız şirket adına eşit olmadığı sürece ve listedeki düğümler bitmediği sürece listede ilerle ve şirket var mı kontrol et
            {
                temp = temp.Next;
            }
            return temp;
        }

        public override void DeletePos(object Position)
        {
            if (Head != null)
            {
                Node temp = Head;

                Node posPreNode = new Node();
                posPreNode = Head;

                if (((Sirket)temp.Data).Ad == ((Sirket)Position).Ad) //Silinecek düğüm head ise head'i bir sonraki düğüm yap
                {
                    Head = temp.Next;
                }
                while (temp != null) //silinecek değer bulunana kadar (şirket adı ile kontrol edilecek) listede ilerle
                {
                    if (((Sirket)temp.Data).Ad == ((Sirket)Position).Ad) //silinecek değer bulunduğunda silinecek değerin next'ini bi önceki değerin next'i yap böylece listede artık temp'i gösteren eleman kalmadı ve silme işlemi gerçekleşti
                        posPreNode.Next = temp.Next;
                    else
                        posPreNode = temp;

                    temp = temp.Next;
                }
                Size--;
            }
        }

        public override string DisplayElements()
        {
            string temp = "";
            Node i = Head;
            while (i != null) //Liste null olana kadar listedeki iş bilgilerini temp'e ekle ve ilerle
            {
                temp += "İşyeri adı : " + ((Sirket)i.Data).Ad + Environment.NewLine + "İşyeri adresi : " + ((Sirket)i.Data).Adresi.ToString() + Environment.NewLine + "Telefon : " + ((Sirket)i.Data).Telefon.ToString() + Environment.NewLine + "Eposta : " + ((Sirket)i.Data).Eposta.ToString() + Environment.NewLine + "Faks : " + ((Sirket)i.Data).Faks.ToString() + Environment.NewLine + Environment.NewLine;
                i = i.Next;
            }
            return temp;
        }
    }
}
