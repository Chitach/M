using M.Configuration;
using M.Data;
using M.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;

namespace M.Controllers {
	[Route("")]
	public class PostController : Controller {
		private IHostingEnvironment _env = null;
		private AppConfiguration _configuration = null;
		private MDbContext _db = null;

		public PostController(MDbContext db, AppConfiguration appConfiguration, IHostingEnvironment env) {
			_db = db;
			_env = env;
			_configuration = appConfiguration;
		}

		[Route("addpost")]
		public IActionResult AddPost() {
			return View();
		}

		[Route("addpost")]
		[HttpPost]
		public IActionResult AddPost(Post post) {
			string postImagesFolderPath = Path.Combine(_env.WebRootPath, _configuration.AppSettings.PostImagesFolder);

			post.PostingTime = DateTime.Now;
			IFormFile image = (Request.Form.Files != null && Request.Form.Files.Count > 0) ? Request.Form.Files[0] : null;
			if (image != null) {
				string fileName = ContentDispositionHeaderValue.Parse(image.ContentDisposition).FileName.Trim('"');
				post.ImageUrl = Path.Combine(_configuration.AppSettings.PostImagesFolder, Guid.NewGuid() + fileName);
				using (FileStream fileStream = new FileStream(Path.Combine(_env.WebRootPath, post.ImageUrl), FileMode.Create)) {
					image.CopyTo(fileStream);
				}
			}

			_db.Posts.Add(post);
			_db.SaveChanges();

			return RedirectToAction("Index", "Home");
		}
	}
}
