using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class Board : MonoBehaviour {
    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }
    public Piece mirroredPiece { get; private set; }
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
        Piece[] pieces = GetComponentsInChildren<Piece>();

        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = pieces[0];
        if (mirrorMode)
            this.mirroredPiece = pieces[1];

        //get value from log.cs
        this.score = GetComponentInChildren<Scoring>();

        for (int i = 0; i < this.tetrominos.Length; i++) {
            tetrominos[i].Initialize();
        }

        score.onClearEffects.Add("red", new List<Effect>());
        score.onClearEffects["red"].Add(new RedCombo());

        Log.actionLog = actionLogReference;
    }

    private void Start() {
        //initialize log text
        SpawnPiece();
        Log.printToGame("Game has started");
    }

    public void SpawnPiece() {
        int random = UnityEngine.Random.Range(0, this.tetrominos.Length);
        TetrominoData data = tetrominos[random];

        if (mirrorMode) {
            this.activePiece.Initialize(this, new Vector3Int((Bounds.xMin + this.spawnPosition.x) / 2, this.spawnPosition.y), data);
            this.mirroredPiece.Initialize(this, new Vector3Int((Bounds.xMax + this.spawnPosition.x) / 2, this.spawnPosition.y), data);
            if (IsValidPosition(this.activePiece, this.spawnPosition)) {
                Set(this.activePiece);
                Set(this.mirroredPiece);
            }
            else
                GameOver();
        }
        else {
            this.activePiece.Initialize(this, this.spawnPosition, data);
            if (IsValidPosition(this.activePiece, this.spawnPosition))
                Set(this.activePiece);
            else
                GameOver();
        }
    }

    private void GameOver() {
        this.tilemap.ClearAllTiles();
    }

    // placing tiles of a piece on the board/tilemap
    public void Set(Piece piece) {
        for (int i = 0; i < piece.cells.Length; i++) {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }

    // removes tiles of a piece from the board/tilemap
    public void Clear(Piece piece) {
        for (int i = 0; i < piece.cells.Length; i++) {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
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
            Log.printToGame(combo + " Lines have been cleared");
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

        // clears current row
        for (int col = bounds.xMin; col < bounds.xMax; col++) {
            Vector3Int position = new Vector3Int(col, row, 0);
            // add to a linear data structure what tile color got removed(whatever is currently there)
            colors[col - bounds.xMin] = this.tilemap.GetTile(position);
            this.tilemap.SetTile(position, null);
        }

        // drops all lines above one row
        while (row < bounds.yMax) {
            for (int col = bounds.xMin; col < bounds.xMax; col++) {
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase above = this.tilemap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                this.tilemap.SetTile(position, above);
            }

            row++;
        }

        score.LineScore(colors, combo);
    }
}
