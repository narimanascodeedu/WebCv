using MyResume.Domain.AppCode.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyResume.Domain.AppCode.Extensions;
using MyResume.Domain.Models.Entities.Membership;
using MyResume.Domain.Models.FormModels;
using System.Threading.Tasks;

namespace MyResume.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<MyResumeUser> signInManager;
        private readonly UserManager<MyResumeUser> userManager;
        private readonly EmailService emailService;

        public AccountController(SignInManager<MyResumeUser> signInManager, UserManager<MyResumeUser> userManager, EmailService emailService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.emailService = emailService;
        }

        [AllowAnonymous]
        [Route("/signin.html")]
        public IActionResult Signin()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/signin.html")]
        public async Task<IActionResult> Signin(LoginFormModel user)
        {
            if (ModelState.IsValid)
            {
                MyResumeUser foundedUser = null;

                if (user.UserName == null || user.Password == null)
                {
                    ViewBag.Message = "Invalid username or password!";
                    goto end;
                }


                if (user.UserName.IsEmail())
                {
                    foundedUser = await userManager.FindByEmailAsync(user.UserName);
                }
                else
                {
                    foundedUser = await userManager.FindByNameAsync(user.UserName);
                }



                if (foundedUser == null)
                {
                    ViewBag.Message = "Invalid username or password!";
                    goto end;
                }

                var signInResult = await signInManager.PasswordSignInAsync(foundedUser, user.Password, true, true);

                if (!signInResult.Succeeded)
                {
                    ViewBag.Message = "Invalid username or password!";
                    goto end;
                }

                var callbackUrl = Request.Query["ReturnUrl"];

                if (!string.IsNullOrWhiteSpace(callbackUrl))
                {
                    return Redirect(callbackUrl);
                }
                return RedirectToAction("Index", "Home");
            }

        end:
            return View(user);
        }

        [AllowAnonymous]
        [Route("/register.html")]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/register.html")]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new MyResumeUser();

                user.Email = model.Email;
                user.UserName = model.Email;
                user.Name = model.Name;
                user.Surname = model.Surname;

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    string path = $"{Request.Scheme}://{Request.Host}/registration-confirm.html?email={user.Email}&token={token}";

                    var emailResponse = await emailService.SendMailAsync(user.Email, "Registration for Ramin's blog", $"Confirm your subscription with this <a href='{path}'>link</a>.");

                    if (emailResponse)
                    {
                        ViewBag.Message = "Registration is successfully completed";
                    }
                    else
                    {
                        ViewBag.Message = "An error has occurred while sending email!";
                    }

                    await userManager.AddToRoleAsync(user, "user");
                    await signInManager.SignInAsync(user, false);

                    return RedirectToAction(nameof(Signin));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }

            return View(model);

        }

        [AllowAnonymous]
        [Route("registration-confirm.html")]
        public async Task<IActionResult> RegisterConfirm(string email, string token)
        {
            var foundedUser = await userManager.FindByEmailAsync(email);
            if (foundedUser == null)
            {
                ViewBag.Message = "Invalid token";
                goto end;
            }
            token = token.Replace(" ", "+");
            var result = await userManager.ConfirmEmailAsync(foundedUser, token);

            if (!result.Succeeded)
            {
                ViewBag.Message = "Invalid token";
                goto end;
            }

            ViewBag.Message = "Your account is approved!";
        end:
            return RedirectToAction(nameof(Signin));
        }

        [Route("/login.html")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "home");
        }
    }
}
