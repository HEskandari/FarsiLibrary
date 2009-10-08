<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Styles.aspx.cs" Inherits="FarsiLibrary.WebDemo.Styles" %>
<%@ Register Assembly="FarsiLibrary.Web" Namespace="FarsiLibrary.Web.Controls" TagPrefix="fl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Table runat="server" CellPadding="5" CellSpacing="5" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell><fl:FAMonthView ID="mv1" runat="server" Width="220" Height="180" DayNameFormat="FirstLetter" SelectionMode="Day" SelectedDate="1/15/2009" VisibleDate="1/1/2009" Caption="Styled Calendar" OtherMonthDayStyle-ForeColor="#999999" SelectorStyle-BorderStyle="NotSet" SelectedDayStyle-BackColor="#99CCFF" SelectedDayStyle-BorderStyle="NotSet" SelectedDayStyle-BorderColor="#0066FF" SelectedDayStyle-ForeColor="#003399" ShowNextPrevMonth="False" ShowTitle="True" TitleFormat="MonthYear" BorderWidth="1" BorderColor="#0099FF" DayHeaderStyle-BackColor="#7A9BDE" DayHeaderStyle-ForeColor="#00376F" NextPrevStyle-BackColor="#7A9BDE" NextPrevStyle-ForeColor="#00376F" TitleStyle-BackColor="#7A9BDE" TitleStyle-ForeColor="#00376F" /></asp:TableCell>
                <asp:TableCell><fl:FAMonthView ID="mv2" runat="server" Width="220" Height="180" Caption="Default Style" /></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
