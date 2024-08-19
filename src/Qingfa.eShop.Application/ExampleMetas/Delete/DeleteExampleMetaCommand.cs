using MediatR;

using QingFa.EShop.Application.Core.Models;

namespace QingFa.EShop.Application.ExampleMetas.Delete
{
    public class DeleteExampleMetaCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
