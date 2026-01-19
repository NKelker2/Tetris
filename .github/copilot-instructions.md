# Tetris Game - AI Coding Agent Instructions

## Project Overview

A Unity-based Tetris implementation with a tilemap-based grid system, piece management, collision detection, wall kick mechanics, and ghost piece preview functionality.

## Architecture & Key Components

### Board (`Assets/Scripts/Board.cs`)

- **Responsibility**: Game grid management, piece placement, line clearing, collision validation
- **Key Properties**:
  - `Tilemap`: Stores placed pieces; centered coordinate system (origin at center)
  - `boardSize`: Default 10×20 (width × height)
  - `Bounds`: Returns RectInt with board boundaries for collision checks
  - `activePiece`: Currently falling piece reference
  - `tetrominos[]`: Array of all 7 Tetromino types (initialized at Awake)
- **Critical Methods**:
  - `IsValidPosition(piece, position)`: Returns false if position exceeds bounds OR overlaps placed tiles
  - `Set(piece)`: Places piece cells on tilemap at piece.position + piece.cells offsets
  - `Clear(piece)`: Removes piece from tilemap (called before and after movement/rotation)
  - `ClearLines()`: Scans rows, clears full lines, drops rows above downward
  - `SpawnPiece()`: Random tetromino selection, calls GameOver if spawn position invalid

### Piece (`Assets/Scripts/Piece.cs`)

- **Responsibility**: Active piece control, input handling, physics (stepping/locking), rotation
- **Movement System**:
  - `stepDelay` (1.0s default): Auto-drop interval
  - `lockDelay` (0.5s default): Time before piece locks after touching bottom
  - `lockTime`: Accumulates on movement; resets to 0 when piece moves (enables sliding)
- **Input Keys** (hardcoded, not using InputSystem despite asset presence):
  - Q/E: Rotate left/right
  - A/D: Move left/right
  - S: Soft drop
  - Space: Hard drop (instant lock)
- **Rotation Logic**:
  - Uses `ApplyRotationMatrix()` with SRS (Super Rotation System) via wall kicks
  - Special handling: I-pieces and O-pieces use 0.5f center offset; others use standard rounding
  - Reverts rotation if wall kick tests fail
- **Data Flow**: Board clears piece → Update physics → Set piece on tilemap each frame

### Tetromino & TetrominoData (`Assets/Scripts/Tetromino.cs`)

- **Enum**: I, O, T, J, L, S, Z (7 types)
- **TetrominoData struct**:
  - `cells[]`: Relative offsets for 4 block positions
  - `tile`: Colored tile visual (serialized in inspector)
  - `wallKicks[,]`: 2D array of wall kick offset attempts per rotation state
  - `Initialize()`: Populates cells and wallKicks from Data static tables

### Data (`Assets/Scripts/Data.cs`)

- **Global lookup tables**:
  - `RotationMatrix[]`: Precomputed cos/sin for 90° rotation (multiplied by direction ±1)
  - `Cells[]`: Dictionary mapping each Tetromino type to its 7 cell shapes (initial spawn orientations)
  - `WallKicks[]`: SRS wall kick offsets (I-pieces have different table than JLOSTZ group)
- **Wall Kick Structure**: 8 rows × 5 columns (4 rotation states × 2 directions, 5 offset candidates each)

### Ghost (`Assets/Scripts/Ghost.cs`)

- **Responsibility**: Preview where active piece will land (LateUpdate ensures it renders after piece)
- **Mechanism**:
  - Mirrors trackingPiece cells and position
  - Iterates downward from current position until collision
  - Updates every frame in LateUpdate to render on separate tilemap layer

## Developer Workflows

### Building & Running

- Open `Tetris.slnx` in Visual Studio or edit scripts in Unity Editor
- Attach Inspector to Tetris Scene in `Assets/Scenes/Tetris.unity`
- Configure tetrominos array in Board inspector (7 slots, each with TetrominoData and Tile assignment)

### Debugging Piece Behavior

- Use `Board.IsValidPosition()` validation to test collision logic
- Check rotation: verify `rotationIndex` progression (0→1→2→3→0 cycle)
- Wall kick failures silently revert rotation; add debug logs to `TestWallKicks()` if rotation doesn't respond

### Common Patterns

- **Clear-before-placement**: Always `board.Clear(piece)` before position changes, then `board.Set()` after
- **Bounds checking**: `Bounds` uses centered coordinate system; negative Y goes down
- **Line clear iteration**: Starts from yMin, advances only when non-full rows; clears leave gaps that get filled by downward shift

## Critical Implementation Details

### Coordinate System

- **Origin**: Center of board (0, 0)
- **X-axis**: Left (negative) to right (positive)
- **Y-axis**: Down (negative) to up (positive)
- Spawn position typically (0, boardSize.y/2) for top-center entry

### Lock Mechanics

- Piece locks when `lockTime >= lockDelay` during Step()
- Movement resets `lockTime = 0f`, allowing slide-under behavior
- Hard drop bypasses step timing, immediately locks

### Rotation with SRS

- Wall kick attempts occur in order (index 0→4); first successful offset wins
- O-piece uses same wallKicks table as others but has no actual rotatable state (all orientations identical)
- I-piece uses exclusive wall kick table with larger offset ranges

### Tilemap Persistence

- Placed pieces stay on Board's tilemap permanently; only removed by line clears
- Ghost uses separate tilemap layer for non-destructive preview
- Collision checks scan tilemap's `HasTile()` state

## Integration Points

- **Input**: Hardcoded KeyCode polling (not New Input System despite InputSystem_Actions asset)
- **Physics**: Time.time-based stepping, no rigidbody integration
- **Rendering**: Unity's Tilemap visual system (no manual sprite management)
- **Scene**: Single scene (Tetris.unity) with Board + Piece + Ghost hierarchically linked

## Next Steps for Extensions

- Add score/level tracking by hooking `Board.ClearLines()` calls
- Implement next-piece preview queue (add Queue&lt;TetrominoData&gt; to Board)
- Switch to New Input System if InputSystem_Actions needs use
- Add audio by listening for Lock and ClearLines events
