import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpCallService } from "./http-call.service";
import * as _ from 'lodash';
import { parseJwt } from "src/app/common/common-functions";
import { User } from "src/app/models/models";
export const USER_TOKEN: string = "USER_TOKEN";

@Injectable({
    providedIn: "root"
})
export class UserService {

    get userLogin() {
        return !_.isEmpty(localStorage.getItem(USER_TOKEN))
    }

    get userObject() {
        let userToken = localStorage.getItem(USER_TOKEN);

        if (!_.isEmpty(userToken)) {
            let userObj = parseJwt(userToken);
            let user: User = {
                FirstName: userObj.given_name,
                LastName: userObj.family_name
            }
            return user;
        }
        return undefined;
    }

    constructor(private httpCallService: HttpCallService) {

    }

    logout() {
        localStorage.removeItem(USER_TOKEN);
    }

    login(request: any): Observable<boolean> {
        return new Observable((subscriber) => {
            this.httpCallService.post<any>("User", "Login", request).subscribe((res) => {
                if (res.Result.IsSuccess) {
                    localStorage.setItem(USER_TOKEN, res.Token);
                    subscriber.next(res);
                }
                else {
                    subscriber.next(res);
                }
            });
        })
    }

    register(request: any): Observable<boolean> {
        return new Observable((subscriber) => {
            this.httpCallService.post<any>("User", "Register", request).subscribe((res) => {
                subscriber.next(res);
            });
        })
    }
}