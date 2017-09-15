using System;
using System.Text;

namespace M.Utilities.Providers {
	public class MDateFormatsProvider {
		public static string GetUkrMonth(DateTime postingTime) {
			string month = "";
			switch (postingTime.Month) {
				case 1: { month = "Січня"; break; }
				case 2: { month = "Лютого"; break; }
				case 3: { month = "Березня"; break; }
				case 4: { month = "Квітня"; break; }
				case 5: { month = "Травня"; break; }
				case 6: { month = "Червня"; break; }
				case 7: { month = "Липня"; break; }
				case 8: { month = "Серпня"; break; }
				case 9: { month = "Вересня"; break; }
				case 10: { month = "Жовтня"; break; }
				case 11: { month = "Листопада"; break; }
				case 12: { month = "Грудня"; break; }
			}

			return month;
		}

		public static string GetUkrTime(DateTime postingTime) {
			StringBuilder sb = new StringBuilder();
			if (postingTime.Hour > 9) {
				sb.Append($"{postingTime.Hour}");
			} else {
				sb.Append($"0{postingTime.Hour}");
			}
			if (postingTime.Minute > 9) {
				sb.Append($":{postingTime.Minute}");
			} else {
				sb.Append($":0{postingTime.Minute}");
			}

			return sb.ToString();
		}
	}
}
