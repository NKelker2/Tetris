using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TokenTilemap : MonoBehaviour {
    public Tilemap tilemap;
    public Tile curr;

    //Ordered: blue, cyan, green, orange, purple, red, yellow    
    public Tile[] colors;

    public Board board;
    public Piece followingPiece;

    void Start() {
        foreach (List<Token> list in PlayerData.onClearTokens.Values) {
            foreach (Token token in list) {
                token.Initialize(this.followingPiece, this.board.mirrorMode);
            }
        }
    }

    void LateUpdate() {
        this.Set();
    }

    public void Set() {
        if (PlayerData.onClearTokens.ContainsKey(curr)) {
            foreach (Token token in PlayerData.onClearTokens[curr]) {
                if (token.isActive) {
                    this.tilemap.SetTile(token.position, null);
                    if (board.mirrorMode)
                        this.tilemap.SetTile(token.mirrorPosition, null);
                }

                token.UpdatePosition();

                this.tilemap.SetTile(token.position, token.icon);
                if (board.mirrorMode)
                    this.tilemap.SetTile(token.mirrorPosition, token.icon);

                token.isActive = true;
            }
        }
    }

    public void HardSet() {
        if (PlayerData.onClearTokens.ContainsKey(curr)) {
            foreach (Token token in PlayerData.onClearTokens[curr]) {
                if (token.isActive) {
                    this.tilemap.SetTile(token.position, null);
                    if (board.mirrorMode)
                        this.tilemap.SetTile(token.mirrorPosition, null);
                }

                token.UpdatePosition();

                this.tilemap.SetTile(token.position, token.icon);
                if (board.mirrorMode)
                    this.tilemap.SetTile(token.mirrorPosition, token.icon);

                token.isActive = false;
            }
        }
    }
}
