using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace WellingtonTrains.Web
{
	public enum Day
	{
		Today,
		Monday,
		Tuesday,
		Wednesday,
		Thursday,
		Friday,
		Saturday,
		Sunday}
	;

	public class DataGetter
	{
		private List<DateTime> depart;

		public List<DateTime> Depart {
			get {
				return depart;
			}
		}

		private List<DateTime> arrive;

		public List<DateTime> Arrive {
			get {
				return arrive;
			}
		}

		private DateTime the_past;
		//		public static StringEventFirer StringEventHandler;

		public DataGetter ()
		{
//			StringEventHandler = new StringEventFirer ();

			the_past = stringToDate ("1984/09/02 12:34 pm");
			depart = new List<DateTime> ();
			arrive = new List<DateTime> ();
			GoGoGadget ();
		}

		private void GoGoGadget ()
		{
			var webClient = new WebClient ();

			string url = "http://www.metlink.org.nz/timetables/train/" + Trip.Line.Id + "/" + Trip.Direction + "/" + "?date=" + DateTime.Today.AddDays ((double)Trip.Day).ToString ("M/d/yyyy") + "&allStops=1";

			webClient.Headers ["User-Agent"] = "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
			string result = webClient.DownloadString (new Uri (url));
//				webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler (webClient_DownloadDataCompleted);
			webClient_DownloadDataCompleted (result);
		}

		void webClient_DownloadDataCompleted (String result)
		{
				
			try {
				string the_page = result;
				if (the_page.IndexOf ("This timetable is valid for ") > 0) {
					the_page = the_page.Substring (the_page.IndexOf ("This timetable is valid for "));
					the_page = the_page.Substring (the_page.IndexOf (">") + 1);
				} else if (the_page.IndexOf ("This service does not have a timetable for ") > 0) {
					the_page = the_page.Substring (the_page.IndexOf ("This service does not have a timetable for ") + "This service does not have a timetable for ".Length);
				}
				the_page = the_page.Substring (the_page.IndexOf (" ") + 1);
//					string day_string = the_page.Substring (0, the_page.IndexOf ("<")).Trim ().Trim ('.');
//					String month_string = day_string.Substring (day_string.IndexOf (" ")).Trim ();
//
				DateTime daydt = DateTime.Today.AddDays ((double)Trip.Day);

				the_page = the_page.Substring (the_page.IndexOf (@"<div id=""" + Trip.Direction));
				List<String> bfgds = getTimeTableRows (the_page);
				foreach (String timeTableRow in bfgds) {
					if (timeTableRow.IndexOf ("name=\"" + Trip.From.Id + "\"") != -1)
						depart = getTrainTimes (timeTableRow, depart, Trip.Day);
					else if (timeTableRow.IndexOf ("name=\"" + Trip.To.Id + "\"") != -1)
						arrive = getTrainTimes (timeTableRow, arrive, Trip.Day);
				}
			} catch (Exception err) {
				if ((Trip.Day == 6 || Trip.Day == 7) && Trip.Line.Id == "MEL") {
//						StringEventHandler.SetDepartTimes (day, "No trains." + err);
				}
			}

//			ParseData (depart, arrive, Trip.Day);

		}

		private List<String> getTimeTableRows (string HTML)
		{
			return new List<string> (Regex.Split (HTML, "\\n<tr.*?timing\\\">"));
		}

		private List<DateTime> getTrainTimes (string HTML, List<DateTime> list, int day)
		{
			MatchCollection herp = Regex.Matches (HTML, "\\<td.*\\/td>");
			foreach (Match derp in herp) {
				MatchCollection spans = Regex.Matches (derp.Value, "\\d{1,2}:\\d{2} (a|p)m");
				int count = list.Count;
				foreach (Match span in spans) {
					DateTime time = stringToDate (DateTime.Now.AddDays (day).ToString ("yyyy/MM/dd") + " " + span.Value);
					if (list.IndexOf (time) == -1)
						list.Add (time);
				}
				if (list.Count == count) {
					list.Add (the_past);
				}
			}
			return list;
		}

		private DateTime stringToDate (string dateAsString)
		{
			char[] delimiters = new char[] { '/', ':', ' ' };
			string[] parts = dateAsString.Split (delimiters, StringSplitOptions.RemoveEmptyEntries);
			DateTime date = new DateTime (int.Parse (parts [0]), int.Parse (parts [1]), int.Parse (parts [2]), parts [5] == "am" ? int.Parse (parts [3]) % 12 : parts [3] == "12" ? 12 : int.Parse (parts [3]) + 12, int.Parse (parts [4]), 0);
			return date;
		}

//		private void ParseData (List<DateTime> departs, List<DateTime> arrives, int day)
//		{
//			String departText = "";
//			String arriveText = "";
//			for (int i = 0; i < departs.Count; i++) {
//				if (arrives [i] != the_past && departs [i] != the_past) {
//					if ((day == 0 && departs [i].CompareTo (DateTime.Now) > 0) || (day >= 1 && day <= 7)) {
//						departText += departs [i].ToString ("h:mm tt") + "\r";
//						arriveText += arrives [i].ToString ("h:mm tt") + "\r";
//					}
//				}
//			}
//
//			if (departText == "") {
//				departText = "No trains.";
//			}

//			StringEventHandler.SetDepartTimes (day, departText);
//			StringEventHandler.SetArriveTimes (day, arriveText);
//		}
	}
}
