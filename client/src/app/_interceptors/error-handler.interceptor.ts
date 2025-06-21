import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs';

export const errorHandlerInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const toastr = inject(ToastrService)
  return next(req).pipe(
    catchError(error => {
      if(error){
        switch(error.status){
          case 401: toastr.error("Unauthorized", error.status)
            break;
          case 404: router.navigateByUrl("not-found");
            break;
          case 500:       
            const navigationEtras: NavigationExtras = { state: { error: error.error } }
            router.navigateByUrl("server-error",navigationEtras);
            break;
          default:
            toastr.error("Unexpected error occurred.");
            break;
        }
      }
      throw error;
    })
  );
};
