using API.DTOs;
using MediatR;

namespace API.Application.Queries
{
    public class GetAllContatosQuery : IRequest<IEnumerable<ContatoReadDTO>>
    {
    }
}
