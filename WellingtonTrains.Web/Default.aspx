<%@ Page Language="C#" Inherits="WellingtonTrains.Web.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head runat="server">
	<title>Default</title>
</head>
<body>
	<form id="form1" runat="server">
		<asp:DropDownList ID="DropDownListLines" runat="server" />
		<br/>
		<asp:DropDownList ID="DropDownListFrom" runat="server" />
		<br/>
		<asp:DropDownList ID="DropDownListTo" runat="server" />
		<br/>
		<asp:DropDownList ID="DropDownListDay" runat="server" >
			<asp:ListItem Selected="True" Value="0">Today</asp:ListItem>
			<asp:ListItem Value="1">Monday</asp:ListItem>
			<asp:ListItem Value="2">Tuesday</asp:ListItem>
			<asp:ListItem Value="3">Wednesday</asp:ListItem>
			<asp:ListItem Value="4">Thursday</asp:ListItem>
			<asp:ListItem Value="5">Friday</asp:ListItem>
			<asp:ListItem Value="6">Saturday</asp:ListItem>
			<asp:ListItem Value="7">Sunday</asp:ListItem>
		</asp:DropDownList>
		<br/>
		<asp:Button ID="ButtonLookup" runat="server" Text="Find Trains" OnClick="buttonLookupClicked" />
		<br/>
		<asp:Button id="ButtonSwap" runat="server" Text="Swap Direction" OnClick="buttonSwapClicked" />
		<br/>
		<asp:Literal ID="LiteralTimeTable" runat="server" />
	</form>
</body>
</html>
