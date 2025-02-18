import { Component, OnInit } from '@angular/core';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  chatHistory: { timestamp: string; messages: { user: string; text: string }[] }[] = [];

  constructor() {}

  ngOnInit() {
  }  

  title = 'ChiAI';
}
