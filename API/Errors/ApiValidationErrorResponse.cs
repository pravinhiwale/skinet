using System.Collections.Generic;

namespace API.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse() : base(400)
        {

        }
        public IEnumerable<string> Errors { get; set; }
    }

    /*Valiation Error occurs  from the APIController attribute  and if we go back to our controller and comment out "ApiController" attribute
controller for now and then head back to postman and we click send again (buggy controller) then we don't see the response
because we've removed the tool that's checking to see what kind of value this is in our route parameter.
And if it encounters what it sees as a validation error then it adds the error to something called to modelstate 
and then it's  model state error response to our API server is generating and sending back to us 
but we don't want to remove this APIcontroller We want to keep this on there because this is quite useful it saves us from manually checking to see
if there's any validation errors.And it's quite useful just to let this handle this for us.

But what we want to do is override the behavior of this and to override this behavior we can actually
do this inside our startup class so that we can configure this particular attribute.

*/ 

}