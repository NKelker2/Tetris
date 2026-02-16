using System;

using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class Board : MonoBehaviour {
    public Tilemap tilemap { get; private set; }

    public TokenTilemap tokenTileMap;

    public Piece activePiece { get; private set; }

    public Mirror mirroredPiece { get; private set; }

    public TetrominoData[] tetrominos;
    public Vector3Int spawnPosition;
    public Vector2Int boardSize = new Vector2Int(10, 20);

    public TextMeshProUGUI actionLogReference;

    public Boolean mirrorMode = false;

    private Scoring score;

    //moved log string var from here log.cs
    //public TextMeshProUGUI actionLog;

    public RectInt Bounds {
        get {
            Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
        }
    }

    private void Awake() {

        this.tilemap = GetComponentInChildren<Tilemap>();

        this.activePiece = GetComponentInChildren<Piece>();

        this.mirroredPiece = GetComponentInChildren<Mirror>();

        //get value from log.cs
        this.score = GetComponentInChildren<Scoring>();

        for (int i = 0; i < this.tetrominos.Length; i++) {
            tetrominos[i].Initialize();
        }

        Log.actionLog = actionLogReference;
    }

    public void Start() {

        if (PlayerData.currRound % 3 == 0)
            mirrorMode = true;

        SpawnPiece();
        Log.PrintToGame("Round has started");
        if (mirrorMode) Log.PrintToGame("Mirror mode is active");
    }

    public void SpawnPiece() {
        int random = UnityEngine.Random.Range(0, this.tetrominos.Length);
        TetrominoData data = tetrominos[random];

        if (mirrorMode) {
            this.activePiece.Initialize(this, new Vector3Int((Bounds.xMin + this.spawnPosition.x) / 2, this.spawnPosition.y), data);
            this.mirroredPiece.Initialize(this, new Vector3Int(this.activePiece.position.x * -1 - 1, this.spawnPosition.y), data);


            if (IsValidPosition(this.activePiece, this.activePiece.position)) {
                Set(this.activePiece);
            }
            else {
                Log.PrintToGame("Game over");
                GameOver();
            }
        }
        else {
            this.mirroredPiece.enabled = false;

            this.activePiece.Initialize(this, this.spawnPosition, data);

            if (IsValidPosition(this.activePiece, this.spawnPosition))
                Set(this.activePiece);
            else
                GameOver();
        }
    }

    private void GameOver() {
        this.tilemap.ClearAllTiles();
        this.tokenTileMap.tilemap.ClearAllTiles();
        SceneSwap.MoveScenes(2);
    }

    // placing tiles of a piece on the board/tilemap
    public void Set(Piece piece) {
        for (int i = 0; i < piece.cells.Length; i++) {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }

    // set method for mirror piece
    public void Set(Mirror mirror) {
        for (int i = 0; i < mirror.cells.Length; i++) {
            Vector3Int tilePosition = mirror.cells[i] + mirror.position;
            this.tilemap.SetTile(tilePosition, mirror.data.tile);
        }
    }

    // removes tiles of a piece from the board/tilemap
    public void Clear(Piece piece) {
        for (int i = 0; i < piece.cells.Length; i++) {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }

    // removes mirror's tiles from the board
    public void Clear(Mirror mirror) {
        for (int i = 0; i < mirror.cells.Length; i++) {
            Vector3Int tilePosition = mirror.cells[i] + mirror.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position) {
        RectInt bounds = this.Bounds;

        for (int i = 0; i < piece.cells.Length; i++) {
            Vector3Int tilePosition = piece.cells[i] + position;
            if (!bounds.Contains((Vector2Int)tilePosition)) {
                return false;
            }
            if (this.tilemap.HasTile(tilePosition)) {
                return false;
            }
        }
        return true;
    }

    public bool IsValidPosition(Mirror mirror, Vector3Int position) {
        RectInt bounds = this.Bounds;

        for (int i = 0; i < mirror.cells.Length; i++) {
            Vector3Int tilePosition = mirror.cells[i] + position;
            if (!bounds.Contains((Vector2Int)tilePosition)) {
                return false;
            }
            if (this.tilemap.HasTile(tilePosition)) {
                return false;
            }
        }
        return true;
    }

    // log in the action log
    // how many lines got cleared at once
    public void ClearLines() {
        int combo = 0;

        RectInt bounds = this.Bounds;
        int row = bounds.yMin;

        while (row < bounds.yMax) {
            if (IsLineFull(row)) {
                combo++;
                LineClear(row, combo);
            }
            else {
                row++;
            }
        }
        if (combo >= 1)
            Log.PrintToGame(combo + " Lines have been cleared");
    }
    private bool IsLineFull(int row) {
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++) {
            Vector3Int position = new Vector3Int(col, row, 0);

            if (!this.tilemap.HasTile(position)) {
                return false;
            }
        }
        return true;
    }

    // log in the action log
    // color of tile used to complete the line (check what color the current piece is)
    // color tiles that got cleared
    private void LineClear(int row, int combo) {
        RectInt bounds = this.Bounds;
        TileBase[] colors = new TileBase[10];

        int bonusPoints = 0;

        // clears current row
        for (int col = bounds.xMin; col < bounds.xMax; col++) {
            Vector3Int position = new Vector3Int(col, row, 0);
            // add to a linear data structure what tile color got removed(whatever is currently there)
            colors[col - bounds.xMin] = this.tilemap.GetTile(position);

            TileBase currToken = this.tokenTileMap.tilemap.GetTile(position);
            if (currToken != null) {
                if (PlayerData.allTokens.ContainsKey(currToken))
                    bonusPoints += PlayerData.allTokens[currToken].TokenEffect();
            }
            this.tilemap.SetTile(position, null);
        }

        // drops all lines above one row
        while (row < bounds.yMax) {
            for (int col = bounds.xMin; col < bounds.xMax; col++) {
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase above = this.tilemap.GetTile(position);
                TileBase aboveToken = this.tokenTileMap.tilemap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                this.tilemap.SetTile(position, above);
                this.tokenTileMap.tilemap.SetTile(position, aboveToken);
            }

            row++;
        }

        score.LineScore(colors, combo, bonusPoints);
    }
}
