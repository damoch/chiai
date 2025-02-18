import { Component, OnInit } from '@angular/core';
import { ChatService, Message } from '../../services/chat.service';

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

  chatMessages: { user: string; text: string }[] = [];
  ngOnInit(): void {
    this.chatService.connect().subscribe((message) => {
      if (message.user === 'ChiAI') {
        const lastMessage = this.chatMessages[this.chatMessages.length - 1];
        if (lastMessage && lastMessage.user === 'ChiAI') {
          lastMessage.text = message.text;
        } else {
          this.chatMessages.push(message);
        }
      } else {
        this.chatMessages.push(message);
      }
    });
  }

  sendMessage() {
    if (!this.userMessage.trim()) return;
    this.chatService.sendMessage(this.userMessage);
    this.userMessage = "";
  }
}