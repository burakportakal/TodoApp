import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn:"root"
})
export class HttpCallService {
    url: string = environment.apiUrl;
    constructor(private http: HttpClient) { }

    post<T>(controller: string, action: string ,request: any): Observable<T> {
        const headers = new HttpHeaders({ "Authorization": `Bearer ${localStorage.getItem("USER_TOKEN")}` });
        return this.http.post<T>(`${this.url}api/${controller}/${action}`, request, { headers: headers });
    }
}