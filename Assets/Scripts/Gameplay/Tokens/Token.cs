using System;
using Unity.VisualScripting;
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

    public Token(Piece followingPiece, Tile icon, int cellPosition, bool mirrorMode) {
        this.followingPiece = followingPiece;
        this.icon = icon;
        this.cellPosition = cellPosition;

        isActive = true;

        this.position = followingPiece.position + followingPiece.cells[cellPosition];
        if (mirrorMode) {
            this.followingMirror = followingPiece.mirror;
            this.mirrorPosition = followingMirror.position + followingMirror.cells[cellPosition];
        }
    }

    public abstract int TokenEffect();

    public void updatePosition() {
        this.position = this.followingPiece.position + this.followingPiece.cells[cellPosition];
        if (followingMirror != null)
            this.mirrorPosition = this.followingMirror.position + this.followingMirror.cells[cellPosition];
    }
}

public class TokenTest : Token {
    public TokenTest(Piece followingPiece, Tile icon, int cellPosition, bool mirrorMode) : base(followingPiece, icon, cellPosition, mirrorMode) {
        Log.PrintToGame("Created new token at: " + cellPosition);
    }

    public override int TokenEffect() {
        Log.PrintToGame("TokenTest provided 5 points");
        return 5;
    }

    public override String ToString() {
        return "TokenTest: " + this.position.x + ", " + this.position.y;
    }
}
