import { Component, EventEmitter, Output } from '@angular/core';
import { HistoryService } from '../../services/history.service';
import { HistoryItem } from "../../shared/historyItem"

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.scss'],
  standalone: false
})
export class HistoryComponent {
  history: HistoryItem[] = [];
  @Output() chatSelected = new EventEmitter<string>();
  @Output() newChatStarted = new EventEmitter();

  constructor(private historyService: HistoryService){

  }
  ngOnInit(): void {
    this.history = this.historyService.getHistory();
  }

  selectChat(chatId: string) {
    this.chatSelected.emit(chatId);
  }
}
