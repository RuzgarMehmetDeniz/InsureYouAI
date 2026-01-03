using InsureYouAI.Context;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace InsureYouAI.Controllers
{
    public class ServiceController : Controller
    {
        private readonly InsureContext _context;

        public ServiceController(InsureContext context)
        {
            _context = context;
        }

        public IActionResult ServiceList()
        {
            var values = _context.Services.ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateService()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateService(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
            return RedirectToAction("ServiceList");
        }
        public IActionResult UpdateService(int id)
        {
            var value = _context.Services.Find(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateService(Service service)
        {
            _context.Services.Update(service);
            _context.SaveChanges();
            return RedirectToAction("ServiceList");
        }
        public IActionResult DeleteService(int id)
        {
            var value = _context.Services.Find(id);
            _context.Services.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("ServiceList");
        }
        [HttpGet]
        public async Task<IActionResult> CreateServiceWithGoogleGemini()
        {
            var apiKey = "";
            var model = "models/gemini-2.5-flash";
            var url = $"https://generativelanguage.googleapis.com/v1beta/{model}:generateContent?key={apiKey}";

            var requestBody = new
            {
                contents = new[]
                {
            new
            {
                parts = new[]
                {
                    new
                    {
                        text = "Bir sigorta şirketi için hizmetler bölümü hazırla. " +
                               "5 farklı hizmet yaz. " +
                               "Her biri maksimum 100 karakter olsun. " +
                               "Numaralı liste şeklinde yaz."
                    }
                }
            }
        }
            };

            using var httpClient = new HttpClient();
            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");

            var response = await httpClient.PostAsync(url, content);
            var responseJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.services = new List<string>
        {
            "Gemini API hata verdi: " + responseJson
        };
                return View();
            }

            string generatedText = "";

            using var jsonDoc = JsonDocument.Parse(responseJson);
            var root = jsonDoc.RootElement;

            if (root.TryGetProperty("candidates", out var candidates) &&
                candidates.GetArrayLength() > 0 &&
                candidates[0].TryGetProperty("content", out var contentEl) &&
                contentEl.TryGetProperty("parts", out var parts) &&
                parts.GetArrayLength() > 0 &&
                parts[0].TryGetProperty("text", out var textEl))
            {
                generatedText = textEl.GetString();
            }

            var services = generatedText?
                .Split('\n')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.TrimStart('1', '2', '3', '4', '5', '.', ' '))
                .ToList();

            ViewBag.services = services;

            return View();
        }

    }
}