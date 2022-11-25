import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class SmsService {

    constructor(private http: HttpClient) {
    }
    baseUrl = environment.baseUrl;

    sendMsg(body: any): Observable<any> {
        const url = this.baseUrl + `SMS/Send`;
        return this.http.post(url, body);
    }

    sendOTP(body: any): Observable<any> {
        const url = this.baseUrl + `MFA/Send`;
        return this.http.post(url, body);
    }

    verifyOTP(body: any): Observable<any> {
        const url = this.baseUrl + `MFA/Verify`;
        return this.http.post(url, body);
    }


    smsStatus(sid: String): Observable<any> {
        const url = this.baseUrl + `SMS/${sid}/Status`;
        return this.http.get(url);
    }

}