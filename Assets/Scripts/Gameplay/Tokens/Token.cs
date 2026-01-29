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
        this.position = followingPiece.position + followingPiece.cells[cellPosition];
        if (followingMirror != null)
            this.mirrorPosition = followingMirror.position + followingMirror.cells[cellPosition];
    }
}

public class TokenTest : Token {
    public TokenTest(Piece followingPiece, Tile icon, int cellPosition, bool mirrorMode) : base(followingPiece, icon, cellPosition, mirrorMode) { }

    public override int TokenEffect() {
        Log.PrintToGame("TokenTest provided 5 points");
        return 5;
    }
}
