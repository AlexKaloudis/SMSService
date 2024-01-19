using Moq;
using SMSApi.Models;
using SMSApi.SMS.Repository;
using SMSApi.SMS.Strategy;

namespace SMSUnitTests
{
    public class SMSVendorCYTests
    {
        private Mock<ISMSRepository> _mockRepo;
        private SMSVendorCY _vendor;

        public SMSVendorCYTests()
        {
            _mockRepo = new Mock<ISMSRepository>();
            _vendor = new SMSVendorCY(_mockRepo.Object);
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

        [Theory]
        [InlineData("Hello world", 5, false, new string[] { "Hello", "world" })]
        [InlineData("Hello world", 10, false, new string[] { "Hello worl", "d" })]
        public void Split_WithValidStringAndDesiredLength_ReturnsCorrectChunks(string value, int desiredLength, bool strict, string[] expectedChunks)
        {
            // Act
            var result = _vendor.Split(value, desiredLength, strict);

            // Assert
            Assert.Equal(expectedChunks.Length, result.Length);
            for (int i = 0; i < expectedChunks.Length - 1; i++)
            {
                Assert.Equal(expectedChunks[i], result[i]);
            }
            for (int i = 0; i < expectedChunks.Length - 1; i++)
            {
                Assert.True(expectedChunks[i].Length <= desiredLength);
            }
            
        }
    }
}
