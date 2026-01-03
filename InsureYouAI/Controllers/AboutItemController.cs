using InsureYouAI.Context;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace InsureYouAI.Controllers
{
    public class AboutItemController : Controller
    {
        private readonly InsureContext _context;
        public AboutItemController(InsureContext context)
        {
            _context = context;
        }
        public IActionResult AboutItemList()
        {
            ViewBag.ControllerName = "Hakkımızda Ögeleri";
            ViewBag.PageName = "Mevcut Hakkımızda Ögeleri";
            var values = _context.AboutItems.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateAboutItem()
        {
            ViewBag.ControllerName = "Hakkımızda Ögeleri";
            ViewBag.PageName = "Yeni Hakkımızda Öge Girişi";
            return View();
        }

        [HttpPost]
        public IActionResult CreateAboutItem(AboutItem aboutItem)
        {
            _context.AboutItems.Add(aboutItem);
            _context.SaveChanges();
            return RedirectToAction("AboutItemList");
        }

        [HttpGet]
        public IActionResult UpdateAboutItem(int id)
        {
            ViewBag.ControllerName = "Hakkımızda";
            ViewBag.PageName = "Hakkımızda Ögeleri Güncelleme Sayfası";
            var value = _context.AboutItems.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateAboutItem(AboutItem aboutItem)
        {
            _context.AboutItems.Update(aboutItem);
            _context.SaveChanges();
            return RedirectToAction("AboutItemList");
        }

        public IActionResult DeleteAboutItem(int id)
        {
            var value = _context.AboutItems.Find(id);
            _context.AboutItems.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("AboutItemList");
        }
        [HttpGet]
        public async Task<IActionResult> CreateAboutItemWithGoogleGemini()
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
                        text = "Kurumsal bir sigorta firması için etkileyici, güven verici ve profesyonel bir 'Hakkımızda alanları (about item)' yazısı oluştur. Örneğin: 'Geleceğinizi güvence altına alan kapsamlı sigorta çözümleri sunuyoruz.' şeklinde veya bunun gibi ve buna benzer daha zengin içerikler gelsin. En az 10 tane item istiyorum."
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
