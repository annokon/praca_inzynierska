using backend.Trip.DTOs;

namespace backend.Trip;

public interface ITripService
{
    Task<TripDTO> GetAllAsync();
    Task<TripDTO?> GetByIdTripAsync(int id);
    Task<TripDTO> CreateAsync(CreateTripDTO dto);
    Task<bool> UpdateAsync(int id, UpdateTripDTO dto);
    Task<bool> DeleteAsync(int id);
}