using IranTimeFlow.WebApp.Models;
using MediatR;

namespace IranTimeFlow.WebApp.Commands
{
    public class EditTimelineCommand : IRequest
    {
        public TimelineEntity Model { get; }

        public EditTimelineCommand(TimelineEntity model) =>
            Model = model;
    }
}
