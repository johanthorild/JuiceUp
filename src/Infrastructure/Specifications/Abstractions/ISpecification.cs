using System.Linq.Expressions;

namespace Infrastructure.Specifications.Abstractions;
public interface ISpecification<TEntity>
{
    Expression<Func<TEntity, bool>>? Criteria { get; }
    Expression<Func<TEntity, object>>? OrderByExpression { get; }
    Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; }

    bool IsPagingEnabled { get; }
    int Take { get; }
    int Skip { get; }
}
