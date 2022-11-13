import { Component, OnInit } from '@angular/core';
import { ChatService } from '../shared/chat.service';
import { Router, RouterLink } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';

import * as signalR from '@microsoft/signalr'; 


@Component({
  selector: 'app-chat-ui',
  templateUrl: './chat-ui.component.html',
  styleUrls: ['./chat-ui.component.css']
})
export class ChatUiComponent{
  sendMsgForm!: FormGroup;

  user:any = localStorage.getItem('loggedInUser');
  userObj : any = JSON.parse(this.user);
  loggedUserId:number = Number(this.userObj['usersId']);
  loggedUserName:string = this.userObj['firstName'];

  SenderId:number= this.loggedUserId;
  SenderName:string = this.loggedUserName;

  myMessage:any[]= [];
  othersMessage:any[]= [];
  mergedAndSortedMessage:any[]= [];
  chatUsers:any[];
  chatWith:number[];
  
  
  constructor(
    public chatService: ChatService,
    private formBuilder: FormBuilder,
    private route: Router){
      
    }
    

  ngOnInit(){
    this.myMessage= [];
    this.othersMessage= [];
    this.mergedAndSortedMessage= [];
    this.chatUsers= [];
    this.chatWith= [];

    this.sendMsgForm = this.formBuilder.group({
      text: [''], 
      SenderId: [],  
      ReceiverId: []
    });

    this.chatService.getUsers().subscribe(data=> {
    this.chatService.usersInfo = data;
    this.chatService.usersInfo= this.chatService.usersInfo
      .filter(u=> u.usersId != this.SenderId);
      
    });

    this.chatService.getMessage().subscribe(data=>{
      this.chatService.messageInfo = data;
      this.getChatUsers();
      this.getReceiverData();
      
    });
    
    
    
    //signalr connection
    const connection = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information)
    .withUrl('https://localhost:7005/'+'notify')
    .build();
    connection.start().then(function(){
      console.log('signalR connected');
    }).catch(function (err){
      return console.error(err.toString());
    });
    connection.on("BroadcastMessage", ()=>{
      console.log("broadcast hitted");
      this.chatService.getMessage().subscribe(data=>{
        this.chatService.messageInfo = data;
        this.conversation();
        this.getChatUsers();
      });
      this.chatService.getUsers().subscribe(data=> {
        this.chatService.usersInfo = data;
        this.chatService.usersInfo= this.chatService.usersInfo.filter(u=> u.usersId != this.SenderId);
      });
    });
    //signalr connection is ended
  }
  
  
  setReceiverDataToBrowser(data: any){
    localStorage.setItem('chatPerson', JSON.stringify(data));
    this.getReceiverData(); 
  }
  getReceiverData(){
    const user: any = localStorage.getItem('chatPerson'); 
    const userObj:any = JSON.parse(user); 
    this.chatService.ReceiverId = Number(userObj['usersId']);
    this.chatService.ReceiverName = userObj['firstName'];
    this.conversation(); 
      
  }
  conversation(){
    this.messageSentMe(this.chatService.ReceiverId, this.SenderId );
    this.ISentMessage(this.chatService.ReceiverId, this.SenderId );
    this.mergeAndSortMessagesList();
  }


  messageSentMe(rId:number, sId:number){
   
    this.myMessage = this.chatService.messageInfo.filter(function(chat:{
      msg: string
      senderId: number
      receiverId: number
    }):boolean{
      return chat.senderId == rId && chat.receiverId ==sId 
    })
  }
  ISentMessage(rId:number, sId:number){
 
    this.othersMessage = this.chatService.messageInfo.filter(function(chat:{
      msg: string
      senderId: number
      receiverId: number
    }):boolean{
      return chat.senderId == sId && chat.receiverId ==rId 
    })
  }
  
  mergeAndSortMessagesList(){
    this.mergedAndSortedMessage= this.myMessage.concat(this.othersMessage).sort(function(a:any, b:any) {
      return a.msgId - b.msgId;
    });  
  }
  
  sendMessage(){
    this.sendMsgForm.value.SenderId = this.SenderId;
    this.sendMsgForm.value.ReceiverId = this.chatService.ReceiverId;
    this.chatService.saveMessage(this.sendMsgForm.value).subscribe(data=>{
        console.log("message sent");
        this.sendMsgForm.reset();
    }, err=>{
      console.log("message not sent!");
    })
    
  }

  
  deleteMessage(id:number){
    // if(confirm("Do you want to delete the message?")){
      this.chatService.deleteCurrentMessage(id).subscribe(data=>{
        console.log('Message deleted');
      }, err=>{
        alert("Error deleting message!");
      });
    
    this.getReceiverData();
  }
  logOutUser(){
    this.route.navigate(['/']); 
  }
  getChatUsers(){
    this.chatService.messageInfo.forEach((c)=>{
      if(!this.chatWith.includes(c.senderId)) {
        this.chatWith.push(c.senderId)
      }
      if(!this.chatWith.includes(c.receiverId)){
        this.chatWith.push(c.receiverId); 
      }
      this.chatWith = this.chatWith
        .filter((u)=> u!=this.loggedUserId); 
      this.chatUsers = this.chatService.usersInfo
        .filter((u)=>this.chatWith.includes(u.usersId)); 
    });
  }
}


