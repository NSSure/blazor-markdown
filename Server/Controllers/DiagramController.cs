using Blazor.Markdown.Core.Mediator;
using Blazor.Markdown.Core.Mediator.Request;
using Blazor.Markdown.Shared.Model;
using Blazor.Markdown.Shared.Model.Response;
using Blazor.Markdown.Shared.Model.Returns;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        [Route("list")]
        public async Task<ActionResult<List<DiagramModel>>> ListDiagrams()
        {
            try
            {
                DiagramQueryResponse _response = await this.Mediator.Send(new QueryRequest<DiagramQueryResponse>());
                return StatusCode(200, _response.DiagramModels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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
