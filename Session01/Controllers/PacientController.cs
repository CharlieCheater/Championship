using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCoder;
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
        public IActionResult CreatePacient()
        {
            return View();
        }
        // url/Pacient/DeletePacient/1
        public IActionResult DeletePacient(int id)
        {
            var pacient = _context.Pacients.FirstOrDefault(x => x.Id == id);
            if (pacient == null)
                return NotFound();

            _context.Pacients.Remove(pacient);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> CreatePacient([FromForm]AddPacientViewModel pacientViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(pacientViewModel);
            }
            if(_context.Pacients.Any(x => x.Patronymic == pacientViewModel.Patronymic && x.FirstName == pacientViewModel.FirstName))
            {
                ModelState.TryAddModelError("LastName", "Такой пациент с ФИО уже существует");
                return View(pacientViewModel);
            }
            Pacient pacient = new Pacient()
            {
                FirstName = pacientViewModel.FirstName,
                LastName = pacientViewModel.LastName,
                PhoneNumber = pacientViewModel.PhoneNumber,
                Address = pacientViewModel.Address,
                Birthday = pacientViewModel.Birthday.Value,
                Email = pacientViewModel.Email,
                GenderId = pacientViewModel.GenderId,
                RegistrationDate = DateTime.Now,
                Patronymic = pacientViewModel.Patronymic,
                MedCard = new MedCard()
                {
                    EntryDate = DateTime.Now,
                    QrCode = ""
                }
            };
            if(pacientViewModel.Avatar != null)
            {
                using (var ms = new BinaryReader(pacientViewModel.Avatar.OpenReadStream()))
                {
                    var bytes = ms.ReadBytes((int)pacientViewModel.Avatar.Length);
                    pacient.Avatar = Convert.ToBase64String(bytes);
                }
            }
            await _context.AddAsync(pacient);
            await _context.SaveChangesAsync();
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();

            QRCodeData data = qRCodeGenerator.CreateQrCode("MedCard: " + pacient.MedCard.Id, QRCodeGenerator.ECCLevel.M);
            var png = new PngByteQRCode(data);
            var imgBytes = png.GetGraphic(8);
            var qrBase64 = Convert.ToBase64String(imgBytes);
            pacient.MedCard.QrCode = qrBase64;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
