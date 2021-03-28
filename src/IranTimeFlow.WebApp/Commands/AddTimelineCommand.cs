using IranTimeFlow.WebApp.Models;
using MediatR;

namespace IranTimeFlow.WebApp.Commands
{
    public class AddTimelineCommand : IRequest
    {
        public TimelineEntity Model { get; }

        public AddTimelineCommand(TimelineEntity model) =>
            Model = model;
    }
}
