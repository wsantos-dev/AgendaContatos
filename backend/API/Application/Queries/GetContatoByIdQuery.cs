using API.DTOs;
using MediatR;

namespace API.Application.Queries
{
    public class GetContatoByIdQuery : IRequest<ContatoReadDTO>
    {
        public Guid Id { get; }

        public GetContatoByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
