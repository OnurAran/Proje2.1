﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System.Collections;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;
using RabbitMQ.Client.Events;
using StackExchange.Redis;

namespace yazlab2._1
{
    public partial class harita : System.Web.UI.Page
    {

        string tarihTemp, latTemp, lngTemp, idTemp, saatTemp;
        int sure = 30;
        int indexTut;
        string girisSaati = DateTime.Now.ToString();
        string girisSaati_son;
        string[] girisSaati_saatDakika;

        ArrayList tarihGirisSaati = new ArrayList();


        bool guvenlik = false;
        ArrayList tarihRedis = new ArrayList();
        ArrayList latRedis = new ArrayList();
        string tutSaat;

        protected void Button30_Click(object sender, EventArgs e)
        {
            sure = Convert.ToInt32(tb.Text);




            for (int i = 0; i < sure; i++)
            {
                if (Convert.ToInt32(girisSaati_saatDakika[1]) < 10)
                {
                    girisSaati_saatDakika[1] = ("0"+girisSaati_saatDakika[1]).ToString();
                }

                girisSaati_son = girisSaati_saatDakika[0] + ":" + girisSaati_saatDakika[1];
                tarihGirisSaati.Add(girisSaati_son);

                girisSaati_saatDakika[1] = (Convert.ToInt32(girisSaati_saatDakika[1]) + -1).ToString();//dakika azaltma

                if (Convert.ToInt32(girisSaati_saatDakika[1]) <= 0)
                {
                    girisSaati_saatDakika[1] = (Convert.ToInt32(girisSaati_saatDakika[1]) + 59).ToString();
                    girisSaati_saatDakika[0] = (Convert.ToInt32(girisSaati_saatDakika[0]) - 1).ToString();//saat azaltma
                }


            }
            for (int i = 0; i < sure; i++)
            {
                //Response.Write(tarihGirisSaati[i]+"****"+i+"****");
            }


            Sayfa();
        }


        protected void Page_Load()
        {
            string[] girisSaati_tarihSaat = girisSaati.Split(' ');
            girisSaati_saatDakika = girisSaati_tarihSaat[1].Split(':');

            
                

        }

        ArrayList lngRedis = new ArrayList();
        ArrayList idRedis = new ArrayList();
        protected void Sayfa()
        {



            //connection redis//
            ConnectionMultiplexer redisCon = ConnectionMultiplexer.Connect("localhost:6379,allowAdmin=true");
            var server = redisCon.GetServer("localhost:6379");
            IDatabase redDb = redisCon.GetDatabase();
            server.FlushDatabase();
            //connection redis//


            int x = Convert.ToInt32(Request.QueryString["ID"].ToString());

            var streamReader = File.OpenText(@"C:\Users\Onur Aran\Documents\GitHub\Proje2.1\yazlab2.1\allCars.csv");
            var csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);
            ArrayList tarih = new ArrayList();
            ArrayList lat = new ArrayList();
            ArrayList lng = new ArrayList();
            ArrayList id = new ArrayList();

            csv arac = new csv();
            int counter = 0;
            while (csvReader.Read())
            {

                arac.tarih.Add(csvReader.GetField(0));
                arac.lat.Add(csvReader.GetField(1));
                arac.lng.Add(csvReader.GetField(2));
                arac.id.Add(csvReader.GetField(3));


                //redis
                tarihTemp = (arac.tarih[counter]).ToString();
                latTemp = (arac.lat[counter]).ToString();
                lngTemp = (arac.lng[counter]).ToString();
                idTemp = (arac.id[counter]).ToString();

                saatTemp = tarihTemp;

                string[] date = saatTemp.Split(' ');

                string[] saat = date[1].Split(':');

                int[] clock = Array.ConvertAll<string, int>(saat, int.Parse);


                redDb.ListRightPush("title", tarihTemp);
                redDb.ListRightPush("title", latTemp);
                redDb.ListRightPush("title", lngTemp);
                redDb.ListRightPush("title", idTemp);
                // redis
                counter++;


            }
            int j = 0;
            int i = 0;
            for (i = 0; i < arac.tarih.Count; i++)
            {
                if (arac.id[i] == x.ToString() & j < 30)
                {
                    j++;
                    break;
                }
            }
            csv a = new csv();



            a.id.Add(arac.id[i - 1]);
            a.tarih.Add(arac.tarih[i - 1]);
            a.lat.Add(arac.lat[i - 1]);
            a.lng.Add(arac.lng[i - 1]);
            a.id.Add(arac.id[i - 2]);
            a.tarih.Add(arac.tarih[i - 2]);
            a.lat.Add(arac.lat[i - 2]);
            a.lng.Add(arac.lng[i - 2]);
            tb.Text = "" + a.id[0];
            string z = "";


            /*
            //rabbitmq 

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "arac",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );
                var message = JsonConvert.SerializeObject(a);
                var body = Encoding.UTF8.GetBytes(message);
                var consumer = new EventingBasicConsumer(channel);
                channel.BasicPublish(exchange: "",
                                     routingKey: "arac",
                                     basicProperties: null,
                                     body: body);


            }
            var factory1 = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
            };

            using (IConnection connection = factory1.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "arac",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, mq) =>
                 {
                     var message = JsonConvert.SerializeObject(arac);
                     var mesaj = Encoding.UTF8.GetBytes(message);
                     csv email = JsonConvert.DeserializeObject<csv>(message);
                     Console.WriteLine($"Email adresi kuyruktan alındı.Email Adı: {email.tarih[1]}");
                     tb.Text = "dfasd" + email.tarih[0];

                 };
                channel.CallbackException += (chann, args) =>
                {
                    var message = JsonConvert.SerializeObject(arac);
                    var mesaj = Encoding.UTF8.GetBytes(message);
                    csv email = JsonConvert.DeserializeObject<csv>(message);
                    Console.WriteLine($"Email adresi kuyruktan alındı.Email Adı: {email.tarih[1]}");
                    tb.Text = "dfasd" + email.tarih[0];

                };
                channel.BasicConsume(queue: "arac",
                                     autoAck: true,//true ise mesaj otomatik olarak kuyruktan silinir
                                     consumer: consumer);


            }
            tb.Text = z;
            //  rabbitmq
            */
            // redis
            for (int o = 0; o < 3867; o = o + 4)//tarihin son indexini bulup sonraki for'a gönderiyor
                                                //yukarıdan aşağıya doğru bakmak için
            {
                if (redDb.ListGetByIndex("title", o) == tarihTemp)
                {
                    indexTut = o;
                }
            }
            //  if (sure == 30)
            //{
            for (int c = 0; c < 3867; c++)
            {
                for(int k = 0; k < tarihGirisSaati.Count; k++) 
                {
                    tutSaat = redDb.ListGetByIndex("title", indexTut);
                    Response.Write(tutSaat);
                    string[] RedisTarih = tutSaat.Split(' ');
                    
                    if (RedisTarih[1].Equals(tarihGirisSaati[k]))
                    {
                        tarihRedis.Add(redDb.ListGetByIndex("title", indexTut));
                        indexTut++;
                        latRedis.Add(redDb.ListGetByIndex("title", indexTut));
                        indexTut++;
                        lngRedis.Add(redDb.ListGetByIndex("title", indexTut));
                        indexTut++;
                        idRedis.Add(redDb.ListGetByIndex("title", indexTut));

                        indexTut -= 7;
                    }
                    else
                    {
                        indexTut -= 4;
                    }
                    
                }
                

                

            }
            //}
            /*   else if (sure == 60)
               {
                   for (int c = 0; c < sure; c++)
                   {

                       tarihRedis.Add(redDb.ListGetByIndex("title", indexTut));
                       indexTut++;
                       latRedis.Add(redDb.ListGetByIndex("title", indexTut));
                       indexTut++;
                       lngRedis.Add(redDb.ListGetByIndex("title", indexTut));
                       indexTut++;
                       idRedis.Add(redDb.ListGetByIndex("title", indexTut));

                       indexTut -= 7;

                   }
               }
            */


            for (int c = 0; c < tarihRedis.Count; c++)
            {
                Response.Write("__("+tarihGirisSaati[c]+")__");
                Response.Write(lngRedis[c] + "*" + c + "*");
                Console.WriteLine("\n");
            }
            redisCon.Close();
            // //redis
            for (int o = 0; o < latRedis.Count; o++)
            {
                HiddenField1.Value += latRedis[o] + " ";
                HiddenField2.Value += lngRedis[o] + " ";
            }

        }
        protected void Button1_Click(object sender, EventArgs e)
        {/*
            int x = Convert.ToInt32(Request.QueryString["ID"].ToString());

            DataSet1TableAdapters.TBLUSERTableAdapter a = new DataSet1TableAdapters.TBLUSERTableAdapter();
            DataSet1TableAdapters.aracTableAdapter b = new DataSet1TableAdapters.aracTableAdapter();
            int y = b.GetDataBy1(x)[0].id;
            var zaman = DateTime.Now.ToString();
            a.UpdateQuery(zaman, y);
            Response.Redirect("WebForm1.Aspx?");*/
        }

    }
    public class csv
    {
        public ArrayList tarih = new ArrayList();
        public ArrayList lat = new ArrayList();
        public ArrayList lng = new ArrayList();
        public ArrayList id = new ArrayList();
    }
}