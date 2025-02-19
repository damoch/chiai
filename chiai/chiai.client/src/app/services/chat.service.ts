import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { interval, Observable, Subject, take } from 'rxjs';
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

  connect(chatId:number): Observable<ChatMessage> {
    this.chatSubject = new Subject<ChatMessage>();
    return this.chatSubject.asObservable();
  }
  
  startNewChat(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/new/${this.currentUser.userId}`);
  }

  sendMessage(message: string ) {
    var msg:ChatMessage = {id:"", content:message, author:this.currentUser.userName, date: new Date(), isFromAi: false};
    this.chatSubject.next(msg);

    const aiResponse = 'Lorem ipsum dolor sit amet...';
    this.sendChunkedResponse(aiResponse);
  }

  private sendChunkedResponse(fullText: string) {
    let currentText = '';

    interval(50)
      .pipe(take(fullText.length)) 
      .subscribe((index) => {
        currentText += fullText[index];
        this.chatSubject.next({ author: 'ChiAI', content: currentText, id:"", date: new Date(), isFromAi:true });
      });
  }
}