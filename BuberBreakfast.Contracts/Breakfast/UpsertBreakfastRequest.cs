namespace BuberBreakfast.Contracts.Breakfast;

    public record UpsertBreakfastRequest(
        string Name,
        string Description,
        DateTime StartDate,
        DateTime EndDate,
        List<string> Savory,
        List<string> Sweet
    );
