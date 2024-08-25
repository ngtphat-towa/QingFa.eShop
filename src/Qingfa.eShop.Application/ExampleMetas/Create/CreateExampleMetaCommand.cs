using QingFa.EShop.Application.Core.Abstractions.Messaging;

namespace QingFa.EShop.Application.ExampleMetas.Create
{
    public class CreateExampleMetaCommand : ICommand<Guid>
    {
        public string Name { get; set; } = default!;
        public string CreatedBy { get; set; } = default!;
    }
}
