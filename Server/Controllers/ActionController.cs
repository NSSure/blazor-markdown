using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.Mediator.Request;
using Blazor.Markdown.Shared.Model.Options;
using Blazor.Markdown.Shared.Model.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.Markdown.Server.Controllers
{
    [ApiController]
    [Route("api/action")]
    public class ActionController : ControllerBase
    {
        public readonly IMediator Mediator;

        public ActionController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        [HttpPost]
        [Route("user/remove")]
        public async Task<ActionResult<List<User>>> RemoveUserAction([FromBody] UserActionRemovalOptions removalOptions)
        {
            try
            {
                UserActionRemovalResponse _response = await this.Mediator.Send(new UserActionRemovalRequest(removalOptions));
                return StatusCode(200, _response.IsRemoved);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
