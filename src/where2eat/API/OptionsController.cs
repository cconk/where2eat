using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using where2eat.Models;
using where2eat.ViewModels;
using where2eat.Services;
using where2eat.Data;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace where2eat.API
{
    [Route("api/[controller]")]
    public class OptionsController : Controller
    {
        private OptionsService _optionService;
        public OptionsController(OptionsService optionService)
        {
            _optionService = optionService;
        }

        // GET: api/values
        [HttpGet]
        public IList<OptionVM> Get()
        {
            return _optionService.ListAllOptions();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IList<OptionVM> Get(int id)
        {
            return _optionService.ListAllOptionsByEvent(id);
        }

        // POST api/values
        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody]Option newOption)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            if (newOption.OptionName == "")
            {
                return NotFound();
            }

            else
            {
                _optionService.AddNewOption(id, newOption);
                return Ok(newOption);
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Option optionToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            //if (optionToUpdate.OptionName == "")
            //{
            //    return NotFound();
            //}

            else
            {
                _optionService.UpdateOption(optionToUpdate);
                return Ok(optionToUpdate);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);

            }

            else
            {
                _optionService.DeleteOption(id);
                return Ok();
            }
        }
    }
}
