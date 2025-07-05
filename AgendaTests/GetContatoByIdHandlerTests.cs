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
    public class GetContatoByIdHandlerTests
    {
        [Fact]
        public async Task Deve_Retornar_Contato_Quando_Id_Existir()
        {
            var contato = new Contato
            {
                Nome = "Maria",
                Email = "maria@email.com",
                Telefone = "11988888888"
            };

            var contatoDto = new ContatoReadDTO { Id = contato.Id, Nome = contato.Nome!, Email = contato.Email!, Telefone = contato.Telefone! };

            var mockRepo = new Mock<IContatoRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(contato.Id)).ReturnsAsync(contato);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<ContatoReadDTO>(contato)).Returns(contatoDto);

            var handler = new GetContatoByIdHandler(mockRepo.Object, mockMapper.Object);

            var query = new GetContatoByIdQuery(contato.Id);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(contatoDto.Nome, result.Nome);
        }
    }
}
