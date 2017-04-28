using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsanKaynaklariBilgiSistemi
{
    public class LinkedListIlan:ListADT
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

        public override void DeletePos(object Position)
        {
            if (Head != null)
            {
                Node temp = Head;

                Node posPreNode = new Node();
                posPreNode = Head;

                if (((Ilan)temp.Data).IlanId == ((Ilan)Position).IlanId) //Silinecek düğüm head ise head'i bir sonraki düğüm yap
                {
                    Head = temp.Next;
                }
                while (temp != null) //silinecek değer bulunana kadar (ilan id ile kontrol edilecek) listede ilerle
                {
                    if (((Ilan)temp.Data).IlanId == ((Ilan)Position).IlanId) //silinecek değer bulunduğunda silinecek değerin next'ini bi önceki değerin next'i yap böylece listede artık temp'i gösteren eleman kalmadı ve silme işlemi gerçekleşti
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
                temp += "İşyeri adı : " + ((Ilan)i.Data).sirket.Ad + Environment.NewLine + "İşyeri adresi : " + ((Ilan)i.Data).sirket.Adresi.ToString() + Environment.NewLine + "Telefon : " + ((Ilan)i.Data).sirket.Telefon.ToString() + Environment.NewLine + "Eposta : " + ((Ilan)i.Data).sirket.Eposta.ToString() + Environment.NewLine + "Faks : " + ((Ilan)i.Data).sirket.Faks.ToString() + Environment.NewLine + "Ilan ID : " + ((Ilan)i.Data).IlanId.ToString() + "İş tanımı : " + ((Ilan)i.Data).IsTanimi.ToString() + Environment.NewLine + "Eleman Özellik : " + ((Ilan)i.Data).ElemanOzellik.ToString() + Environment.NewLine + Environment.NewLine;
                i = i.Next;
            }
            return temp;
        }
    }
}
