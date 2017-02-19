namespace M.Configuration {
	public class AppConfiguration {
		public DBConnectionStrings ConnectionStrings { get; set; }
		public ApplicationSettings AppSettings { get; set; }

		public class DBConnectionStrings {
			public string DefaultConnection { get; set; }
		}

		public class ApplicationSettings {
			public string PostImagesFolder { get; set; }
		}
	}
}
