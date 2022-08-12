using Dapper;
using Npgsql;
using OFFICE.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace OFFICE.Service
{
    public class DapperService
    {
        NpgsqlConnection conn;

        public DapperService()
        {
            conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SinifConnStr"].ConnectionString);
        }



        #region queriler 
        public string musteri_crud(Models.Firma bilgiler, string firma_id)
        {

            try
            {

                var parameters = new
                {
                    unvan = bilgiler.unvan,
                    yetkili = bilgiler.yetkili,
                    gsm = bilgiler.gsm.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", ""),
                    id = firma_id,
                };
                sqlOpen();
                if (firma_id == null || firma_id == "" || firma_id == "00000000-0000-0000-0000-000000000000")
                {
                    // kaydet
                    var gelen_id = conn.ExecuteScalar<object>("insert into firmalar  (unvan,yetkili,gsm) values (@unvan,@yetkili,@gsm) RETURNING id", parameters);
                    return gelen_id.ToString();
                }
                else
                {
                    //guncelle
                    var gelen_id = conn.ExecuteScalar<object>("update firmalar set unvan=@unvan,yetkili=@yetkili,gsm=@gsm where id=uuid(@id) RETURNING id", parameters);
                    return gelen_id.ToString();
                }



            }
            catch (Exception ex)
            {

                throw new Exception("Dapper Error:" + ex.Message);

            }
            finally
            {
                sqlClose();
            }
        }
        public string gorev_crud(Models.GorevKayit bilgiler, string gorev_id)
        {

            try
            {

                var parameters = new
                {
                    firma_id = bilgiler.firma_id,
                    user_id = bilgiler.user_id,
                    konu = bilgiler.konu,
                    aciklama = bilgiler.aciklama,
                    aciliyet_durumu = bilgiler.aciliyet_durumu,
                };
                sqlOpen();
                if (gorev_id == null || gorev_id == "")
                {
                    // kaydet
                    var gelen_id = conn.ExecuteScalar<object>("insert into gorevler  (firma_id,konu,aciklama,aciliyet_durumu) values (@firma_id,@konu,@aciklama,@aciliyet_durumu) RETURNING id", parameters);
                    return gelen_id.ToString();
                }
                else
                {
                    //guncelle
                    var gelen_id = conn.ExecuteScalar<object>("update gorevler set firma_id=@firma_id,konu=@konu,aciklama=@aciklama,aciliyet_durumu=@aciliyet_durumu where id=uuid(@id) RETURNING id", parameters);
                    return gelen_id.ToString();
                }



            }
            catch (Exception ex)
            {

                throw new Exception("Dapper Error:" + ex.Message);

            }
            finally
            {
                sqlClose();
            }
        }
        public string gorev_hareket_crud(Models.Hareketler bilgiler, string hareket_id)
        {

            try
            {

                var parameters = new
                {
                    id = hareket_id,
                    gorev_id = bilgiler.gorev_id,
                    aciklama = bilgiler.aciklama,
                };
                sqlOpen();
                if (hareket_id == null || hareket_id == "" || hareket_id == "00000000-0000-0000-0000-000000000000")
                {
                    // kaydet
                    var gelen_id = conn.ExecuteScalar<object>("insert into gorev_hareketleri (gorev_id,aciklama) values (@gorev_id,@aciklama) RETURNING id", parameters);
                    return gelen_id.ToString();
                }
                else
                {
                    //guncelle
                    var gelen_id = conn.ExecuteScalar<object>("update gorev_hareketleri set aciklama=@aciklama where id=uuid(@id) RETURNING id", parameters);
                    return gelen_id.ToString();
                }



            }
            catch (Exception ex)
            {

                throw new Exception("Dapper Error:" + ex.Message);

            }
            finally
            {
                sqlClose();
            }
        }

        public List<Kullanici> kullanicilar()
        {
            try
            {

                sqlOpen();
                List<Kullanici> sinifList = conn.Query<Kullanici>("select id,concat(ad,' ',soyad) as adsoyad from kullanicilar").ToList();
                return sinifList;
            }
            catch (Exception ex)
            {

                throw new Exception("Dapper Error:" + ex.Message);

            }
            finally
            {
                sqlClose();
            }
        }
        public List<Gorevlerim> gorevlerim()
        {
            try
            {

                sqlOpen();
                List<Gorevlerim> sinifList = conn.Query<Gorevlerim>("select g.id,unvan,konu,aciklama,aciliyet_durumu from firmalar f  inner join gorevler g on f.id = g.firma_id ").ToList();
                return sinifList;
            }
            catch (Exception ex)
            {

                throw new Exception("Dapper Error:" + ex.Message);

            }
            finally
            {
                sqlClose();
            }
        }
        public List<Gorevlerim> gorev_detaylari(string gorev_id)
        {
            try
            {
                var parameters = new
                {
                    id = gorev_id,
                };
                sqlOpen();
                List<Gorevlerim> sinifList = conn.Query<Gorevlerim>(@"select 
                gorev.id as gorev_id, 
                hareketler.id as hareket_id, 
                unvan, 
                konu,
                gorev.aciklama as konu_detay,
                hareketler.aciklama, 
                aciliyet_durumu, 
                hareketler.olusturma_tarihi 
                from 
                firmalar firma 
                inner join gorevler gorev on firma.id = gorev.firma_id 
                left join gorev_hareketleri hareketler on gorev.id = hareketler.gorev_id
                where gorev.id=uuid(@id)", parameters).ToList();
                return sinifList;
            }
            catch (Exception ex)
            {

                throw new Exception("Dapper Error:" + ex.Message);

            }
            finally
            {
                sqlClose();
            }
        }
        public List<Hareketler> gorev_hareket_detay(string hareket_id)
        {
            try
            {
                var parameters = new
                {
                    id = hareket_id,
                };
                sqlOpen();
                List<Hareketler> sinifList = conn.Query<Hareketler>(@"select * from gorev_hareketleri where id=uuid(@id)", parameters).ToList();
                return sinifList;
            }
            catch (Exception ex)
            {

                throw new Exception("Dapper Error:" + ex.Message);

            }
            finally
            {
                sqlClose();
            }
        }
        public List<Firma> Firmalar()
        {
            try
            {

                sqlOpen();
                List<Firma> sinifList = conn.Query<Firma>("select id, unvan,yetkili,gsm from firmalar").ToList();
                return sinifList;
            }
            catch (Exception ex)
            {

                throw new Exception("Dapper Error:" + ex.Message);

            }
            finally
            {
                sqlClose();
            }
        }
        public List<Firma> Firma(string firma_id)
        {
            try
            {
                var parameters = new
                {
                    id = firma_id,
                };
                sqlOpen();
                List<Firma> sinifList = conn.Query<Firma>("select id, unvan,yetkili,gsm from firmalar where id=uuid(@id)", parameters).ToList();

                return sinifList;
            }
            catch (Exception ex)
            {

                throw new Exception("Dapper Error:" + ex.Message);

            }
            finally
            {
                sqlClose();
            }
        }


        #region delete
        public string gorev_hareketi_sil(string hareket_id)
        {

            try
            {
                var parameters = new
                {
                    id = hareket_id,
                };
                sqlOpen();
                var gelen_id = conn.ExecuteScalar<object>("delete from gorev_hareketleri where id=uuid(@id) RETURNING id", parameters);
                return gelen_id.ToString();
            }
            catch (Exception ex)
            {

                throw new Exception("Dapper Error:" + ex.Message);

            }
            finally
            {
                sqlClose();
            }
        }
        #endregion
        #endregion
        #region Connection Open/Close Method
        private void sqlClose()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
        private void sqlOpen()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
        }
        #endregion
    }
}