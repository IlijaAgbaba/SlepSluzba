using RsokProject.Models;
using RsokProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RsokProject.DataLayer
{
    public class Baza
    {
        //Izlistavanje svih komentara
        public List<Komentar> sviKomentari
        {
            get
            {
                List<Komentar> komentari = new List<Komentar>();
                //Konekcija sa bazom
                string cs = "data source=ILIJAAGBABA\\SQLEXPRESS; database = Accounts; integrated security=SSPI";
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from komentari_tbl", con);
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Komentar f = new Komentar();
                            f.id = Convert.ToInt32(rdr["id"]);
                            f.ime = rdr["ime"].ToString();
                            f.tekstKomentara = rdr["tekstKomentara"].ToString();
                            f.datum = (DateTime)rdr["datum"];

                            komentari.Add(f);
                        }
                    }
                }
                return komentari;
            }
        }

        //Upisivanje komentara u bazu
        public void upisiKomentar(Komentar komentar)
        {
            string cs = "data source=ILIJAAGBABA\\SQLEXPRESS; database = Accounts; integrated security=SSPI";
            
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string stringKomentara = "Insert into komentari_tbl values('"+ komentar.ime + "','" + komentar.tekstKomentara + "','" + komentar.datum + "')";
                SqlCommand cmd = new SqlCommand(stringKomentara, con);
                cmd.ExecuteNonQuery();
            }
        }
    }
}