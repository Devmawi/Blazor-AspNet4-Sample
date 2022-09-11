using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BlazorAppLoginApi.Server.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IAntiforgery _antiforgery;

        public IndexModel(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }
        public void OnGet()
        {
            
        }
    }
}
