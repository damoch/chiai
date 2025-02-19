import { Injectable } from '@angular/core';
import { HistoryItem } from '../shared/historyItem';
import { ChatMessage } from '../shared/chatMessage';
import { User } from '../shared/user';

@Injectable({
  providedIn: 'root'
})
export class HistoryService {

  constructor() {
    this.currentUser = {userId: 1, userName: "PromptNG"}
   }

  currentUser:User;
  getHistory():HistoryItem[]{
    return [
      {chatId: "0000", chatName:"Hello fellow gamers" },
      {chatId: "111-222", chatName:"Lorem ipsum" },
      {chatId: "111-dsss", chatName:"test 123" },
    ];
  }

  getChatHistory(chatId:string):ChatMessage[]{
    return [
      {id:"0", author: "PromptEngineer", content: "Test 123", date: new Date(), isFromAi: false},
      {id:"1", author: "ChiAI", content: "Lorem ipsum bla bla bla", date: new Date(), isFromAi: true},
    ];
  }
}

