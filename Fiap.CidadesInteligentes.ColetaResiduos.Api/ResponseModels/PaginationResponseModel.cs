namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.ResponseModels
{
    public class PaginationResponseModel<T>
    {
        public IEnumerable<T> List { get; set; }
        public string UrlBase { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => List.Count() == PageSize;
        public string PreviousPageUrl => HasPreviousPage ? $"/{UrlBase}?page={CurrentPage - 1}&size={PageSize}" : "";
        public string NextPageUrl => HasNextPage ? $"/{UrlBase}?page={CurrentPage + 1}&size={PageSize}" : "";
    }
}
