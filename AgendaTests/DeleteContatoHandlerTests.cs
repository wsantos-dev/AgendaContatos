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

namespace AgendaTests
{
    public class DeleteContatoHandlerTests
    {
        [Fact]
        public async Task Deve_Deletar_Contato_Quando_Existir()
        {
            var id = Guid.NewGuid();

            var contato = new Contato { Nome = "José", Email = "josé@email.com", Telefone = "11999999999" };

            var repoMock = new Mock<IContatoRepository>();
            repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(contato);
            repoMock.Setup(r => r.DeleteAsync(contato)).Returns(Task.CompletedTask);

            var handler = new DeleteContatoHandler(repoMock.Object);

            var command = new DeleteContatoCommand(id);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);
            repoMock.Verify(r => r.DeleteAsync(contato), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_False_Se_Contato_Nao_Existir()
        {
            var id = Guid.NewGuid();

            var repoMock = new Mock<IContatoRepository>();
            repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Contato?)null);

            var handler = new DeleteContatoHandler(repoMock.Object);

            var command = new DeleteContatoCommand(id);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.False(result);
            repoMock.Verify(r => r.DeleteAsync(It.IsAny<Contato>()), Times.Never);
        }
    }
}
