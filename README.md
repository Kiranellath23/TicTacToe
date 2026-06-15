# TicTacToe

Monorepo containing a .NET backend and an Angular frontend for a Tic-Tac-Toe application.

Contents
- `TicTacToeBackend/` — .NET Web API backend and tests
- `TicTacToeUI/` — Angular frontend application

Overview
- Backend: REST API exposing game and scoreboard endpoints (see `TicTacToeBackend/Controllers`).
- Frontend: Angular single-page app that calls the backend API.

Prerequisites
- .NET SDK (targeting `net10.0`) — https://dotnet.microsoft.com
- Node.js (16+) and npm
- (Optional) Angular CLI: `npm install -g @angular/cli`

Backend — Build & Run
1. Change to the backend project folder and build:

```powershell
cd TicTacToeBackend/TicTacToeBackend
dotnet build
```

2. Run the API locally:

```powershell
dotnet run
```

3. Run unit tests:

```powershell
cd ../..
dotnet test TicTacToeBackend.Tests
```

Notes:
- Configuration files: `appsettings.json` and `appsettings.Development.json` are under `TicTacToeBackend/TicTacToeBackend`.
- Use `Properties/launchSettings.json` to find the launch URL/port.

Frontend — Build & Run
1. Change to the frontend folder, install dependencies and start the dev server:

```bash
cd TicTacToeUI/TicTacToeFrontend
npm install
npm start
# or with Angular CLI:
ng serve --open
```

Notes:
- The frontend expects the backend API to be reachable. Adjust proxy or environment config if needed.

API / Endpoints
- See `TicTacToeBackend/Controllers/GamesController.cs` and `ScoreboardController.cs` for routes and DTOs in `TicTacToeBackend/DTOs`.

Development Tips
- Rebuild and restart the backend after changing models or DTOs.
- Use browser devtools and the Angular dev server console to inspect requests.

Project Structure (quick)
- `TicTacToeBackend/` — Web API, Services, Models, DTOs, Controllers, Tests
- `TicTacToeUI/TicTacToeFrontend/` — Angular app under `src/`

Contributing
- Fork, branch from `main`, add tests in `TicTacToeBackend.Tests`, and open a PR with description and testing notes.

License
- Add a `LICENSE` file if you plan to publish this repository.
