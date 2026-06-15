import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  GameStateResponse,
  CreateGameRequest,
  MakeMoveRequest,
  Scoreboard,
  ApiResponse
} from '../models/game.model';

@Injectable({
  providedIn: 'root'
})
export class GameApiService {
  private apiUrl = 'https://localhost:7271/api';

  constructor(private http: HttpClient) {}

  createGame(request: CreateGameRequest): Observable<ApiResponse<GameStateResponse>> {
    return this.http.post<ApiResponse<GameStateResponse>>(
      `${this.apiUrl}/games`,
      request
    );
  }

  getGameState(gameId: string): Observable<ApiResponse<GameStateResponse>> {
    return this.http.get<ApiResponse<GameStateResponse>>(
      `${this.apiUrl}/games/${gameId}`
    );
  }

  makeMove(gameId: string, request: MakeMoveRequest): Observable<ApiResponse<GameStateResponse>> {
    return this.http.post<ApiResponse<GameStateResponse>>(
      `${this.apiUrl}/games/${gameId}/moves`,
      request
    );
  }

  undoLastMove(gameId: string): Observable<ApiResponse<GameStateResponse>> {
    return this.http.post<ApiResponse<GameStateResponse>>(
      `${this.apiUrl}/games/${gameId}/undo`,
      {}
    );
  }

  resetGame(gameId: string): Observable<ApiResponse<GameStateResponse>> {
    return this.http.post<ApiResponse<GameStateResponse>>(
      `${this.apiUrl}/games/${gameId}/reset`,
      {}
    );
  }

  getScoreboard(): Observable<ApiResponse<Scoreboard>> {
    return this.http.get<ApiResponse<Scoreboard>>(
      `${this.apiUrl}/scoreboard`
    );
  }

  resetScoreboard(): Observable<ApiResponse<Scoreboard>> {
    return this.http.post<ApiResponse<Scoreboard>>(
      `${this.apiUrl}/scoreboard/reset`,
      {}
    );
  }
}