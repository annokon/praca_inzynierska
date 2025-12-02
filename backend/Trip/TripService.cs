using backend.Trip.DTOs;

namespace backend.Trip;

public class TripService : ITripService
{
    public Task<TripDTO> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TripDTO?> GetByIdTripAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<TripDTO> CreateAsync(CreateTripDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(int id, UpdateTripDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}