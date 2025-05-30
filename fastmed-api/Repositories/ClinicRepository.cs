using fastmed_api.Data;
using fastmed_api.Models;
using Microsoft.EntityFrameworkCore;

namespace fastmed_api.Repositories;

public class ClinicRepository : IClinicRepository
{
    private readonly FastmedContext _context;

    public ClinicRepository(FastmedContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<ClinicCard>> GetAllClinicCardsAsync()
    {
        return await _context.ClinicCards
            .Include(c => c.WorkingHours)
            .Include(c => c.Doctors)
            .ToListAsync();
    }

    public async Task<ClinicCard> GetClinicCardByIdAsync(int id)
    {
        return await _context.ClinicCards
            .Include(c => c.WorkingHours)
            .Include(c => c.Doctors)
            .FirstOrDefaultAsync(c => c.ClinicId == id)
            ?? throw new NullReferenceException("ClinicCard id: {id} not found");
    }

    public async Task<IEnumerable<ClinicCard>> GetClinicCardsByNameAsync(string name)
    {
        return await _context.ClinicCards
            .Where(c => c.Name.Contains(name.Trim().ToLower()))
            .Include(c => c.WorkingHours)
            .Include(c => c.Doctors)
            .ToListAsync();
    }

    public async Task AddClinicCardAsync(ClinicCard clinicCard)
    {
        await _context.ClinicCards.AddAsync(clinicCard);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateClinicCardAsync(ClinicCard clinicCard)
    {
        _context.ClinicCards.Update(clinicCard);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteClinicCardAsync(int clinicCardId)
    {
        var clinicCard = await _context.ClinicCards.FindAsync(clinicCardId);
        if (clinicCard != null)
            _context.ClinicCards.Remove(clinicCard);
        else
            throw new NullReferenceException("ClinicCard id: {clinicCardId} not found");
        await _context.SaveChangesAsync();
    }
}