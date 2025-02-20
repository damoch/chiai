export interface ChatMessage {
    id: number; 
    author: string;
    content: string;
    date: Date; 
    isFromAi: boolean;
    rating: number;
    isBeingGenerated: boolean;
  }
  