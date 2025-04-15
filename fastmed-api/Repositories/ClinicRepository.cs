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
        return await _context.ClinicCards.ToListAsync();
    }

    public async Task<ClinicCard> GetClinicCardByIdAsync(int id)
    {
        return await _context.ClinicCards.FindAsync(id) ?? throw new NullReferenceException();
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
        _context.ClinicCards.Remove(clinicCard);
        await _context.SaveChangesAsync();
    }
}