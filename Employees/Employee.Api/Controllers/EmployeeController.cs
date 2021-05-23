using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Employee.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EmployeeEntity>> Get()
        {
            try
            {
                return Ok(this._employeeService.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult<EmployeeEntity> Add(EmployeeEntity employee)
        {
            try
            {
                this._employeeService.Add(employee);
                return Ok(employee);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpPut]
        public ActionResult<EmployeeEntity> Update(EmployeeEntity employee)
        {
            try
            {
                var foundEmp = this._employeeService.Get(employee.Id);
                if (!$"{foundEmp.FirstName} {foundEmp.LastName}".Equals($"{employee.FirstName} {employee.LastName}"))
                {
                    this._employeeService.PublishUpdateEmployeeEvent(employee);
                }
                this._employeeService.Update(employee);
                return Ok(employee);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpPut]
        public ActionResult<EmployeeEntity> Delete(EmployeeEntity employee)
        {
            try
            {
                this._employeeService.Delete(employee);
                return Ok(employee);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpDelete]
        public ActionResult<EmployeeEntity> Delete(int id)
        {
            try
            {
                this._employeeService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}