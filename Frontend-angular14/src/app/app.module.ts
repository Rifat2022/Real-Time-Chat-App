import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http'; 


import { RegistrationComponent } from './registration/registration.component';
import { LoginComponent } from './login/login.component';
import { ChatUiComponent } from './chat-ui/chat-ui.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  imports: [
    BrowserModule,
    AppRoutingModule, 
    HttpClientModule, 
    FormsModule, 
    ReactiveFormsModule,
    
  ],
  declarations: [
    AppComponent, 
    RegistrationComponent, LoginComponent, ChatUiComponent
  ],
  
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
