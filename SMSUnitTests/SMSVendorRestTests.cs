using Moq;
using SMSApi.Models;
using SMSApi.SMS.Repository;
using SMSApi.SMS.Strategy;

namespace SMSUnitTests
{
    public class SMSVendorRestTests
    {
        private Mock<ISMSRepository> _mockRepo;
        private SMSVendorRest _vendor;

        public SMSVendorRestTests()
        {
            _mockRepo = new Mock<ISMSRepository>();
            _vendor = new SMSVendorRest(_mockRepo.Object);
        }

        [Fact]
        public void Send_WithValidSMS_CallsInsertSMSAndSaveOnRepository()
        {
            // Arrange
            var sms = new BasicSMS { Message = "Hello", PhoneNumber = "1234567890" };
            _mockRepo.Setup(m => m.InsertSMS(It.IsAny<BasicSMS>()));
            _mockRepo.Setup(m => m.Save());

            // Act
            _vendor.Send(sms);

            // Assert
            _mockRepo.Verify(m => m.InsertSMS(It.IsAny<BasicSMS>()), Times.Exactly(1));
            _mockRepo.Verify(m => m.Save(), Times.Exactly(1));
        }
    }
}
