<%@ Page Language="C#" Inherits="WellingtonTrains.Web.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head runat="server">
	<title>Choo Choo</title>
	<link href="content/bootstrap.css" rel="stylesheet" type="text/css" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
</head>
<body>
	<div class="container">
		<h2 title="Wellington Trains">Wellington Trains</h2>
		<form id="form1" runat="server">
			<div class="form-group">
				<asp:Label id="lblLine" runat="server" AssociatedControlID="DropDownListLines" CssClass="control-label">Line</asp:Label>
				<asp:DropDownList ID="DropDownListLines" runat="server" onchange="setupStations()" CssClass="form-control">
					<asp:ListItem Selected="True" Value="JVL">Johnsonville</asp:ListItem>
					<asp:ListItem Value="KPL">Kapiti</asp:ListItem>
					<asp:ListItem Value="HVL">Hutt Valley</asp:ListItem>
					<asp:ListItem Value="MEL">Melling</asp:ListItem>
					<asp:ListItem Value="WRL">Wairarapa</asp:ListItem>
				</asp:DropDownList>
			</div>
			<div class="form-group">
				<asp:Label id="lblFrom" runat="server" AssociatedControlID="DropDownListFrom" CssClass="control-label">From</asp:Label>
				<asp:DropDownList ID="DropDownListFrom" runat="server" CssClass="form-control" />
			</div>
			<div class="form-group">
				<asp:Label id="lblTo" runat="server" AssociatedControlID="DropDownListTo" CssClass="control-label">To</asp:Label>
				<asp:DropDownList ID="DropDownListTo" runat="server" CssClass="form-control" />
			</div>
			<div class="form-group">
				<asp:Label id="lblDay" runat="server" AssociatedControlID="DropDownListDay" CssClass="control-label">Day</asp:Label>
				<asp:DropDownList ID="DropDownListDay" runat="server" CssClass="form-control">
					<asp:ListItem Selected="True" Value="0">Today</asp:ListItem>
					<asp:ListItem Value="1">Monday</asp:ListItem>
					<asp:ListItem Value="2">Tuesday</asp:ListItem>
					<asp:ListItem Value="3">Wednesday</asp:ListItem>
					<asp:ListItem Value="4">Thursday</asp:ListItem>
					<asp:ListItem Value="5">Friday</asp:ListItem>
					<asp:ListItem Value="6">Saturday</asp:ListItem>
					<asp:ListItem Value="7">Sunday</asp:ListItem>
				</asp:DropDownList>
			</div>
			<div class="form-group">
				<button id="ButtonSwap" OnClick="swapDirections()" type="button" class="btn btn-default btn-block">Swap Direction <span class="glyphicon glyphicon-random"></span></button>
				<asp:LinkButton ID="ButtonLookup" runat="server" OnClick="buttonLookupClicked" CssClass="btn btn-primary btn-block">Find Trains <span class="glyphicon glyphicon-search"></span></asp:LinkButton>
			  <asp:Literal id="literalLink" runat="server" />
			</div>
			<asp:HiddenField id="lineSelection" runat="server" />
			<asp:HiddenField id="fromSelection" runat="server" />
			<asp:HiddenField id="toSelection" runat="server" />
			<asp:HiddenField id="daySelection" runat="server" />
			<div class="form-group">
				<asp:Table runat="server" id="TableTimes" class="table table-condensed table-hover" />
			</div>
		</form>
	</div>
	<script src="Scripts/jquery-2.0.3.min.js"></script>
	<script src="Scripts/bootstrap.min.js"></script>
	<script>
	  (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
	  (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
	  m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
	  })(window,document,'script','//www.google-analytics.com/analytics.js','ga');

	  ga('create', 'UA-46462711-1', 'omuch.info');
	  ga('send', 'pageview');

	</script>
	<script src="Scripts/TotesAwesJs.js"></script>
</body>
</html>
