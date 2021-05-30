using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.Mediator.Query;
using Blazor.Markdown.Core.Mediator.Request;
using Blazor.Markdown.Shared.Model.Options;
using Blazor.Markdown.Shared.Model.Returns;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.Markdown.Server.Controllers
{
    [ApiController]
    [Route("api/settings")]
    public class SettingsController : ControllerBase
    {
        public readonly IMediator Mediator;

        public SettingsController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<bool>> Add([FromBody] SettingsCreationOptions creationOptions)
        {
            try
            {
                SettingsCreationReturn _rtn = await this.Mediator.Send(new SettingsCreationRequest(creationOptions));
                return StatusCode(200, true);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<List<Settings>>> List()
        {
            try
            {
                SettingsQueryReturn _rtn = await this.Mediator.Send(new SettingsQueryRequest());
                return StatusCode(200, _rtn.Settings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
