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

			lookup();
		}

		DataProvider dataProvider;

		protected void Page_Load(object sender, EventArgs e)
		{
			dataProvider = new DataProvider(XDocument.Load("Resources/xml/Lines.xml"), XDocument.Load("Resources/xml/Stations.xml"), XDocument.Load("Resources/xml/StationLines.xml"));

			lineSelection.Value = Request.Form ["DropDownListLines"];
			fromSelection.Value = Request.Form ["DropDownListFrom"];
			toSelection.Value = Request.Form ["DropDownListTo"];			
		}

		void lookup()
		{
			DataGetter dataGetter = new DataGetter();

			List<DateTime> departs = dataGetter.Depart;
			List<DateTime> arrives = dataGetter.Arrive;

			if(departs.Count == 0 || arrives.Count == 0) {
				LiteralDepart.Text = "No Trains Found";
				LiteralArrive.Text = "";
			} else {
				LiteralDepart.Text = "";
				LiteralArrive.Text = "";

				for (int i = 0; i < departs.Count && i < arrives.Count; i++) {
					if(Trip.Day > 0 || (Trip.Day == 0 && departs [i] > DateTime.Now)) {
						LiteralDepart.Text += departs [i].ToShortTimeString() + "\n<br/>";
						LiteralArrive.Text += arrives [i].ToShortTimeString() + "\n<br/>";
					}
				}
			}
		}
	}
}
