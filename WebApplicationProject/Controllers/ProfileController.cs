using Microsoft.AspNetCore.Mvc;
using WebApplicationProject.Models;

namespace WebApplicationProject.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult profilepage()
        {
            // จำลองข้อมูล User 
            var userData = new User
            {
                Id = 1,
                FName = "สมชาย",
                SName = "ใจดี",
                Email = "somchai@email.com",
                Gender   = "Men",
                Birthday = new DateTime(1990, 5, 20),
                Image = "https://i.pravatar.cc/150?img=11"
            };

            // จำลองข้อมูลรายการ Event
            var joineventList = new List<Event>
            {
                new Event
                {
                    Id = 101,
                    Title = "งานหนังสือแห่งชาติ",
                    Description = "ไปเดินซื้อหนังสือกันเถอะ",
                    DateTime = new DateTime(2026, 2, 15, 10, 0, 0),
                    Tag = new List<string> { "Education", "Book" },
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
                    Tag = new List<string> { "IT", "Seminar" },
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
                    Tag = new List<string> { "IT", "Seminar" },
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
                UserInfo = userData,
                HostedEvents = hosteventList,
                JoinedEvents = joineventList,
                Reviews = reviewList
            };

            return View(viewModel);
        }
    }
}