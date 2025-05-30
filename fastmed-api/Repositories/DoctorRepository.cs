using fastmed_api.Data;
using fastmed_api.Models;
using Microsoft.EntityFrameworkCore;

namespace fastmed_api.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly FastmedContext _context;

    public DoctorRepository(FastmedContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DoctorCard>> GetDoctorCardsAsync()
    {
        return await _context.DoctorCards.ToListAsync();
    }

    public async Task<DoctorCard> GetDoctorCardAsync(int id)
    {
       // return await _context.DoctorCards.FindAsync(id) ?? throw new NullReferenceException("DoctorCard id: {id} not found");
       return await _context.DoctorCards
           .Include(d => d.Clinic)
           .ThenInclude(c => c.WorkingHours)
           .FirstOrDefaultAsync(d => d.DoctorId == id) ?? throw new NullReferenceException();
    }
    
    public async Task AddDoctorCardAsync(DoctorCard doctorCard)
    {
        await _context.DoctorCards.AddAsync(doctorCard);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateDoctorCardAsync(DoctorCard doctorCard)
    {
        _context.DoctorCards.Update(doctorCard);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteDoctorCardAsync(int dococtorCardId)
    {
        var doctorCard = await _context.DoctorCards.FindAsync(dococtorCardId);
        if (doctorCard != null)
            _context.DoctorCards.Remove(doctorCard);
        else
            throw new NullReferenceException();
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<DoctorCard>> GetDoctorCardsByClinicIdAsync(int clinicId)
    {
        return await _context.DoctorCards.Where(c => c.ClinicId == clinicId).ToListAsync();
    }
    
    public async Task<IEnumerable<DoctorCard>> GetDoctorCardsBySpecialityAsync(string speciality)
    {
        string searchTerm = speciality.Trim().ToLower();
        return await _context.DoctorCards
            .Where(d => d.Speciality.ToLower().Contains(searchTerm))
            .ToListAsync();
    }

    public async Task<IEnumerable<DoctorCard>> GetDoctorCardsByNameAsync(string name)
    {
        return await _context.DoctorCards
            .Where(d => d.Name.Contains(name))
            .ToListAsync();
    }
    public async Task<IEnumerable<DoctorCard>> GetFilteredDoctorCardsAsync(int? clinicId, string? speciality, string? name)
    {
        var query = _context.DoctorCards.AsQueryable();

        if (clinicId.HasValue)
        {
            query = query.Where(d => d.ClinicId == clinicId.Value);
        }

        if (!string.IsNullOrWhiteSpace(speciality))
        {
            string specialityTerm = speciality.Trim().ToLower();
            query = query.Where(d => d.Speciality.ToLower().Contains(specialityTerm));
        }

        if (!string.IsNullOrWhiteSpace(name))
        {
            string nameTerm = name.Trim().ToLower();
            query = query.Where(d => d.Name.ToLower().Contains(nameTerm));
        }

        return await query.Include(d => d.Clinic) 
            .ToListAsync();
    }
}