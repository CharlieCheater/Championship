// See https://aka.ms/new-console-template for more information
using ImageSniffer.Models;
using QRCoder;
using System.Text;

MainContext context = new MainContext();
var genders = context.Genders.ToList();

Console.WriteLine("Hello, World!");
HttpClient client = new HttpClient();
QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();

var lines = File.ReadAllLines("D:\\конкурс\\КЗ-по-сессиям-и-ресурсы\\Gener.csv", Encoding.UTF8);
var rand = new Random();
List<Pacient> pacients = new List<Pacient>();
foreach (var line in lines)
{
    var bytes = await client.GetByteArrayAsync("https://thispersondoesnotexist.com/");
    string imageBase64 = Convert.ToBase64String(bytes);
    var columns = line.Split(';');

    var pacient = new Pacient()
    {
        LastName = columns[0],
        FirstName = columns[1],
        Patronymic = columns[2],
        PhoneNumber = columns[3],
        Email = columns[4],
        Address = columns[5],
        GenderId = int.Parse(columns[6]),
        MedCard = new MedCard
        {
            EntryDate = DateTime.Now,
            QrCode = ""
        },
        Avatar = imageBase64,
        RegistrationDate = DateTime.Now,
        Birthday = new DateTime(rand.Next(2000, 2010), 1, rand.Next(1,30))
    };
    pacients.Add(pacient);
    Thread.Sleep(50);
}
context.Pacients.AddRange(pacients);
context.SaveChanges();

foreach (var item in pacients)
{
    QRCodeData data = qRCodeGenerator.CreateQrCode("MedCard: " + item.MedCard.Id, QRCodeGenerator.ECCLevel.M);
    var png = new PngByteQRCode(data);
    var imgBytes = png.GetGraphic(8);
    var qrBase64 = Convert.ToBase64String(imgBytes);
    item.MedCard.QrCode = qrBase64;
}
context.SaveChanges();
Console.ReadKey();