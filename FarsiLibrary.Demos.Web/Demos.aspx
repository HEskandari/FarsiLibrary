<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Demos.aspx.cs" Inherits="FarsiLibrary.WebDemo.Demos" %>
<%@ Register Assembly="FarsiLibrary.Web" Namespace="FarsiLibrary.Web.Controls" TagPrefix="fl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style='width:70%; margin:0 auto'>
        <div style="font-family:Tahoma">
            <asp:Button Text="Cultures" runat="server" Width="150" ID="btnCultures" OnClick="btnCultures_Clicked" />&nbsp;
            <asp:Button Text="Styles" runat="server" Width="150" ID="btnStyles" OnClick="btnStyles_Clicked" />
            <asp:Button Text="Custom Rendering" runat="server" Width="150" ID="btnCustomRendering" OnClick="btnCustomRendering_Clicked" />
            <br />
        </div>
        
        <div style="text-align:center">
            <asp:Label runat="server" Text="Farsi Library Web Controls. All rights reserved Hightech.IR" />
        </div>
    </div>
    </form>
</body>
</html>
