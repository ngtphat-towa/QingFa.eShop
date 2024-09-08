using QingFa.EShop.Application.Core.Abstractions.Messaging;
using QingFa.EShop.Application.ExampleMetas.Models;

namespace QingFa.EShop.Application.ExampleMetas.Gets
{
    public class GetExampleMetaByIdQuery : IQuery<ExampleMetaResponse>
    {
        public Guid Id { get; set; }
    }
}
