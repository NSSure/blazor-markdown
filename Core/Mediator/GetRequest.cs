using Blazor.Markdown.Core.DAL.Repository;
using MediatR;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.Markdown.Core.Mediator
{
    public class GetRequestHandler<TDocument, TResponse, TRepository> : IRequestHandler<GetRequest<TDocument, TResponse, TRepository>, TResponse>
    {
        public readonly TRepository Repository;

        public GetRequestHandler(TRepository repository)
        {
            this.Repository = repository;
        }

        public async Task<TResponse> Handle(GetRequest<TDocument, TResponse, TRepository> request, CancellationToken cancellationToken)
        {
            BaseRepository<TDocument> _repository = this.Repository as BaseRepository<TDocument>;
            return (TResponse)Activator.CreateInstance(typeof(TResponse), await _repository.Get(request.Expression));
        }
    }

    public class GetProjectionRequestHandler<TDocument, TProjection, TRepository> : IRequestHandler<GetProjectionRequest<TDocument, TProjection, TRepository>, TProjection>
    {
        public readonly TRepository Repository;

        public GetProjectionRequestHandler(TRepository repository)
        {
            this.Repository = repository;
        }

        public async Task<TProjection> Handle(GetProjectionRequest<TDocument, TProjection, TRepository> request, CancellationToken cancellationToken)
        {
            BaseRepository<TDocument> _repository = this.Repository as BaseRepository<TDocument>;
            return await _repository.Get(request.Expression, request.Projection);
        }
    }


    public class GetRequest<TDocument, TResponse, TRepository> : IRequest<TResponse>
    {
        public Expression<Func<TDocument, bool>> Expression { get; set; }

        public GetRequest(Expression<Func<TDocument, bool>> expression)
        {
            this.Expression = expression;
        }
    }

    public class GetProjectionRequest<TDocument, TProjection, TRepository> : IRequest<TProjection>
    {
        public Expression<Func<TDocument, bool>> Expression { get; set; }

        public Expression<Func<TDocument, TProjection>> Projection { get; set; }

        public GetProjectionRequest(Expression<Func<TDocument, bool>> expression, Expression<Func<TDocument, TProjection>> projection)
        {
            this.Expression = expression;
            this.Projection = projection;
        }
    }
}
