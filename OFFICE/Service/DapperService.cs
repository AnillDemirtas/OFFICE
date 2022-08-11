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
                if (firma_id == null || firma_id == "")
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
        public List<Firma> Firmalar()
        {
            try
            {

                sqlOpen();
                List<Firma> sinifList = conn.Query<Firma>("select id::text as id, unvan,yetkili,gsm from firmalar").ToList();
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
        public List<Users> users()
        {
            try
            {

                sqlOpen();
                List<Users> sinifList = conn.Query<Users>("select ad from users").ToList();
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
                List<Firma> sinifList = conn.Query<Firma>("select id::text as id, unvan,yetkili,gsm from firmalar where id=uuid(@id)", parameters).ToList();
            
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