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
        return await _context.DoctorCards.FindAsync(id) ?? throw new NullReferenceException("DoctorCard id: {id} not found");
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
}