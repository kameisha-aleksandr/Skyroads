using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(ShipMover))]
[RequireComponent(typeof(ScoreManager))]
public class Spaceship : MonoBehaviour
{
    public event UnityAction GameOver;

    [SerializeField] private float speed = 20;
    [SerializeField] private float tiltAngle = -45;

    private float minShipX, maxShipX;
    private MeshRenderer rend;
    private ShipMover shipMover;
    private ScoreManager scoreManager;

    private void Awake()
    {
        shipMover = GetComponent<ShipMover>();
        scoreManager = GetComponent<ScoreManager>();
        scoreManager.LoadBestScore();
        StartCoroutine(ScoreChanger());
    }

    private void Update()
    {
        shipMover.Move(speed, tiltAngle, minShipX, maxShipX);
    }

    //death if faced with an obstacle
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Die();
        }
    }
    //if you passed the obstacle scoring zone
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ScoreZone scoreZone))
        {
            scoreManager.IncreaseScore(5);
            scoreManager.IncreaseCountOfPassedObstacles();
        }
    }

    //set the boundaries of movement of the spaceship
    public void SetShipMovementLimits(float minTrackX, float maxTrackX)
    {
        rend = GetComponent<MeshRenderer>();
        minShipX = minTrackX + rend.bounds.extents.x;
        maxShipX = maxTrackX - rend.bounds.extents.x;
    }
      
    public int GetScore()
    {
        return scoreManager.Score;
    }
    public void ResetParametres()
    {
        scoreManager.ResetScore();
        shipMover.ResetSpeed(speed);
    }
    public void Die()
    {
        scoreManager.SaveBestScore();
        GameOver?.Invoke();
    }

    //scoring for time in the game and speed
    private IEnumerator ScoreChanger()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (shipMover.GetCurrentSpeed() > speed * 1.5f)
                scoreManager.IncreaseScore(2);
            else
                scoreManager.IncreaseScore(1);
        }
    }
}
