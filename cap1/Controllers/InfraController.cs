using System.Runtime.InteropServices;
using System.Security.Cryptography.Xml;
using cap1.Models.Infra;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;


namespace cap1.Controllers
{
    [Authorize]
    public class InfraController : Controller
    {
        private readonly UserManager<UsuarioDaAplicacao>? _userManager;
        private readonly SignInManager<UsuarioDaAplicacao>? _signInManager;
        private readonly ILogger<InfraController> _logger;
        /*		private bool isPersistent;
        */
        public InfraController(UserManager<UsuarioDaAplicacao>? userManager, SignInManager<UsuarioDaAplicacao>? signInManager, ILogger<InfraController>? logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Acessar(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegistrarNovoUsuario(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarNovoUsuario(RegistrarNovoUsuarioViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new UsuarioDaAplicacao { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Usurio criou uma nova conta com senha.");
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("Usuario acessou com a conta criada.");
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Sair()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Usuario realizou logout");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Acessar(AcessarViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid) 
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Senha, model.LembrarDeMim, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Usuario Autenticado.");
                    return RedirectToLocal(returnUrl);
                }
            }
            ModelState.AddModelError(string.Empty, "Falha na tentiva de login.");
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
				return Redirect(returnUrl);
			}
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

        }
        

	}
	}
