using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Controllers.Forms.Components;
using WebApplication.Models;
using WebApplication.Services.Interfaces;
using WebApplication.Utils;

namespace WebApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentController : Controller
    {
        private readonly IComponentService _componentService;
        private readonly IValidationService _validationService;

        public ComponentController(IComponentService componentService, IValidationService validationService)
        {
            _componentService = componentService;
            _validationService = validationService;
        }


        [HttpGet]
        [Authorize(Roles = Constants.ADMIN_ROLE)]
        public ActionResult<List<Components>> GetComponents()
        {
            return Ok(_componentService.GetComponents());
        }

        [HttpPost("create")]
        [Authorize(Roles = Constants.ADMIN_ROLE)]
        public ActionResult<Status> CreateComponent([FromBody] ComponentForm componentForm)
        {
            _validationService.ValidateComponentForm(componentForm);
            _componentService.CreateComponent(componentForm);
            return Ok(new Status("Component is created successfully"));
        }

        [HttpPost("update")]
        [Authorize(Roles = Constants.ADMIN_ROLE)]
        public ActionResult<Status> UpdateComponent([FromBody] UpdatedComponentForm updatedComponentForm)
        {
            _validationService.ValidateComponentForm(updatedComponentForm.updatedComponentDetails);
            _componentService.UpdateComponent(updatedComponentForm);
            return Ok(new Status("Component is updated successfully"));
        }
        
        [HttpPost("delete")]
        [Authorize(Roles = Constants.ADMIN_ROLE)]
        public ActionResult<Status> DeleteComponent([FromBody] DeleteComponentRequestForm deleteComponentRequestForm)
        {
            _componentService.DeleteComponent(deleteComponentRequestForm.type);
            return Ok(new Status("Component is deleted successfully"));
        }
        
        
    }
}