using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Token {
    protected Piece followingPiece;
    protected Mirror followingMirror;

    public Tile icon;
    public Vector3Int position;
    public Vector3Int mirrorPosition;

    protected int cellPosition;
    public bool isActive;

    public Token(Tile icon) {
        this.icon = icon;
        isActive = false;
    }

    public void Initialize(Piece followingPiece, bool mirrorMode) {
        this.followingPiece = followingPiece;

        this.position = followingPiece.position + followingPiece.cells[cellPosition];
        if (mirrorMode) {
            this.followingMirror = followingPiece.mirror;
            this.mirrorPosition = followingMirror.position + followingMirror.cells[cellPosition];
        }
    }

    public void SetCellPosition(int cellPosition) {
        this.cellPosition = cellPosition;
    }

    public abstract int TokenEffect();

    public void UpdatePosition() {
        this.position = this.followingPiece.position + this.followingPiece.cells[cellPosition];
        if (followingMirror != null)
            this.mirrorPosition = this.followingMirror.position + this.followingMirror.cells[cellPosition];
    }
}

public class RedToken : Token {
    public RedToken(Tile icon) : base(icon) { }

    public override int TokenEffect() {
        Log.PrintToGame("TokenTest provided 5 points");
        return 5;
    }

    public override String ToString() {
        return "TokenTest: " + this.position.x + ", " + this.position.y;
    }
}
