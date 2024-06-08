namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Handlers
{
    public class HttpResponseException : Exception
    {
        public HttpResponseException(int statusCode, object? value = null) =>
            (StatusCode, Value) = (statusCode, value);

        public int StatusCode { get; set; }
        public object? Value { get; set; }
    }
}
