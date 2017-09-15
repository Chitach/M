using System;

namespace M.Models {
	public class Comment {
		public Post Post { get; set; }
		public int Id { get; set; }
		public User User { get; set; }
		public DateTime CreatedTime { get; set; }
		public string Text { get; set; }

		public string GetCommentDate() {
			return CreatedTime.Day + "" 
				+ CreatedTime.Month + ""
				+ CreatedTime.Year + ""
				+ CreatedTime.Hour + ""
				+ CreatedTime.Minute;
		}
	}
}
