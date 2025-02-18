import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { interval, Observable, Subject, take } from 'rxjs';
import { ChatMessage } from '../shared/chatMessage';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private currentUser = "PromptEngineer"
  private chatSubject = new Subject<ChatMessage>();

  constructor() {}

  connect(chatId:string): Observable<ChatMessage> {
    return this.chatSubject.asObservable();
  }

  sendMessage(message: string ) {
    var msg:ChatMessage = {id:"", content:message, author:this.currentUser, date: new Date(), isFromAi: false};//
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