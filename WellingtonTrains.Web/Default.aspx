<%@ Page Language="C#" Inherits="WellingtonTrains.Web.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head runat="server">
	<title>Default</title>
</head>
<body>
	<form id="form1" runat="server">
		<asp:DropDownList ID="DropDownListLines" runat="server" onchange="setupStations()" >
			<asp:ListItem Selected="True" Value="JVL">Johnsonville</asp:ListItem>
			<asp:ListItem Value="KPL">Kapiti</asp:ListItem>
			<asp:ListItem Value="HVL">Hutt Valley</asp:ListItem>
			<asp:ListItem Value="MEL">Melling</asp:ListItem>
			<asp:ListItem Value="WRL">Wairarapa</asp:ListItem>
		</asp:DropDownList>
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
	  <asp:HiddenField id="lineSelection" runat="server" />
		<asp:HiddenField id="fromSelection" runat="server" />
		<asp:HiddenField id="toSelection" runat="server" />
	</form>
	<button id="ButtonSwap" OnClick="swapDirections()">Swap Direction</button>
	<table>
		<tr>
		<th>Depart</th>
		<th>Arrive</th>
		</tr>
		<tr>
			<td><asp:Literal ID="LiteralDepart" runat="server" /></td>
			<td><asp:Literal ID="LiteralArrive" runat="server" /></td>
		</tr>
	</table>
	<script>
	  (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
	  (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
	  m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
	  })(window,document,'script','//www.google-analytics.com/analytics.js','ga');

	  ga('create', 'UA-46462711-1', 'omuch.info');
	  ga('send', 'pageview');

	</script>
	<script src="./TotesAwesJs.js"></script>
</body>
</html>
