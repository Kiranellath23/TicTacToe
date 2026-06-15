import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GameStateResponse, GameStatus } from '../../models/game.model';

@Component({
  selector: 'app-game-board',
  standalone: false,
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.css']
})
export class GameBoardComponent {
  @Input() gameState: GameStateResponse | null = null;
  @Output() cellClicked = new EventEmitter<number>();

  // expose enum to template without shadowing the imported symbol
  gameStatusEnum = GameStatus;

  onCellClick(cellIndex: number): void {
    console.log(`Cell ${cellIndex} clicked`);
    if (!this.gameState || this.gameState.status !== GameStatus.InProgress) {
      console.log('Game not in progress, ignoring click');
      return; // Game not in progress
    }

    if (this.gameState.board[cellIndex] !== ' ') {
      return; // Cell already occupied
    }

    this.cellClicked.emit(cellIndex);
  }

  isWinningCell(cellIndex: number): boolean {
    return this.gameState?.winningCells?.includes(cellIndex) ?? false;
  }

  getCellContent(cellIndex: number): string {
    return this.gameState?.board[cellIndex] ?? ' ';
  }

  getCellClass(cellIndex: number): string {
    const cell = this.getCellContent(cellIndex);
    let classes = 'cell';

    if (cell === 'X') classes += ' x-player';
    if (cell === 'O') classes += ' o-player';
    if (this.isWinningCell(cellIndex)) classes += ' winning';
    if (cell === ' ' && this.gameState?.status === GameStatus.InProgress) {
      classes += ' clickable';
    }

    return classes;
  }
}
