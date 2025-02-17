import { Component, OnInit } from '@angular/core';
import { ChatService, Message } from '../chat.service';

@Component({
  selector: 'app-chat',
  standalone: false,
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent implements OnInit {
  sessionId: number | null = null;
  messages: Message[] = [];
  userMessage: string = '';

  constructor(private chatService: ChatService) {}

  ngOnInit(): void {
    this.chatService.startChat().subscribe(session => {
      this.sessionId = session.id;
    });
  }

  sendMessage() {
    if (!this.userMessage.trim() || !this.sessionId) return;

    const newMessage: Message = {
      id: 0,
      content: this.userMessage,
      isUserMessage: true,
      timestamp: new Date().toISOString()
    };

    this.messages.push(newMessage);

    this.chatService.sendMessage(this.sessionId, this.userMessage)
      .subscribe(response => {
        this.messages.push(response);
      });

    this.userMessage = '';
  }
}