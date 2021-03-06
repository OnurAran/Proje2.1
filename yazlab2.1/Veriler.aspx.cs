using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using StackExchange.Redis;

using System.Drawing;
using System.Threading.Tasks;

namespace yazlab2._1
{
    public partial class Veriler : System.Web.UI.Page
    {



        protected void Page_Load(object sender, EventArgs e)
        {
            string username = Request.QueryString["ad"].ToString();
            DataSet1TableAdapters.TBLUSERTableAdapter dt = new DataSet1TableAdapters.TBLUSERTableAdapter();
            DataSet1TableAdapters.aracTableAdapter d = new DataSet1TableAdapters.aracTableAdapter();

            int id=dt.GetData(username)[0].ID;   

            Repeater2.DataSource = d.GetDataBy(id);
            Repeater2.DataBind();
            DataSet1TableAdapters.TBLUSERTableAdapter a = new DataSet1TableAdapters.TBLUSERTableAdapter();
            var zaman=DateTime.Now.ToString();
            a.update(zaman, username);




            
            /*
            ConnectionMultiplexer redisCon = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase redDb = redisCon.GetDatabase();

            redDb.StringSet("onur","aran");

            redDb.StringSet(username, zaman);

            redisCon.Close();


            /*
            redDb.StringSet("foo", "bar");

            String val = redDb.StringGet("foo");

            Console.WriteLine("output is {0}", val);
            Console.ReadKey();


            /*

            ConnectionMultiplexer redisCon = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase redDb = redisCon.GetDatabase();
            redDb.StringSet("key1", "value1");
            Console.WriteLine(redDb.StringGet("key1"));
            Console.Read();
            */



        }

        
    }
}