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
		public void button1Clicked (object sender, EventArgs args)
		{
			button1.Text = "You clicked me";
		}

		public void buttonLookupClicked (object sender, EventArgs args)
		{
//			TextBox1.Text = DropDownListFrom.SelectedItem.Value;
			lookup ();
		}

		public void buttonSwapClicked (object sender, EventArgs args)
		{
//			swapDirection ();
			ButtonSwap.Text = "trololol not working yet";
		}

		DataProvider dataProvider;
		private bool swapping = false;

		protected void Page_Load (object sender, EventArgs e)
		{
            dataProvider = new DataProvider(XDocument.Load("Resources/xml/Lines.xml"), XDocument.Load("Resources/xml/Stations.xml"), XDocument.Load("Resources/xml/StationLines.xml"));

			SetUpLine ();
			SetUpStationFrom ();
			SetUpStationTo ();
		}

		private void SetUpLine ()
		{		
			List<LineClass> lines = (List<LineClass>)dataProvider.GetLines ();

			this.DropDownListLines.Items.Clear ();

			foreach (LineClass line in lines)
				this.DropDownListLines.Items.Add (new ListItem (line.Name, line.Id));

			this.DropDownListLines.DataBind ();       
//
//			if(loading)
			DropDownListFrom.SelectedIndexChanged += new EventHandler (dropDownListLine_ItemSelected);
		}

		private void SetUpStationFrom ()
		{		
			List<StationClass> stations = (List<StationClass>)dataProvider.GetStationsForLine (DropDownListLines.SelectedItem.Text);

			this.DropDownListFrom.Items.Clear ();

			foreach (StationClass station in stations)
				this.DropDownListFrom.Items.Add (new ListItem (station.Name, station.Id));

			this.DropDownListFrom.DataBind ();



			DropDownListFrom.SelectedIndexChanged += new EventHandler (dropDownListFrom_ItemSelected);
		}

		private void SetUpStationTo ()
		{
//			List<StationClass> stations = (List<StationClass>)dataProvider.GetOtherStationsForLine (DropDownListLines.SelectedItem.Text, DropDownListFrom.SelectedItem.Text);
			List<StationClass> stations = (List<StationClass>)dataProvider.GetStationsForLine (DropDownListLines.SelectedItem.Text);

			this.DropDownListTo.Items.Clear ();

			foreach (StationClass station in stations)
				this.DropDownListTo.Items.Add (new ListItem (station.Name, station.Id));

			this.DropDownListTo.SelectedIndex += 1;

			this.DropDownListTo.DataBind ();

		}

		void dropDownListLine_ItemSelected (object sender, EventArgs e)
		{
			if (!swapping)
				SetUpStationFrom ();
		}

		void dropDownListFrom_ItemSelected (object sender, EventArgs e)
		{
			if (!swapping)
				SetUpStationTo ();
			swapping = false;
		}

		void swapDirection ()
		{
			swapping = true;
			int to = DropDownListFrom.SelectedIndex;
			int from = DropDownListTo.SelectedIndex;

			DropDownList temp = new DropDownList ();
			temp.DataSource = DropDownListFrom.DataSource;
			DropDownListFrom.DataSource = DropDownListTo.DataSource;
			DropDownListTo.DataSource = temp.DataSource;

			temp = DropDownListFrom;
			DropDownListFrom = DropDownListTo;
			DropDownListTo = temp;

			DropDownListFrom.SelectedIndex = from;
			DropDownListTo.SelectedIndex = to;
			DropDownListFrom.DataBind ();
			DropDownListTo.DataBind ();

		}

		void lookup ()
		{

			Trip.Line = dataProvider.GetLineClassFromName (DropDownListLines.SelectedItem.ToString ());
			Trip.From = dataProvider.GetStationClassFromName (DropDownListFrom.SelectedItem.ToString ());
			Trip.To = dataProvider.GetStationClassFromName (DropDownListTo.SelectedItem.ToString ());

			DataGetter dataGetter = new DataGetter ();
//				RunOnUiThread(delegate { 
//					StartActivity(typeof(TimetableList));
//				});
			Literal1.Text += dataGetter.Depart.Count;
			List<DateTime> departs = dataGetter.Depart;
			List<DateTime> arrives = dataGetter.Arrive;
			for (int i = 0; i < departs.Count; i++) {
				Literal1.Text += departs[i] + " " + arrives[i] + "<br/>";
			}
		}
	}
}

