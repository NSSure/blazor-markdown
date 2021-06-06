using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Repository;
using Blazor.Markdown.Core.Mediator;
using Blazor.Markdown.Core.Mediator.Request;
using Blazor.Markdown.Shared.Model;
using Blazor.Markdown.Shared.Model.Options;
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

        [HttpPost]
        [Route("component/add")]
        public async Task<ActionResult<List<DiagramModel>>> AddDiagramComponent([FromBody] ComponentCreationOptions creationOptions)
        {
            try
            {
                ComponentCreationResponse _response = await this.Mediator.Send(new CreationRequest<ComponentCreationOptions, ComponentCreationResponse>(creationOptions));
                return StatusCode(200, _response.Component);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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
        [Route("get/{diagramId}")]
        public async Task<ActionResult<DiagramFetchResponse>> GetDiagram([FromRoute] Guid diagramId)
        {
            try
            {
                DiagramModel _response = await this.Mediator.Send(new GetProjectionRequest<Diagram, DiagramModel, DiagramRepository>(a => a.Id == diagramId, x => new DiagramModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Tags = x.Tags,
                    DateAdded = x.DateAdded,
                    DateLastUpdated = x.DateLastUpdated
                }));

                return StatusCode(200, _response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
