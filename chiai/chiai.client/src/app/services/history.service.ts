import { Injectable } from '@angular/core';
import { Chat } from '../shared/historyItem';
import { ChatMessage } from '../shared/chatMessage';
import { User } from '../shared/user';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HistoryService {
  private apiUrl:string = "/history";
  constructor(private http: HttpClient) {
    this.currentUser = {userId: 1, userName: "PromptNG"}
   }

  currentUser:User;
  getHistory():Observable<Chat[]>{  
    const params = new HttpParams().set('userId', this.currentUser.userId.toString());
    return this.http.get<Chat[]>(this.apiUrl, { params });
  }

  getChatHistory(chatId:number):ChatMessage[]{
    return [
      {id:"0", author: "PromptEngineer", content: "Test 123", date: new Date(), isFromAi: false},
      {id:"1", author: "ChiAI", content: "Lorem ipsum bla bla bla", date: new Date(), isFromAi: true},
    ];
  }
}

