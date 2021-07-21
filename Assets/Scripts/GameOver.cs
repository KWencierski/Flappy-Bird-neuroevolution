using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    //[SerializeField] GameObject wall1;
    //[SerializeField] GameObject wall2;
    //[SerializeField] GameObject birdObject;
    Brain brain;
    //PlayerControl bird;
    [SerializeField] WallMovement wall1;
    [SerializeField] WallMovement wall2;
    // Start is called before the first frame update
    void Start()
    {
        //bird = FindObjectOfType<PlayerControl>();
        //bird.OnPlayerDeath += OnGameOver;
        brain = FindObjectOfType<Brain>();
    }

    //void OnGameOver()
    //{
    //    /*bird.rotationSpeed = 0;
    //    birdObject.transform.position = bird.initPos;
    //    wall1.transform.position = new Vector3(3.5f, 4.5f, 3.474609f);
    //    wall2.transform.position = new Vector3(7.8f, 4.7f, 3.474609f);
    //    */
    //    brain.incrementBird();
    //    //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}

    public void RealGameOver()
    {
        brain.ResetGame();
        //bird.ResetGame();
        wall1.ResetGame();
        wall2.ResetGame();
    }
}
