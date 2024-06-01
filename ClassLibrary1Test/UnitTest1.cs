using Xunit;
using ClassLibrary1;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;

namespace ClassLibrary1Test
{
    public class UnitTest1
    {
        private readonly IConfiguration _config;
        public UnitTest1()
        {
            _config = new ConfigurationBuilder()
                .AddUserSecrets<UnitTest1>()
                .Build();
        }

        [Fact]
        [Trait("Type", "Unit")]
        public void TestMethodWorks()
        {
            var api = new StudentMockApi(_config);

            var result = api.TestMethod(true);

            Assert.Equal(200, result);
        }

        [Fact]
        [Trait("Type", "Unit")]
        public void TestMethodDoesntWork()
        {
            var api = new StudentMockApi(_config);

            var result = api.TestMethod(false);

            Assert.Equal(404, result);
        }


        [Theory]
        [InlineData(true, 200)]
        [InlineData(false, 404)]
        [InlineData(null, 500)]
        [Trait("Type", "Unit")]
        public void TestMethodMultipleCases(bool? input, int expectedResult)
        {
            var api = new StudentMockApi(_config);

            var result = api.TestMethod(input);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        [Trait("Type", "Integration")]
        public async Task GetStudentDataTest()
        {
            var api = new StudentMockApi(_config);
            var result = await api.GetStudentData();
            
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            var listResult = result.ToList();
            Assert.Equal(10, result.Count());
            Assert.Equal(10, listResult.Count);
            Assert.IsType<Student>(listResult[0]);
            Assert.IsType<string>(listResult[0].Address.Number);
            Assert.True(listResult[0].Address.Number.GetType() == typeof(string));
        }
    }
}