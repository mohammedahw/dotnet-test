using ErrorOr;

namespace BuberBreakfast.ServiceErrors;

public class Errors
{
    public static class Breakfast
    {
        public static Error NotFound => Error.NotFound(
            code: "Breakfast.NotFound",
             description: "Breakfast not found");

        public static Error InvalidName => Error.Validation(
            code: "Breakfast.InvalidName",
            description: $"Breakfast name must be between {Models.Breakfast.MinNameLength} and {Models.Breakfast.MaxNameLength} characters long");

        public static Error InvalidDescription => Error.Validation(
            code: "Breakfast.InvalidDescription",
            description: $"Breakfast description must be between {Models.Breakfast.MinDescriptionLength} and {Models.Breakfast.MaxDescriptionLength} characters long");
    }
}