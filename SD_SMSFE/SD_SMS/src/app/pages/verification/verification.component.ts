import { SmsService } from 'src/app/services/sms.services';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-verification',
  templateUrl: './verification.component.html',
  styleUrls: ['./verification.component.scss']
})
export class VerificationComponent implements OnInit {
  mobileNo: String = '';
  otpmsg: string = '';
  showOTPUI: boolean = false;
  showMsg: boolean = false;
  status: string = '';
  constructor(
    public _SmsService: SmsService,
    public _router: Router
  ) { }

  ngOnInit(): void {
  }

  // Send OTP method
  sendOTP() {
    const reqDom = {
      "phoneNo": this.mobileNo
    }
    this._SmsService.sendOTP(reqDom).subscribe(result => {
      if (result.status === 200) {
        this.showOTPUI = true;
      } else {
        this.status = "Something is wrong. Please try again.";
      }
    }, error => {
      this.status = "Something is wrong. Please try again.";
    });
  }

  // Veridy OTP method
  verifyOTP() {
    const reqDom = {
      "phoneNo": "91" + this.mobileNo,
      "verificationCode": this.otpmsg
    }
    this._SmsService.verifyOTP(reqDom).subscribe(result => {
      if (result.status === 200) {
        if (result.response.messageStatus === 'approved') {
        this.status = 'Verification ' + result.response.messageStatus+ '.';
        this.showOTPUI = false;
        this.mobileNo = '';
        this.showMsg = true;
        } else {
          this.status = 'Verification failed. Please try again.';
          this.showMsg = false;
        }
      } else {
        this.status = "Something is wrong. Please try again.";
        this.showMsg = false;
      }
    }, error => {
      this.status = "Something is wrong. Please try again.";
      this.showMsg = false;
    });
  }

  //ony number allow 
  onlyNumberKey(event: any) {
    return (event.charCode == 8 || event.charCode == 0) ? null : event.charCode >= 48 && event.charCode <= 57;
  }
}
