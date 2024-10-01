# Legend of the Power Rangers - Sprint 2

## Overview

This README provides information about our game project **Legend of the Power Rangers** for Sprint 2.

## Controls

### Player Controls (Link)
- **Arrow Keys / WASD**: Move Link in four directions (up, down, left, right).
- **Z / N**: Attack with sword.
- **1, 2, 3...**: Select different items (placeholder for future menu system).
- **E**: Cause Link to take damage.
  
### Block/Obstacle Controls
- **T / Y**: Cycle between the current block/obstacle being displayed (stationary, no interactions).

### Item Controls
- **U / I**: Cycle between items (items move and animate but do not interact).

### Enemy/NPC Controls
- **O / P**: Cycle between enemies or NPCs (they move and animate but do not interact).

### Other Controls
- **Q**: Quit the game.
- **R**: Reset the game to the initial state.

## Features Implemented
- **Object Categories**:
  - Player (Link)
  - Blocks/Obstacles
  - Items
  - Enemies/NPCs
- **Basic Gameplay Mechanics**:
  - Movement for all objects.
  - State changes for Link and other objects.
  - Sprite animation for different objects.
- **Object List Cycling**: Ability to cycle through blocks, items, and enemies with keyboard inputs.
- **Command Pattern**: Used for handling user input to separate input from object behavior.
- **State Pattern**: Implemented for object state management (e.g., Link's direction, attack, damage).

## Known Bugs
- When attacking, if Link attempts to move at the same time he becomes stuck in place
- When attacking, if link is already holding a movement key and holds attack he will continuously attack
