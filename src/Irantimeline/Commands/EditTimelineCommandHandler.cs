using Irantimeline.Data;
using Irantimeline.Models;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Irantimeline.Commands
{
    public class EditTimelineCommandHandler : IRequestHandler<EditTimelineCommand>
    {
        private readonly ApplicationDbContext _repository;

        public EditTimelineCommandHandler(ApplicationDbContext repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(
            EditTimelineCommand request,
            CancellationToken ct)
        {
            await UpdateAsync(
                request.Model,
                ct,
                a => a.Approved,
                a => a.Content,
                a => a.Month,
                a => a.Published,
                a => a.Resources,
                a => a.Tags,
                a => a.Title,
                a => a.Year);

            return Unit.Value;
        }

        private async Task UpdateAsync(
            TimelineEntity model,
            CancellationToken ct = default,
            params Expression<Func<TimelineEntity, object>>[] updatedProperties)
        {
            var entity = await _repository.Timelines.FindAsync(new object[] { model.Id }, ct);

            var m = _repository.Entry(model);
            var e = _repository.Entry(entity);

            if (updatedProperties.Any())
                foreach (var property in updatedProperties)
                {
                    var currentPropertyName = GetMemberName(property.Body);

                    e.Property(currentPropertyName).CurrentValue =
                        m.Property(currentPropertyName).CurrentValue;

                    _repository.Entry(entity).Property(currentPropertyName).IsModified = true;
                }

            await _repository.SaveChangesAsync(true, ct);
        }

        private static string GetMemberName(Expression expression)
        {
            return expression.NodeType switch
            {
                ExpressionType.MemberAccess => ((MemberExpression)expression).Member.Name,
                ExpressionType.Convert => GetMemberName(((UnaryExpression)expression).Operand),
                _ => throw new NotSupportedException(expression.NodeType.ToString()),
            };
        }
    }
}
