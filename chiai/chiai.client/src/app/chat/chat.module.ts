import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatComponent } from './chat/chat.component';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';

import { MatListModule } from '@angular/material/list';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';



@NgModule({
  declarations: [
    ChatComponent
  ],
  imports: [
    CommonModule, FormsModule,    MatInputModule,
        MatButtonModule,
        MatCardModule,
        MatToolbarModule,
        MatListModule
  ],
  exports: [ChatComponent]
})
export class ChatModule { }
