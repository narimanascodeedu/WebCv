    using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyResume.Domain.AppCode.Extensions;
using MyResume.Domain.Business.BlogPostModule;
using MyResume.Domain.Models.DataContexts;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using MyResume.Domain.Business.BlogPostCommentModule;

namespace MyResume.WebUI.Controllers
{
    public class BlogController : Controller
    {
        private readonly MyResumeDbContext db;
        private readonly IMediator mediator;

        public BlogController(MyResumeDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(BlogPostGetAllQuery query)
        {
            var response = await mediator.Send(query);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PostsBody", response);
            }
            return View(response);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(ResumeBioGetSingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(BlogPostCommentSendCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                return NotFound();
            }
            return PartialView("_AddedComment", response);
        }
    }
}
