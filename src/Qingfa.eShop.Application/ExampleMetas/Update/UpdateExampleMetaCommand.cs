using MediatR;

using QingFa.EShop.Application.Core.Models;

namespace QingFa.EShop.Application.ExampleMetas.Update
{
    public class UpdateExampleMetaCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string LastModifiedBy { get; set; } = default!;
    }
}
