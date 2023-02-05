using System.Linq.Expressions;

using Infrastructure.Specifications.Abstractions;

namespace Infrastructure.Specifications.NewFolder;
public abstract class BaseSpecification<TEntity> : ISpecification<TEntity>
{
    protected BaseSpecification(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }
    protected BaseSpecification()
    {
    }

    public Expression<Func<TEntity, bool>>? Criteria { get; }
    public Expression<Func<TEntity, object>>? OrderByExpression { get; private set; }
    public Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; private set; }

    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; } = false;

    protected virtual void ApplyOrderBy(
        Expression<Func<TEntity, object>> orderByExpression) =>
            OrderByExpression = orderByExpression;

    protected virtual void ApplyOrderByDescending(
        Expression<Func<TEntity, object>> orderByDescendingExpression) =>
            OrderByDescendingExpression = orderByDescendingExpression;

    protected virtual void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}