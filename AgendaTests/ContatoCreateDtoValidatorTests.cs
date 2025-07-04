using API.DTOs;
using API.Validators;

namespace AgendaTests
{
    public class ContatoCreateDtoValidatorTests
    {
        private readonly ContatoCreateDtoValidator _validator = new();

        [Fact]
        public void Deve_Falhar_Se_Nome_Estiver_Vazio()
        {
            var dto = new ContatoCreateDTO { Nome = "", Email = "joao.silva@email.com", Telefone = "11912345678" };
            var result = _validator.Validate(dto);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Nome");
        }

        [Fact]
        public void Deve_Falhar_Se_Email_Estiver_Vazio()
        {
            var dto = new ContatoCreateDTO { Nome = "João Silva", Email = "", Telefone = "11912345678" };
            var result = _validator.Validate(dto);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Email");
        }

        [Theory]
        [InlineData("joao@email")]
        [InlineData("joao@.com")]
        [InlineData("joao@email..com")]
        [InlineData("joao@email@com")]
        public void Deve_Falhar_Se_Email_For_Invalido(string email)
        {
            var dto = new ContatoCreateDTO { Nome = "João Silva", Email = email, Telefone = "11912345678" };
            var result = _validator.Validate(dto);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Email");
        }

        [Fact]
        public void Deve_Falhar_Se_Telefone_For_Invalido()
        {
            var dto = new ContatoCreateDTO { Nome = "João Silva", Email = "joao.silva@email.com", Telefone = "123456" };
            var result = _validator.Validate(dto);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Telefone");
        }

        [Theory]
        [InlineData("1191234567")]   // 10 dígitos, DDD válido
        [InlineData("11912345678")]  // 11 dígitos, DDD válido
        public void Deve_Passar_Para_Telefone_Valido(string telefone)
        {
            var dto = new ContatoCreateDTO
            {
                Nome = "João Silva",
                Email = "joao.silva@email.com",
                Telefone = telefone
            };
            var result = _validator.Validate(dto);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Deve_Passar_Para_Entrada_Valida()
        {
            var dto = new ContatoCreateDTO
            {
                Nome = "João Silva",
                Email = "joao.silva@email.com",
                Telefone = "11998765432"
            };

            var result = _validator.Validate(dto);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Deve_Falhar_Se_Nome_Tiver_Menos_De_3_Caracteres()
        {
            var dto = new ContatoCreateDTO { Nome = "Jo", Email = "joao@email.com", Telefone = "11912345678" };
            var result = _validator.Validate(dto);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Nome");
        }

        [Fact]
        public void Deve_Falhar_Se_Telefone_Tiver_DDD_Invalido()
        {
            var dto = new ContatoCreateDTO { Nome = "Teste", Email = "teste@email.com", Telefone = "1012345678" };
            var result = _validator.Validate(dto);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Telefone");
        }

    }
}