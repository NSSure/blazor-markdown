using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Repository;
using Blazor.Markdown.Core.Mediator.Query;
using Blazor.Markdown.Shared.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.Markdown.Core.Mediator.Handler
{
    public class GetDiagramQueryHandler : IRequestHandler<GetDiagramQueryRequest, DiagramModel>
    {
        public readonly DiagramRepository DiagramRepository;

        public GetDiagramQueryHandler(DiagramRepository diagramRepository)
        {
            this.DiagramRepository = diagramRepository;
        }

        public async Task<DiagramModel> Handle(GetDiagramQueryRequest request, CancellationToken cancellationToken)
        {
            return await this.DiagramRepository.Get(request.Expression, request.Projection);
        }
    }
}
