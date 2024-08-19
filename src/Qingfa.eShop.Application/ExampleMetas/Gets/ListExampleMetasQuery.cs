using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.ExampleMetas.Models;

namespace QingFa.EShop.Application.ExampleMetas.Gets
{
    public class ListExampleMetasQuery : IRequest<PaginatedList<ExampleMetaResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Name { get; set; } 
        public string? CreatedBy { get; set; }
        public string SortField { get; set; } = "Name"; // Default sort field
        public Guid? Id { get; set; }
        public bool SortDescending { get; set; }
    }
}

