using System.Data;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Models.Errors;
using RamsoftApi.Models.Requests;
using Services.Interfaces;

namespace RamsoftApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DashboardController 
{

    private readonly IDashBoardService _dashboardService;

    public DashboardController(IDashBoardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        try
        {
            var boardFound = _dashboardService.Get(id);
            return new OkObjectResult(boardFound);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(new ErrorResponseModel(HttpStatusCode.BadRequest, ex.Message));
        }
    }

    [HttpGet("list")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> List()
    {
        try
        {
            var allBoards = _dashboardService.List();
            return new OkObjectResult(allBoards);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(new ErrorResponseModel(HttpStatusCode.BadRequest, ex.Message));
        }
    }


    [HttpPost]
    [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnprocessableEntityObjectResult), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> AddAsync(DashBoardRequest request)
    {
        try
        {
            var savedBoard = _dashboardService.Add(request);
            return new OkObjectResult(savedBoard);
        }
        catch (DuplicateNameException ex)
        {
            return new BadRequestObjectResult(new ErrorResponseModel(HttpStatusCode.Conflict, "Duplicated name")) ;
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(new ErrorResponseModel(HttpStatusCode.BadRequest, ex.Message));
        }
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnprocessableEntityObjectResult), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        try
        {
            _dashboardService.Delete(id);
            return new OkResult();
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(new ErrorResponseModel(HttpStatusCode.BadRequest, ex.Message));
        }
    }

}


