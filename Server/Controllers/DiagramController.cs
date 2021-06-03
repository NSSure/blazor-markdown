using Blazor.Markdown.Core.Mediator.Request;
using Blazor.Markdown.Shared.Model.Returns;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Blazor.Markdown.Server.Controllers
{
    [ApiController]
    [Route("api/diagram")]
    public class DiagramController : ControllerBase
    {
        public readonly IMediator Mediator;

        public DiagramController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        [HttpGet]
        [Route("get")]
        public async Task<ActionResult<DiagramFetchResponse>> GetDiagram()
        {
            try
            {
                DiagramFetchResponse _response = await this.Mediator.Send(new DiagramFetchRequest());
                return StatusCode(200, _response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
