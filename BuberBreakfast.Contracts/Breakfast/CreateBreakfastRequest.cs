namespace BuberBreakfast.Contracts.Breakfast;

    public record CreateBreakfastRequest(
        string Name,
        string Description,
        DateTime StartDate,
        DateTime EndDate,
        List<string> Savory,
        List<string> Sweet
    );
