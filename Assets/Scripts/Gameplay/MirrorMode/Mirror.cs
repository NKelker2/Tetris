using UnityEngine;

public class Mirror : MonoBehaviour {

    public Board board { get; private set; }
    public Vector3Int position { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public TetrominoData data { get; private set; }

    public Piece mirrorPiece;

    public void Initialize(Board board, Vector3Int position, TetrominoData data) {
        this.board = board;
        this.position = position;
        this.data = data;

        if (this.cells == null) {
            cells = new Vector3Int[data.cells.Length];
        }

        for (int i = 0; i < data.cells.Length; i++) {
            this.cells[i] = new Vector3Int(this.mirrorPiece.cells[i].x * -1, this.mirrorPiece.cells[i].y);
        }

        board.Set(this);
    }

    private void LateUpdate() {
        board.Clear(this);

        Vector3Int newPosition = new Vector3Int(this.mirrorPiece.position.x * -1 - 1, this.mirrorPiece.position.y);
        this.position = newPosition;

        this.Copy();
    }

    public void hardDrop() {
        board.Clear(this);

        Vector3Int newPosition = this.position;
        Vector3Int testPosition = newPosition;

        testPosition.y -= 1;

        while (board.IsValidPosition(this, testPosition)) {
            newPosition = testPosition;
            testPosition.y -= 1;
        }

        this.position = newPosition;
        board.Set(this);
    }

    public void Copy() {
        for (int i = 0; i < this.cells.Length; i++) {
            this.cells[i] = new Vector3Int(this.mirrorPiece.cells[i].x * -1, this.mirrorPiece.cells[i].y);
        }

        board.Set(this);
    }
}
