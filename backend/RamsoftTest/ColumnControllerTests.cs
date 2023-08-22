using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Errors;
using Moq;
using RamsoftApi.Controllers;
using RamsoftApi.Models.Requests;
using Services.Interfaces;
using System.Data;
using System.Net;

namespace RamsoftTest
{
    public class ColumnControllerTests
    {

        private readonly Mock<IDashBoardService> _dashService;
        private readonly DashboardController _controller;


        public ColumnControllerTests()
        {
            _dashService = new Mock<IDashBoardService>();
            _controller = new DashboardController(_dashService.Object);
        }


        [Fact(DisplayName = "[001] - Successfully gets a dashboard")]
        [Trait("Category", "Success")]
        public async Task GET_GetByIdAsync_Success()
        {
            var id = Guid.NewGuid();

            var expectedResult = new DashBoard();
            expectedResult.DashBoardId = id;

            _dashService.Setup(s => s.Get(It.IsAny<string>())).Returns(expectedResult);

            var result = await _controller.GetByIdAsync(id.ToString()) as OkObjectResult;

            Assert.NotNull(result);
            var response = result.Value as DashBoard;
            Assert.Equal(id, response.DashBoardId);

           Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact(DisplayName = "[002] - Error getting  dashboard")]
        [Trait("Category", "Fail")]
        public async Task GET_GetByIdAsync_Error()
        {
            var id = Guid.NewGuid();

            _dashService.Setup(s => s.Get(It.IsAny<string>())).Throws(new Exception("Some error"));

            var result = await _controller.GetByIdAsync(id.ToString()) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            var response = result.Value as ErrorResponseModel;
            Assert.Equal("Some error", response.Message);
        }

        [Fact(DisplayName = "[003] - Successfully Lists dashboards")]
        [Trait("Category", "Success")]
        public async Task GET_List_Success()
        {
            var expectedResult = new List<DashBoard>();
            var newBoard = new DashBoard();
            var id = Guid.NewGuid();
            newBoard.DashBoardId = id;
            expectedResult.Add(newBoard);

            _dashService.Setup(s => s.List()).Returns(expectedResult);

            var result = await _controller.List() as OkObjectResult;

            Assert.NotNull(result);
            var response = result.Value as List<DashBoard>;
            
            Assert.Equal(id, response.First().DashBoardId);

            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact(DisplayName = "[004] - Error Listing dashboards")]
        [Trait("Category", "Fail")]
        public async Task GET_List_Error()
        {
            _dashService.Setup(s => s.List()).Throws(new Exception("Some error"));

            var result = await _controller.List() as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            var response = result.Value as ErrorResponseModel;
            Assert.Equal("Some error", response.Message);

        }

        [Fact(DisplayName = "[005] - Sucessfully adds dashboard")]
        [Trait("Category", "Success")]
        public async Task POST_AddAsync_Success()
        {
            var id = Guid.NewGuid();

            var expectedResult = new DashBoard();
            expectedResult.DashBoardId = id;
            expectedResult.Name = "Test Name";

            _dashService.Setup(s => s.Add(It.IsAny<DashBoardRequest>())).Returns(expectedResult);

            var request = new DashBoardRequest();
            request.Name = "Test Name";

            var result = await _controller.AddAsync(request) as OkObjectResult;

            Assert.NotNull(result);
            var response = result.Value as DashBoard;
            Assert.Equal(id, response.DashBoardId);
            Assert.Equal("Test Name", response.Name);

            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact(DisplayName = "[006] - Error Adding dashboard")]
        [Trait("Category", "Fail")]
        public async Task POST_AddAsync_Error()
        {
            var request = new DashBoardRequest();
            request.Name = "Test Name";


            _dashService.Setup(s => s.Add(It.IsAny<DashBoardRequest>())).Throws(new Exception("Some error"));

            var result = await _controller.AddAsync(request) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            var response = result.Value as ErrorResponseModel;
            Assert.Equal("Some error", response.Message);

        }

        [Fact(DisplayName = "[007] - Error Adding dashboard - repeated name")]
        [Trait("Category", "Fail")]
        public async Task POST_AddAsync_Error_Repeated_Name()
        {
            var request = new DashBoardRequest();
            request.Name = "Test Name";


            _dashService.Setup(s => s.Add(It.IsAny<DashBoardRequest>())).Throws(new DuplicateNameException("Some error"));

            var result = await _controller.AddAsync(request) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            var response = result.Value as ErrorResponseModel;
            Assert.Equal("Duplicated name", response.Message);

        }


        [Fact(DisplayName = "[008] - Successfully deletes")]
        [Trait("Category", "Success")]
        public async Task DEL_DeleteAsync_Success()
        {
            var id = Guid.NewGuid();

            var expectedResult = new DashBoard();
            expectedResult.DashBoardId = id;

            _dashService.Setup(s => s.Delete(It.IsAny<string>()));

            var result = await _controller.DeleteAsync(id.ToString()) as OkResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);

        }

        [Fact(DisplayName = "[009] - Error deleting")]
        [Trait("Category", "Fail")]
        public async Task DEL_DeleteAsync_Error()
        {
            var id = Guid.NewGuid();

            _dashService.Setup(s => s.Delete(It.IsAny<string>())).Throws(new Exception("Some error"));

            var result = await _controller.DeleteAsync(id.ToString()) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            var response = result.Value as ErrorResponseModel;
            Assert.Equal("Some error", response.Message);
        }
    }
}