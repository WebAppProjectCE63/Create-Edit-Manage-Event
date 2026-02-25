using Microsoft.AspNetCore.Mvc;
using WebApplicationProject.Models;
using System.Net.Http;
using System.Text.Json;
using System.IO;
using WebApplicationProject.Data;
namespace WebApplicationProject.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Myevent()
        {
            return View(MockDB.EventList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Event newEvent, IFormFile uploadImage)
        {
            string ImageUrl = await UploadImageAsync(uploadImage);
            newEvent.Image = ImageUrl ?? "https://img2.pic.in.th/image-icon-symbol-design-illustration-vector.md.jpg";
            newEvent.Tags = ProcessTags(Request.Form["Tag"]);
            newEvent.UserHostId = 101;
            newEvent.Id = MockDB.EventList.Count + 1;
            MockDB.EventList.Add(newEvent);
            return RedirectToAction("Create");
        }

        public IActionResult Edit(int id)
        {   

            var eventToEdit = MockDB.EventList.FirstOrDefault(e => e.Id == id);
            if (eventToEdit == null) return NotFound();
            return View(eventToEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Event editEvent, IFormFile uploadImage)
        {
            var ogEvent = MockDB.EventList.FirstOrDefault(e => e.Id == editEvent.Id);
            if (ogEvent != null)
            {
                string newImageUrl = await UploadImageAsync(uploadImage);
                if (newImageUrl != null)
                {
                    ogEvent.Image = newImageUrl;
                }
                ogEvent.Title = editEvent.Title;
                ogEvent.Description = editEvent.Description;
                ogEvent.Tags = ProcessTags(Request.Form["Tag"]);
                ogEvent.MaxParticipants = editEvent.MaxParticipants;
                ogEvent.DateTime = editEvent.DateTime;
                ogEvent.Location = editEvent.Location;
            }
            return RedirectToAction("Edit", new {id = editEvent.Id});

        }

        public IActionResult Manage(int id)
        {
            var eventToManage = MockDB.EventList.FirstOrDefault(e => e.Id == id);
            if (eventToManage == null) return NotFound();
            var participantIds = eventToManage.Participants.Select(p => p.UserId).ToList();
            var participants = MockDB.UsersList.Where(u => participantIds.Contains(u.Id)).ToList();
            ViewBag.ParticipantList = participants;
            return View(eventToManage);
        }

        private List<string> ProcessTags(string rawTags)
        {
            if (string.IsNullOrEmpty(rawTags))
                return new List<string>();

            return rawTags.Split(',')
                          .Select(t => t.Trim())
                          .Where(t => !string.IsNullOrEmpty(t))
                          .ToList();
        }
        private async Task<string> UploadImageAsync(IFormFile uploadImage)
        {
            if (uploadImage == null || uploadImage.Length == 0)
                return null;

            using var ms = new MemoryStream();
            await uploadImage.CopyToAsync(ms);
            string base64Image = Convert.ToBase64String(ms.ToArray());

            string apiKey = "d0389bb796bb619e0b8f1503873fbc8a";
            using var client = new HttpClient();
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("image", base64Image)
            });

            var response = await client.PostAsync($"https://api.imgbb.com/1/upload?key={apiKey}", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                return doc.RootElement.GetProperty("data").GetProperty("url").GetString();
            }

            return null; // ถ้าอัปโหลดล้มเหลว
        }
    }
}
