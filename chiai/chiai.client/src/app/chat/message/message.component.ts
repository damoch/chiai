import { Component, Input } from '@angular/core';
import { ChatMessage } from '../../shared/chatMessage';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css'],
  standalone: false
})
export class MessageComponent {
  @Input() message!: ChatMessage;

  aiLiked: boolean | null = null;

  giveFeedback(like: boolean) {
    this.aiLiked = like;
    console.log(`User ${like ? 'liked' : 'disliked'} the AI response: "${this.message.content}"`);
  }
}
