using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Repository;
using Blazor.Markdown.Core.Mediator.Query;
using Blazor.Markdown.Shared.Model;
using Blazor.Markdown.Shared.Model.Response;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.Markdown.Core.Mediator.Handler
{
    public class RoleQueryHandler : IRequestHandler<RoleQueryRequest, RoleQueryResponse>
    {
        public class UnwoundRole
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Key { get; set; }
            public DAL.Entity.Action Action { get; set; }
            public string ActionKeys { get; set; }
            public DateTime DateAdded { get; set; }
        }


        public readonly RoleRepository RoleRepository;

        public RoleQueryHandler(RoleRepository roleRepository)
        {
            this.RoleRepository = roleRepository;
        }

        public async Task<RoleQueryResponse> Handle(RoleQueryRequest request, CancellationToken cancellationToken)
        {
            //var lookup = new BsonDocument("$lookup", new BsonDocument("from", "Action")).Add("localField", "Role.ActionIds").Add("foreignField", "_id").Add("as", "Role.Action";

            //this.RoleRepository.Collection.Aggregate().Unwind(x => x.ActionIds).Lookup<Action, RoleModel>(this.RoleRepository.Context.Action, )

            //this.RoleRepository.Collection.AsQueryable().GroupJoin(this.RoleRepository.Context.Action, a => a.ActionIds, a => a.Id, (outer, inner) => new RoleModel()
            //{
            //    Id = outer.Id,
            //    Name = outer.Name,
            //    Key = outer.Key,
            //    Actions = inner
            //    DateAdded = outer.DateAdded,
            //});

            //List<RoleModel> _roles = await this.RoleRepository.Collection.Aggregate()
            //    .Lookup<Role, Action>("Action", "ActionIds", "Id", "Actions")
            //    .Group(x => x.Id, a=>)
            //    .ToListAsync();

            //var unwound = var results = this.RoleRepository.Collection.Aggregate().Unwind<Role, UnwoundRole>(x => x.ActionIds);

            //var results = this.RoleRepository.Collection.Aggregate().Unwind<Role, UnwoundRole>(x => x.ActionIds).Group(x => x.Id, u => new
            //{
            //    Id = u.Key,
            //    Actions = x.
            //});

            //string.Empty;

            //List<RoleModel> _roles = await this.RoleRepository.Collection.Aggregate()
            //    .Lookup<Role, Action, RoleModel>(this.RoleRepository.Context.Action, x => x.Id, x => x.RoleIds, x => x.Actions)
            //    .Unwind<Action, RoleModel>(x => x.)
            //    .ToListAsync();

            //var t = await this.RoleRepository.Collection.Aggregate().Unwind<Role, UnwoundRole>(x => x.ActionKeys).ToListAsync();

            //var test = await this.RoleRepository.Collection.Aggregate()
            //    .Match(x => x.ActionKeys != null)
            //    .Unwind<Role, UnwoundRole>(x => x.ActionKeys)
            //    .Lookup<UnwoundRole, DAL.Entity.Action, UnwoundRole>(this.RoleRepository.Context.Action, a => a.ActionKeys, a => a.Key, a => a.Action).ToListAsync();

            //var tt = await this.RoleRepository.Collection.Aggregate()
            //    .Unwind<Role, UnwoundRole>(x => x.ActionKeys)
            //    .Lookup<UnwoundRole, DAL.Entity.Action, UnwoundRole>(this.RoleRepository.Context.Action, a => a.ActionKeys, a => a.Key, a => a.Action)
            //    .Group(uwr => uwr.Id, g => new RoleModel()
            //    {
            //        Id = g.First().Id,
            //        Key = g.First().Key,
            //        Name = g.First().Name,
            //        DateAdded = g.First().DateAdded,
            //        Actions = g.Select(xx => new ActionModel()
            //        { 
            //            Id = xx.Action.Id,
            //            Name = xx.Action.Name,
            //            Key = xx.Action.Key,
            //            DateAdded = xx.Action.DateAdded
            //        })
            //        .ToList()
            //    })
            //    .ToListAsync();


            //            var tt = await this.RoleRepository.Collection.Aggregate().Unwind<Role, UnwoundRole>(x => x.ActionKeys).Lookup<UnwoundRole, DAL.Entity.Action, RoleModel>(this.RoleRepository.Context.Action, a => a.ActionKeys, a => a.Key, a => new
            //            {

            //            }).Group<UnwoundRole, Guid, RoleModel>(uwr => uwr.Id, g => new RoleModel()
            //            {
            //                ActionKeys = g.Select(x => x.ActionKeys).ToList()
            //            })
            //.ToListAsync();

            List<RoleModel> _diagramModels = await this.RoleRepository.ListAll(x => new RoleModel()
            {
                Id = x.Id,
                Name = x.Name,
                Key = x.Key,
                DateAdded = x.DateAdded
            });

            return new RoleQueryResponse()
            {
                Roles = _diagramModels
            };
        }
    }
}
