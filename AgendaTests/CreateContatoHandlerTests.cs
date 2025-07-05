using API.Application.Commands;
using API.Application.Handlers;
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

    public class CreateContatoHandlerTests
    {
        [Fact]
        public async Task Deve_Criar_Contato_Quando_Command_For_Valido()
        {
            var mockRepo = new Mock<IContatoRepository>();
            var mockMapper = new Mock<IMapper>();
            var handler = new CreateContatoHandler(mockRepo.Object, mockMapper.Object);

            var command = new CreateContatoCommand
            {
                Nome = "João",
                Email = "joao@email.com",
                Telefone = "11999999999"
            };

            var id = await handler.Handle(command, CancellationToken.None);

            mockRepo.Verify(r => r.AddAsync(It.IsAny<Contato>()), Times.Once);
            Assert.NotEqual(Guid.Empty, id);
        }
    }
}
