using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Session01.Domain;
using Session01.Models;

namespace Session01.Controllers
{
    public class PacientController : Controller
    {
        private readonly MainContext _context;
        public PacientController(MainContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var data = _context.Pacients.Select(x => new PacientViewModel
            {
                GenderName = x.Gender.Name,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Patronymic = x.Patronymic,
                Avatar = x.Avatar,
                QrCode = x.MedCard.QrCode
            });
            return View(data);
        }
    }
}
