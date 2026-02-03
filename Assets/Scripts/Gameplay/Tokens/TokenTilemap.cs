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

    public void addOnClearToken(Tile tile, int tokenType) {
        if (!PlayerData.onClearTokens.ContainsKey(tile))
            PlayerData.onClearTokens.Add(tile, new List<Token>());

        PlayerData.onClearTokens[tile].Add(new TokenTest(followingPiece, TokenIcons[tokenType], PlayerData.onClearTokens[tile].Count, this.board.mirrorMode));

        if (!PlayerData.allTokens.ContainsKey(PlayerData.onClearTokens[tile][0].icon))
            PlayerData.allTokens.Add(PlayerData.onClearTokens[tile][0].icon, PlayerData.onClearTokens[tile][0]);
    }
}
