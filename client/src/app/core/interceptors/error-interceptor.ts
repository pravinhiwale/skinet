import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import {Router, NavigationExtras} from '@angular/router';
import {catchError} from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

// if we don't make this injectable it will never able to be utilized and will never handle our errors

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private router: Router, private toastr: ToastrService){

    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError( error =>  {
                if (error) {
                                if (error.status === 404 ) {
                                this.router.navigateByUrl('/not-found');
                                }
                                if (error.status === 500) {
                                    const navigationExtras: NavigationExtras = {state: {error: error.error} };
                                    this.router.navigateByUrl('/server-error', navigationExtras);
                                }
                                
                                

                                /*
                                for other errors 400
                                Now what 400 error this is typically generated when a user submits some something to the server and t's just
                                 a bad request for whatever reason and for this type of error then we're just going to display
                                a pop up toast notification to the user , 400 validation error is slightly different
                                because the user sent up some form data and we would want to display the results of what
                                they've sent up to the server and why the server rejected it and we would want to display
                                the list of errors on the form itself or inside the components itself.

                                */
                                if (error.status === 400) {
                                    if (error.error.errors){
                                        throw error.error;
                                        //this will throw error back to the component
                                    }
                                    else{
                                        this.toastr.error(error.error.message, error.error.statusCode);
                                    }
                                    /*
                                    So we'll get the toast for the normal 400 bad request.And if it's a validation error we're throwing it
                                    back to the component.
                                    */
                                                                  }
                                if (error.status === 401) {
                                this.toastr.error(error.error.message, error.error.statusCode);
                           }
                            }
                return throwError(error);
                        })
                );
        }
}

// this helps intercept http request which is our outgoing request so we can do something on the way out and we've got the "next"
// which is the HttpResponse which is coming back and what we're going to want to do is catch any errors inside the response coming
// back from our API and that will give us an opportunity to do something with the particular errors.

// we need a constructor because we would need routing functionality


// In order to use this HTTPInterceptor we need to add this as a provider inside our appModule component

