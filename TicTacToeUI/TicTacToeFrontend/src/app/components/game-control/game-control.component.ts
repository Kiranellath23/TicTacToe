import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GameMode, GameStatus } from '../../models/game.model';

@Component({
  selector: 'app-game-controls',
  standalone: false,
  templateUrl: './game-control.component.html',
  styleUrls: ['./game-control.component.css']
})
export class GameControlsComponent {
  @Input() gameMode: GameMode = GameMode.TwoPlayer;
  @Input() gameStatus: GameStatus = GameStatus.InProgress;
  @Input() moveHistoryLength: number = 0;
  @Input() isLoading: boolean = false;

  @Output() modeSelected = new EventEmitter<GameMode>();
  @Output() resetGameClicked = new EventEmitter<void>();
  @Output() undoClicked = new EventEmitter<void>();
  @Output() resetScoreboardClicked = new EventEmitter<void>();

  GameMode = GameMode;
  GameStatus = GameStatus;

  selectMode(mode: GameMode): void {
    this.modeSelected.emit(mode);
  }

  onResetGame(): void {
    if (confirm('Reset the current game?')) {
      this.resetGameClicked.emit();
    }
  }

  onUndo(): void {
    this.undoClicked.emit();
  }

  onResetScoreboard(): void {
    if (confirm('Reset the scoreboard? This cannot be undone.')) {
      this.resetScoreboardClicked.emit();
    }
  }

  isUndoDisabled(): boolean {
    return this.moveHistoryLength === 0 || this.isLoading || this.gameStatus !== GameStatus.InProgress;
  }
}
