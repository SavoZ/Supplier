import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { Injectable } from '@angular/core';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private toastr: ToastrService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(catchError(err => {
            if (err.status === 401 || err.status === 403) {
                return throwError(err.error);
            } else {
                if (err.error.Message.includes(';')) {
                    const errors = err.error.Message.split(';');
                    errors.forEach(message => {
                        this.toastr.error(message);
                    });
                } else {
                    this.toastr.error(err.error.Message);
                }
            }
            return throwError(err.error);
        }));
    }
}
