using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public event UnityAction<int> ScoreChanged;
    public event UnityAction<int> BestScoreChanged;
    public event UnityAction<int> CountOfPassedObstaclesChanged;

    private int score = 0;
    private int bestScore = 0;
    private int countOfPassedObstacles = 0;

    public int Score { get => score; }

    public void IncreaseScore(int incr)
    {
        score += incr;
        ScoreChanged?.Invoke(score);
        if (score > bestScore)
        {
            bestScore = score;
            BestScoreChanged?.Invoke(bestScore);
        }
    }
    
    public void ResetScore()
    {
        score = 0;
        countOfPassedObstacles = 0;
        ScoreChanged?.Invoke(score);
        CountOfPassedObstaclesChanged?.Invoke(countOfPassedObstacles);
    }

    public bool CheckRecord()
    {
        return bestScore == score;
    }

    public void IncreaseCountOfPassedObstacles()
    {
        countOfPassedObstacles++;
        CountOfPassedObstaclesChanged?.Invoke(countOfPassedObstacles);
    }

    public void SaveBestScore()
    {
        if (bestScore == score)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat");
            bf.Serialize(file, bestScore);
            file.Close();
        } 
    }

    public void LoadBestScore()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            bestScore = (int)bf.Deserialize(file);
            file.Close();
        }
        BestScoreChanged?.Invoke(bestScore);
    }

    public void ResetBestScore()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySaveData.dat");
            bestScore = 0;
            Debug.Log("Data reset complete!");
        }
        else
            Debug.Log("No save data to delete.");
    }
}
