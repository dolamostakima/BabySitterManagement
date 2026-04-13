using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartBabySitter.Data;
using SmartBabySitter.Services.DTOs;
using System.Security.Claims;
using SmartBabySitter.Models;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SitterProfileController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SitterProfileController(ApplicationDbContext context)
    {
        _context = context;
    }

    // ================= GET PROFILE =================
    [HttpGet("me/profile")]
    public async Task<IActionResult> GetMyProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userId, out int id))
            return BadRequest("Invalid user id");

        var user = await _context.Users.FindAsync(id);

        var profile = await _context.SitterProfiles
            .Include(x => x.Skills)
            .FirstOrDefaultAsync(x => x.UserId == userId);

        if (user == null) return NotFound();

        return Ok(new
        {
            fullName = user.FullName,
            email = user.Email,
            mobileNo = user.PhoneNumber,

            nid = user?.NidNo,
            gender = user?.Gender,
            dateOfBirth = user?.DateOfBirth,
            address = user?.Address,
            photoUrl = profile?.PhotoUrl,

            skillsText = profile?.SkillsText,
            experienceYears = profile?.ExperienceYears,
            hourlyRate = profile?.HourlyRate,
            locationText = profile?.LocationText,

            skills = profile?.Skills.Select(s => s.Name).ToList()
        });
    }

    // ================= CREATE / UPDATE =================
    [HttpPost]
    public async Task<IActionResult> SaveProfile([FromBody] SitterProfileDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _context.Users.FindAsync(userId);
        if (user == null) return Unauthorized();

        // ===== Update USER TABLE =====
        user.FullName = dto.FullName;
        user.Email = dto.Email;
        user.PhoneNumber = dto.MobileNo;

        // ===== Get or Create Profile =====
        var profile = await _context.SitterProfiles
            .Include(x => x.Skills)
            .FirstOrDefaultAsync(x => x.UserId == userId);

        if (profile == null)
        {
            profile = new SitterProfile
            {
                UserId = userId
            };
            _context.SitterProfiles.Add(profile);
        }

        // ===== Update PROFILE =====
        profile.Nid = dto.Nid;
        profile.Gender = dto.Gender;
        profile.DateOfBirth = dto.DateOfBirth;
        profile.Address = dto.Address;
        profile.PhotoUrl = dto.PhotoUrl;

        profile.SkillsText = dto.SkillsText;
        profile.ExperienceYears = dto.ExperienceYears;
        profile.HourlyRate = dto.HourlyRate;
        profile.LocationText = dto.LocationText;

        // ===== Skills Update =====
        if (profile.Skills != null)
            _context.SitterSkills.RemoveRange(profile.Skills);

        profile.Skills = dto.Skills?
            .Select(s => new SitterSkill { Name = s })
            .ToList();

        await _context.SaveChangesAsync();

        return Ok(new { message = "Saved Successfully" });
    }

    // ================= DELETE =================
    [HttpDelete]
    public async Task<IActionResult> DeleteProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var profile = await _context.SitterProfiles
            .Include(x => x.Skills)
            .FirstOrDefaultAsync(x => x.UserId == userId);

        if (profile == null) return NotFound();

        if (profile.Skills != null)
            _context.SitterSkills.RemoveRange(profile.Skills);

        _context.SitterProfiles.Remove(profile);

        await _context.SaveChangesAsync();

        return Ok(new { message = "Profile Deleted" });
    }
}