namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.ResponseModels
{
    public class UserPaginationResponseModel
    {
        public IEnumerable<UserResponseModel> Users { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => Users.Count() == PageSize;
        public string PreviousPageUrl => HasPreviousPage ? $"/User?page={CurrentPage - 1}&size={PageSize}" : "";
        public string NextPageUrl => HasNextPage ? $"/User?page={CurrentPage + 1}&size={PageSize}" : "";
    }
}
