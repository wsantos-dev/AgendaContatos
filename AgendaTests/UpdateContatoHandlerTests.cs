using API.Application.Commands;
using API.Application.Handlers;
using API.Models;
using API.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AgendaTests
{
    public class UpdateContatoHandlerTests
    {
        [Fact]
        public async Task Deve_Atualizar_Contato_Se_Existir()
        {
            var id = Guid.NewGuid();

            var contato = new Contato
            {
                Nome = "Fulano",
                Email = "fulano@email.com",
                Telefone = "81999999999"
            };

            var mockRepo = new Mock<IContatoRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(contato);
            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Contato>())).Returns(Task.CompletedTask);

            var handler = new UpdateContatoHandler(mockRepo.Object);

            var command = new UpdateContatoCommand
            {
                Id = id,
                Nome = "Fulano Atualizado",
                Email = "novo@email.com",
                Telefone = "11988888888"
            };


            var resultado = await handler.Handle(command, CancellationToken.None);


            Assert.True(resultado);
            mockRepo.Verify(r => r.UpdateAsync(It.Is<Contato>(c =>
                c.Nome == command.Nome &&
                c.Email == command.Email &&
                c.Telefone == command.Telefone
            )), Times.Once);
        }
    }
}
