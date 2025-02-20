import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { ChatMessage } from '../shared/chatMessage';
import { User } from '../shared/user';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private apiUrl = '/chat';
  private currentUser:User;
  private chatSubject = new Subject<ChatMessage>();

  constructor(
    private http:HttpClient
   ) {    
    this.currentUser = {userId: 1, userName: "PromptNG"}
  }

  rateMessageAsHelpful(chatId: number, messageId:number, rating:number){
    return this.http.post(`${this.apiUrl}/rate/${chatId}/${messageId}/${rating}`, {});
  }

  connect(chatId:number): Observable<ChatMessage> {
    this.chatSubject = new Subject<ChatMessage>();
    return this.chatSubject.asObservable();
  }
  
  startNewChat(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/new/${this.currentUser.userId}`);
  }
  sendMessage(message: string, chatId: number) {
    const msg: ChatMessage = {
      id: 0,
      content: message,
      author: this.currentUser.userName,
      date: new Date(),
      isFromAi: false,
      rating: 0
    };
    this.chatSubject.next(msg); 
  
    this.receiveChunkedResponse(chatId, msg);
  }

  private parseIncompleteJson(incompleteString:string):string {
    if(!incompleteString.endsWith(']')){
      incompleteString+= ']';
    }

    let parsedArray = JSON.parse(incompleteString);
    return parsedArray.join('');

  }
  
  private async receiveChunkedResponse(chatId: number, message: ChatMessage) {
    const response = await fetch(`${this.apiUrl}/send/${chatId}`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(message)
    });
  
    if (!response.body) {
      console.error("No response body from API");
      return;
    }
  
    const reader = response.body.getReader();
    const decoder = new TextDecoder();
    let currentText = '';
    while (true) {
      const { value, done } = await reader.read();
      if (done){

        break; 
      }
      currentText += decoder.decode(value, { stream: true });
  
      this.chatSubject.next({
        id: 0,
        content: this.parseIncompleteJson(currentText),
        author: "ChiAI",
        date: new Date(),
        isFromAi: true,
        rating: 0
      });
    }
  }
  
}
