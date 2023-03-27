using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class UserController : Controller
    {

        private readonly MyDatabaseContext _context;

        public UserController(MyDatabaseContext context)
        {
            _context = context;
        }



        // for user page 

        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            var userEmail = HttpContext.Session.GetString("userEmail");

            var user = await _context.Users
       .Include(u => u.UserAuth)
       .FirstOrDefaultAsync(u => u.UserAuth.Email.ToLower() == userEmail.ToLower());

            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new RegisterUserViewModel
            {
                User = user,
                UserAuth = user.UserAuth
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditMyProfile(int userId, RegisterUserViewModel model)
        {
            // Retrieve the user and user authentication information from the database
            var userModel = await _context.Users.FirstOrDefaultAsync(u => u.User_Id == userId);
            var userAuth = await _context.UserAuths.FirstOrDefaultAsync(ua => ua.Id == userModel.UserAuth_Id);

            if (userModel == null || userAuth == null)
            {
                return NotFound();
            }


            // Update the user information
            userAuth.Email = model.UserAuth.Email;
            userAuth.UserName = model.UserAuth.UserName;
            userModel.FirstName = model.User.FirstName;
            userModel.LastName = model.User.LastName;
            userModel.Phone = model.User.Phone;

            await _context.SaveChangesAsync();

            return RedirectToAction("UserPage", "Account");
        }



    }
}
