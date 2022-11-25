import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SmsComponent } from './pages/sms/sms.component';
import { VerificationComponent } from './pages/verification/verification.component';
import { SmsService } from './services/sms.services';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    SmsComponent,
    VerificationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
     BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [SmsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
