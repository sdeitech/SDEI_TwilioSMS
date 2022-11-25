import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SmsComponent } from './pages/sms/sms.component';
import { VerificationComponent } from './pages/verification/verification.component';

const routes: Routes = [
  {
    path: '',
    component: SmsComponent
  },
  {
    path: 'sms',
    component: SmsComponent
  },
  {
    path: 'mfa',
    component: VerificationComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
