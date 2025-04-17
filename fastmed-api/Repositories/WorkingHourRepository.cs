using fastmed_api.Data;
using fastmed_api.Models;
using Microsoft.EntityFrameworkCore;

namespace fastmed_api.Repositories;

public class WorkingHourRepository : IWorkingHourRepository
{
    private readonly FastmedContext _context;

    public WorkingHourRepository(FastmedContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<WorkingHour>> GetAllWorkingHoursAsync()
    {
        return await _context.WorkingHours.ToListAsync();
    }

    public async Task<WorkingHour> GetWorkingHourByIdAsync(int id)
    {
        return await _context.WorkingHours.FindAsync(id) ?? throw new NullReferenceException();
    }

    public async Task AddWorkingHourAsync(WorkingHour workingHour)
    {
        await _context.WorkingHours.AddAsync(workingHour);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateWorkingHourAsync(WorkingHour workingHour)
    {
        _context.WorkingHours.Update(workingHour);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteWorkingHourAsync(int id)
    {
        var workingHour = await _context.WorkingHours.FindAsync(id);
        if (workingHour != null)
            _context.WorkingHours.Remove(workingHour);
        else 
            throw new NullReferenceException();
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<WorkingHour>> GetWorkingHoursByClinicIdAsync(int clinicId)
    {
        return await _context.WorkingHours.Where(c => c.ClinicId == clinicId).ToListAsync();
    }
}