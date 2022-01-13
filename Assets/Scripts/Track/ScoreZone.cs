using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ScoreZone : MonoBehaviour
{
    private BoxCollider boxCollider;
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    //put the zone in the position of the obstacle and set the width equal to the width of the track
    public void SetWidthAndPos(float width, Vector3 pos)
    {
        transform.position = pos;
        boxCollider.size = new Vector3(width,1,1);
    }
}
