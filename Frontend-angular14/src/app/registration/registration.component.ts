import { HttpClient } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import {FormGroup, FormBuilder} from '@angular/forms';
import { Router } from "@angular/router";

@Component({
    selector: 'user-reg', 
    templateUrl: './registration.component.html', 
    styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit{
    public registerForm!: FormGroup;

    constructor(
        private formBuilder: FormBuilder, 
        private http: HttpClient, 
        private router: Router
        ) {}

    ngOnInit(): void {
        
        this.registerForm = this.formBuilder.group({
            firstName: [''], 
            lastName: [''],
            email: ['']
            
        })
    }
    
    registerUser():void {
        this.http.post<any>("https://localhost:7005/api/UsersInfo", this.registerForm.value)
        .subscribe(res=> {
            console.log("SignUp Successfull"); 
            this.registerForm.reset();
            this.router.navigate(['login']); 

        }, err=> {
            alert("something is wrong.!");
        })
    }
}