using API.DTOs;
using API.Models;
using API.Repositories;
using API.Services;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;

public class ContatoServiceTests
{
    private readonly Mock<IContatoRepository> _repositoryMock;
    private readonly IMapper _mapper;
    private readonly ContatoService _service;

    public ContatoServiceTests()
    {
        _repositoryMock = new Mock<IContatoRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Contato, ContatoReadDTO>();
            cfg.CreateMap<ContatoCreateDTO, Contato>();
        });
        _mapper = config.CreateMapper();

        _service = new ContatoService(_repositoryMock.Object, _mapper);
    }

    [Fact]
    public async Task GetAllAsync_DeveRetornarListaDeContatos()
    {
        var contatos = new List<Contato>
        {
            new Contato { Id = Guid.NewGuid(), Nome = "João", Email = "joao@email.com", Telefone = "11999999999" }
        };

        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(contatos);

        var result = (await _service.GetAllAsync()).ToList();

        Assert.Single(result);
        Assert.Equal("João", result[0].Nome);
    }

    [Fact]
    public async Task GetByIdAsync_DeveRetornarContato_QuandoExistir()
    {
        var id = Guid.NewGuid();
        var contato = new Contato { Id = id, Nome = "Maria", Email = "maria@email.com", Telefone = "11988888888" };

        _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(contato);

        var result = await _service.GetByIdAsync(id);

        Assert.NotNull(result);
        Assert.Equal("Maria", result!.Nome);
    }

    [Fact]
    public async Task AddAsync_DeveExecutarSemExcecao()
    {
        var dto = new ContatoCreateDTO { Nome = "Carlos", Email = "carlos@email.com", Telefone = "11977777777" };

        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Contato>())).Returns(Task.CompletedTask);

        await _service.CreateAsync(dto); 
    }

    [Fact]
    public async Task DeleteAsync_DeveExecutarSemExcecao()
    {
        var id = Guid.NewGuid();
        var contato = new Contato { Id = id, Nome = "Fulano", Email = "f@f.com", Telefone = "11988888888" };

        _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(contato);
        _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Contato>())).Returns(Task.CompletedTask);

        await _service.DeleteAsync(id); 
    }

    [Fact]
    public async Task UpdateAsync_DeveExecutarSemExcecao()
    {
        var id = Guid.NewGuid();
        var dto = new ContatoCreateDTO { Nome = "Novo Nome", Email = "novo@email.com", Telefone = "11966666666" };
        var contatoExistente = new Contato { Id = id, Nome = "Antigo", Email = "a@a.com", Telefone = "11900000000" };

        _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(contatoExistente);
        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Contato>())).Returns(Task.CompletedTask);

        await _service.UpdateAsync(id, dto); 
    }
}
