# Legend of the Power Rangers - Sprint 5
## Overview

This README provides information about our game project **Legend of the Power Rangers** for Sprint 5.

## Setup & Run

### macOS Prerequisites
1.  **Install Dependencies**:
    You need `freeimage` and `freetype` installed. If you have Homebrew, run:
    ```bash
    brew install freeimage freetype
    ```

2.  **Update MonoGame Tools**:
    Ensure you have the latest `dotnet-mgcb` tools (especially for Apple Silicon support):
    ```bash
    dotnet tool update dotnet-mgcb
    dotnet tool update dotnet-mgcb-editor
    dotnet tool update dotnet-mgcb-editor-mac
    ```

### Running the Game
To start the game, run:
```bash
dotnet run
```

## Controls

### Player Controls (Link)
- WASD**: Move Link in four directions (up, down, left, right).
- **Z / N**: Attack with sword.
- **1, 2, 3...**: Select different items (Developer).
- **E**: Cause Link to take damage (Developer).
- **I**: Open/Close Inventory
- **B**: Use active item
- **V**: Shoot orange portal
  
### Mouse Control
-- **Left/Right Click**: Cycle Through Rooms (Developer)

### Other Controls
- **Q**: Quit the game.
- **R**: Reset the game to the initial state.

## Features Implemented
- All features of the original Legend of Zelda's first dungeon
