﻿namespace QingFa.EShop.Domain.Core.Entities
{
    public interface IAuditable
    {
        DateTimeOffset Created { get; set; }
        string? CreatedBy { get; set; }
        DateTimeOffset LastModified { get; set; }
        string? LastModifiedBy { get; set; }
    }
}
