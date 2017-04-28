using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsanKaynaklariBilgiSistemi
{
    public class Sirket
    {
        public Sirket()
        {
            Ilanlar = new LinkedListIlan();
        }
        public string Ad { get; set; }
        public string Adresi { get; set; }
        public string Telefon { get; set; }
        public string Faks { get; set; }
        public string Eposta { get; set; }
        public LinkedListIlan Ilanlar;
    }
}
