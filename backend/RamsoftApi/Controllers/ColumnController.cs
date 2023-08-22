using System.Data;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Errors;
using RamsoftApi.Models.Requests;
using RamsoftApi.Models.Responses;
using RamsoftApi.Services.Interfaces;
using Services.Interfaces;

namespace RamsoftApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ColumnController
{

    private readonly IColumnService _ColumnService;

    public ColumnController(IColumnService ColumnService)
    {
        _ColumnService = ColumnService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        try
        {
            var boardFound = _ColumnService.Get(id);
            return new OkObjectResult(boardFound);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(new ErrorResponseModel(HttpStatusCode.BadRequest, ex.Message));
        }
    }

    [HttpGet("list/{dashBoardId}")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListByDashBoard(string dashBoardId)
    {
        try
        {
            var allColumns = _ColumnService.ListByDashBoard(dashBoardId);
            return new OkObjectResult(allColumns);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(new ErrorResponseModel(HttpStatusCode.BadRequest, ex.Message));
        }
    }


    [HttpPost]
    [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnprocessableEntityObjectResult), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> AddAsync(ColumnRequest request)
    {
        try
        {
            var savedColumn = _ColumnService.Add(request);

            var columnResponse = new AddColumnResponse();
            columnResponse.ColumnId = savedColumn.ColumnId;
            columnResponse.DashBoardId = request.DashBoardId;
            columnResponse.Name = savedColumn.Name;
            columnResponse.Tasks = savedColumn.Tasks;

            return new OkObjectResult(columnResponse);
        }
        catch (DuplicateNameException ex)
        {
            return new BadRequestObjectResult(new ErrorResponseModel(HttpStatusCode.Conflict, "Duplicated name"));
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(new ErrorResponseModel(HttpStatusCode.BadRequest, ex.Message));
        }
    }


    [HttpDelete]
    [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnprocessableEntityObjectResult), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        try
        {
            _ColumnService.Delete(id);
            return new OkObjectResult("{ Message: \"Column Deleted\"}");
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(new ErrorResponseModel(HttpStatusCode.BadRequest, ex.Message));
        }
    }

}


