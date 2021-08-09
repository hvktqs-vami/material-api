using System.Threading.Tasks;
using Mta.Vami.Models.TokenAuth;
using Mta.Vami.Web.Controllers;
using Shouldly;
using Xunit;

namespace Mta.Vami.Web.Tests.Controllers
{
    public class HomeController_Tests: VamiWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}