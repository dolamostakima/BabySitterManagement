using Microsoft.AspNetCore.Mvc;
using SmartBabySitter.Data;
using SmartBabySitter.DTOs;
using SmartBabySitter.Models;
using SmartBabySitter.Services;
using System.Security.Cryptography;
using System.Text;

namespace SmartBabySitter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(ApplicationDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        // Password Hash
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        // Parent Register
        [HttpPost("register-parent")]
        public IActionResult RegisterParent(RegisterUserDto dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                Location = dto.Location,
                Role = "Parent",
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("Parent registered successfully");
        }

        // BabySitter Register
        [HttpPost("register-sitter")]
        public IActionResult RegisterSitter(RegisterBabySitterDto dto)
        {
            var sitter = new BabySitter
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                Skills = dto.Skills,
                ExperienceYears = dto.ExperienceYears,
                HourlyRate = dto.HourlyRate,
                Location = dto.Location,
                IsApproved = false,
                CreatedAt = DateTime.Now
            };

            _context.BabySitters.Add(sitter);
            _context.SaveChanges();

            return Ok("Baby sitter registered, waiting for admin approval");
        }

        // Login
        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);

            if (user == null || user.PasswordHash != HashPassword(dto.Password))
                return Unauthorized("Invalid credentials");

            var token = _jwtService.GenerateToken(user.Email, user.Role);

            return Ok(new { token });
        }
    }
}
