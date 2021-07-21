using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brain : MonoBehaviour
{
    [SerializeField] GameObject birdObject;
    [SerializeField] GameObject[] wall = new GameObject[2];
    const int generationSize = 100;
    GameObject[] birds = new GameObject[generationSize];
    float[] scoreBirds = new float[generationSize];
    PlayerControl[] birdsPC = new PlayerControl[generationSize];
    const int inputNeurons = 7;
    static float[,] weights = new float[generationSize, inputNeurons];
    //const int hiddenNeurons = 5;
    //static float[,] hiddenLayer = new float[generationSize, hiddenNeurons];
    //static float[,] weights2 = new float[generationSize, hiddenNeurons];
    //float bias = 0f;
    float[] biases = new float[generationSize];
    static int currentBird = 0;
    static int generation = 0;
    //static float[] fitness = new float[generationSize];
    int nexWall;
    //PlayerControl bird;
    [SerializeField] Text scoreMl;
    [SerializeField] Text score;
    [SerializeField] Text birdsLeft;
    [SerializeField] Text generationText;
    [SerializeField] Text bestScoreText;
    const float mutationRate = 0.05f;
    float[] dx = new float[generationSize];
    float[] px = new float[generationSize];
    int bestScore;
    static int bestOvr = 0;
    [SerializeField] GameOver gameOverManager;
    void Start()
    {
        for(int i = 0; i < generationSize; i++)
        {
            dx[i] = birdObject.transform.position.y;
        }

        if (generation == 0)
        {
            for (int i = 0; i < generationSize; i++)
            {
                for (int j = 0; j < inputNeurons; j++)
                {
                    weights[i, j] = Random.Range(-1f, 1f);
                }
                /*for (int j = 0; j < hiddenNeurons; j++)
                {
                    weights2[i, j] = Random.Range(-1f, 1f);
                    hiddenLayer[i, j] = Random.Range(-1f, 1f);
                }*/
                biases[i] = Random.Range(-1f, 1f);
            }
        }

        for(int i = 0; i < generationSize; i++)
        {
            birds[i] = Instantiate(birdObject);
            birds[i].SetActive(true);
            birdsPC[i] = birds[i].GetComponent<PlayerControl>();
        }

        generationText.text = "Generation: 1";
        bestScoreText.text = "Best score ovr: 0";
        birdsLeft.text = "Birds left: " + generationSize.ToString();
        //bird = FindObjectOfType<PlayerControl>();
    }

    public static float Sigmoid(double value)
    {
        return 1.0f / (1.0f + (float)System.Math.Exp(-value));
    }

    void Update()
    {
        for(int i = 0; i < generationSize; i++)
        {
            if(!birdsPC[i].done)
            {
                dx[i] = birds[i].transform.position.y - px[i];
                px[i] = birds[i].transform.position.y;
                //print(dx + "    " + px);

                if (wall[0].transform.position.x + 2 < birds[i].transform.position.x)
                    nexWall = 1;
                else if (wall[1].transform.position.x + 2 < birds[i].transform.position.x)
                    nexWall = 0;
                else if (wall[0].transform.position.x < wall[1].transform.position.x)
                    nexWall = 0;
                else
                    nexWall = 1;

                //wall[nexWall].transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                //wall[nexWall].transform.GetChild(1).GetComponent<Renderer>().material.SetColor("_Color", Color.red);

                int prevWall = 0;
                if (nexWall == 0)
                    prevWall = 1;

                //wall[prevWall].transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                //wall[prevWall].transform.GetChild(1).GetComponent<Renderer>().material.SetColor("_Color", Color.green);


                float neuron = (wall[nexWall].transform.position.y - birds[i].transform.position.y - 8f) * weights[i, 0] +
                        (wall[nexWall].transform.position.y - birds[i].transform.position.y - 6f) * weights[i, 1] +
                        (wall[nexWall].transform.position.x - birds[i].transform.position.x + 2f) * weights[i, 2] +
                        birdsPC[i].rotationSpeed * weights[i, 3] +
                        birdsPC[i].dt * weights[i, 4] +
                        birds[i].transform.rotation.z * weights[i, 5] +
                        //dx[i] * weights[i, 6] +
                        birdsPC[i].getVelocity() * weights[i, 6] + 1.0f;
                        //biases[i];
                //for (int i = 0; i < generationSize; i++)
                //{
                /*for (int j = 0; j < hiddenNeurons; j++)
                {
                    hiddenLayer[currentBird, j] = (wall[nexWall].transform.position.y - birdObject.transform.position.y - 8f) * weights[currentBird, 0] +
                        (wall[nexWall].transform.position.y - birdObject.transform.position.y - 6f) * weights[currentBird, 1] +
                        (wall[nexWall].transform.position.x - birdObject.transform.position.x + 2f) * weights[currentBird, 2] +
                        bird.rotationSpeed * weights[currentBird, 3] + bird.dt * weights[currentBird, 4] +
                        birdObject.transform.rotation.z * weights[currentBird, 5] + bias;
                }*/
                //}
                /*
                float neuron = 0;

                for (int i = 0; i < hiddenNeurons; i++)
                {
                    neuron += hiddenLayer[currentBird, i] * weights2[currentBird, i];
                }
                neuron += bias;
                */

                //print(currentBird);
                if (Sigmoid(neuron) > 0.99f)
                {
                    birdsPC[i].Jump();
                }

                //if (Mathf.Abs(birds[i].transform.position.y + 5f - wall[nexWall].transform.position.y) < 3 && !birdsPC[i].done)
                //{
                    //scoreBirds[i] = float.Parse(scoreMl.text);
                //    birdsPC[i].fitness = float.Parse(scoreMl.text);
                //}
            }
        }
        scoreMl.text = (float.Parse(scoreMl.text) + Time.deltaTime).ToString();
    }

    public void incrementBird(PlayerControl bird)
    {
        bird.fitness = float.Parse(scoreMl.text);
        if (int.Parse(score.text) > bestScore)
            bestScore = int.Parse(score.text);
        if (bestScore > bestOvr)
            bestOvr = bestScore;

        currentBird++;
        birdsLeft.text = "Birds left: " + (generationSize - currentBird).ToString();
        //Debug.Log(currentBird);

        if (currentBird == generationSize)
        {
            //Debug.Log(currentBird);
            currentBird = 1;
            nextGen();
            //Debug.Log("Kuniec");
            gameOverManager.RealGameOver();
            for(int i = 0; i < generationSize; i++)
            {
                birdsPC[i].ResetGame();
            }
            birdsLeft.text = "Birds left: " + generationSize.ToString();
        }
    }

    public void nextGen()
    {
        generation++;
        float[,] newWeights = new float[generationSize, inputNeurons];
        //float[,] newWeights2 = new float[generationSize, hiddenNeurons];
        float[] newBiases = new float[generationSize];
        int best = 0, secondBest = 0;

        for (int i = 1; i < generationSize; i++)
        {
            if (birdsPC[i].fitness > birdsPC[best].fitness)
                best = i;
        }

        for (int i = 1; i < generationSize; i++)
        {
            if (birdsPC[i].fitness > birdsPC[secondBest].fitness && i != best)
                secondBest = i;
        }

        int from = best;
        //selekcja
        /*
        for (int i = 0; i < generationSize; i++)
        {
            for (int j = 0; j < inputNeurons; j++)
            {
                if (Random.Range(0, 1f) < 0.5)
                {
                    from = best;
                }
                else
                {
                    from = secondBest;
                }

                if (Random.Range(0, 1f) < mutationRate)
                {
                    newWeights[i, j] = Random.Range(-1f, 1f);
                }
                else 
                {
                    newWeights[i, j] = weights[from, j];
                }
            }

            for(int j = 0; j < hiddenNeurons; j++)
            {
                if (Random.Range(0, 1f) < 0.5)
                {
                    from = best;
                }
                else
                {
                    from = secondBest;
                }

                if (Random.Range(0, 1f) < mutationRate)
                {
                    newWeights2[i, j] = Random.Range(-1f, 1f);
                }
                else
                {
                    newWeights2[i, j] = weights2[from, j];
                }
            }
        }
        */

        //losowanie
        float fitnessSum = 0f;
        for (int i = 0; i < generationSize; i++)
        {
            fitnessSum += birdsPC[i].fitness;
        }
        for (int i = 0; i < generationSize; i++)
        {
            for (int j = 0; j < inputNeurons; j++)
            {
                float choice = Random.Range(0f, fitnessSum);
                int index = 0;
                while (choice > 0)
                {
                    if (index == generationSize)
                        break;
                    choice -= birdsPC[index].fitness;
                    index++;
                }
                index--;
                if (index == -1)
                    index = 0;

                if (Random.Range(0, 1f) < mutationRate)
                {
                    newWeights[i, j] = Random.Range(-1f, 1f);
                }
                else
                {
                    newWeights[i, j] = weights[index, j];
                }
            }

            float choice2 = Random.Range(0f, fitnessSum);
            int index2 = 0;
            while (choice2 > 0)
            {
                if (index2 == generationSize)
                    break;
                choice2 -= birdsPC[index2].fitness;
                index2++;
            }
            index2--;
            if (index2 == -1)
                index2 = 0;

            if (Random.Range(0, 1f) < mutationRate)
            {
                newBiases[i] = Random.Range(-1f, 1f);
            }
            else
            {
                newBiases[i] = biases[index2];
            }

            /*for (int j = 0; j < hiddenNeurons; j++)
            {
                float choice = Random.Range(0f, fitnessSum);
                int index = 0;
                while (choice > 0)
                {
                    if (index == generationSize)
                        break;
                    choice -= fitness[index];
                    index++;
                }
                index--;
                if (index == -1)
                    index = 0;

                if (Random.Range(0, 1f) < mutationRate)
                {
                    newWeights2[i, j] = Random.Range(-1f, 1f);
                }
                else
                {
                    newWeights2[i, j] = weights2[index, j];
                }
            }*/
        }


        //wybieranie najlepszego
        /*
        for (int i = 0; i < generationSize; i++)
        {
            for (int j = 0; j < inputNeurons; j++)
            {
                if (Random.Range(0, 1f) < mutationRate)
                {
                    newWeights[i, j] = Random.Range(-1f, 1f);
                }
                else
                {
                    newWeights[i, j] = weights[best, j];
                }
            }
        }
        */

        for (int i = 0; i < generationSize; i++)
        {
            for (int j = 0; j < inputNeurons; j++)
            {
                weights[i, j] = newWeights[i, j];
            }
            /*for (int j = 0; j < hiddenNeurons; j++)
            {
                weights2[i, j] = newWeights2[i, j];
            }*/
            biases[i] = newBiases[i];
        }

        float averageFitness = 0;
        float bestFitness = 0;
        for (int i = 0; i < generationSize; i++)
        {
            averageFitness += birdsPC[i].fitness;

            if (birdsPC[i].fitness > bestFitness)
                bestFitness = birdsPC[i].fitness;
        }
        averageFitness /= generationSize;
        //float bestFitness = Mathf.Max(fitness);

        generationText.text = "Generation: " + generation.ToString();
        bestScoreText.text = "Best score ovr: " + bestOvr.ToString();

        print("Gen: " + generation + "      AvrFit: " + averageFitness + "      BestFit: " + bestFitness + "    BestScore: " + bestScore + "    BestOvr: " + bestOvr);
        bestScore = 0;
    }

    public void addWallPoints()
    {
        scoreMl.text = (float.Parse(scoreMl.text) + 10f).ToString();
    }

    public void ResetGame()
    {
        score.text = "0";
        scoreMl.text = "0";
    }
}
