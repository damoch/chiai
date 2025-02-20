import { Component, Input } from '@angular/core';
import { ChatMessage } from '../../shared/chatMessage';
import { ChatService } from '../../services/chat.service';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css'],
  standalone: false
})
export class MessageComponent {

  constructor(
    private chatService: ChatService
  ) {
    this.chatId = -1;
  }
  @Input() chatId: number;
  @Input() message!: ChatMessage;

  aiLiked: boolean | null = null;

  giveFeedback(like: boolean) {
    let ratingValue = like ? 1 : 2
    this.chatService.rateMessageAsHelpful(this.chatId, this.message.id, ratingValue).subscribe(() => {
      this.message.rating = ratingValue;//a bit naive... but its 2 AM...
    })
  }
}
