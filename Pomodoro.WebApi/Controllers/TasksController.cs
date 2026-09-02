using Microsoft.AspNetCore.Mvc;
using Pomodoro.Application.Features.Tasks.Commands.AbandonToDoTask;
using Pomodoro.Application.Features.Tasks.Commands.CompleteToDoTask;
using Pomodoro.Application.Features.Tasks.Commands.CompleteToDoTaskWithSession;
using Pomodoro.Application.Features.Tasks.Commands.CreateToDoTask;
using Pomodoro.Application.Features.Tasks.Commands.DeleteToDoTask;
using Pomodoro.Application.Features.Tasks.Commands.IncrementTaskPomodoro;
using Pomodoro.Application.Features.Tasks.Commands.ToggleToDoTaskPriority;
using Pomodoro.Application.Features.Tasks.Commands.UpdateToDoTaskDetails;
using Pomodoro.Application.Features.Tasks.Common;
using Pomodoro.Application.Features.Tasks.Queries.GetActiveTasks;
using Pomodoro.Application.Features.Tasks.Queries.GetAllTasks;
using Pomodoro.Application.Features.Tasks.Queries.GetTask;
using Pomodoro.WebApi.Extensions;

namespace Pomodoro.WebApi.Controllers
{
    public sealed class TasksController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreateToDoTaskCommand command, CancellationToken cancellationToken)
        {
            var taskId = await Mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(Create), new { id = taskId }, taskId);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType<TaskDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTaskById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var verifiedUserId = User.GetUserId();
            var response = await Mediator.Send(new GetTaskQuery(id, verifiedUserId), cancellationToken);
            return Ok(response);
        }

        [HttpGet("active")]
        [ProducesResponseType<IEnumerable<TaskDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActiveTasks(CancellationToken cancellationToken)
        {
            var verifiedUserId = User.GetUserId();
            var response = await Mediator.Send(new GetActiveTaskQuery(verifiedUserId), cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType<IEnumerable<TaskDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllTask(CancellationToken cancellationToken)
        {
            var verifiedUserId = User.GetUserId();
            var response = await Mediator.Send(new GetAllTasksQuery(verifiedUserId), cancellationToken);
            return Ok(response);
        }

        [HttpPut("{id:guid}/complete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Complete(Guid id, CancellationToken cancellationToken)
        {
            var verifiedUserId = User.GetUserId();
            await Mediator.Send(new CompleteToDoTaskCommand(id, verifiedUserId), cancellationToken);
            return NoContent();
        }

        [HttpPut("{taskId:guid}/toggle-priority")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ToggleTaskPriority([FromRoute] Guid taskId, CancellationToken cancellationToken)
        {
            var verifiedUserId = User.GetUserId();
            await Mediator.Send(new ToggleToDoTaskPriorityCommand(taskId, verifiedUserId), cancellationToken);
            return NoContent();
        }

        [HttpPut("{taskId:guid}/abandon-task")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AbandonToDoTask([FromRoute]Guid taskId, CancellationToken cancellationToken)
        {
            var verifiedUserId = User.GetUserId();
            await Mediator.Send(new AbandonToDoTaskCommand(taskId, verifiedUserId),cancellationToken);
            return NoContent();
        }
        
        [HttpPut("{taskId:guid}/complete-with-session")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CompleteWithSession([FromRoute]Guid taskId, [FromBody] CompleteToDoTaskWithSessionCommand command,
            CancellationToken cancellationToken)
        {
            var verifiedUserId = User.GetUserId();
            command.ToDoTaskId = taskId;
            command.UserId = verifiedUserId;
            await Mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpPut("{id}/increment-pomodoro")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> IncrementPomodoro([FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var verifiedUserId = User.GetUserId();
            await Mediator.Send(new IncrementTaskPomodoroCommand(id, verifiedUserId), cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var verifiedUserId = User.GetUserId();
            await Mediator.Send(new DeleteToDoTaskCommand(id, verifiedUserId), cancellationToken);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateToDoTaskDetailsCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.TaskId)
                return BadRequest("Task ID mismatch between URL and request body.");
            await Mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
