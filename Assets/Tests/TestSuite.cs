using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestSuiteSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // 1
    private Game game;

    [SetUp]
    public void Setup()
    {
        GameObject gameGameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        game = gameGameObject.GetComponent<Game>();
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.Destroy(game.gameObject);
    }

    // 2
    [UnityTest]
    public IEnumerator AsteroidsMoveDown()
    {
        // 4
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        // 5
        float initialYPos = asteroid.transform.position.y;
        // 6
        yield return new WaitForSeconds(0.1f);
        // 7
        Assert.Less(asteroid.transform.position.y, initialYPos);
        // 8
    }

    [UnityTest]
    public IEnumerator GameOverOccursOnAsteroidCollision()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        //1
        asteroid.transform.position = game.GetShip().transform.position;
        //2
        yield return new WaitForSeconds(0.1f);

        //3
        Assert.True(game.isGameOver);
    }

    //1
    [Test]
    public void NewGameRestartsGame()
    {
        //2
        game.isGameOver = true;
        game.NewGame();
        //3
        Assert.False(game.isGameOver);
    }


    [UnityTest]
    public IEnumerator LaserDestroysAsteroid()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        UnityEngine.Assertions.Assert.IsNull(asteroid);
    }

    [UnityTest]
    public IEnumerator LaserMovesUp()
    {
        // 1
        GameObject laser = game.GetShip().SpawnLaser();
        // 2
        float initialYPos = laser.transform.position.y;
        yield return new WaitForSeconds(0.1f);
        // 3
        Assert.Greater(laser.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator AsteroidDestroyedOffScreen()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();

        asteroid.transform.position = new Vector3(asteroid.transform.position.x, -10f, asteroid.transform.position.z);

        yield return new WaitForSeconds(0.1f);

        UnityEngine.Assertions.Assert.IsNull(asteroid);
    }

    [UnityTest]
    public IEnumerator ScoreResetsOnNewGame()
    {
        game.score = 1;
        //2
        game.isGameOver = true;
        game.NewGame();

        yield return new WaitForSeconds(0.1f);

        //3
        Assert.True(game.score == 0);
    }

    [UnityTest]
    public IEnumerator UpMovement()
    {
        //Create ship and get its start position
        Ship ship = game.GetShip();
        Vector2 startPos = game.GetShip().transform.position;

        //Move the ship up
        game.GetShip().MoveUp();

        //Wait
        yield return new WaitForSeconds(0.1f);

        //Store new posisiton after moving up
        Vector2 newPos = game.GetShip().transform.position;

        //If new possition is greater than start position we succesfully moved up
        Assert.True(startPos.y < newPos.y);
    }

    [UnityTest]
    public IEnumerator DownMovement()
    {
        //Create ship and get its start position
        Ship ship = game.GetShip();
        Vector2 startPos = game.GetShip().transform.position;

        //Move the ship Down
        game.GetShip().MoveDown();

        //Wait
        yield return new WaitForSeconds(0.1f);

        //Store new posisiton after moving down
        Vector2 newPos = game.GetShip().transform.position;

        //If new possition is less than start position we succesfully moved down
        Assert.True(startPos.y > newPos.y);
    }
    //Test by Ruslan
    [UnityTest]
    public IEnumerator ShipStaysInBoundsWhenMovingUp()
    {
        float maxUp = -35;

        Ship ship = game.GetShip();

        ship.transform.position = new Vector3(0.0f, maxUp, 0.0f);

        Vector2 startPos = ship.transform.position;

        //Move the ship Down
        game.GetShip().MoveUp();

        //Wait
        yield return new WaitForSeconds(0.5f);

        //If new possition is less than start position we succesfully moved down
        Assert.Greater(game.GetShip().transform.position.y, maxUp);
    }
    //Test by Ruslan
    [UnityTest]
    public IEnumerator ShipStaysInBoundsWhenMovingDown()
    {
        float maxDown = 3;

        Ship ship = game.GetShip();

        ship.transform.position = new Vector3(0.0f, maxDown, 0.0f);

        Vector2 startPos = ship.transform.position;

        //Move the ship Down
        game.GetShip().MoveDown();

        //Wait
        yield return new WaitForSeconds(0.5f);

        //If new possition is less than start position we succesfully moved down
        Assert.Less(game.GetShip().transform.position.y, maxDown);
    }

    //Test by Ruslan
    [UnityTest]
    public IEnumerator ShipIsNotDestroyedWhenCollidingWithBullet()
    {
        // 1
        Ship Ship = game.GetShip();
        Ship.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        UnityEngine.Assertions.Assert.IsNotNull(Ship);
    }
}
