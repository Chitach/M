using System;
using System.Text;

namespace M.Models {
	public class Post {
		#region private
		#endregion

		#region Public properties
		public int Id { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public string ImageUrl { get; set; }
		public DateTime PostingTime { get; set; }
		#endregion

		public string GetPostingMonth() {
			string month = "";
			switch (PostingTime.Month) {
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

		public string GetPostingTime() {
			StringBuilder sb = new StringBuilder();
			if (PostingTime.Hour > 9) {
				sb.Append($"{PostingTime.Hour}");
			} else {
				sb.Append($"0{PostingTime.Hour}");
			}
			if (PostingTime.Minute > 9) {
				sb.Append($":{PostingTime.Minute}");
			} else {
				sb.Append($":0{PostingTime.Minute}");
			}

			return sb.ToString();
		}

	}
}
