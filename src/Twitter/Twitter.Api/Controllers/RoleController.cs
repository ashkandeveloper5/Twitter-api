
using Twitter.Core.DTOs.Role;

namespace Twitter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        #region CRUD Role
        [HttpPost("AddRole")]
        public ActionResult AddRole([FromQuery] AddRoleDto addRoleDto)
        {
            if (_roleService.AddRole(addRoleDto))
                return NoContent();
            return BadRequest();
        }
        [HttpPost("EditRole")]
        public ActionResult EditRole([FromQuery] EditRoleDto editRoleDto)
        {
            if (_roleService.EditRole(editRoleDto))
                return NoContent();
            return BadRequest();
        }
        [HttpPost("DeleteRole")]
        public ActionResult DeleteRole([FromQuery] string roleName)
        {
            if (_roleService.DeleteRole(roleName))
                return NoContent();
            return BadRequest();
        }
        #endregion
    }
}
