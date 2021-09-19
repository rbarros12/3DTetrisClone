using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TetrominoTests
{
    [UnityTest]
    public IEnumerator MoveLeft()
    {
        var gameObject = new GameObject();
        var tetromino = gameObject.AddComponent<Tetromino>();

        tetromino.MoveTetromino(-1, 0);

        yield return null;

        Assert.AreEqual(new Vector3(-1, 0, 0), gameObject.transform.position);
    }

    [UnityTest]
    public IEnumerator MoveRight()
    {
        var gameObject = new GameObject();
        var tetromino = gameObject.AddComponent<Tetromino>();

        tetromino.MoveTetromino(1, 0);

        yield return null;

        Assert.AreEqual(new Vector3(1, 0, 0), gameObject.transform.position);
    }
}
