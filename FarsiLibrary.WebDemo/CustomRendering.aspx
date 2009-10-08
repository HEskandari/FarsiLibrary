<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomRendering.aspx.cs" Inherits="FarsiLibrary.WebDemo.CustomRendering" %>
<%@ Register Assembly="FarsiLibrary.Web" Namespace="FarsiLibrary.Web.Controls" TagPrefix="fl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 0 auto">
        First day of Farvardin is rendered as Holiday (in Red).<br />
        <fl:FAMonthView ID="mv" runat="server" Width="220" Height="180" OnRenderCalendarCell="mv_RenderCalendarCell"  />
    </div>
    </form>
</body>
</html>
