﻿using System.Collections.Concurrent;
using CRM_API.Sessions.Models;
using Domain.Client.CustomerAgregate.Data;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : CrmController
    {
        public CustomerController(ConcurrentDictionary<string, Session> sessions) : base(sessions)
        {
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (!this.UserAuthenticated)
                return Unauthorized($"Invalid token");

            var customerDomain = this.Container.GetService<IDomainClientAgregate<Customer, ReturnFlag>>();


            var selectAllAsync = await customerDomain!.SelectAllAsync();
            if (selectAllAsync.TryGetValue(out var list, out var alert))
            {
                return Ok(list);
            }
            else
            {
                return BadRequest(alert.ToString());
            }
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!this.UserAuthenticated)
                return Unauthorized($"Invalid token");

            var customerDomain = this.Container.GetService<IDomainClientAgregate<Customer, ReturnFlag>>();

            var selectAsync = await customerDomain!.SelectAsync(id);

            if (selectAsync.TryGetValue(out var customer, out var alert))
            {
                return Ok(customer);
            }
            else
            {
                return BadRequest(alert.ToString());
            }
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer value)
        {
            if (!this.UserAuthenticated)
                return Unauthorized($"Invalid token");

            var customerDomain = this.Container.GetService<IDomainClientAgregate<Customer, ReturnFlag>>();

            var insertAsync = await customerDomain!.InsertAsync(value);

            if (insertAsync.TryGetValue(out var customer, out var alert))
            {
                return Ok(customer);
            }
            else
            {
                return BadRequest(alert.ToString());
            }
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer value)
        {
            if (!this.UserAuthenticated)
                return Unauthorized($"Invalid token");

            var customerDomain = this.Container.GetService<IDomainClientAgregate<Customer, ReturnFlag>>();

            var updateAsync = await customerDomain!.UpdateAsync(value);

            if (updateAsync.TryGetValue(out var customer, out var alert))
            {
                return Ok(customer);
            }
            else
            {
                return BadRequest(alert.ToString());
            }
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!this.UserAuthenticated)
                return Unauthorized($"Invalid token");

            var customerDomain = this.Container.GetService<IDomainClientAgregate<Customer, ReturnFlag>>();

            var selectAsync = await customerDomain!.SelectAsync(id);

            if (selectAsync.TryGetValue(out var customer, out var alert))
            {
                var deleteAsync = await customerDomain.DeleteAsync(customer);

                if (deleteAsync.TryGetValue(out var deleted, out alert))
                {
                    return Ok(deleted);
                }
                else
                {
                    return BadRequest(alert.ToString());
                }
            }
            else
            {
                return BadRequest(alert.ToString());
            }
        }
    }
}
