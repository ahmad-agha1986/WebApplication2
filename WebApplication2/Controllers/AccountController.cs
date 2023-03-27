using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyDatabaseContext _context;



        public AccountController(MyDatabaseContext context)
        {
            _context = context;

        }



        [HttpGet]
        public IActionResult LoginView()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserAuth model)
        {
            var user = await _context.UserAuths
                .Include(u => u.UserRole)
                 .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email.ToLower() == model.Email.ToLower());

            if (user != null)
            {
                var passwordHasher = new PasswordHasher<UserAuth>();
                var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.PasswordHash);


                if (result == PasswordVerificationResult.Success)
                {
                    // Check if user is an admin
                    if (user.UserRole.Any(ur => ur.Role.Name == "Admin"))
                    {

                        HttpContext.Session.SetString("userEmail", model.Email);
                        // Redirect to admin page
                        return RedirectToAction("AdminPage");
                    }
                    else
                    {

                        HttpContext.Session.SetString("userEmail", model.Email);
                        // Redirect to user page                      
                        return RedirectToAction("UserPage");

                    }

                }
            }
            // Remove  validation error message for UserRole field
            ModelState["UserRole"].Errors.Clear();

            ModelState.AddModelError("", "Invalid email or password.");
            return View("LoginView", model);
        }



        [HttpGet]
        public IActionResult RegisterView()
        {
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            var existingUser = await _context.UserAuths.FirstOrDefaultAsync(u => u.Email == model.UserAuth.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "A user with that email address already exists.");
                return View("RegisterView", model);
            }

            if (model.UserAuth.PasswordHash != model.UserAuth.ConfirmPassword)
            {
                ModelState.AddModelError("", "Passwords not Matching.");
                return View("RegisterView", model);
            }

            // hash the password and confirm password
            var passwordHasher = new PasswordHasher<UserAuth>();
            model.UserAuth.PasswordHash = passwordHasher.HashPassword(model.UserAuth, model.UserAuth.PasswordHash);
            model.UserAuth.ConfirmPassword = passwordHasher.HashPassword(model.UserAuth, model.UserAuth.ConfirmPassword);

            // create the user
            var userAuth = new UserAuth { Email = model.UserAuth.Email, UserName = model.UserAuth.UserName, PasswordHash = model.UserAuth.PasswordHash, ConfirmPassword = model.UserAuth.ConfirmPassword };
            _context.UserAuths.Add(userAuth);
            await _context.SaveChangesAsync();

            var role = new Roles { Name = "User" };
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            var userRole = new UserRoles { UserAuth = userAuth, Role = role };
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();

            var userEntity = new User
            {
                Job_Title = model.User.Job_Title,
                FirstName = model.User.FirstName,
                LastName = model.User.LastName,
                Phone = model.User.Phone,
                Registration_date = model.User.Registration_date,
                UserAuth_Id = userAuth.Id
            };
            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();

            // userEntity.UserAuth_Id = userAuth.Id

            await _context.SaveChangesAsync();

            return RedirectToAction("AdminPage");
        }



        public IActionResult ChangePassword(int userId, string userEmail)
        {
            var model = new ChangePasswordViewModel
            {
                UserId = userId,
                UserEmail = userEmail
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Get the UserAuth object from the database
                var user = await _context.UserAuths.FindAsync(model.UserId);

                // Verify the old password
                var passwordHasher = new PasswordHasher<UserAuth>();
                var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.OldPassword);
                if (result == PasswordVerificationResult.Success)
                {
                    // Hash and store the new password
                    user.PasswordHash = passwordHasher.HashPassword(user, model.NewPassword);
                    _context.Update(user);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("UserPage");
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "Incorrect password.");
                }
            }

            return View(model);
        }







        public IActionResult AdminPage()
        {
            var userEmail = HttpContext.Session.GetString("userEmail");
            var userName = userEmail;
            userName = char.ToUpper(userName[0]) + userName.Substring(1);
            ViewData["UserName"] = userName;


            return View("AdminPage");
        }





        public IActionResult UserPage()
        {
            var userEmail = HttpContext.Session.GetString("userEmail");
            var userName = userEmail;
            userName = char.ToUpper(userName[0]) + userName.Substring(1);
            ViewData["UserName"] = userName;
            return View("UserPage");
        }
        public async Task<IActionResult> Logout()
        {
            return RedirectToAction("LoginView");
        }






    }
}
