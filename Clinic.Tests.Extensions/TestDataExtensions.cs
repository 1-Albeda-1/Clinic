using Clinic.Context.Contracts.Models;

namespace Clinic.Tests.Extensions
{
    public static class TestDataExtensions
    {
        public static void BaseAuditSetParamtrs<TEntity>(this TEntity entity) where TEntity : BaseAuditEntity
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTimeOffset.UtcNow;
            entity.CreatedBy = $"CreatedBy{Guid.NewGuid():N}";
            entity.UpdatedAt = DateTimeOffset.UtcNow;
            entity.UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}";
        }
    }
}
