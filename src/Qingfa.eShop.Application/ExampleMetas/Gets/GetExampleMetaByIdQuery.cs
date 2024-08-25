using MediatR;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.ExampleMetas.Models;

namespace QingFa.EShop.Application.ExampleMetas.Gets
{
    public class GetExampleMetaByIdQuery : IRequest<Result<ExampleMetaResponse>>
    {
        public Guid Id { get; set; }
    }
}
