using System.ComponentModel.DataAnnotations;

using QingFa.EShop.Domain.DomainModels;

namespace Qingfa.EShop.Domain.DomainModels.Bases
{
    public abstract class Outbox : AggregateRoot<Guid>
    {
        public new Guid Id { get; set; } = Guid.NewGuid();
        public string Type { get; set; } = string.Empty;
        public string AggregateType { get; set; } = string.Empty;
        public Guid AggregateId { get; set; } = Guid.NewGuid();
        public byte[] Payload { get; set; } = Array.Empty<byte>();

        public bool Validate()
        {
            if (Guid.Empty == Id)
            {
                throw new ValidationException("Id of the Outbox entity couldn't be null.");
            }

            if (string.IsNullOrEmpty(Type))
            {
                throw new ValidationException("Type of the Outbox entity couldn't be null or empty.");
            }

            if (string.IsNullOrEmpty(AggregateType))
            {
                throw new ValidationException("AggregateType of the Outbox entity couldn't be null or empty.");
            }

            if (Guid.Empty == AggregateId)
            {
                throw new ValidationException("AggregateId of the Outbox entity couldn't be null.");
            }

            if (Payload is null || Payload.Length == 0)
            {
                throw new ValidationException("Payload of the Outbox entity couldn't be null or empty (should be an Avro format).");
            }

            return true;
        }
    }
}
