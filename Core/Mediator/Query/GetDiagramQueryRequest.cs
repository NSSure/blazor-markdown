using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Shared.Model;
using MediatR;
using System;
using System.Linq.Expressions;

namespace Blazor.Markdown.Core.Mediator.Query
{
    public class GetDiagramQueryRequest : IRequest<DiagramModel>
    {
        public Expression<Func<Diagram, bool>> Expression { get; set; }

        public Expression<Func<Diagram, DiagramModel>> Projection { get; set; }

        public GetDiagramQueryRequest(Expression<Func<Diagram, bool>> expression, Expression<Func<Diagram, DiagramModel>> projection)
        {
            this.Expression = expression;
            this.Projection = projection;
        }
    }
}
