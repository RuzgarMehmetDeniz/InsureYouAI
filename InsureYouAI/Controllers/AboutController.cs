using InsureYouAI.Context;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace InsureYouAI.Controllers
{
    public class AboutController : Controller
    {
        private readonly InsureContext _context;
        public AboutController(InsureContext context)
        {
            _context = context;
        }
        public IActionResult AboutList()
        {
            ViewBag.ControllerName = "Hakkımızda";
            ViewBag.PageName = "Mevcut Hakkımızda Yazısı";
            var values = _context.Abouts.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateAbout()
        {
            ViewBag.ControllerName = "Hakkımızda";
            ViewBag.PageName = "Yeni Hakkımızda Yaazı Girişi (Tema Bütünlüğünü Korumak İçin 1 Adet Hakkımızda Yazısı Giriniz)";
            return View();
        }

        [HttpPost]
        public IActionResult CreateAbout(About about)
        {
            _context.Abouts.Add(about);
            _context.SaveChanges();
            return RedirectToAction("AboutList");
        }

        public IActionResult DeleteAbout(int id)
        {
            var value = _context.Abouts.Find(id);
            _context.Abouts.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("AboutList");
        }

        [HttpGet]
        public IActionResult UpdateAbout(int id)
        {
            ViewBag.ControllerName = "Hakkımızda";
            ViewBag.PageName = "Hakkımızda Yazı Güncelleme Sayfası";
            var value = _context.Abouts.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateAbout(About about)
        {
            _context.Abouts.Update(about);
            _context.SaveChanges();
            return RedirectToAction("AboutList");
        }
        [HttpGet]
        public async Task<IActionResult> CreateAboutWithGoogleGemini()
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
                        text = "Kurumsal bir sigorta firması için etkileyici, güven verici ve profesyonel bir 'Hakkımızda' yazısı oluştur."
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
                ViewBag.value = "Gemini API hata verdi: " + responseJson;
                return View();
            }

            string aboutText = "İçerik üretilemedi.";

            using var jsonDoc = JsonDocument.Parse(responseJson);
            var root = jsonDoc.RootElement;

            if (root.TryGetProperty("candidates", out var candidates) &&
                candidates.GetArrayLength() > 0 &&
                candidates[0].TryGetProperty("content", out var contentEl) &&
                contentEl.TryGetProperty("parts", out var parts) &&
                parts.GetArrayLength() > 0 &&
                parts[0].TryGetProperty("text", out var textEl))
            {
                aboutText = textEl.GetString();
            }

            ViewBag.value = aboutText;
            return View();
        }
    }
}
//AIzaSyDeKL2u3ONWxYHzQwPpL3zMxy7Lfc4RF8A