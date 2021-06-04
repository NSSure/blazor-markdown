using Blazor.Markdown.Core.DAL.Repository;
using Blazor.Markdown.Shared.Model;
using Blazor.Markdown.Shared.Model.Response;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.Markdown.Core.Mediator.Handler
{
    public class DiagramQueryHandler : IRequestHandler<QueryRequest<DiagramQueryResponse>, DiagramQueryResponse>
    {
        public readonly DiagramRepository DiagramRepository;

        public DiagramQueryHandler(DiagramRepository diagramRepository)
        {
            this.DiagramRepository = diagramRepository;
        }

        public async Task<DiagramQueryResponse> Handle(QueryRequest<DiagramQueryResponse> request, CancellationToken cancellationToken)
        {
            List<DiagramModel> _diagramModels = await this.DiagramRepository.ListAll(x => new DiagramModel()
            {
                Id = x.Id,
                Name = x.Name,
                Tags = x.Tags,
                DateAdded = x.DateAdded,
                DateLastUpdated = x.DateLastUpdated
            });

            return new DiagramQueryResponse()
            {
                DiagramModels = _diagramModels
            };
        }
    }
}
