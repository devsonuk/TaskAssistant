﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAssistant.Api.Services;
using TaskAssistant.Api.Services.Interfaces;
using TaskAssistant.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskAssistant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PendingTasksController : ControllerBase
    {
        private readonly IPendingTaskService _pendingTaskService;

        public PendingTasksController(IPendingTaskService pendingTaskService)
        {
            _pendingTaskService = pendingTaskService;
        }


        // GET: api/<PendingTasksController>
        [HttpGet]
        public IEnumerable<PendingTask> Get()
        {
            return _pendingTaskService.GetPendingTasks();
        }

        // GET api/<PendingTasksController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PendingTasksController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PendingTasksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PendingTasksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
