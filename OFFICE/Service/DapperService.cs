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