using System;
using System.Collections.Generic;
using System.Text;

namespace M.Models {
	public class Post {
		#region private
		#endregion

		private const int ConstDefaultShortPostTextLength = 150;
		private const int ConstMaxShortPostTextLength = 200;

		#region Public properties
		public int Id { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public string ImageUrl { get; set; }
		public DateTime PostingTime { get; set; }
		public ICollection<Comment> Comments { get; set; }
		#endregion

		public string GetShortText() {
			if (Text.Length < ConstDefaultShortPostTextLength) {
				return Text;
			}

			int nextSpacePos = Text.IndexOf(" ", ConstDefaultShortPostTextLength);
			char[] charsToTrim = { '.', ',', '!', '?', ':' };

			if (nextSpacePos == -1) {
				// break if too long word (> (ConstDefaultShortPostTextLength - ConstMaxShortPostTextLength))
				if (Text.Length > ConstMaxShortPostTextLength) {
					return Text.Substring(0, ConstMaxShortPostTextLength).Trim(charsToTrim) + "...";
				}

				return Text;
			}

			return Text.Substring(0, nextSpacePos).Trim(charsToTrim) + "...";
		}

	}
}
