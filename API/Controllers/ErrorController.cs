using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)] //Swagger will ignore this endpoint, swagger needs exact endpoint with HTTPGEt,Post , etc . but here we want to catch all exceptions 
    public class ErrorController:BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
            /*
            So how do we get a request that's come into our API and get it passed to this particular controller. 
            What we can do this by a middleware in our startup class 
            */
        }
    }

    /* 
   we will override the routes that we get from our base API controller
    */
}