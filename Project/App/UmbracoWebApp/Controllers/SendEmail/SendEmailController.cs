using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;
using UmbracoWebApp.Interfaces.SendEmail;
using UmbracoWebApp.Models.GeneralResponse;
using UmbracoWebApp.Models.SendEmail.Dtos;

namespace UmbracoWebApp.Controllers.SendEmail
{
    public class SendEmailController : SurfaceController
    {
        private readonly ISendEmail _IEmailSender;

        public SendEmailController(
            ISendEmail IEmailSender,
            IUmbracoContextAccessor umbracoContextAccessor,
            IUmbracoDatabaseFactory databaseFactory,
            ServiceContext services,
            AppCaches appCaches,
            IProfilingLogger profilingLogger,
            IPublishedUrlProvider publishedUrlProvider)
            : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _IEmailSender = IEmailSender;
        }


        [HttpPost(Name = "Send")]
        public async Task<IActionResult> Send([FromBody] EmailRequestDto emailRequestDto)
        {
            try
            {
                string body = $"Full Name: {emailRequestDto.FullName}<br>Message: {emailRequestDto.Message}";
                await _IEmailSender.SendEmailAsync(emailRequestDto.Email, emailRequestDto.Subject, body);

                GeneralResponseModel generalResponse = new ()
                {
                    ErrCode = "1",
                    ErrMsg = "Email sent successfully.",
                };
                
                return Ok(generalResponse);
            }
            catch (Exception ex)
            {
                GeneralResponseModel generalResponse = new ()
                {
                    ErrCode = "-1",
                    ErrMsg = $"{ex.Message}",
                };
                return BadRequest(generalResponse);
            }
        }

        [HttpPost(Name = "SendByUmbraco")]
        public async Task<IActionResult> SendByUmbraco([FromBody] EmailRequestDto emailRequestDto)
        {
            try
            {
                await _IEmailSender.SendEmailUmbraco(emailRequestDto);

                GeneralResponseModel generalResponse = new()
                {
                    ErrCode = "1",
                    ErrMsg = "Email sent successfully.",
                };

                return Ok(generalResponse);
            }
            catch (Exception ex)
            {
                GeneralResponseModel generalResponse = new()
                {
                    ErrCode = "-1",
                    ErrMsg = $"{ex.Message}",
                };
                return BadRequest(generalResponse);
            }
        }
    }
}
