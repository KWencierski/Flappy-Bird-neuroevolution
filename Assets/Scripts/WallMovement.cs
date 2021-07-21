using UnityEngine;
using UnityEngine.UI;

public class WallMovement : MonoBehaviour
{
    static float wallSpeed = 2.5f;
    float screenHalfWidthInWorldUnits;
    float heightMin = 3;
    float heightMax = 8;
    [SerializeField] Text score;
    //[SerializeField] Text scoreMl;
    bool pointAdded = false;
    [SerializeField] GameObject bird;
    [SerializeField] GameObject otherWall;
    float diffX;
    Brain brain;
    float initX;
    void Start()
    {
        float wallWidth = transform.localScale.x / 2f;
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize + wallWidth;
        transform.position = new Vector2(transform.position.x, Random.Range(heightMin, heightMax));
        initX = transform.position.x;

        diffX = Mathf.Abs(transform.position.x - otherWall.transform.position.x);

        brain = FindObjectOfType<Brain>();
    }
    private void Update()
    {
        //transform.Translate(Vector3.left * wallSpeed * Time.deltaTime);
        transform.position += Vector3.left * wallSpeed * Time.deltaTime;

        if (transform.position.x < -screenHalfWidthInWorldUnits - 2)
        {
            //transform.position = new Vector2(screenHalfWidthInWorldUnits - 2, Random.Range(heightMin, heightMax));
            transform.position = new Vector2(otherWall.transform.position.x + diffX, Random.Range(heightMin, heightMax));
            pointAdded = false;
        }
        
        if (!pointAdded && transform.position.x + 2 <= bird.transform.position.x)
        {
            score.text = (int.Parse(score.text) + 1).ToString();
            pointAdded = true;
            brain.addWallPoints();
        }
    }


    public void ResetGame()
    {
        transform.position = new Vector3(initX, Random.Range(heightMin, heightMax));
    }
    //void fixedUpdate()
    //{
        
    //}
}
