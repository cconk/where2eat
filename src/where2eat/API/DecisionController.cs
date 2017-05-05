using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using where2eat.Services;
using where2eat.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace where2eat.API
{
    [Route("api/[controller]")]
    public class DecisionController : Controller
    {
        private DecisionsService _decisionService;
        public DecisionController(DecisionsService decisionService)
        {
            _decisionService = decisionService;
        }
       
        // GET api/values/5
        [HttpGet("{id}")]
        public IList<OptionVM> Get(int id)
        {
            return _decisionService.ListWinningOptionsByEvent(id);
        }

       
    }
}
