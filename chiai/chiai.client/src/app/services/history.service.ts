import { Injectable } from '@angular/core';
import { Chat } from '../shared/chat';
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
    return this.http.get<Chat[]>(`${this.apiUrl}/${this.currentUser.userId}`);
  }

  getChatHistory(chatId:number):Observable<ChatMessage[]>{
    return this.http.get<ChatMessage[]>(`${this.apiUrl}/chats/${chatId}/messages`);
  }
}

