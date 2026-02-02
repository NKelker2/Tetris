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

    // Tile key will be the color they will spawn on
    public Dictionary<Tile, List<Token>> onClearTokens;
    public Dictionary<TileBase, Token> allTokens;

    void Start() {
        onClearTokens = new Dictionary<Tile, List<Token>>();
        allTokens = new Dictionary<TileBase, Token>();
    }

    void LateUpdate() {
        this.Set();
    }

    public void Set() {
        if (onClearTokens.ContainsKey(curr)) {
            foreach (Token token in onClearTokens[curr]) {
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
        if (onClearTokens.ContainsKey(curr)) {
            foreach (Token token in onClearTokens[curr]) {
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
        if (!onClearTokens.ContainsKey(tile))
            onClearTokens.Add(tile, new List<Token>());
        onClearTokens[tile].Add(new TokenTest(followingPiece, TokenIcons[tokenType], 0, this.board.mirrorMode));
        allTokens.Add(onClearTokens[tile][0].icon, onClearTokens[tile][0]);
    }
}
