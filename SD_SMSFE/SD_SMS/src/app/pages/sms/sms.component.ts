import { Component, OnInit } from '@angular/core';
import { SmsService } from 'src/app/services/sms.services';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-sms',
  templateUrl: './sms.component.html',
  styleUrls: ['./sms.component.scss']
})
export class SmsComponent implements OnInit {
  mobileNo: String = '';
  msg: string = '';
  status: string = '';
  constructor(
    public _SmsService: SmsService,
    public _router: Router,
  ) {
  }

  ngOnInit(): void {

  }

  // send message to mobile no.
  sendMsg() {
    const reqDom = {
      "phoneNo": "91" + this.mobileNo,
      "message": this.msg
    }

    this._SmsService.sendMsg(reqDom).subscribe(result => {
      if (result.status === 200) {
        // this._router.navigate(['/verify']);
        this.callStatusAPI(result.response.sid);
      } else {
        this.status = "Something is wrong. Please try again.";
      }
    }, error => {
      this.status = "Something is wrong. Please try again.";
    });
  }

  //ony number allow 
  onlyNumberKey(event: any) {
    return (event.charCode == 8 || event.charCode == 0) ? null : event.charCode >= 48 && event.charCode <= 57;
  }

  callStatusAPI(sid: string) {
    this._SmsService.smsStatus(sid).subscribe(result => {
      if (result.status === 200) {
        this.status = "Thank You. SMS send Successfully.";
      } else {
        this.status = "Something is wrong. Please try again.";
      }
    }, error => {
      this.status = "Something is wrong. Please try again.";
    });
  }

}
