using QingFa.EShop.Application.Core.Abstractions.Messaging;

namespace QingFa.EShop.Application.ExampleMetas.Delete
{
    public class DeleteExampleMetaCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
