using Microsoft.AspNetCore.Mvc;
using WebApplicationProject.Models;
using System.Net.Http;
using System.Text.Json;
using System.IO;
using WebApplicationProject.Data;
namespace WebApplicationProject.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult profilepage(int? id = null)
        {
            int targetId = id ?? MockDB.CurrentLoggedInUserId;

            var joineventList = MockDB.EventList.Where(ev => ev.Participants.Any(p => p.UserId == targetId)).ToList();

            var hosteventList = GetMyHostedEvents(MockDB.EventList, targetId);

            // 3. จำลองข้อมูล Review
            var reviewList = MockDB.UsersList.FirstOrDefault(u => u.Id == targetId).Reviewslist;

            // นำข้อมูลทั้ง มาใส่ใน ViewModel
            var viewModel = new ProfilePageViewModel
            {
                UserInfo = MockDB.UsersList.FirstOrDefault(u => u.Id == targetId),
                HostedEvents = hosteventList,
                JoinedEvents = joineventList,
            };

            return View(viewModel);
        }

        private List<Event> GetMyHostedEvents(List<Event> allEvents ,int UserID)
        {
            if (allEvents == null) return new List<Event>();

            return allEvents.Where(ev => ev.UserHostId == UserID).ToList();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(User editUser, IFormFile uploadImage)
        {
            var ogUser = MockDB.UsersList.FirstOrDefault(u => u.Id == editUser.Id);

            if (ogUser != null)
            {
                string newImageUrl = await UploadImageAsync(uploadImage);
                if (newImageUrl != null)
                {
                    ogUser.Image = newImageUrl;
                }

                ogUser.FName = editUser.FName;
                ogUser.SName = editUser.SName;
                ogUser.Email = editUser.Email;
            }

            return RedirectToAction("profilepage", new { id = ogUser.Id });
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