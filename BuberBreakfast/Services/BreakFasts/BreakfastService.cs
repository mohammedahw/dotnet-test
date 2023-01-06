using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    private static readonly Dictionary<Guid, Breakfast> _breakfasts = new();
    
    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
    {
        _breakfasts.Add(breakfast.Id, breakfast);
        return Result.Created;
    }   

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        if (!_breakfasts.ContainsKey(id))
            {
                return Errors.Breakfast.NotFound;
            }
            
        return _breakfasts[id];
    }

    public List<Breakfast> GetBreakfasts()
    {
        return _breakfasts.Values.ToList();
    }

    public ErrorOr<UpsertedBreakfastResult> UpdateBreakfast(Breakfast breakfast)
    {
        var isNewlyCreated = !_breakfasts.ContainsKey(breakfast.Id);

        _breakfasts[breakfast.Id] = breakfast;
        return new UpsertedBreakfastResult(isNewlyCreated);
    }
    
    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        _breakfasts.Remove(id);
        return Result.Deleted;
    }
}
