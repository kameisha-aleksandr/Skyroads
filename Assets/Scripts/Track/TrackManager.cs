using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrackManager : MonoBehaviour
{
    public event UnityAction SegmentPassed;

    [SerializeField] private TrackSegment segmentPrefab;
    [SerializeField] private Spaceship spaceship;
    [SerializeField] private GameObject planet;
    [SerializeField] private int countOfSegments = 3;
    [SerializeField] private float startChance = 0.1f;

    private List<TrackSegment> trackSegments;
    private float lengthSegment;
    private Vector3 planetStartPos;

    public float StartChance { get => startChance;}

    void Awake()
    {
        planetStartPos = planet.transform.position;
        //generating track segments
        trackSegments = new List<TrackSegment>();
        TrackSegment trackSegment;
        for (int i = 0; i < countOfSegments; i++)
        {
            trackSegment = Instantiate(segmentPrefab, transform);
            if(i==0)
            {
                lengthSegment = trackSegment.LengthSegment;
                spaceship.SetShipMovementLimits(trackSegment.LeftBorderX, trackSegment.RightBorderX);
            }
            trackSegment.gameObject.transform.localPosition = Vector3.forward * (lengthSegment / 2 + i * lengthSegment);
            trackSegments.Add(trackSegment);
        }
    }
    void Update()
    {
        //if the spaceship has passed the segment, then move everything by the length of the segment
        if (spaceship.transform.position.z > lengthSegment)
        {
            SegmentPassed?.Invoke();
            spaceship.transform.position = new Vector3(spaceship.transform.position.x, spaceship.transform.position.y, 0);
            planet.transform.position = planet.transform.position - new Vector3(0, 0, lengthSegment);
        }
    }

    //determination of the chance of activation of obstacles
    public float GetChance()
    {
        return spaceship.GetScore() / 1000f + startChance;
    }
    
    public int GetSegmentsCount()
    {
        return trackSegments.Count;
    }

    //resetting segments to the state of the beginning of the game
    public void ResetSegments()
    {
        foreach (var segment in trackSegments)
        {
            if (segment.transform.position.z < lengthSegment)
                segment.PlaceObstacles(0f);
            else
                segment.PlaceObstacles(startChance);
        }
        planet.transform.position = planetStartPos;
    }
}
