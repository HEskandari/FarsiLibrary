<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cultures.aspx.cs" Inherits="FarsiLibrary.WebDemo.Cultures" %>
<%@ Register Assembly="FarsiLibrary.Web" Namespace="FarsiLibrary.Web.Controls" TagPrefix="fl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family:Tahoma">
        <fl:FAMonthView ID="mv" runat="server" Width="200"
            OnSelectionChanged="mv_SelectionChanged"
            ShowGridLines="False" BorderColor="#DEDEDE"
            BackColor="#5E5E5E" ForeColor="#FFF5C7">
            <DayHeaderStyle Font-Names="Tahoma" Font-Size="Small" ForeColor="#FF8B00" BackColor="#5E5E5E" />
            <TodayDayStyle Font-Names="Tahoma" />
            <DayStyle Font-Names="Tahoma" Font-Size="Small" />
            <NextPrevStyle Font-Names="Tahoma" ForeColor="Black" />
            <OtherMonthDayStyle Font-Names="Tahoma" ForeColor="#A0A0A0" />
            <SelectedDayStyle Font-Names="Tahoma" Font-Bold="true" BackColor="#F0AA56" ForeColor="Black" />
            <TitleStyle Font-Names="Tahoma" BackColor="#C57A23" />
        </fl:FAMonthView>
        <br />
        Current Date : <asp:Label runat="server" ID="currentDate" Font-Names="Tahoma" /><br />
        Current Persian Date : <asp:Label runat="server" ID="currentPersianDate" /><br />
        <br /><br />
        <asp:Button Text="Set Invariant Culture" runat="server" Width="150" OnClick="SetThreadCulture" ID="cultureInvariant" /><br />
        <asp:Button Text="Set To Arabic Culture" runat="server" Width="150" OnClick="SetThreadCulture" ID="cultureArabic" /><br />
        <asp:Button Text="Set To Persian Culture" runat="server" Width="150" OnClick="SetThreadCulture" ID="culturePersian" /><br />
    </div>
    </form>
</body>
</html>
