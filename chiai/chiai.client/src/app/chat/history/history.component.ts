import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.scss'],
  standalone: false
})
export class HistoryComponent {
  @Input() history: { timestamp: string; messages: { user: string; text: string }[] }[] = [];

  selectChat(chat: any) {
    console.log("Selected Chat:", chat);
    // Logic to load chat session can be implemented here
  }
}
