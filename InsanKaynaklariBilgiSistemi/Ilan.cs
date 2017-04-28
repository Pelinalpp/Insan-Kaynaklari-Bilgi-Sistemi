using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsanKaynaklariBilgiSistemi
{
    public class Ilan
    {
        public Ilan()
        {
            heapBasvuru = new HeapBasvuru(100);
        }
        public int IlanId { get; set; }
        public string IsTanimi { get; set; }
        public string ElemanOzellik { get; set; }
        public Sirket sirket { get; set; }

        public HeapBasvuru heapBasvuru;
    }
}
