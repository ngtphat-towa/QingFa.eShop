using MediatR;

using QingFa.EShop.Application.ExampleMetas.Models;

namespace QingFa.EShop.Application.ExampleMetas.Gets
{
    public class GetExampleMetaByIdQuery : IRequest<ExampleMetaResponse>
    {
        public Guid Id { get; set; }
    }
}
