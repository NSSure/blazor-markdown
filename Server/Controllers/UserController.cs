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
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        public readonly IMediator Mediator;

        public UserController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<List<User>>> List([FromBody] UserQueryOptions queryOptions)
        {
            try
            {
                UserQueryResponse _response = await this.Mediator.Send(new UserQueryRequest(queryOptions));
                return StatusCode(200, _response.Users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
