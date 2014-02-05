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

		public DataGetter()
		{
			depart = new List<DateTime>();
			arrive = new List<DateTime>();
			GoGoGadget();
		}

		private void GoGoGadget()
		{
			var webClient = new WebClient();

			string url = "http://www.metlink.org.nz/timetables/train/" + Trip.Line.Id + "/" + Trip.Direction + "/" + "?date=" + DateTime.Today.AddDays((double)Trip.Day).ToString("M/d/yyyy") + "&allStops=1";

			webClient.Headers ["User-Agent"] = "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
			string result = webClient.DownloadString(new Uri(url));
			webClient_DownloadDataCompleted(result);
		}

		void webClient_DownloadDataCompleted(String result)
		{
			string the_page = result;
			if(the_page.IndexOf("td class=\"stop") > 0) {
				the_page = the_page.Substring(the_page.IndexOf("td class=\"stop"));
			} else if(the_page.IndexOf("This service does not have a timetable for ") > 0) {
				the_page = the_page.Substring(the_page.IndexOf("This service does not have a timetable for ") + "This service does not have a timetable for ".Length);
			}

			// hack off the footer div
			int index = the_page.LastIndexOf(@"<div id=");
			the_page = the_page.Substring(0, index - 8);

			List<String> bfgds = getTimeTableRows(the_page);
			List<String> departs = new List<String>();
			List<String> arrives = new List<String>();
			foreach (String timeTableRow in bfgds) {
				if(timeTableRow.IndexOf("name=\"" + Trip.From.Id + "\"") != -1)
					departs.AddRange(getTableCells(timeTableRow));
				else if(timeTableRow.IndexOf("name=\"" + Trip.To.Id + "\"") != -1)
					arrives.AddRange(getTableCells(timeTableRow));
			}

			getTrainTimes(departs);
			getTrainTimes(arrives);

			for (int i = 0; i < arrives.Count; i++) {
				if(arrives [i] != "" && departs [i] != "") {
					depart.Add(stringToDate(departs [i]));
					arrive.Add(stringToDate(arrives [i]));
				}
			}

			depart.Sort();
			arrive.Sort();
		}

		private List<String> getTimeTableRows(string HTML)
		{
			return new List<string>(Regex.Split(HTML, "<tr.*?timing"));
		}

		private List<String> getTableCells(string HTML)
		{
			List<String> list = new List<string>();
			MatchCollection herp = Regex.Matches(HTML, "\\<td.*?\\/td>");
			foreach (Match derp in herp) {
				list.Add(derp.Value);
			}
			return list;
		}

		private List<String> getTrainTimes(List<String> list)
		{
			for (int i = 0; i < list.Count; i++) {
				MatchCollection spans = Regex.Matches(list [i], "\\d{1,2}:\\d{2} (a|p)m");
				list [i] = "";
				foreach (Match span in spans) {
					list [i] += DateTime.Now.AddDays(Trip.Day).ToString("yyyy/MM/dd") + " " + span.Value;
				}
			}
			return list;
		}

		private DateTime stringToDate(string dateAsString)
		{
			System.Diagnostics.Debug.Print("dateAsString = '" + dateAsString + "'");
			char[] delimiters = new char[] { '/', ':', ' ' };
			string[] parts = dateAsString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
			DateTime date = new DateTime(int.Parse(parts [0]), int.Parse(parts [1]), int.Parse(parts [2]), parts [5] == "am" ? int.Parse(parts [3]) % 12 : parts [3] == "12" ? 12 : int.Parse(parts [3]) + 12, int.Parse(parts [4]), 0);
			return date;
		}
	}
}
