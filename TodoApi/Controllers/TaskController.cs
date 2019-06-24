using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoApi.Models.Entities;
using TodoApi.Models.Responses;
using TodoApi.Models.Requests;
using Task = TodoApi.Models.Entities.Task;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TaskController : ControllerBase
    {
        protected readonly ILogger Logger;
        protected readonly TodoDbContext DbContext;

        public TaskController(ILogger<TaskController> logger, TodoDbContext dbContext)
        {
            Logger = logger;
            DbContext = dbContext;
        }

        [HttpGet("Task")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetTasksAsync(int pageSize=10, int pageNumber=1)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetTaskAsync));

            var response = new PagedResponse<Task>();

            try
            {
                //Get the "propose" query from repository
                var query = DbContext.GetTasks();

                //Set pagins values
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;

                //Get the total rows
                response.ItemsCount = await query.CountAsync();

                //Get the specific page from database
                response.Model = await query.Paging(pageSize, pageNumber).ToListAsync();

                response.Message = string.Format("Page {0} of {1}, total of tasks: {2}.", pageNumber,
                    response.PageCount, response.ItemsCount);
                Logger?.LogInformation("The tasks have been retrieved successfully");
            }
            catch(Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GetTaskAsync), ex);
            }

            return response.ToHttpResponse();
        }

        [HttpGet("Task/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetTaskAsync(int id)
        {
            Logger?.LogDebug("'{0} has been invoked", nameof(GetTaskAsync));

            var response = new SingleResponse<Task>();

            try
            {
                response.Model = await DbContext.GetTaskAsync(new Task(id));
            }
            catch(Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GetTaskAsync), ex);
            }

            return response.ToHttpResponse();
        }

        public async Task<IActionResult> PostTaskAsync([FromBody]PostTaskRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PostTaskAsync));

            var response = new SingleResponse<Task>();

            try
            {
                var existingEntity = await DbContext.GetTaskByNameAsync(new Task { Name = request.Name });
                if (existingEntity != null)
                    ModelState.AddModelError("TaskName", "Task name already exists");

                if (!ModelState.IsValid)
                    return BadRequest();

                var entity = request.ToEntity();

                DbContext.Add(entity);

                await DbContext.SaveChangesAsync();

                response.Model = entity;
            }
            catch(Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PostTaskAsync), ex);
            }
            return response.ToHttpResponse();
        }

        [HttpPut("Task/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutTaskAsync(int id, [FromBody]PutTaskRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PutTaskAsync));
            var response = new Response();

            try
            {
                var entity = await DbContext.GetTaskAsync(new Task(id));

                if (entity == null)
                    return NotFound();

                entity.Name = request.Name;

                DbContext.Update(entity);

                await DbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PutTaskAsync), ex);
            }

            return response.ToHttpResponse();
        }

        public async Task<IActionResult> DeleteTaskAsync(int id)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(DeleteTaskAsync));

            var response = new Response();

            try
            {
                var entity = await DbContext.GetTaskAsync(new Task(id));

                if (entity == null)
                    return NotFound();

                DbContext.Remove(entity);
                await DbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(DeleteTaskAsync), ex);
            }

            return response.ToHttpResponse();
        }
    }
}