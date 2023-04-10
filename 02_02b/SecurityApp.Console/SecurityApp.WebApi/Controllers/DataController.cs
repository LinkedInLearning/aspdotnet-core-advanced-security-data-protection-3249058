using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurityApp.WebApi.Data;

namespace SecurityApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        public DataController(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }

        [HttpGet]
        public IActionResult Get()
        {
            string title = "Welcome to this course!";

            var dataProtector = _dataProtectionProvider.CreateProtector("DataController");
            var protectedTitle = dataProtector.Protect(title);
            var unProtectedTitle = dataProtector.Unprotect(protectedTitle);

            return Ok(new DataProtection()
            {
                Title = title,
                ProtectedTitle = protectedTitle,
                UnProtectedTitle = unProtectedTitle
            });
        }
    }
}
