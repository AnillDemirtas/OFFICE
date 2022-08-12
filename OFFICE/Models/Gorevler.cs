using System;
using System.Collections.Generic;

namespace OFFICE.Models
{
    public class Gorevler
    {

        public List<Firma> firma_model { get; set; }
        public List<Kullanici> kullanici_model { get; set; }

    }

    public class GorevKayit
    {
        public string id { get; set; }
        public Guid firma_id { get; set; }
        public string user_id { get; set; }
        public string konu { get; set; }
        public string aciklama { get; set; }
        public bool aciliyet_durumu { get; set; }


    }
    public class Gorevlerim
    {
        public Guid id { get; set; }
        public Guid gorev_id { get; set; }
        public Guid hareket_id { get; set; }
        public Guid firma_id { get; set; }
        public string user_id { get; set; }

        public string unvan { get; set; }
        public string konu { get; set; }
        public string aciklama { get; set; }

        public string konu_detay { get; set; }
        public bool aciliyet_durumu { get; set; }
        public DateTime olusturma_tarihi { get; set; }


    }
    public class Hareketler
    {
        public Guid id { get; set; }
        public Guid gorev_id { get; set; }
        public string user_id { get; set; }
        public string aciklama { get; set; }
        public DateTime guncelleme_tarihi { get; set; }


    }

}