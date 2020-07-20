using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDepartmentManager.Models;
using ApiDepartmentManager.Responses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiDepartmentManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        // GET: api/<DepartmentController>
        [HttpGet]
        public List<DepartmentModel> Get()
        {
            return new DepartmentModel().GetAll();
        }

        // GET api/<DepartmentController>/5
        [HttpGet("{id}")]
        public DepartmentModel Get(int id)
        {
            return new DepartmentModel().GetById(id);
        }

        // POST api/<DepartmentController>
        [HttpPost]
        public ApiResponse Post([FromBody] DepartmentModel value)
        {
            return value.Insert();
        }

        // PUT api/<DepartmentController>/5
        [HttpPut]
        public ApiResponse Put([FromBody] DepartmentModel value)
        {
            return value.Update();
        }

        // DELETE api/<DepartmentController>/5
        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {
            return new DepartmentModel().Delete(id);
        }
    }
}
