# TicTacToe

Monorepo containing a .NET backend and an Angular frontend for a Tic-Tac-Toe application.

**Contents**
- `TicTacToeBackend/` — .NET Web API backend and tests
- `TicTacToeUI/` — Angular frontend application

**Overview**
- Backend: REST API exposing game and scoreboard endpoints (controllers: `GamesController`, `ScoreboardController`).
- Frontend: Angular single-page app that calls the backend API.

**Prerequisites**
- .NET SDK (targeting `net10.0`) — install from https://dotnet.microsoft.com
- Node.js (16+) and npm — needed for the Angular frontend
- (Optional) Angular CLI: `npm install -g @angular/cli`

**Backend — Build & Run**
- Change to the backend project folder and build:

```bash
cd TicTacToeBackend/TicTacToeBackend
dotnet build
```

- Run the API locally:

```bash
dotnet run
```

- Run unit tests:

```bash
cd ../..
dotnet test TicTacToeBackend.Tests
```

Notes:
- Configuration files: `appsettings.json` and `appsettings.Development.json` are present under TicTacToeBackend/TicTacToeBackend.
- Check `Properties/launchSettings.json` if you need the exact launch URL/port used by the project.

**Frontend — Build & Run**
- Change to the frontend folder, install dependencies and start the dev server:

```bash
cd TicTacToeUI/TicTacToeFrontend
npm install
npm start
# or use the Angular CLI directly:
ng serve --open
```

Notes:
- The frontend expects the backend API to be reachable (check environment or proxy config in the frontend if needed).

**API / Endpoints**
- The API surface is implemented in `TicTacToeBackend/Controllers` — look at `GamesController.cs` and `ScoreboardController.cs` for available routes and request/response DTOs (`DTOs/`).

**Development Tips**
- If you change backend models or DTOs, rebuild and restart the backend before testing from the frontend.
- Use browser devtools and the Angular dev server console to debug frontend requests.

**Project Structure (quick)**
- `TicTacToeBackend/` — Web API project, Services, Models, DTOs, Controllers, Tests
- `TicTacToeUI/TicTacToeFrontend/` — Angular app under `src/`

**Contributing**
- Fork, branch from `main`, add tests for backend logic in `TicTacToeBackend.Tests`, and open a PR with description and testing notes.

**License**
- Add a license file to the repo if you wish to make this project public.
