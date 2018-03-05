﻿using Assets.Scripts.HexImpl;
using GraphAlgorithms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestUnit : MonoBehaviour, IUnit<HexNode>
{
    [SerializeField] private float moveSpeed = 1.2f;
    [SerializeField] private float rotateSpeed = 0.2f;
    [SerializeField] private float displacementY = 1;

    private ITile<HexNode> tile;
    private HexDirection orientation;
    private bool performingAction = false;

    public void Move(IEnumerable<IPathNode<HexNode>> path)
    {
        StartCoroutine(MoveWaiter(path));
    }

    private IEnumerator MoveWaiter(IEnumerable<IPathNode<HexNode>> path)
    {
        if (path == null)
        {
            Debug.Log("Can't find path!!");
            yield break;
        }
        performingAction = true;

        IEnumerator<IPathNode<HexNode>> enumerator = path.GetEnumerator();
        enumerator.MoveNext(); // Skips first;
        while(enumerator.MoveNext())
        {
            IPathNode<HexNode> node = enumerator.Current;
            yield return Step(node);
        }

        performingAction = false;
    }

    private IEnumerator Step(IPathNode<HexNode> pathNode)
    {
        HexNode node = pathNode.GetNode();

        //if (tile.X == node.X && tile.Z == node.Z && orientation == node.Direction)
        //    yield break;

        if (node.Direction != orientation)
            yield return Rotate(node);
        else
            yield return Walk(node);
    }

    private IEnumerator Walk(HexNode node)
    {
        ITile<HexNode> tile = node.Tile;

        Vector3 startPoint = transform.position;
        Vector3 nodePoint = new Vector3(tile.WorldPosX, tile.WorldPosY, tile.WorldPosZ) + Vector3.up * displacementY;

        float elapsedTime = 0;
        while (elapsedTime < moveSpeed)
        {
            Vector3 between = Vector3.Lerp(startPoint, nodePoint, elapsedTime / moveSpeed);
            transform.position = between;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.position = nodePoint;
        HexEngine.Singleton.MoveUnit(this, tile.X, tile.Z, node.X, node.Z);
    }

    private IEnumerator Rotate(HexNode node)
    {
        float elapsedTime = 0;
        Quaternion start = transform.rotation;
        Vector3 startEuler = start.eulerAngles;

        float yIncrement = HexUtil.StepRotation(orientation, node.Direction);
        Quaternion end = Quaternion.Euler(startEuler.x, startEuler.y + yIncrement, startEuler.z);

        while (elapsedTime < rotateSpeed)
        {
            Quaternion between = Quaternion.Lerp(start, end, elapsedTime / rotateSpeed);
            transform.rotation = between;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        orientation = node.Direction;
        transform.rotation = end;
    }

    public bool PerformingAction()
    {
        return performingAction;
    }

    public int MaxActionPoints
    {
        get
        {
            return 2;
        }
    }

    public int CurrentActionPoints
    {
        get
        {
            return 2;
        }
    }

    public int Direction
    {
        get
        {
            return (int)orientation;
        }
    }

    public float DisplacementY
    {
        get
        {
            return displacementY;
        }
    }

    public ITile<HexNode> Tile
    {
        get
        {
            return tile;
        }

        set
        {
            tile = value;
        }
    }
}
