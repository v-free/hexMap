﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexUtil
{
    public static HexDirection OppositeDirection(this HexDirection dir)
    {
        if ((int)dir < 3)
            return dir + 3;
        else
            return dir - 3;
    }

    public static HexDirection PreviousDirection(this HexDirection direction)
    {
        if (direction == HexDirection.NE)
            return HexDirection.NW;
        else
            return direction - 1;
    }

    public static HexDirection NextDirection(this HexDirection direction)
    {
        if (direction == HexDirection.NW)
            return HexDirection.NE;
        else
            return direction + 1;
    }

    public static int DirectionRotation(this HexDirection direction)
    {
        return (int)direction * 60 + 30;
    }

    public static int StepRotation(this HexDirection direction, HexDirection other)
    {
        int side = (int)Mathf.Sign((int)direction - (int)other);
        return side * 60;
    }
}

public enum HexDirection
{
    NE,
    E,
    SE,
    SW,
    W,
    NW
}

public enum HexSlopeType
{
    Flat,
    Slope,
    Cliff
}