export enum GameMode {
  TwoPlayer = 0,
  Computer = 1
}

export enum GameStatus {
  InProgress = 'InProgress',
  Won = 'Won',
  Draw = 'Draw'
}

export interface GameStateResponse {
  gameId: string;
  board: string[];
  currentPlayer: string;
  mode: GameMode;
  status: GameStatus;
  winner: string | null;
  winningCells: number[] | null;
  moveHistory: MoveRecord[];
  scoreboard: Scoreboard;
}

export interface MoveRecord {
  moveNumber: number;
  player: string;
  row: number;
  column: number;
  cellIndex: number;
  position: string;
}

export interface Scoreboard {
  xWins: number;
  oWins: number;
  draws: number;
  totalGames: number;
}

export interface CreateGameRequest {
  mode: GameMode;
}

export interface MakeMoveRequest {
  cellIndex: number;
  row?: number;
  column?: number;
}

export interface ApiResponse<T> {
  success: boolean;
  data: T;
  message: string;
}