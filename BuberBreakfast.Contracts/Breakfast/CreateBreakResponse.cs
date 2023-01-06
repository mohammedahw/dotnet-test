namespace BuberBreakfast.Contracts.Breakfast;

    public record CreateBreakResponse(
        Guid Id,
        string Name,
        string Description,
        DateTime StartDate,
        DateTime EndDate,
        DateTime LastModified,
        List<string> Savory,
        List<string> Sweet
    );
