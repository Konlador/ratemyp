using Moq;
using Moq.Protected;
using NUnit.Framework;
using RateMyP.Client.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RateMyP.Entities;

namespace RateMyP.Tests.Managers
    {
    public class TeacherManagerTests
        {
        //[Test]
        public async Task GetAllTeachers_NoTeacher()
            {
            // ARRANGE
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                    {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(new List<Teacher>()),
                        Encoding.UTF8,
                        "application/json"),
                    })
                .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
                {
                BaseAddress = new Uri("http://test.com/"),
                };

            var manager = new TeachersManager(httpClient);

            // ACT
            var result = await manager.GetAll();

            // ASSERT
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);

            // also check the 'http' call was like we expected it
            var expectedUri = new Uri("http://test.com/api/teachers");

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1), // we expected a single external request
                ItExpr.Is<HttpRequestMessage>(req =>
                                                  req.Method == HttpMethod.Get  // we expected a GET request
                                                  && req.RequestUri == expectedUri // to this uri
                ),
                ItExpr.IsAny<CancellationToken>()
            );
            }
        }
    }
