using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Models;

public class Breakfast 
{
    public const int MinNameLength = 3;
    public const int MaxNameLength = 50;
    public const int MinDescriptionLength = 50;
    public const int MaxDescriptionLength = 150;
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
    public DateTime LastModified { get; }
    public List<string> Savory { get; }
    public List<string> Sweet { get; }

    private Breakfast(
        Guid id,
        string name,
        string description,
        DateTime startDate,
        DateTime endDate,
        DateTime lastModified,
        List<string> savory,
        List<string> sweet)
    {
        Id = id;
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        LastModified = lastModified;
        Savory = savory;
        Sweet = sweet;
    }

    public static ErrorOr<Breakfast> Create(
        string name,
        string description,
        DateTime startDate,
        DateTime endDate,
        List<string> savory,
        List<string> sweet,
        Guid? id = null)
    {

        List<Error> errors = new();

        if (name.Length < MinNameLength || name.Length > MaxNameLength)
        {
            errors.Add(Errors.Breakfast.InvalidName);
        }

        if (description.Length < MinDescriptionLength || description.Length > MaxDescriptionLength)
        {
            errors.Add(Errors.Breakfast.InvalidDescription);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Breakfast(
            id ?? Guid.NewGuid(),
            name,
            description,
            startDate,
            endDate,
            DateTime.UtcNow,
            savory,
            sweet);
    }

    public static ErrorOr<Breakfast> From(CreateBreakfastRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.Savory,
            request.Sweet);
    }

    public static ErrorOr<Breakfast> From(Guid id, UpsertBreakfastRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.Savory,
            request.Sweet,
            id);
    }
}