import { Component, EventEmitter, Output } from '@angular/core';
import { HistoryService } from '../../services/history.service';
import { Chat } from "../../shared/chat"

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.scss'],
  standalone: false
})
export class HistoryComponent {
  history: Chat[] = [];
  @Output() chatSelected = new EventEmitter<number>();
  @Output() newChatStarted = new EventEmitter();

  constructor(private historyService: HistoryService){
  }
  
  ngOnInit(): void {
    this.loadHistory();
  }
  loadHistory() {
    this.historyService.getHistory().subscribe((result: Chat[]) => {
      
      this.history = result.sort((a, b) => 
           new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
    );
    });
  }

  selectChat(chatId: number) {
    this.chatSelected.emit(chatId);
  }
}
