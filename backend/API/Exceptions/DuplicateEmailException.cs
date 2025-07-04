namespace API.Exceptions
{
    public class DuplicateEmailException : AppException
    {
        public DuplicateEmailException(string email)
            : base($"Já existe um contato com o e-mail '{email}'.") { }
    }

}
