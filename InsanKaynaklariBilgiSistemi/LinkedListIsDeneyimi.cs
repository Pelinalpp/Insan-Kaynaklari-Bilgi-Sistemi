using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsanKaynaklariBilgiSistemi
{
    public class LinkedListIsDeneyimi:ListADT
    {
        public override void InsertFirst(object value)
        {
            Node tempHead = new Node() { Data = value };

            if (Head == null)  //ilk düğüm null ise (yani liste boşsa) heade ekle
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

                if (((IsDeneyimi)temp.Data).IsAd == ((IsDeneyimi)Position).IsAd) //Silinecek düğüm head ise head'i bir sonraki düğüm yap
                {
                    Head = temp.Next;
                }
                while (temp != null) //silinecek değer bulunana kadar (iş adı ile kontrol edilecek) listede ilerle
                {
                    if (((IsDeneyimi)temp.Data).IsAd == ((IsDeneyimi)Position).IsAd) //silinecek değer bulunduğunda silinecek değerin next'ini bi önceki değerin next'i yap böylece listede artık temp'i gösteren eleman kalmadı ve silme işlemi gerçekleşti
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
                temp += "İş adı : " + ((IsDeneyimi)i.Data).IsAd.ToString() + Environment.NewLine + "Görevi : " + ((IsDeneyimi)i.Data).Gorev.ToString() + Environment.NewLine + "İşyeri adresi : " + ((IsDeneyimi)i.Data).IsAdres.ToString();
                i = i.Next;
            }
            return temp;
        }
    }
}
