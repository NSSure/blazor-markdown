using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Repository;
using Blazor.Markdown.Core.Mediator.Request;
using Blazor.Markdown.Shared.Model.Returns;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.Markdown.Core.Mediator.Handler
{
    public class DiagramFetchHandler : IRequestHandler<DiagramFetchRequest, DiagramFetchResponse>
    {
        public readonly DiagramRepository DiagramRepository;

        public DiagramFetchHandler(DiagramRepository diagramRepository)
        {
            this.DiagramRepository = diagramRepository;
        }

        public async Task<DiagramFetchResponse> Handle(DiagramFetchRequest request, CancellationToken cancellationToken)
        {
            Diagram _diagram = (await this.DiagramRepository.Collection.FindAsync(new BsonDocument())).ToList()[0];

            return new DiagramFetchResponse()
            {
                Id = _diagram.Id,
                Name = _diagram.Name,
                Components = _diagram.Components,
                DateAdded = _diagram.DateAdded,
                DateLastUpdated = _diagram.DateLastUpdated
            };
        }
    }
}
