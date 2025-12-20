using System;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour {
    public Tilemap tilemap { get; private set; }
    public TetrominoData[] tetrominoes;
    public void Awake() {
        this.tilemap = GetComponentInChildren<Tilemap>();

        for (int i = 0; i < tetrominoes.Length; i++) {
            this.tetrominoes[i].Initialize();
        }
    }
}