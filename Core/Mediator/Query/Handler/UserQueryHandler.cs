using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Repository;
using Blazor.Markdown.Core.Mediator.Query;
using Blazor.Markdown.Shared.Model;
using Blazor.Markdown.Shared.Model.Options;
using Blazor.Markdown.Shared.Model.Returns;
using MediatR;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.Markdown.Core.Mediator.Handler
{
    public class UserQueryHandler : IRequestHandler<UserQueryRequest, UserQueryResponse>
    {
        public readonly UserRepository UserRepository;

        public UserQueryHandler(UserRepository userRepository)
        {
            this.UserRepository = userRepository;
        }

        public async Task<UserQueryResponse> Handle(UserQueryRequest request, CancellationToken cancellationToken)
        {
            FilterDefinition<User> _filter = FilterDefinition<User>.Empty;

            FilterDefinitionBuilder<User> _filterBuilder = Builders<User>.Filter;

            if(request.QueryOptions == null)
            {
                request.QueryOptions = new UserQueryOptions();
            }

            if (!string.IsNullOrEmpty(request.QueryOptions.Action))
            {
                FieldDefinition<User> _actionIdsField = "ActionIds";
                _filter = _filterBuilder.AnyEq(_actionIdsField, request.QueryOptions.Action);
            }

            // TODO: Is this the best practice for converting cursors into lists?
            List<User> _users = await (await this.UserRepository.Collection.FindAsync(_filter)).ToListAsync();

            return new UserQueryResponse()
            {
                Users = _users.Select(a => new UserModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    RoleIds = a.RoleIds,
                    ActionIds = a.ActionIds,
                    DateAdded = a.DateAdded,
                    DateLastUpdated = a.DateLastUpdated
                })
                .ToList()
            };
        }
    }
}
