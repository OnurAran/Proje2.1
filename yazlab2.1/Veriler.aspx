﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Veriler.aspx.cs" Inherits="yazlab2._1.Veriler" %>

<html>

<head>
    <title>Anasayfa</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />

    

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
<body >

    <ul>
        <li><a href="#home">Anasayfa</a></li>
        <li style="float: right"><a class="active" href="WebForm1.aspx">Çıkış yap</a></li>
    </ul>
    <form runat="server">
        <div style="margin-top: 15px">

            <table class="table table-bordered">

                <tr>


                    <th>Kullanıcı</th>
                    <th>Araç ID</th>

                </tr>

                <asp:Repeater ID="Repeater2" runat="server">
                    <ItemTemplate>
                        <tr>

                            <td><%# Eval("id") %></td>
                            <td><%# Eval("aracid") %></td>
                            <td>
                                <asp:HyperLink NavigateUrl='<%# "harita.aspx?ID="+Eval("ARACID") %>' ID="HyperLink2" runat="server" class="btn btn-success">Güncelle</asp:HyperLink></td>

                            </td>  
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>



            </table>

        </div>
    </form>


</body>
</html>
