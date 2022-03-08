using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Linq;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System.Collections;

namespace yazlab2._1
{
    public partial class harita : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(Request.QueryString["ID"].ToString());
            
            var streamReader = File.OpenText(@"C:\Kullanıcılar\Onur Aran\Masaüstü/allCars4.csv");
             var csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);
            ArrayList tarih = new ArrayList();
            ArrayList lat = new ArrayList();
            ArrayList lng = new ArrayList();
            ArrayList id = new ArrayList();
        
            while (csvReader.Read())
            {
             
                    tarih.Add(csvReader.GetField(0));
                    lat.Add(csvReader.GetField(1));
                    lng.Add(csvReader.GetField(2));
                    id.Add(csvReader.GetField(3));
                
               

            }
            int j = 0;
            int i = 0;
            for ( i= 0; i < tarih.Count; i++)
            { 
                if (id[i] == x.ToString() & j<30)
                {
                    j++;
                    break;
                   // tb.Text += tarih[i];
                }
            }
          
                tb.Text += tarih[i-1];
            

        }
    }
   
}