using System.Collections.Generic;
using System.Xml.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WellingtonTrains.Web
{
	using System;
	using System.Web;
	using System.Web.UI;

	public partial class Default : System.Web.UI.Page
	{
		public void buttonLookupClicked(object sender, EventArgs args)
		{
			Trip.From = dataProvider.GetStationClassFromID(Request.Form ["DropDownListFrom"]);
			Trip.To = dataProvider.GetStationClassFromID(Request.Form ["DropDownListTo"]);
			Trip.Line = dataProvider.GetLineClassFromID(Request.Form ["DropDownListLines"]);
			Trip.Day = Convert.ToInt16(Request.Form ["DropDownListDay"]);

			String bookmarkURL = "./?line=" + Trip.Line.Id + "&from=" + Trip.From.Id + "&to=" + Trip.To.Id + "&day=" + Trip.Day;
			literalLink.Text = "<a href=" + bookmarkURL + " class=\"btn btn-default btn-block\" >Permanent Link to this Timetable</a>";

			lookup();
		}

		DataProvider dataProvider;

		protected void Page_Load(object sender, EventArgs e)
		{
			dataProvider = new DataProvider(XDocument.Load("Resources/xml/Lines.xml"), XDocument.Load("Resources/xml/Stations.xml"), XDocument.Load("Resources/xml/StationLines.xml"));

			lineSelection.Value = Request.Form ["DropDownListLines"];
			fromSelection.Value = Request.Form ["DropDownListFrom"];
			toSelection.Value = Request.Form ["DropDownListTo"];	
			daySelection.Value = Request.Form ["DropDownListDay"];

			if(Request.RequestType != "POST") {
				if(lineSelection.Value == "") {
					lineSelection.Value = Request.Params ["line"];
				}	
				if(toSelection.Value == "") {
					toSelection.Value = Request.Params ["to"];
				}	
				if(fromSelection.Value == "") {
					fromSelection.Value = Request.Params ["from"];
				}	
				if(Request.Params ["day"] != "") {
					daySelection.Value = Request.Params ["day"];
				}	
				if(lineSelection.Value == Request.Params ["line"] && fromSelection.Value == Request.Params ["from"] && toSelection.Value == Request.Params ["to"] && daySelection.Value == Request.Params ["day"]) {
					Trip.From = dataProvider.GetStationClassFromID(fromSelection.Value);
					Trip.To = dataProvider.GetStationClassFromID(toSelection.Value);
					Trip.Line = dataProvider.GetLineClassFromID(lineSelection.Value);
					Trip.Day = Convert.ToInt16(daySelection.Value);

					String bookmarkURL = "./?line=" + Trip.Line.Id + "&from=" + Trip.From.Id + "&to=" + Trip.To.Id + "&day=" + Trip.Day;
					literalLink.Text = "<a href=" + bookmarkURL + " class=\"btn btn-default btn-block\" >Permanent Link to this Timetable</a>";

					lookup();
				}
			}
		}

		void lookup()
		{
			DataGetter dataGetter = new DataGetter();

			List<DateTime> departs = dataGetter.Depart;
			List<DateTime> arrives = dataGetter.Arrive;

			TableTimes.Rows.Clear();
			TableHeaderRow hRow = new TableHeaderRow();
			TableTimes.Rows.Add(hRow);

			if(departs.Count == 0 || arrives.Count == 0) {
				TableHeaderCell cell = new TableHeaderCell();
				cell.Text = "No Trains Found";
				cell.CssClass = "text-center";
				hRow.Cells.Add(cell);
				hRow.CssClass = "danger";
			} else {
				TableHeaderCell departHeaderCell = new TableHeaderCell();
				departHeaderCell.Text = "Depart";
				departHeaderCell.CssClass = "text-center";
				hRow.Cells.Add(departHeaderCell);
				TableHeaderCell arriveHeaderCell = new TableHeaderCell();
				arriveHeaderCell.Text = "Arrive";
				arriveHeaderCell.CssClass = "text-center";
				hRow.Cells.Add(arriveHeaderCell);

				for (int i = 0; i < departs.Count && i < arrives.Count; i++) {
					if(Trip.Day > 0 || (Trip.Day == 0 && departs [i] > DateTime.Now)) {
						TableCell departCell = new TableCell();
						departCell.Text = departs [i].ToShortTimeString();
						departCell.CssClass = "text-center";
						TableCell arriveCell = new TableCell();
						arriveCell.Text = arrives [i].ToShortTimeString();
						arriveCell.CssClass = "text-center";
						TableRow row = new TableRow();
						row.Cells.Add(departCell);
						row.Cells.Add(arriveCell);
						TableTimes.Rows.Add(row);
					}
				}
			}
		}
	}
}
