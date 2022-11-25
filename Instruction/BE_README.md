# Introduction 
This service has been created to send SMS and Multi factor authentication messages using twilio library. This service can be cloned and used in projects as needed. 

# Getting Started
Before this service can be used, we need to perform below steps.:
1.	Have Twilio Account Id and Token. Sign up using link https://www.twilio.com/try-twilio if you don't have Twilio account yet.
2.  Have Twilio Id, Twilio Token and Phone number ready for us.
	a. In Twilio trial account, you need to register all the numbers that you need to send the messages to. In twilio paid account, the messages can be sent to any number.
3.	Create Twilio verify service which will be used for Multi factor authentication and have ServiceId ready.
	a. In Twilio trial account, custom messages in MFA messages cannot be sent.

# Build 
In appsettings.json, replace the placeholders of TwilioId, TwilioToken, TwilioPhoneNumber and SMSMFAServiceId with actual values from your twilio account. Below is the section in appsettings.json that needs to be updated:
 "TwilioConfig": {
    "TwilioId": "{TwilioId}",
    "TwilioToken": "{TwilioToken}",
    "TwilioPhoneNumber": "{TwilioPhoneNumber}",
    "SMSMFAServiceId": "{Twilio verify service Id}"
  }
# Testing
Once all the prerequisites are done, execute your serice and test the endpoints from swagger page. 