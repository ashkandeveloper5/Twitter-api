
using Microsoft.AspNetCore.Authorization;
using Twitter.Core.DTOs.Role;

namespace Twitter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        #region CRUD Role
        [HttpPost("AddRole")]
        public ActionResult AddRole([FromBody] AddRoleDto addRoleDto)
        {
            if (_roleService.AddRole(addRoleDto))
                return Ok();
            return BadRequest();
        }
        [HttpPut("EditRole")]
        public ActionResult EditRole([FromBody] EditRoleDto editRoleDto)
        {
            if (_roleService.EditRole(editRoleDto))
                return Ok();
            return BadRequest();
        }
        [HttpDelete("DeleteRole")]
        public ActionResult DeleteRole([FromBody] string roleName)
        {
            if (_roleService.DeleteRole(roleName))
                return Ok();
            return BadRequest();
        }
        #endregion
    }
}
