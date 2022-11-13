import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, forwardRef, OnInit, Output } from '@angular/core';
import {FormGroup, FormBuilder } from '@angular/forms'; 
import { Router } from '@angular/router';
import { ChatService } from '../shared/chat.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm!:FormGroup;

  constructor(
    private  formBuilder: FormBuilder,
    private router: Router, 
    public chatService: ChatService
  ) {}
   
  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      email: [''],
    })
  }

  myLoginMethod(){
    this.chatService.getUsers().subscribe(data=>{
      
      const user = data.find((a:any)=>{
          localStorage.setItem('loggedInUser', JSON.stringify(a));         
          return a.email === this.loginForm.value.email;
      })
      if(user){
          console.log("login success");
          this.loginForm.reset();
          this.router.navigate(['chatui']); 
      }else{
          alert('Not found');
      }    
      },err=>{
      alert('Something wrong!');
    });
  }

}
