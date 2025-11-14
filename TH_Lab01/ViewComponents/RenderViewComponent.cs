using Microsoft.AspNetCore.Mvc;
using TH_Lab01.Models;

namespace TH_Lab01.ViewComponents
{
    public class RenderViewComponent:ViewComponent
    {
        private List<MenuItem> MenuItems=new List<MenuItem>()
        {
            new MenuItem(){Id=1, Name="Branches", Link="#"},
            new MenuItem(){Id=2, Name="Students", Link="/admin/Student/List"},
            new MenuItem(){Id=3, Name="Subjects", Link="#"},
            new MenuItem(){Id=4, Name="Courses", Link="#"},
        };
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderLeftMenu",MenuItems);
        }
    }    
}
