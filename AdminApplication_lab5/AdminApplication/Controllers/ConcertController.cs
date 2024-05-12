using AdminApplication.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AdminApplication.Controllers
{
    public class ConcertController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ImportConcerts(IFormFile file) //imeto na ovaa promenliva treba da e kako u Index.cshtml za User
        {
            //make a copy
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}";

            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream); ;
                fileStream.Flush();
            }
            //read from file

            List<Concert> concerts = getAllConcertsFromFile(file.FileName);

            HttpClient client = new HttpClient();
            string URL = "http://localhost:5054/api/Admin/ImportAllConcerts";

            HttpContent content = new StringContent(JsonConvert.SerializeObject(concerts), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<bool>().Result;
            return RedirectToAction("Index", "Order");

        }
        private List<Concert> getAllConcertsFromFile(string fileName)
        {
            List<Concert> concerts = new List<Concert>();
            string filePath = $"{Directory.GetCurrentDirectory()}\\files\\{fileName}";
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {

                //za excelReader simnuvame paket ExcelDataReader
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        concerts.Add(new Concert
                        {
                            ConcertName = reader.GetValue(0).ToString(),
                            ConcertDescription = reader.GetValue(1).ToString(),
                            ConcertImage = reader.GetValue(2).ToString(),
                            Rating = Convert.ToDouble(reader.GetValue(3))
                        });

                    }
                }
            }
            return concerts;
        }
    }
}
