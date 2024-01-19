using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SMSApi.Dtos;
using SMSApi.Models;
using SMSApi.SMS.Controller;
using SMSApi.SMS.Repository;
using SMSApi.SMS.Strategy.Interfaces;


namespace SMSUnitTests
{
    public class SMSControllerTests
    {
        private Mock<ISMSRepository> _mockRepo;
        private Mock<ISMSVendorContext> _mockVendorContext;
        private Mock<IMapper> _mockMapper;
        private SMSController _controller;

        public SMSControllerTests()
        {
            _mockRepo = new Mock<ISMSRepository>();
            _mockVendorContext = new Mock<ISMSVendorContext>();
            _mockMapper = new Mock<IMapper>();
            _controller = new SMSController( _mockVendorContext.Object, _mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task SendMessage_WithValidSMSRequest_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var smsRequest = new SMSRequest { CountryCode = "+30", Message = "Hello" };
            _mockMapper.Setup(m => m.Map<BasicSMS>(smsRequest)).Returns(new BasicSMS());
            _mockVendorContext.Setup(v => v.SetVendor(It.IsAny<ISMSVendor>()));
            _mockVendorContext.Setup(v => v.SendMessage(It.IsAny<BasicSMS>()));

            // Act
            var result = await _controller.SendMessage(smsRequest);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
            _mockMapper.Verify(m => m.Map<BasicSMS>(smsRequest), Times.Once);
            _mockVendorContext.Verify(v => v.SetVendor(It.IsAny<ISMSVendor>()), Times.Once);
            _mockVendorContext.Verify(v => v.SendMessage(It.IsAny<BasicSMS>()), Times.Once);
        }

    }
}
