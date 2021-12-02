using ApiProjet.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Controllers
{
    
    public class ApiProjetController : Controller
    {
        public ITodoServiceApiProjet _todoService;

        public ApiProjetController(ITodoServiceApiProjet todoService)
        {
            _todoService = todoService;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AllComment()
        {
            try
            {
                string accessToken = await HttpContext.GetTokenAsync("access_token");
                var result = await _todoService.GetAllCommentAsync(accessToken);
                return View(result);
            }
            catch (Exception aex)
            {
                string msg = aex.ToString();
                if (msg.Contains("Forbidden")) return RedirectToAction("AccessDenied", "Account", 
                    new Model.DeniedMessage { message = "403 Forbidden. Vous n'avez pas les droits pour consulter ou utiliser cette ressource." });
                else return RedirectToAction("AccessDenied", "Account");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CommentById(string Id)
        {
            try
            {
                string accessToken = await HttpContext.GetTokenAsync("access_token");
                var result = await _todoService.GetCommentByIdAsync(Id, accessToken);
                return View(result);
            }
            catch (Exception aex)
            {
                string msg = aex.ToString();
                if (msg.Contains("Forbidden")) return RedirectToAction("AccessDenied", "Account", 
                    new Model.DeniedMessage { message = "403 Forbidden. Vous n'avez pas les droits pour consulter ou utiliser cette ressource."  });
                else return RedirectToAction("AccessDenied", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostComment([Bind("Id,Author,Body,Email,PostId,UrlPhoto")]ApiProjet.Models.Comment comment) 
        {
            try
            {
                string accessToken = await HttpContext.GetTokenAsync("access_token");
                await _todoService.PostCommentAsync(comment, accessToken);
                return RedirectToAction("VueOnImage", "Basic", new NasaImage.Model.TodoResult { identifier = comment.UrlPhoto });
            }
            catch (Exception aex)
            {
                string msg = aex.ToString();
                if (msg.Contains("Forbidden")) return RedirectToAction("AccessDenied", "Account",
                    new Model.DeniedMessage { message = "403 Forbidden. Vous n'avez pas les droits pour consulter ou utiliser cette ressource." });
                else return RedirectToAction("AccessDenied", "Account");
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditAComment(int id)
        {
            try
            {
                string accessToken = await HttpContext.GetTokenAsync("access_token");
                var result = await _todoService.GetaCommentByIdAsync(id, accessToken);
                if (result == null)
                {
                    return NotFound();
                }
                return View(result);
            }
            catch (Exception aex )
            {
                string msg = aex.ToString();
                if (msg.Contains("Forbidden")) return RedirectToAction("AccessDenied", "Account",
                    new Model.DeniedMessage { message = "403 Forbidden. Vous n'avez pas les droits pour consulter ou utiliser cette ressource." });
                else return RedirectToAction("AccessDenied", "Account");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAComment(int id, [Bind("Id,Author,Body,Email,PostId,UrlPhoto")] ApiProjet.Models.Comment comment)
        {
            try
            {
                string accessToken = await HttpContext.GetTokenAsync("access_token");
                await _todoService.UpdateACommentAsync(comment, accessToken);
                return RedirectToAction(nameof(AllComment));
            }
            catch (Exception aex)
            {
                string msg = aex.ToString();
                if (msg.Contains("Forbidden")) return RedirectToAction("AccessDenied", "Account",
                    new Model.DeniedMessage { message = "403 Forbidden. Vous n'avez pas les droits pour consulter ou utiliser cette ressource." });
                else return RedirectToAction("AccessDenied", "Account");
            }
        }
        public async Task<IActionResult> DeleteAComment(int id) 
        {
            try
            {
                string accessToken = await HttpContext.GetTokenAsync("access_token");

                await _todoService.DeleteACommentAsync(id, accessToken);
                return RedirectToAction(nameof(AllComment));
            }
            catch (Exception aex)
            {
                string msg = aex.ToString();
                if (msg.Contains("Forbidden")) return RedirectToAction("AccessDenied", "Account",
                    new Model.DeniedMessage { message = "403 Forbidden. Vous n'avez pas les droits pour consulter ou utiliser cette ressource." });
                else return RedirectToAction("AccessDenied", "Account");
            }
        }

    }
}

