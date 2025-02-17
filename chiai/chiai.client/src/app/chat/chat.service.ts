import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private apiUrl = 'https://localhost:5001/api/chat';

  constructor(private http: HttpClient) {}

  startChat(): Observable<{ id: number }> {
    return this.http.post<{ id: number }>(`${this.apiUrl}/start`, {});
  }

  sendMessage(sessionId: number, message: string): Observable<Message> {
    return this.http.post<Message>(`${this.apiUrl}/send`, { sessionId, message });
  }

  getChatHistory(sessionId: number): Observable<Message[]> {
    return this.http.get<Message[]>(`${this.apiUrl}/${sessionId}`);
  }
}

export interface Message {
  id: number;
  content: string;
  isUserMessage: boolean;
  timestamp: string;
}