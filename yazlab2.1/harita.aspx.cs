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
using System.Drawing;
using GoogleMaps.Markers;
using GoogleMaps.Overlays;
using GoogleMaps;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace yazlab2._1
{
    public partial class harita : System.Web.UI.Page
    {
        string tarihTemp, latTemp, lngTemp, idTemp;
        int sure = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button30_Click(object sender, EventArgs e)
        {
            sure = 30;
            Sayfa();
        }

        protected void Button60_Click(object sender, EventArgs e)
        {
            sure = 60;
            Sayfa();
        }


        protected void Sayfa()
        {

            ConnectionMultiplexer redisCon = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase redDb = redisCon.GetDatabase();

            int x = Convert.ToInt32(Request.QueryString["ID"].ToString());

            var streamReader = File.OpenText(@"C:\Users\Onur Aran\Documents\GitHub\Proje2.1\yazlab2.1\allCars7.csv");
            var csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);
            ArrayList tarih = new ArrayList();
            ArrayList lat = new ArrayList();
            ArrayList lng = new ArrayList();
            ArrayList id = new ArrayList();

            var girisSaati = DateTime.Now;



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


                for(int o =0; o<sure;o++)
                {
                    if(tarihTemp== "2018-10-06 " +)
                    {

                    }
                }


                redDb.ListRightPush(tarihTemp, latTemp);
                redDb.ListRightPush(tarihTemp, lngTemp);
                redDb.ListRightPush(tarihTemp, idTemp);


                Console.WriteLine(redDb.ListGetByIndex("2018-10-15 13:15", 2));
                Console.WriteLine(redDb.ListLength("2018-10-15 13:15"));

                var tut = redDb.ListGetByIndex("2018-10-15 13:15", 2);
                redDb.StringSet("key1", tut);
                redDb.StringGet("key1");
                /*
                    tarihTemp = (tarih[counter]).ToString();
                    latTemp = (lat[counter]).ToString();
                    lngTemp = (lng[counter]).ToString();
                    idTemp = (id[counter]).ToString();


                    redDb.SetAdd(tarihTemp,idTemp);
                    redDb.SetAdd(tarihTemp,latTemp);
                    redDb.SetAdd(tarihTemp, lngTemp);*/

                counter++;

            }
            int j = 0;
            int i = 0;
            for (i = 0; i < tarih.Count; i++)
            {
                if (id[i] == x.ToString() & j < 30)
                {
                    j++;
                    break;
                    // tb.Text += tarih[i];
                }
            }

            tb.Text += tarih[i - 1];

            redisCon.Close();


            markerEkle();






        }

        private void markerEkle()
        {
            GMapOverlay markersOverlay = new GMapOverlay("markers");
            GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(-25.966688, 32.580528), GMarkerGoogleType.green);
            markersOverlay.Markers.Add(marker);

        }




    }

}