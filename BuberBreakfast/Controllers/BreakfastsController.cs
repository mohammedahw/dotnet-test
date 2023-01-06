using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.Services.Breakfasts;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;

public class BreakfastsController : ApiController 
{
    private readonly IBreakfastService _breakfastService;

    public BreakfastsController(IBreakfastService breakfastService)
    {
        _breakfastService = breakfastService;
    }

    private static BreakfastResponse MapBreakfastResponse(Breakfast breakfast)
    {
        return new BreakfastResponse(
                    breakfast.Id,
                    breakfast.Name,
                    breakfast.Description,
                    breakfast.StartDate,
                    breakfast.EndDate,
                    breakfast.LastModified,
                    breakfast.Savory,
                    breakfast.Sweet
                );
    }

    private IActionResult CreatedAtGetBreakfast(Breakfast breakfast)
    {
        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new { id = breakfast.Id },
            value: MapBreakfastResponse(breakfast));
    }

    [HttpPost("")]
    public IActionResult CreateBreakFast(CreateBreakfastRequest request)
    {
        ErrorOr<Breakfast> requestToBreakfastResult =  Breakfast.From(request);

        if (requestToBreakfastResult.IsError)
        {
            return Problem(requestToBreakfastResult.Errors);
        }

        var breakfast = requestToBreakfastResult.Value;

        ErrorOr<Created> createBreakfastResult = _breakfastService.CreateBreakfast(breakfast);

        return createBreakfastResult.Match(
            created => CreatedAtGetBreakfast(breakfast),
            errors => Problem(errors)
        );
    }


    [HttpGet()]
    public IActionResult GetBreakfasts()
    {   
        List<Breakfast> breakfasts = _breakfastService.GetBreakfasts();
        return Ok(breakfasts);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<Breakfast> getBreakfastResults = _breakfastService.GetBreakfast(id);

        if (getBreakfastResults.IsError)
        {
            return Problem(getBreakfastResults.Errors);
        }

        return getBreakfastResults.Match(
            value => Ok(MapBreakfastResponse(value)),
            errors => Problem(errors)
        );

    }

    [HttpPatch("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {
        
        ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.From(id, request);

        if (requestToBreakfastResult.IsError)
        {
            return Problem(requestToBreakfastResult.Errors);
        }

        var breakfast = requestToBreakfastResult.Value;

        ErrorOr<UpsertedBreakfastResult> upsertedBreakfastResult = _breakfastService.UpdateBreakfast(breakfast);

        return upsertedBreakfastResult.Match(
            upserted => upserted.isNewlyCreated ? CreatedAtGetBreakfast(breakfast) : NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        ErrorOr<Deleted> deleteBreakfastResult = _breakfastService.DeleteBreakfast(id);

        return deleteBreakfastResult.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
    }
}