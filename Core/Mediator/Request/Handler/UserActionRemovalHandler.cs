using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Repository;
using Blazor.Markdown.Core.Mediator.Request;
using Blazor.Markdown.Shared.Model.Options;
using Blazor.Markdown.Shared.Model.Response;
using MediatR;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.Markdown.Core.Mediator.Handler
{
    public class UserActionRemovalHandler : IRequestHandler<UserActionRemovalRequest, UserActionRemovalResponse>
    {
        public readonly UserRepository UserRepository;

        public UserActionRemovalHandler(UserRepository userRepository)
        {
            this.UserRepository = userRepository;
        }

        public async Task<UserActionRemovalResponse> Handle(UserActionRemovalRequest request, CancellationToken cancellationToken)
        {
            if (request.Options == null)
            {
                request.Options = new UserActionRemovalOptions();
            }

            // & operator is overloaded. Create filter and update definitions.
            FilterDefinition<User> _filterDefinition = Builders<User>.Filter.Eq(x => x.Id, request.Options.UserId) & Builders<User>.Filter.Ne(x => x.Id, Guid.Empty);
            UpdateDefinition<User> _updateDefinition = Builders<User>.Update.Pull(x => x.ActionIds, request.Options.ActionId);

            UpdateResult _result = await this.UserRepository.Collection.UpdateOneAsync(_filterDefinition, _updateDefinition);

            await this.UserRepository.UpdateFilterAsync(_filterDefinition, _updateDefinition);

            return new UserActionRemovalResponse()
            {
                IsRemoved = _result.ModifiedCount == 1
            };
        }
    }
}
