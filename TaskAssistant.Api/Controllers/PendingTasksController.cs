using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskAssistant.Api.Services.Interfaces;
using TaskAssistant.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskAssistant.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PendingTasksController : BaseController
    {
        private readonly IPendingTaskService _pendingTaskService;

        public PendingTasksController(IPendingTaskService pendingTaskService)
        {
            _pendingTaskService = pendingTaskService;
        }


        // GET: api/<PendingTasksController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _pendingTaskService.GetAll();
            return new JsonResult(result);
        }

        // GET api/<PendingTasksController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _pendingTaskService.Get(id);
            return new JsonResult(result);
        }

        // POST api/<PendingTasksController>
        [HttpPost]
        public IActionResult Post([FromBody] PendingTask pendingTask)
        {
            var result = _pendingTaskService.Add(pendingTask);
            return new JsonResult(result);
        }

        // PUT api/<PendingTasksController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PendingTask pendingTask)
        {
            if (pendingTask.Id != id)
            {
                throw new ArgumentException("Invalid Id", nameof(id));
            }

            var result = _pendingTaskService.Update(pendingTask);
            return new JsonResult(result);
        }

        // DELETE api/<PendingTasksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _pendingTaskService.Delete(id);
        }
    }
}
