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
using StackExchange.Redis;

namespace yazlab2._1
{
    public partial class harita : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            ConnectionMultiplexer redisCon = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase redDb = redisCon.GetDatabase();

            int x = Convert.ToInt32(Request.QueryString["ID"].ToString());
            
            var streamReader = File.OpenText(@"C:\Users\Onur Aran\Documents\GitHub\Proje2.1\yazlab2.1\allCars4.csv");
            var csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);
            ArrayList tarih = new ArrayList();
            ArrayList lat = new ArrayList();
            ArrayList lng = new ArrayList();
            ArrayList id = new ArrayList();

            string tarihTemp, latTemp, lngTemp, idTemp;

            int counter = 0;

            while (csvReader.Read())
            {
             
                    tarih.Add(csvReader.GetField(0));
                    lat.Add(csvReader.GetField(1));
                    lng.Add(csvReader.GetField(2));
                    id.Add(csvReader.GetField(3));

                    tarihTemp = (tarih[counter]).ToString();
                    latTemp = (lat[counter]).ToString();
                    lngTemp = (lng[counter]).ToString();
                    idTemp = (id[counter]).ToString();


                    redDb.SetAdd(tarihTemp,idTemp);
                    redDb.SetAdd(tarihTemp,latTemp);
                    redDb.SetAdd(tarihTemp, lngTemp);

                    counter++;

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