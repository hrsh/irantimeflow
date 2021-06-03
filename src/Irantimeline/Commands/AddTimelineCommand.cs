using Irantimeline.Models;
using MediatR;

namespace Irantimeline.Commands
{
    public class AddTimelineCommand : IRequest
    {
        public TimelineEntity Model { get; }

        public AddTimelineCommand(TimelineEntity model) =>
            Model = model;
    }
}
