using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ClientController : Controller
    {
        private readonly MyDatabaseContext _context;

        public ClientController(MyDatabaseContext context)
        {
            _context = context;
        }
        public IActionResult GetAllClients()
        {
            var clients = _context.Clients.ToList();
            return View(clients);
        }


        public IActionResult CreateClient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Client_Id,FirstName,LastName,Email,Phone,Address,City,State,Country")] Client client)
        {

            var existingClient = await _context.Clients.FirstOrDefaultAsync(c => c.Email == client.Email);
            if (existingClient != null)
            {
                ModelState.AddModelError("", "A client with this email already exists.");
                return View("CreateClient", client);
            }

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetAllClients");
        }


        [HttpGet]
        public async Task<IActionResult> DeleteClient(int? clientId)
        {
            if (clientId == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
               
                .FirstOrDefaultAsync(c => c.Client_Id == clientId);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }



        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int clientId)
        {
            var client = await _context.Clients.FindAsync(clientId);

            if (client == null)
            {
                return NotFound();
            }

            try
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();

                return RedirectToAction("GetAllClients");
            }


            catch (Exception ex)
            {

                return RedirectToAction("Error", new { errorMessage = ex.Message });
            }


        }

        public IActionResult EditClient(int id)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Client_Id == id);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }


        [HttpPost]
        public IActionResult Edit(int id, Client client)
        {
            var existingClient = _context.Clients.FirstOrDefault(c => c.Client_Id == id);

            if (existingClient == null)
            {
                return NotFound();
            }

            existingClient.FirstName = client.FirstName;
            existingClient.LastName = client.LastName;
            existingClient.Email = client.Email;
            existingClient.Phone = client.Phone;
            existingClient.Address = client.Address;
            existingClient.City = client.City;
            existingClient.State = client.State;
            existingClient.Country = client.Country;

            _context.SaveChanges();

            return RedirectToAction("GetAllClients");
        }


    }

}
