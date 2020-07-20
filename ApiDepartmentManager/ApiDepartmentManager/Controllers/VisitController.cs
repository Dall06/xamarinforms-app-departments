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
    public class VisitController : ControllerBase
    {
        // GET: api/<VisitController>
        [HttpGet]
        public List<VisitModel> Get()
        {
            return new VisitModel().GetAll();
        }

        // GET api/<VisitController>/5
        [HttpGet("{id}")]
        public VisitModel Get(int id)
        {
            return new VisitModel().GetById(id);
        }

        // POST api/<VisitController>
        [HttpPost]
        public ApiResponse Post([FromBody] VisitModel value)
        {
            return value.Insert();
        }

        // PUT api/<VisitController>/5
        [HttpPut("{id}")]
        public ApiResponse Put(int id, [FromBody] VisitModel value)
        {
            return value.Update();
        }

        // DELETE api/<VisitController>/5
        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {
            return new VisitModel().Delete(id);
        }
    }
}
