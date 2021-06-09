using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Repository;
using Blazor.Markdown.Shared.Model;
using Blazor.Markdown.Shared.Model.Options;
using Blazor.Markdown.Shared.Model.Response;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.Markdown.Core.Mediator.Handler
{
    public class ComponentCreationHandler : IRequestHandler<CreationRequest<ComponentCreationOptions, ComponentCreationResponse>, ComponentCreationResponse>
    {
        public readonly DiagramRepository DiagramRepository;

        public ComponentCreationHandler(DiagramRepository diagramRepository)
        {
            this.DiagramRepository = diagramRepository;
        }

        public async Task<ComponentCreationResponse> Handle(CreationRequest<ComponentCreationOptions, ComponentCreationResponse> request, CancellationToken cancellationToken)
        {
            Diagram _diagram = await this.DiagramRepository.Get(x => x.Id == request.Options.DiagramId);

            if (_diagram == null)
            {
                throw new Exception("You cannot add a component to a diagram that does not exist");
            }

            Component _component = new Component()
            {
                Position = new Position()
                {
                    X = 500,
                    Y = 500,
                    Width = 200,
                    Height = 100
                }
            };

            // Add the new component to the components field array within the diagram document.
            await this.DiagramRepository.AddArrayItem(x => x.Id == request.Options.DiagramId, x => x.Components, _component);

            return new ComponentCreationResponse()
            { 
                Component = _component
            };
        }
    }
}
