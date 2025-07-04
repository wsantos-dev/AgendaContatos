namespace API.Exceptions
{
    public class DuplicatePhoneNumberException :AppException
    {
        public DuplicatePhoneNumberException(string phone)
            : base($"Já existe um contato com este telefone: '{phone}'") { }
      
    }
}
