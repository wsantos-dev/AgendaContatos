using API.Application.Handlers;
using API.Application.Queries;
using API.DTOs;
using API.Models;
using API.Repositories;
using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaTests
{
    public class GetAllContatosHandlerTests
    {
        [Fact]
        public async Task Deve_Retornar_Todos_Os_Contatos()
        {
            var contatos = new List<Contato>
            {
                new Contato
                {
                    Nome = "João",
                    Email = "joao@email.com",
                    Telefone = "11999999999" 
                },

                new Contato
                {
                    Nome = "Maria",
                    Email = "maria@email.com",
                    Telefone = "11988888888"
                }
            };

            var contatosDto = contatos.Select(c => new ContatoReadDTO
            {
                Id = c.Id,
                Nome = c.Nome!,
                Email = c.Email!,
                Telefone = c.Telefone!
            });


            var mockRepo = new Mock<IContatoRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(contatos);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<IEnumerable<ContatoReadDTO>>(contatos)).Returns(contatosDto);

            var handler = new GetAllContatosHandler(mockRepo.Object, mockMapper.Object);

            var result = await handler.Handle(new GetAllContatosQuery(), CancellationToken.None);

            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.Nome == "João");
            Assert.Contains(result, c => c.Nome == "Maria");
        }
    }
}
