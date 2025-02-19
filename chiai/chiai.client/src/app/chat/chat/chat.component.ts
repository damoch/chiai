import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ChatService } from '../../services/chat.service';
import { ChatMessage } from '../../shared/chatMessage';
import { HistoryService } from '../../services/history.service';

@Component({
  selector: 'app-chat',
  standalone: false,
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent implements OnInit {
  @Output() newChatLoaded = new EventEmitter<void>;
  userMessage: string = '';

  constructor(private chatService: ChatService,
    private historyService: HistoryService
  ) {}

  chatMessages:ChatMessage[]  = [];

  currentChatId:number = -1;
  ngOnInit(): void {
    this.startNewChat();
  }

  startNewChat() {
    this.chatService.startNewChat().subscribe((result: any) => {
      this.currentChatId = result.chatId;
      this.chatService.connect(this.currentChatId).subscribe((message) => {
        if (message.author === 'ChiAI') {
          const lastMessage = this.chatMessages[this.chatMessages.length - 1];
          if (lastMessage && lastMessage.author === 'ChiAI') {
            lastMessage.content = message.content;
          } else {
            this.chatMessages.push(message);
          }
        } else {
          this.chatMessages.push(message);
        }
      });
      this.chatMessages = [];
      this.newChatLoaded.emit();
    });

  }

  sendMessage() {
    if (!this.userMessage.trim()) return;
    this.chatService.sendMessage(this.userMessage);
    this.userMessage = "";
  }

  loadChat(chatId:number){
    this.chatMessages = this.historyService.getChatHistory(chatId);
    this.currentChatId = chatId;
  }
}
