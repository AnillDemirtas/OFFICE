using System;
using System.Collections.Generic;

namespace OFFICE.Models
{
    public class Gorevler
    {

        public List<Firma> firma_model { get; set; }
        public List<Users> user_model { get; set; }

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

}