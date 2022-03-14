<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="harita.aspx.cs" Inherits="yazlab2._1.harita" %>

<!DOCTYPE html>
<html>
<head>
    <script type="text/javascript"
        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDVtwWGpVRzdYxMC3k9HxDsgsrDNCS4kog&sensor=false">
    </script>
    <script type="text/javascript">
        function initialize() {
            var myLatlng = new google.maps.LatLng(-34.397, 150.644)
            var mapOptions = {
                center: myLatlng,
                zoom: 8,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                marker: true
            };
            var map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
        }
    </script>


<style>

ul {
  list-style-type: none;
  margin: 0;
  padding: 0;
  overflow: hidden;
  background-color: #333;
}

li {
  float: left;
}

li a {
  display: block;
  color: white;
  text-align: center;
  padding: 14px 16px;
  text-decoration: none;
}

li a:hover:not(.active) {
  background-color: #111;
}

.active {
  background-color: #04AA6D;
}
</style>
</head>
<body onload="initialize()">

<ul>
  <li><a href="#home">Anasayfa</a></li>
  <li style="float:right"><a class="active" href="WebForm1.aspx">Çıkış yap</a></li>
</ul>

    <form id="form1" runat="server">
     <asp:TextBox Style="margin:15px" ID="tb" runat="server"></asp:TextBox>
        <asp:Button ID="Button30" runat="server" OnClick="Button30_Click" Text="30 Dakika" />
        <asp:Button ID="Button60" runat="server" OnClick="Button60_Click" Text="60 Dakika" />
     <div id="map_canvas" style="width: 700px; height: 500px"></div>
    </form>

    


</body>
</html>
