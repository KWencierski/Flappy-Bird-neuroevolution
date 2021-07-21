
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    //[SerializeField] GameObject wallPrefab;
    //[SerializeField] Vector2 secondsBetweenSpawns;
    //float nextSpawnTime;
    //Vector2 screenHalfSizeWorldUnit;
    //[SerializeField] float minSpawnY;

    //void Start()
    //{
        //screenHalfSizeWorldUnit = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
    //}

    //void Update()
    //{
        /*
         
         if (Time.time > nextSpawnTime)
        {
            float secondsBetweenSpawns = Mathf.Lerp(secondsBetweenSpawnsMinMax.y, secondsBetweenSpawnsMinMax.x, Difficulty.GetDifficultyPercent());
            //print(secondsBetweenSpawns);
            nextSpawnTime = Time.time + secondsBetweenSpawns;

            float spawnAngle = Random.Range(-spawnAngleMax, spawnAngleMax);
            float spawnSize = Random.Range(spawnSizeMinMax.x, spawnSizeMinMax.y);
            Vector2 spawnPosition = new Vector2(Random.Range(-screenHalfSizeWorldUnit.x, screenHalfSizeWorldUnit.x), screenHalfSizeWorldUnit.y + spawnSize);
            GameObject newBlock = (GameObject)Instantiate(fallingBlockPrefab, spawnPosition, Quaternion.Euler(Vector3.forward * spawnAngle));
            newBlock.transform.localScale = Vector2.one * spawnSize;
        }*/
    //}
}
