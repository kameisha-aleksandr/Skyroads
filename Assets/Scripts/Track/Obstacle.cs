using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private MeshRenderer rend;
    private float width;
    private ScoreZone scoreZone;

    public float Width { get => width; }
    public ScoreZone ScoreZone { get => scoreZone; }

    void Awake()
    {
        scoreZone = GetComponentInChildren<ScoreZone>();
        rend = GetComponentInChildren<MeshRenderer>();
        width = rend.bounds.size.x;
    }

}
