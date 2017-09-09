using M.Configuration;
using M.Data;
using M.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Linq;

namespace M.Controllers {
	[Route("[controller]")]
	[Authorize(Roles = "admin")]
	public class PostController : Controller {
		private IHostingEnvironment _env = null;
		private AppConfiguration _configuration = null;
		private MDbContext _db = null;

		private readonly RoleManager<IdentityRole> _roleManager;

		public PostController(MDbContext db, AppConfiguration appConfiguration, IHostingEnvironment env, RoleManager<IdentityRole> roleManager) {
			_db = db;
			_env = env;
			_configuration = appConfiguration;
			_roleManager = roleManager;
		}

		[Route("add")]
		public IActionResult AddPost() {
			return View();
		}

		[Route("add")]
		[HttpPost]
		public IActionResult AddPost(Post post) {
			string postImagesFolderPath = Path.Combine(_env.WebRootPath, _configuration.AppSettings.PostImagesFolder);

			post.PostingTime = DateTime.Now;
			IFormFile image = (Request.Form.Files != null && Request.Form.Files.Count > 0) ? Request.Form.Files[0] : null;
			if (image != null && !string.IsNullOrEmpty(image.FileName)) {
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

		[Route("edit")]
		public IActionResult EditPost(int postId) {
			Post post = _db.Posts.FirstOrDefault(p => p.Id == postId);

			if (post == null) {
				return Content($"There is no post with postId {postId}");
			}

			return View(post);
		}

		[Route("edit")]
		[HttpPost]
		public IActionResult EditPost(Post post) {
			string postImagesFolderPath = Path.Combine(_env.WebRootPath, _configuration.AppSettings.PostImagesFolder);

			Post postToUpdate = _db.Posts.FirstOrDefault(x => x.Id == post.Id);

			post.PostingTime = DateTime.Now;
			IFormFile image = (Request.Form.Files != null && Request.Form.Files.Count > 0) ? Request.Form.Files[0] : null;
			if (image != null && !string.IsNullOrEmpty(image.FileName)) {
				string fileName = ContentDispositionHeaderValue.Parse(image.ContentDisposition).FileName.Trim('"');
				post.ImageUrl = Path.Combine(_configuration.AppSettings.PostImagesFolder, Guid.NewGuid() + fileName);

				if (postToUpdate != null && post.ImageUrl != postToUpdate.ImageUrl) {
					// Upload new image
					using (FileStream fileStream = new FileStream(Path.Combine(_env.WebRootPath, post.ImageUrl), FileMode.Create)) {
						image.CopyTo(fileStream);
					}

					// Try to delete old image
					string oldPostImageLocation = Path.Combine(_env.WebRootPath, postToUpdate.ImageUrl ?? "");
					if (postToUpdate != null && System.IO.File.Exists(oldPostImageLocation)) {
						System.IO.File.Delete(oldPostImageLocation);
					}

					postToUpdate.ImageUrl = post.ImageUrl;
				}
			}

			if (postToUpdate == null) {
				_db.Posts.Add(post);
			} else {
				postToUpdate.Title = post.Title;
				postToUpdate.Text = post.Text;
				postToUpdate.PostingTime = post.PostingTime;
			}

			_db.SaveChanges();

			return RedirectToAction("Index", "Home");
		}

		[Route("delete")]
		public void DeletePost(int postId) {
			Post post = _db.Posts.FirstOrDefault(p => p.Id == postId);

			_db.Posts.Remove(post);
			_db.SaveChanges();
		}
	}
}
