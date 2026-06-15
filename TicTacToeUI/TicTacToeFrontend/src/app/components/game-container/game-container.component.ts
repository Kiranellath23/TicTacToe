import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CreateGameRequest, GameMode, GameStateResponse, GameStatus, MakeMoveRequest, Scoreboard } from '../../models/game.model';
import { GameApiService } from '../../services/game-api.service';

@Component({
  selector: 'app-game-container',
  standalone: false,
  templateUrl: './game-container.component.html',
  styleUrls: ['./game-container.component.css']
})
export class GameContainerComponent implements OnInit {
  gameState: GameStateResponse | null = null;
  currentMode: GameMode = GameMode.TwoPlayer;
  scoreboard:Scoreboard | null = null;
  isLoading = false;
  errorMessage: string | null = null;
  // expose enum to template
  GameStatus = GameStatus;

  constructor(private gameApi: GameApiService) {}

  ngOnInit(): void {
    this.startNewGame(GameMode.TwoPlayer);
    this.loadScoreboard();
  }

  startNewGame(mode: GameMode): void {
    this.isLoading = true;
    this.errorMessage = null;
    this.currentMode = mode;

    const request: CreateGameRequest = { mode };

    this.gameApi.createGame(request).subscribe(
      (response) => {
        if (response.success) {
          this.gameState = response.data;
        } else {
          this.errorMessage = response.message;
        }
        this.isLoading = false;
      },
      (error) => {
        this.errorMessage = 'Failed to create game: ' + error.message;
        this.isLoading = false;
      }
    );
  }

  onCellClick(cellIndex: number): void {
    if (!this.gameState || this.isLoading) return;

    this.isLoading = true;
    this.errorMessage = null;

    const request: MakeMoveRequest = { cellIndex };

    this.gameApi.makeMove(this.gameState.gameId, request).subscribe(
      (response) => {
        if (response.success) {
          this.gameState = response.data;
          if(this.gameState.status === GameStatus.Won || this.gameState.status === GameStatus.Draw) {
            this.loadScoreboard();
          }
        } else {
          this.errorMessage = response.message;
        }
        this.isLoading = false;
      },
      (error) => {
        this.errorMessage = 'Move failed: ' + error.message;
        this.isLoading = false;
      }
    );
  }

  onModeSelected(mode: GameMode): void {
    this.startNewGame(mode);
  }

  onResetGame(): void {
    if (!this.gameState || this.isLoading) return;

    this.isLoading = true;
    this.errorMessage = null;

    this.gameApi.resetGame(this.gameState.gameId).subscribe(
      (response) => {
        if (response.success) {
          this.gameState = response.data;
        } else {
          this.errorMessage = response.message;
        }
        this.isLoading = false;
      },
      (error) => {
        this.errorMessage = 'Reset failed: ' + error.message;
        this.isLoading = false;
      }
    );
  }

  onUndoMove(): void {
    if (!this.gameState || this.isLoading) return;

    this.isLoading = true;
    this.errorMessage = null;

    this.gameApi.undoLastMove(this.gameState.gameId).subscribe(
      (response) => {
        if (response.success) {
          this.gameState = response.data;
        } else {
          this.errorMessage = response.message;
        }
        this.isLoading = false;
      },
      (error) => {
        this.errorMessage = 'Undo failed: ' + error.message;
        this.isLoading = false;
      }
    );
  }
  loadScoreboard(): void {
    this.gameApi.getScoreboard().subscribe((res)=>{
     
        if (res.success) {
          this.scoreboard = res.data;
        }
    },
      (error) => {
        console.error('Failed to load scoreboard:', error);
      }
    );

  }

  onResetScoreboard(): void {
    this.isLoading = true;
    this.errorMessage = null;

    this.gameApi.resetScoreboard().subscribe(
      (response) => {
        if (response.success && this.gameState) {
          this.scoreboard = response.data;
        } else if (!response.success) {
          this.errorMessage = response.message;
        }
        this.isLoading = false;
      },
      (error) => {
        this.errorMessage = 'Scoreboard reset failed: ' + error.message;
        this.isLoading = false;
      }
    );
  }
}
