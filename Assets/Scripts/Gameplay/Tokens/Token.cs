using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Token {
    protected Piece followingPiece;
    public Tile icon;
    public Vector3Int position;
    protected int cellPosition;
    public bool isActive;

    public Token(Piece followingPiece, Tile icon, int cellPosition) {
        this.followingPiece = followingPiece;
        this.icon = icon;
        this.cellPosition = cellPosition;

        isActive = true;

        this.position = followingPiece.position + followingPiece.cells[cellPosition];
    }

    public abstract int TokenEffect();

    public void updatePosition() {
        this.position = followingPiece.position + followingPiece.cells[cellPosition];
    }
}

public class TokenTest : Token {
    public TokenTest(Piece followingPiece, Tile icon, int cellPosition) : base(followingPiece, icon, cellPosition) { }

    public override int TokenEffect() {
        Log.PrintToGame("TokenTest provided 5 points");
        return 5;
    }
}
