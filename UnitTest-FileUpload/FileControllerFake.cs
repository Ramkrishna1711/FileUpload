using FileUpload.Controllers;
using FileUpload.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Xunit;

namespace UnitTest_FileUpload
{
    public class FileControllerFake
    {
        private readonly FileController _controller;
        private readonly IFileService _service;

        public FileControllerFake()
        {
            _service = new FileUploadServiceFake();
            _controller = new FileController(_service);
        }

        [Fact]
        //ALL valid data passed
        public async void Post_WhenCalled_ReturnsOkRequest()
        {
            var content = "Richard 3293982\nRob 3113902p";
            var fileName = "UnitTest1.txt";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile file = new FormFile(stream, 0, stream.Length, "file_form", fileName);

            // Act
            IActionResult response = await _controller.FileImport(file);   //trying to pass empty id here

            // Assert
            ObjectResult objectResponse = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<OkObjectResult>(objectResponse as OkObjectResult);
        }

        [Fact]
        //Invalid Account Name passed
        public async void Post_WhenInvalidName_ReturnsBadRequest()
        {
            var content = "michael 3113902\nXAEA - 12 8293982";
            var fileName = "UnitTest2.txt";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile file = new FormFile(stream, 0, stream.Length, "file_form", fileName);

            // Act
            IActionResult response = await _controller.FileImport(file);  

            // Assert
            ObjectResult objectResponse = Assert.IsType<BadRequestObjectResult>(response);
            Assert.IsType<BadRequestObjectResult>(objectResponse as BadRequestObjectResult);
        }
    

        [Fact]
        //Invalid Account Number lines passed
        public async void Post_WhenInvalidAccountNumber_ReturnsBadRequest()
        {
            var content = "Rose 329a982\nBob 329398";
            var fileName = "UnitTest3.txt";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile file = new FormFile(stream, 0, stream.Length, "file_form", fileName);

            // Act
            IActionResult response = await _controller.FileImport(file);   

            // Assert
            ObjectResult objectResponse = Assert.IsType<BadRequestObjectResult>(response);
            Assert.IsType<BadRequestObjectResult>(objectResponse as BadRequestObjectResult);
        }

        [Fact]
        //Invalid Account name & account Number lines passed
        public async void Post_WhenInvalidAccount_ReturnsBadRequest()
        {
            var content = "XAEA-12 8293982";
            var fileName = "UnitTest4.txt";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile file = new FormFile(stream, 0, stream.Length, "file_form", fileName);

            // Act
            IActionResult response = await _controller.FileImport(file);

            // Assert
            ObjectResult objectResponse = Assert.IsType<BadRequestObjectResult>(response);
            Assert.IsType<BadRequestObjectResult>(objectResponse as BadRequestObjectResult);
        }

        [Fact]
        //ALL valid data passed
        public async void Post_WhenCalledALLInvalid_ReturnsBadRequest()
        {
            var content = "abcdefghijklmnopq12345";
            var fileName = "UnitTest5.txt";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile file = new FormFile(stream, 0, stream.Length, "file_form", fileName);

            // Act
            IActionResult response = await _controller.FileImport(file);   //trying to pass empty id here

            // Assert
            ObjectResult objectResponse = Assert.IsType<BadRequestObjectResult>(response);
            Assert.IsType<BadRequestObjectResult>(objectResponse as BadRequestObjectResult);
        }

        [Fact]
        //Invalid file extension
        public async void Post_WhenCalledInvalidExtension_ReturnsBadRequest()
        {
            var content = "abcdefghijklmnopq12345";
            var fileName = "UnitTest6.json";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            //create FormFile with desired data
            IFormFile file = new FormFile(stream, 0, stream.Length, "file_form", fileName);

            // Act
            IActionResult response = await _controller.FileImport(file);   //trying to pass empty id here

            // Assert
            ObjectResult objectResponse = Assert.IsType<BadRequestObjectResult>(response);
            Assert.IsType<BadRequestObjectResult>(objectResponse as BadRequestObjectResult);
        }

    }
}