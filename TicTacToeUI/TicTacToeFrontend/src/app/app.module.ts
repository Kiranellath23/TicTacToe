import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GameBoardComponent } from './components/game-board/game-board.component';
import { MoveHistoyComponent } from './components/move-histoy/move-histoy.component';
import { ScoreboardComponent } from './components/scoreboard/scoreboard.component';
import { GameControlsComponent } from './components/game-control/game-control.component';

import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { GameContainerComponent } from './components/game-container/game-container.component';

@NgModule({
  declarations: [
    AppComponent,
    GameBoardComponent,
    MoveHistoyComponent,
    ScoreboardComponent,
    GameControlsComponent,
    GameContainerComponent


  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    CommonModule,
    AppRoutingModule,

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
