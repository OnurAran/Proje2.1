using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
namespace yazlab2._1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-T43E7P1;Initial Catalog=yazlab2.1;Integrated Security=True");
            string x = TextBox1.Text;
            baglanti.Open();
            SqlCommand komut =new  SqlCommand("Select * from TBLUSER where KULLANICI=@P1 AND SIFRE =@P2", baglanti);
            komut.Parameters.AddWithValue("@P1",TextBox1.Text);
            komut.Parameters.AddWithValue("@P2", TextBox2.Text);
            string kullanici = TextBox1.Text;
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                
                DataSet1TableAdapters.TBLUSERTableAdapter dt = new DataSet1TableAdapters.TBLUSERTableAdapter();
                DataSet1TableAdapters.aracTableAdapter d = new DataSet1TableAdapters.aracTableAdapter();

                int id = dt.GetData(kullanici)[0].ID;

                DataSet1TableAdapters.TBLUSERTableAdapter a = new DataSet1TableAdapters.TBLUSERTableAdapter();
                var zaman = DateTime.Now.ToString();
                a.update(zaman, kullanici);

                Response.Redirect("Veriler.Aspx?ad="+x);
            }
            else
            {

                Response.Write("Hatalı giriş");
            }
            baglanti.Close();   
        }
    }
}