import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ChatComponent } from './chat/chat/chat.component';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  chatHistory: { timestamp: string; messages: { user: string; text: string }[] }[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
  }  



  title = 'chiai.client';
}
