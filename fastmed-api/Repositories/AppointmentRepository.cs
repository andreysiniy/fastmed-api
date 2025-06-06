﻿using fastmed_api.Data;
using fastmed_api.Models;
using Microsoft.EntityFrameworkCore;

namespace fastmed_api.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly FastmedContext _context;

    public AppointmentRepository(FastmedContext context)
    {
        _context = context; 
    }
    
    public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
    {
        return await _context.Appointments.ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsByUuidAsync(string uuid)
    {
        return await _context.Appointments.Where(d => d.PatientUuid.Equals(uuid)).ToListAsync();
    }

    public async Task<Appointment> GetAppointmentByIdAsync(int id)
    {
        return await _context.Appointments.FindAsync(id) ?? throw new NullReferenceException($"Appointment id: {id} not found");
    }
    public async Task AddAppointmentAsync(Appointment appointment)
    {
        await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAppointmentAsync(Appointment appointment)
    {
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAppointmentAsync(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment != null)
            _context.Appointments.Remove(appointment);
        else
            throw new NullReferenceException($"Appointment id: {id} not found");
        
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId)
    {
        return await _context.Appointments.Where(a => a.DoctorId == doctorId).ToListAsync();
    }
}