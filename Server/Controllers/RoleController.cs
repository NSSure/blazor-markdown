using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.Mediator.Query;
using Blazor.Markdown.Shared.Model.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.Markdown.Server.Controllers
{
    [ApiController]
    [Route("api/role")]
    public class RoleController : ControllerBase
    {
        public readonly IMediator Mediator;

        public RoleController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<List<User>>> List()
        {
            try
            {
                RoleQueryResponse _response = await this.Mediator.Send(new RoleQueryRequest());
                return StatusCode(200, _response.Roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
