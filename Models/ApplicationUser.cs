using Microsoft.AspNetCore.Identity;
using System.Net;

namespace SmartBabySitter.Models;

public class ApplicationUser : IdentityUser<int>
{
    public string FullName { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // optional profile fields
    public string? DefaultLocationText { get; set; }

    // navigation
    public ICollection<Booking> BookingsAsParent { get; set; } = new List<Booking>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public ICollection<Address> Addresses { get; set; } = new List<Address>();
}