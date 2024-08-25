using MediatR;

using QingFa.EShop.Application.Core.Models;

namespace QingFa.EShop.Application.ExampleMetas.Create
{
    public class CreateExampleMetaCommand : IRequest<Result<Guid>>
    {
        public string Name { get; set; } = default!;
        public string CreatedBy { get; set; } = default!;
    }
}
