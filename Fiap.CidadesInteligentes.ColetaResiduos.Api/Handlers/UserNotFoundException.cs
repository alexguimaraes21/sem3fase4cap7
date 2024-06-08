using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Handlers
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(int StatusCode, object? value = null) =>
            (StatusCode, Value) = (StatusCode, value);

        public int StatusCode { get; }
        public object? Value { get; }
    }
}
