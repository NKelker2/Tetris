using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TokenTilemap : MonoBehaviour {
    public Tilemap tilemap;
    public Tile[] TokenIcons;
    public Tile curr;

    //Ordered: blue, cyan, green, orange, purple, red, yellow    
    public Tile[] colors;

    public Board board;
    public Piece followingPiece;

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

                token.updatePosition();

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

                token.updatePosition();

                this.tilemap.SetTile(token.position, token.icon);
                if (board.mirrorMode)
                    this.tilemap.SetTile(token.mirrorPosition, token.icon);

                token.isActive = false;
            }
        }
    }
}
