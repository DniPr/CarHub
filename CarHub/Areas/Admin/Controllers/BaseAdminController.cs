using CarHub.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ApplicationRoles.Administrator)]
    public class BaseAdminController : Controller
    {
    }
}
