using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSegment : MonoBehaviour
{
    [SerializeField] private Obstacle obstaclePrefab;
    [SerializeField] private float obstacleDistanceMin = 10f;

    private List<Obstacle> obstacles;
    private MeshRenderer rend;
    private float leftBorderX, rightBorderX;
    private float lengthSegment, widthSegment;
    private float shiftSize;
    private TrackManager trackManager;
    public float LengthSegment { get => lengthSegment; }
    public float LeftBorderX { get => leftBorderX; }
    public float RightBorderX { get => rightBorderX; }

    private void OnEnable()
    {
        trackManager.SegmentPassed += OnSegmentPassed;
    }
    private void OnDisable()
    {
        trackManager.SegmentPassed -= OnSegmentPassed;
    }

    void Awake()
    {
        trackManager = GetComponentInParent<TrackManager>();
        rend = GetComponent<MeshRenderer>();
        rightBorderX = rend.bounds.max.x;
        leftBorderX = rend.bounds.min.x;
        lengthSegment = rend.bounds.size.z;
        widthSegment = rend.bounds.size.x;
        
        obstacles = new List<Obstacle>();
        GenerateObstacles();
        PlaceObstacles(0);
    }

    private void Start()
    {
        shiftSize = lengthSegment * trackManager.GetSegmentsCount();
    }

    void Update()
    {
        //move the segment to the end of the track if the ship has passed it
        if (transform.position.z < 0)
        {
            transform.position += Vector3.forward * shiftSize;
            PlaceObstacles(trackManager.GetChance());
        }
    }

    //when passing any segment, shift the segment by its length
    private void OnSegmentPassed()
    {
        transform.position += Vector3.back * lengthSegment;
    }

    //generate obstacles depending on the minimum distance between them
    private void GenerateObstacles()
    {
        Obstacle obstacle;
        float currentPos = obstacleDistanceMin + transform.position.z - lengthSegment / 2;
        while (currentPos <= transform.position.z + lengthSegment / 2)
        {
            obstacle = Instantiate<Obstacle>(obstaclePrefab);
            obstacle.transform.position = new Vector3(0, 0, currentPos);
            obstacle.transform.SetParent(transform);
            obstacles.Add(obstacle);
            currentPos += obstacleDistanceMin;
        }
    }

    //arrange and activate obstacles depending on the current score
    public void PlaceObstacles(float chance)
    {
        Vector3 scoreZonePos;
        foreach (var obstacle in obstacles)
        {
            if(Random.value < chance)
            {
                obstacle.gameObject.SetActive(true);
                float randomPos = Random.Range(leftBorderX + obstacle.Width / 2, rightBorderX - +obstacle.Width / 2);
                obstacle.transform.position = new Vector3(randomPos, obstacle.transform.position.y, obstacle.transform.position.z);
                scoreZonePos = new Vector3(transform.position.x, obstacle.transform.position.y, obstacle.transform.position.z);
                obstacle.ScoreZone.SetWidthAndPos(widthSegment, scoreZonePos);
            }
            else
            {
                obstacle.gameObject.SetActive(false);
            }         
        }
    }
}
