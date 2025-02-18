import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { interval, Observable, Subject, take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private currentUser = "PromptEngineer"
  private chatSubject = new Subject<{ user: string; text: string }>();

  constructor() {}

  connect(): Observable<{ user: string; text: string }> {
    return this.chatSubject.asObservable();
  }

  sendMessage(messageText: string ) {
    var message = {
      user: this.currentUser, text: messageText
    }
    this.chatSubject.next(message);

    const aiResponse = 'Lorem ipsum dolor sit amet...';
    this.sendChunkedResponse(aiResponse);
  }

  private sendChunkedResponse(fullText: string) {
    let currentText = '';

    interval(50)
      .pipe(take(fullText.length)) 
      .subscribe((index) => {
        currentText += fullText[index];
        this.chatSubject.next({ user: 'ChiAI', text: currentText });
      });
  }
}

export interface Message {
  content: string;
  isUserMessage: boolean;
  timestamp: string;
}