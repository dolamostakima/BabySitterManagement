using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartBabySitter.Controllers;

public class WebController : Controller
{
    public IActionResult Index() => View();              // Home
    public IActionResult Login() => View();              // Parent/Sitter login
    public IActionResult Register() => View();           // Register
    public IActionResult Sitters() => View();            // Search sitter
    public IActionResult SitterDetails(int id) => View(model: id); // details + booking
    public IActionResult ParentBookings() => View();     // parent booking list
    public IActionResult SitterBookings() => View();     // sitter booking list/manage
    public IActionResult ReviewCreate(int id) // id = bookingId
           => View(id);
    public IActionResult AdminDashboard() => View();     // admin dashboard + approve
    public IActionResult AdminSitters() => View();
    public IActionResult AdminBookings() => View();
    public IActionResult AdminBookingDetails(int id) => View(model: id);
    public IActionResult AdminUsers() => View();
    public IActionResult AdminPayments() => View();

    public IActionResult Favorites() => View();
    public IActionResult Notifications() => View();

    public IActionResult SitterProfile() => View();
    public IActionResult SitterAvailability() => View();

    public IActionResult MyReviews() => View();

    public IActionResult AdminReviews() => View();
}