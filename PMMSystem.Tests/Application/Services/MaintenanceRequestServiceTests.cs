using AutoMapper;
using Moq;
using PMMSystem.Application.RepositoryInterfaces;
using PMMSystem.Application.Services.Interfaces;
using PMMSystem.Application.Services;
using PMMSystem.Application.Dtos;
using PMMSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using PMMSystem.Domain.Exceptions;
using PMMSystem.Domain.Enum;

namespace PMMSystem.Tests.Application.Services
{
  public class MaintenanceRequestServiceTests
  {
    private readonly Mock<IMaintenanceRequestRepository> _mockMaintenanceRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IFileService> _mockFileService;
    private readonly MaintenanceRequestService _service;

    public MaintenanceRequestServiceTests()
    {
      _mockMaintenanceRepo = new Mock<IMaintenanceRequestRepository>();
      _mockMapper = new Mock<IMapper>();
      _mockFileService = new Mock<IFileService>();

      _service = new MaintenanceRequestService(
          _mockMaintenanceRepo.Object,
          _mockMapper.Object,
          _mockFileService.Object);
    }

    [Fact]
    public async Task CreateMaintenanceRequestAsync_WithoutImage_CallsRepositoryWithoutImagePath()
    {
      var createDto = new CreateMaintenanceRequestDto
      {
        MaintenanceEventName = "Test Request",
        PropertyName = "Test",
        Description = "Description",
        Image = null
      };

      var maintenanceObj = new MaintenanceRequest
      {
        MaintenanceEventName = "Test",
        PropertyName = "Test",
        Description = "Description"
      };

      _mockMapper.Setup(m => m.Map<MaintenanceRequest>(It.IsAny<CreateMaintenanceRequestDto>()))
                       .Returns(maintenanceObj);

      await _service.CreateMaintenanceRequestAsync(createDto, "webRootPath", "imageFolder");

      _mockMapper.Verify(m => m.Map<MaintenanceRequest>(createDto), Times.Once);
      _mockFileService.Verify(f => f.SaveFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
      _mockMaintenanceRepo.Verify(r => r.CreateMaintenanceRequestAsync(It.Is<MaintenanceRequest>(mr => mr.ImageUrl == null)), Times.Once);

    }

    [Fact]
    public async Task CreateMaintenanceRequestAsync_WithImage_CallsRepositoryWithImagePath()
    {
      var mockFormFile = new Mock<IFormFile>();
      var fileName = "test-image.jpg";

      mockFormFile.Setup(_ => _.FileName).Returns(fileName);

      var createDto = new CreateMaintenanceRequestDto
      {
        Image = mockFormFile.Object
      };
      var expectedImagePath = "imageFolder/test-image.jpg";
      var maintenanceObj = new MaintenanceRequest { MaintenanceEventName = "Test Request", Description = "Description", PropertyName = "Test" };

      _mockMapper.Setup(m => m.Map<MaintenanceRequest>(It.IsAny<CreateMaintenanceRequestDto>()))
                 .Returns(maintenanceObj);
      _mockFileService.Setup(f => f.SaveFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<string>()))
                      .ReturnsAsync(expectedImagePath);

      await _service.CreateMaintenanceRequestAsync(createDto, "webRootPath", "imageFolder");

      _mockMapper.Verify(m => m.Map<MaintenanceRequest>(createDto), Times.Once);
      _mockFileService.Verify(f => f.SaveFileAsync(mockFormFile.Object, "webRootPath", "imageFolder"), Times.Once);
      _mockMaintenanceRepo.Verify(r => r.CreateMaintenanceRequestAsync(It.Is<MaintenanceRequest>(mr => mr.ImageUrl == expectedImagePath)), Times.Once);
    }

    [Fact]
    public async Task GetMaintenanceRequestByIdAsync_WithValidId_CallRepositoryMethod()
    {
      int id = 1;
      var maintenanceObj = new MaintenanceRequest { Description = "Description", PropertyName = "", MaintenanceEventName = "", Id = id };
      var expectedDto = new MaintenanceRequestDto { Id = id };
      _mockMaintenanceRepo.Setup(r => r.GetMaintenanceRequestByIdAsync(id)).ReturnsAsync(maintenanceObj);
      _mockMapper.Setup(m => m.Map<MaintenanceRequestDto>(maintenanceObj)).Returns(expectedDto);

      var result = await _service.GetMaintenanceRequestByIdAsync(id);

      Assert.NotNull(result);
      Assert.Equal(expectedDto.Id, result.Id);
      _mockMaintenanceRepo.Verify(r => r.GetMaintenanceRequestByIdAsync(id), Times.Once);
      _mockMapper.Verify(m => m.Map<MaintenanceRequestDto>(maintenanceObj), Times.Once);
    }

    [Fact]
    public async Task GetMaintenanceRequestByIdAsync_WithInValidId_ThrowException()
    {
      int invalidId = 1;
      _mockMaintenanceRepo.Setup(r => r.GetMaintenanceRequestByIdAsync(invalidId)).ReturnsAsync((MaintenanceRequest)null);

      var exception = await Assert.ThrowsAnyAsync<MaintenanceNotFoundException>(() => _service.GetMaintenanceRequestByIdAsync(invalidId));

      Assert.NotNull(exception);
      Assert.Equal($"The maintenance request with id : {invalidId} does not exist in the database.", exception.Message);
      _mockMaintenanceRepo.Verify(r => r.GetMaintenanceRequestByIdAsync(invalidId), Times.Once);
      _mockMapper.Verify(m => m.Map<MaintenanceRequestDto>(It.IsAny<MaintenanceRequest>()), Times.Never);
    }

    [Fact]
    public async Task GetMaintenanceReqests_FoundRequests_ReturnRequests()
    {
      var requestList = new List<MaintenanceRequest>() {
        new MaintenanceRequest { Description = "",MaintenanceEventName="",PropertyName = ""},
        new MaintenanceRequest { Description = "",MaintenanceEventName="",PropertyName = ""},

      };
      var dtoList = new List<MaintenanceRequestDto>() {
        new MaintenanceRequestDto { Description = "",MaintenanceEventName="",PropertyName = ""},
        new MaintenanceRequestDto { Description = "",MaintenanceEventName="",PropertyName = ""},
      };
      var search = "";
      MaintenanceStatus? status = null;
      _mockMaintenanceRepo.Setup(r => r.GetMaintenanceRequestsAsync(search,status)).ReturnsAsync(requestList);
      _mockMapper.Setup(m => m.Map<IEnumerable<MaintenanceRequestDto>>(requestList))
                      .Returns(dtoList);

      var result = await _service.GetMaintenanceRequestsAsync(search, status);
      Assert.NotNull(result);
      Assert.Equal(dtoList.Count, result.Count());
      _mockMaintenanceRepo.Verify(r => r.GetMaintenanceRequestsAsync(search, status), Times.Once);
      _mockMapper.Verify(m => m.Map<IEnumerable<MaintenanceRequestDto>>(requestList), Times.Once);
    }

    [Fact]
    public async Task UpdateRequestAsync_RequestNotFound_ThrowsMaintenanceNotFoundException()
    {
      var updateDto = new UpdateMaintenanceRequestDto { Id = 99, MaintenanceEventName = "Update" };
      _mockMaintenanceRepo.Setup(r => r.GetMaintenanceRequestByIdAsync(updateDto.Id))
                          .ReturnsAsync((MaintenanceRequest)null);

      var exception = await Assert.ThrowsAsync<MaintenanceNotFoundException>(
          () => _service.UpdateRequestAsync(updateDto, "webRootPath", "imageFolder")
      );

      Assert.Equal($"The maintenance request with id : {updateDto.Id} does not exist in the database.", exception.Message);
      _mockMaintenanceRepo.Verify(r => r.GetMaintenanceRequestByIdAsync(updateDto.Id), Times.Once);
      _mockMapper.Verify(m => m.Map(It.IsAny<UpdateMaintenanceRequestDto>(), It.IsAny<MaintenanceRequest>()), Times.Never);
      _mockFileService.Verify(f => f.DeleteFileAsync(It.IsAny<string>()), Times.Never);
      _mockFileService.Verify(f => f.SaveFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
      _mockMaintenanceRepo.Verify(r => r.UpdateMaintenanceRequestAsync(It.IsAny<MaintenanceRequest>()), Times.Never);
    }

    [Theory]
    [InlineData(MaintenanceStatus.New, MaintenanceStatus.Accepted, UserRole.PropertyManager)]
    [InlineData(MaintenanceStatus.Rejected, MaintenanceStatus.Accepted, UserRole.PropertyManager)]
    public async Task UpdateRequestAsync_PropertyManagerChangesStatus_ThrowsMockAuthException(
            MaintenanceStatus existingStatus, MaintenanceStatus newStatus, UserRole role)
    {
      // Arrange
      var existingRequest = new MaintenanceRequest { Id = 1, Status = existingStatus, Description ="",MaintenanceEventName="",PropertyName ="" };
      var updateDto = new UpdateMaintenanceRequestDto { Id = 1, Status = newStatus, Role = role };

      _mockMaintenanceRepo.Setup(r => r.GetMaintenanceRequestByIdAsync(updateDto.Id))
                          .ReturnsAsync(existingRequest);

      var exception = await Assert.ThrowsAsync<MockAuthException>(
          () => _service.UpdateRequestAsync(updateDto, "webRootPath", "imageFolder")
      );

      _mockMaintenanceRepo.Verify(r => r.GetMaintenanceRequestByIdAsync(updateDto.Id), Times.Once);
      _mockMapper.Verify(m => m.Map(It.IsAny<UpdateMaintenanceRequestDto>(), It.IsAny<MaintenanceRequest>()), Times.Never); 
    }

    [Fact]
    public async Task UpdateRequestAsync_NoNewImageAndNoExistingImage_UpdatesRepositoryWithoutFileOperations()
    {
      
      var existingRequest = new MaintenanceRequest { Id = 1, MaintenanceEventName = "Original", ImageUrl = null, PropertyName="",Description=""};
      var updateDto = new UpdateMaintenanceRequestDto
      {
        Id = 1,
        MaintenanceEventName = "Updated",
        Image = null
      };

      _mockMaintenanceRepo.Setup(r => r.GetMaintenanceRequestByIdAsync(updateDto.Id))
                          .ReturnsAsync(existingRequest);

      _mockMapper.Setup(m => m.Map(updateDto, existingRequest))
                 .Callback<UpdateMaintenanceRequestDto, MaintenanceRequest>((src, dest) =>
                 {
                   dest.MaintenanceEventName = src.MaintenanceEventName;
                 });

      
      await _service.UpdateRequestAsync(updateDto, "webRootPath", "imageFolder");

      
      _mockMaintenanceRepo.Verify(r => r.GetMaintenanceRequestByIdAsync(updateDto.Id), Times.Once);
      _mockMapper.Verify(m => m.Map(updateDto, existingRequest), Times.Once);
      _mockFileService.Verify(f => f.DeleteFileAsync(It.IsAny<string>()), Times.Never);
      _mockFileService.Verify(f => f.SaveFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
      _mockMaintenanceRepo.Verify(r => r.UpdateMaintenanceRequestAsync(
          It.Is<MaintenanceRequest>(mr => mr.Id == 1 && mr.MaintenanceEventName == "Updated" && mr.ImageUrl == null)), Times.Once);
 
    }

    [Fact]
    public async Task UpdateRequestAsync_NoNewImageButExistingImage_UpdatesRepositoryWithoutFileOperations()
    {
      
      var existingImageUrl = "images/existing.jpg";
      var existingRequest = new MaintenanceRequest { Id = 1, MaintenanceEventName = "Original", ImageUrl = existingImageUrl, PropertyName="",Description="" };
      var updateDto = new UpdateMaintenanceRequestDto
      {
        Id = 1,
        MaintenanceEventName = "Updated",
        Image = null
      };

      _mockMaintenanceRepo.Setup(r => r.GetMaintenanceRequestByIdAsync(updateDto.Id))
                          .ReturnsAsync(existingRequest);
      _mockMapper.Setup(m => m.Map(updateDto, existingRequest))
                 .Callback<UpdateMaintenanceRequestDto, MaintenanceRequest>((src, dest) =>
                 {
                   dest.MaintenanceEventName = src.MaintenanceEventName;
                 });

      
      await _service.UpdateRequestAsync(updateDto, "webRootPath", "imageFolder");

      
      _mockMaintenanceRepo.Verify(r => r.GetMaintenanceRequestByIdAsync(updateDto.Id), Times.Once);
      _mockMapper.Verify(m => m.Map(updateDto, existingRequest), Times.Once);
      _mockFileService.Verify(f => f.DeleteFileAsync(It.IsAny<string>()), Times.Never); 
      _mockFileService.Verify(f => f.SaveFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
      _mockMaintenanceRepo.Verify(r => r.UpdateMaintenanceRequestAsync(
          It.Is<MaintenanceRequest>(mr => mr.Id == 1 && mr.MaintenanceEventName == "Updated" && mr.ImageUrl == existingImageUrl)), Times.Once);
    }

    [Fact]
    public async Task UpdateRequestAsync_WithNewImageAndExistingImage_DeletesOldAndSavesNewImage()
    {
      // Arrange
      var existingImageUrl = "images/old-image.jpg";
      var existingRequest = new MaintenanceRequest { Id = 1, MaintenanceEventName = "Original", ImageUrl = existingImageUrl, PropertyName = "", Description = "" };

      var mockFormFile = new Mock<IFormFile>();
      var newFileName = "new-image.jpg";
      

      mockFormFile.Setup(_ => _.FileName).Returns(newFileName);

      var updateDto = new UpdateMaintenanceRequestDto
      {
        Id = 1,
        MaintenanceEventName = "Updated with new image",
        Image = mockFormFile.Object
      };
      var newSavedImagePath = "imageFolder/new-image.jpg";

      _mockMaintenanceRepo.Setup(r => r.GetMaintenanceRequestByIdAsync(updateDto.Id))
                          .ReturnsAsync(existingRequest);
      _mockMapper.Setup(m => m.Map(updateDto, existingRequest))
                 .Callback<UpdateMaintenanceRequestDto, MaintenanceRequest>((src, dest) =>
                 {
                   dest.MaintenanceEventName = src.MaintenanceEventName;
                 });
      _mockFileService.Setup(f => f.SaveFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<string>()))
                      .ReturnsAsync(newSavedImagePath);
      _mockFileService.Setup(f => f.DeleteFileAsync(It.IsAny<string>()))
                      .Returns(Task.CompletedTask);

      await _service.UpdateRequestAsync(updateDto, "webRootPath", "imageFolder");

      _mockMaintenanceRepo.Verify(r => r.GetMaintenanceRequestByIdAsync(updateDto.Id), Times.Once);
      _mockMapper.Verify(m => m.Map(updateDto, existingRequest), Times.Once);
      _mockFileService.Verify(f => f.DeleteFileAsync(Path.Combine("webRootPath", "imageFolder", existingImageUrl)), Times.Once);
      _mockFileService.Verify(f => f.SaveFileAsync(mockFormFile.Object, "webRootPath", "imageFolder"), Times.Once);
      _mockMaintenanceRepo.Verify(r => r.UpdateMaintenanceRequestAsync(
          It.Is<MaintenanceRequest>(mr => mr.Id == 1 && mr.MaintenanceEventName == "Updated with new image" && mr.ImageUrl == newSavedImagePath)), Times.Once);
    }

  }

}
