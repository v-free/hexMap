﻿using Assets.Scripts.HexImpl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreindalMap : IMapGenerator<HexNode>
{
    private const string tiles = "GGGGGGGGGGGGFFFGGGGGGGGGFFFFFFFFFFFFFFFFWWWWWWWGGGGGFFFGGGGGGFFFFFFFFF" +
        "FFFFFFFFFFGWWWWWWWGGGGFFFGGGGFFFFFFFFFFFFFFFFFFFFFGWWWWWWGGGGGFFFGFFFFFFFGGGFFF" +
        "FFFFFFFFFFFGGWWWWWWGGGGFFFFFFFFFGGGGGGGGGGFFFFFFFFFGWWWWWWGGGGFFFFFFFFGGGGGGGGG" +
        "GGGGGFFFFFFFGGWWWWWGGGGGFFFFFFGGGGGGGGGGGGGGGGGGFFGGGGWWWWGGGGGFFFFFGGGGGGGGGGG" +
        "SGGGGGGGGGGGGGGWWWWGGGGGFFFFFGGGGGGGGGGGSSSSSSSSGGGMGGGWWWGGGGGFFFFFGGGGGGSSSSS" +
        "SSSSSSSSSMMMMMGGGWWGGGGGFFFFGGGGGGSSSSSSSSSFWSSSSSMMMMGGGWGGGGGFFFFGGGGGGGGGGGS" +
        "SSSFFWSSSMMMMMMGGGWWGGGGFFFGGGGGGGGGGGGSSSSSSSSSSSGGGGGGGGWGGGGFFFFGGMMMGGGGGGG" +
        "SSSSSSSSSSSGGGGGGGGGWGGGFFFFGGGMMMMGGGGGGGGSSSSSSSGGGGGGGGGGGGGGFGGGGGGMMMMGGGG" +
        "GGGGGGGSSSGGGGGGGGGGGGGGGGGGGGGGMMMMMGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGMMMMMGG" +
        "GGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGMMMMMMGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGMMMMMMM" +
        "MGGGGGGGGGGGGGGGGGG";

    public ITile<HexNode>[] GenerateTiles(int sizeX, int sizeY)
    {
        sizeX = 40;
        sizeY = 20;
        char[] tileChars = tiles.ToCharArray();
        ITile<HexNode>[] generated = new TileInfo[sizeX * sizeY];
        for (int i = 0; i < tileChars.Length; i++)
        {
            char c = tileChars[i];

            int elevation = Random.Range(0, 5);
            if (c == 'M')
                elevation = 15;
            else if (c == 'W')
                elevation = -5;

            generated[i] = new TileInfo(HexTerrain.GetTerrainFromChar(c), elevation);
        }

        return generated;
    }
}