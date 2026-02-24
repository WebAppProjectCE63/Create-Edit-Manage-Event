using Microsoft.AspNetCore.Mvc;
using WebApplicationProject.Models;
using System.Net.Http;
using System.Text.Json;
using System.IO;
namespace WebApplicationProject.Controllers
{
    public class ProfileController : Controller
    {
        static List<User> Users = new List<User>()
            {
                new User
                {
                Id = 1,
                FName = "สมชาย",
                SName = "ใจดี",
                Email = "somchai@email.com",
                Gender   = "Men",
                Birthday = new DateTime(1990, 5, 20),
                Image = "https://i.pravatar.cc/150?img=11"
                },
                new User
                {
                    Id = 101,
                    Username = "music_host",
                    FName = "ก้องเกียรติ",
                    SName = "ใจดี",
                    Email = "kong@music.com",
                    Gender = "Male",
                    Birthday = new DateTime(1990, 5, 20),
                    Image = "https://ui-avatars.com/api/?name=Kong+J&background=random&size=128"
                },
                new User
                {
                    Id = 102,
                    Username = "art_host",
                    FName = "ปั้นจั่น",
                    SName = "งานละเอียด",
                    Email = "pun@art.com",
                    Gender = "Female",
                    Birthday = new DateTime(1995, 8, 15),
                    Image = "https://ui-avatars.com/api/?name=Pun+N&background=random&size=128"
                }
            };
        public IActionResult profilepage(int id = 1)
        {
            // จำลองข้อมูล User 
            // จำลองข้อมูลรายการ Event
            var joineventList = new List<Event>
            {
                new Event
                {
                    Id = 101,
                    Title = "งานหนังสือแห่งชาติ",
                    Description = "ไปเดินซื้อหนังสือกันเถอะ",
                    DateTime = new DateTime(2026, 2, 15, 10, 0, 0),
                    Tags = new List<string> { "Education", "Book" },
                    MaxParticipants = 60,
                    CurrentParticipants = 30,
                    Image = "https://www.nupress.grad.nu.ac.th/wp-content/uploads/2018/11/NU-BOOK-FAIR-2018-4.jpg"
                },
                new Event
                {
                    Id = 102,
                    Title = "สัมมนาโปรแกรมเมอร์",
                    Description = "แลกเปลี่ยนความรู้ด้าน .NET Core",
                    DateTime = new DateTime(2026, 3, 20, 13, 0, 0),
                    Tags = new List<string> { "IT", "Seminar" },
                    MaxParticipants = 100,
                    CurrentParticipants = 45,
                    Image = "https://eventscase.com/blog/wp-content/uploads/2024/09/como-organizar-un-evento-corporativo-conferencia-768x412.webp"
                }
            };

            var hosteventList = new List<Event>
            {
                new Event
                {
                    Id = 102,
                    Title = "สัมมนาโปรแกรมเมอร์",
                    Description = "แลกเปลี่ยนความรู้ด้าน .NET Core",
                    DateTime = new DateTime(2026, 3, 20, 13, 0, 0),
                    Tags = new List<string> { "IT", "Seminar" },
                    MaxParticipants = 100,
                    CurrentParticipants = 45,
                    Image = "https://eventscase.com/blog/wp-content/uploads/2024/09/como-organizar-un-evento-corporativo-conferencia-768x412.webp"
                }
            };

            var reviewer1 = new User { FName = "สมศรี", SName = "รักษ์ดี", Image = "https://i.pravatar.cc/150?img=20" };
            var reviewer2 = new User { FName = "มานะ", SName = "มานี", Image = "https://i.pravatar.cc/150?img=30" };

            // 3. จำลองข้อมูล Review
            var reviewList = new List<Review>
            {
                new Review
                {
                    Id = 1,
                    stars = 5, // ★  ☆
                    reviewtitle = "โฮสต์ดูแลดีมาก",
                    reviewbody = "กิจกรรมสนุกมากครับ โฮสต์เป็นกันเองสุดๆ",
                    Author = reviewer1 // เชื่อม Review เข้ากับ User คนที่ 1
                },
                new Review
                {
                    Id = 2,
                    stars = 4,
                    reviewtitle = "แนะนำเลย",
                    reviewbody = "เนื้อหาแน่นปึ๊ก แต่สถานที่แอบแคบไปนิดนึง",
                    Author = reviewer2 // เชื่อม Review เข้ากับ User คนที่ 2
                },
                new Review
                {
                    Id = 2,
                    stars = 3,
                    reviewtitle = "แนะนำเลย",
                    reviewbody = "เนื้อหาแน่นปึ๊ก แต่สถานที่แอบแคบไปนิดนึง",
                    Author = reviewer2 // เชื่อม Review เข้ากับ User คนที่ 2
                }
            };

            // นำข้อมูลทั้ง มาใส่ใน ViewModel
            var viewModel = new ProfilePageViewModel
            {
                UserInfo = Users.FirstOrDefault(u => u.Id == id),
                HostedEvents = hosteventList,
                JoinedEvents = joineventList,
                Reviews = reviewList
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(User editUser, IFormFile uploadImage)
        {
            var ogUser = Users.FirstOrDefault(u => u.Id == editUser.Id);

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