using LeaveManagementSystemPractice.web.Services.LeaveAllocations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystemPractice.web.Controllers
{
    [Authorize]
    public class LeaveAllocationsController (ILeaveAllocationsService leaveAllocationsService) : Controller
    {
        // GET: LeaveAllocationsController
        public ActionResult Index()
        {
            return View();
        }

    }
}
