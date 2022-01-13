using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipMover : MonoBehaviour
{
    private Rigidbody _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    //spaceship control
    public void Move(float speed, float tiltAngle, float minShipX, float maxShipX)
    {
        float xAxis = Input.GetAxis("Horizontal");

        Vector3 pos = transform.position;
        if (pos.x <= maxShipX && pos.x >= minShipX)
        {
            pos.x += xAxis * speed * Time.deltaTime;
        }
        if (pos.x > maxShipX)
        {
            pos.x = maxShipX;
        }
        else if (pos.x < minShipX)
        {
            pos.x = minShipX;
        }

        transform.position = pos;
        transform.rotation = Quaternion.Euler(0, 0, xAxis * tiltAngle);
        
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            _rigidbody.AddForce(0, 0, speed * 2, ForceMode.Impulse);
        }
    }

    public void ResetSpeed(float startSpeed)
    {
        transform.position = Vector3.zero;
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, startSpeed);
    }

     public float GetCurrentSpeed()
    {
        return _rigidbody.velocity.z;
    }
}
