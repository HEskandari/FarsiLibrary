<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="FarsiLibrary.Web.Mvc.Helpers"%>
<%@ Import Namespace="FarsiLibrary.Web.Mvc.Controls"%>
<%@ Import Namespace="FarsiLibrary.Utils"%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Demos
</asp:Content>

<asp:Content ID="scripts" ContentPlaceHolderID="HeaderScripts" runat="server">

<script language="javascript" type="text/javascript">
    $(document).ready(function() {
        $('#myCalendar').datePicker();
        
        $('#monthView').Calendar();
    });
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <h3>Displaying Dates</h3>
    <p>
        You can display dates using regular conversions of DateTime instances to PersianDates. Alternatively, you can set thread's culture to PersianCultureInfo.
        
        <i>Today Is </i>: <b><%=DateTime.Now.ToPersianDate().ToWritten()%></b>
    </p>
    
    <hr />
    
    <h3>Calendar Display</h3>
    <p>
        <%
            Html.FarsiLibrary()
                .MonthView()
                .Named("monthView2")
                .SetViewDate(DateTime.Now)
                .SetSelectedDateTime(DateTime.Now)
                .Render();
        %>
        
        <br /><br />
        
        <input type="text" class="date-picker" id="myCalendar" />
        
        <div id="monthView"></div>
        
    </p>
    
    <hr />

			
</asp:Content>
