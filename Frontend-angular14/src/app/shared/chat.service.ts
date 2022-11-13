import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginComponent } from '../login/login.component';
import {MessageInfo, UsersInfo} from './chat.model'; 

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  constructor(private myhttp: HttpClient) { }   //HttpClient for getting the package data from database

  usersInfoUrl:string = "https://localhost:7005/api/UsersInfo";
  msgInfoUrl:string = "https://localhost:7005/api/MessageInfo";
  

  usersInfo:UsersInfo[] = []; // for get data of usersinfo
  messageInfo:MessageInfo[] = []; 
  usersData:UsersInfo = new UsersInfo(); // for post or data insertion 
  messageData:MessageInfo = new MessageInfo();

 
  
  // user:any= localStorage.getItem('chatPerson');
  // userObj?:any = JSON.parse(this.user);

  ReceiverId!:number; 
  ReceiverName!:string; 

 // for get
//for post

  getMessage():Observable<MessageInfo[]>
  {
    return this.myhttp.get<MessageInfo[]>(this.msgInfoUrl);
  }


  getCurrentMessge(id:number):Observable<MessageInfo[]>
  {
    return this.myhttp.get<MessageInfo[]>(`${this.msgInfoUrl}/${id}`);
  }
  saveMessage(data: any){
    return this.myhttp.post(this.msgInfoUrl, data); 
  }
   //get all message and users informations
  getUsers():Observable<UsersInfo[]>
  {
    return this.myhttp.get<UsersInfo[]>(this.usersInfoUrl);
  }
  saveUsers(){
    return this.myhttp.post(this.usersInfoUrl, this.usersData); 
  }
  
  
  //updates users
  updateUsers(){
    return this.myhttp.put(`${this.usersInfoUrl}/${this.usersData.usersId}` , this.usersData);
  }

  deleteCurrentMessage(msgId:number){
    return this.myhttp.delete(`${this.msgInfoUrl}/${msgId}`);
  }

}
