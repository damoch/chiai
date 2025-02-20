import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ChatService } from '../../services/chat.service';
import { ChatMessage } from '../../shared/chatMessage';
import { HistoryService } from '../../services/history.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-chat',
  standalone: false,
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent implements OnInit {
  @Output() newChatLoaded = new EventEmitter<void>;
  userMessage: string = '';
  private subscription!: Subscription;

  constructor(private chatService: ChatService,
    private historyService: HistoryService
  ) {}

  chatMessages:ChatMessage[]  = [];

  currentChatId:number = -1;
  ngOnInit(): void {
    this.subscription = this.chatService.messageGenerated$.subscribe(() => {
      this.loadChat(this.currentChatId)
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  startNewChat() {
    this.chatService.startNewChat().subscribe((result: any) => {
      this.handleNewChatStarted(result);
    });

  }

  private handleNewChatStarted(result: any) {
    this.currentChatId = result.id;
    this.chatService.connect(this.currentChatId).subscribe((message) => {
      this.handleMessage(message);
    });
    this.chatMessages = [];
    this.newChatLoaded.emit();
  }

  private handleMessage(message: ChatMessage) {
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
  }

  sendMessage() {
    if (!this.userMessage.trim()) return;

    if(this.currentChatId < 0){
      this.chatService.startNewChat().subscribe((result: any) => {
        this.handleNewChatStarted(result);
        this.chatService.sendMessage(this.userMessage, this.currentChatId);
        this.loadChat(this.currentChatId);
        this.userMessage = "";
      });
      return;
    }

    this.chatService.sendMessage(this.userMessage, this.currentChatId);
    this.userMessage = "";
  }

  loadChat(chatId:number){
    this.currentChatId = chatId;
    this.historyService.getChatHistory(chatId).subscribe((result:ChatMessage[]) => {
      this.chatMessages = result;
    });
  }
}
