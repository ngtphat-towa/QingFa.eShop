using ErrorOr;

using QingFa.EShop.Domain.DomainModels;

namespace Qingfa.EShop.Domain.DomainModels.Bases
{
    public abstract class Outbox : AggregateRoot<Guid> // Assuming Guid is the identifier
    {
        // Constructor
        protected Outbox(Guid id) : base(id)
        {
        }

        // Properties
        public string Type { get; set; } = string.Empty;
        public string AggregateType { get; set; } = string.Empty;
        public Guid AggregateId { get; set; }
        public byte[] Payload { get; set; } = Array.Empty<byte>();

        // Validation method
        public ErrorOr<bool> Validate()
        {
            if (Id == Guid.Empty)
            {
                return CoreErrors.NullArgument(nameof(Id));
            }

            if (string.IsNullOrWhiteSpace(Type))
            {
                return CoreErrors.ValidationError(nameof(Type), "Type cannot be null or empty.");
            }

            if (string.IsNullOrWhiteSpace(AggregateType))
            {
                return CoreErrors.ValidationError(nameof(AggregateType), "AggregateType cannot be null or empty.");
            }

            if (AggregateId == Guid.Empty)
            {
                return CoreErrors.NullArgument(nameof(AggregateId));
            }

            if (Payload == null || Payload.Length == 0)
            {
                return CoreErrors.ValidationError(nameof(Payload), "Payload cannot be null or empty.");
            }

            return true;
        }
    }
}
