using System.Data;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Errors;
using RamsoftApi.Models.Requests;
using RamsoftApi.Services.Interfaces;
using Services.Interfaces;

namespace RamsoftApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController
{

    private readonly ITaskService _TaskService;

    public TaskController(ITaskService TaskService)
    {
        _TaskService = TaskService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        try
        {
            var boardFound = _TaskService.Get(id);
            return new OkObjectResult(boardFound);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(new ErrorResponseModel(HttpStatusCode.BadRequest, ex.Message));
        }
    }


    [HttpPost]
    [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnprocessableEntityObjectResult), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> AddAsync(DashTaskRequest request)
    {
        try
        {
            var savedTask = _TaskService.Add(request);
            return new OkObjectResult(savedTask);
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

    [HttpPut]
    [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnprocessableEntityObjectResult), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateAsync(DashTask request)
    {
        try
        {
            var savedTask = _TaskService.Update(request);
            return new OkObjectResult(savedTask);
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
            _TaskService.Delete(id);
            return new OkObjectResult("{ Message: \"Task Deleted\"}");
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(new ErrorResponseModel(HttpStatusCode.BadRequest, ex.Message));
        }
    }

}