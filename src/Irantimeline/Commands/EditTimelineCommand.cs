using Irantimeline.Models;
using MediatR;

namespace Irantimeline.Commands
{
    public class EditTimelineCommand : IRequest
    {
        public TimelineEntity Model { get; }

        public EditTimelineCommand(TimelineEntity model) =>
            Model = model;
    }
}
