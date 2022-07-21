using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ControlCharacter gets instance of this
public class PlayerMovement
{
    Rigidbody _rigidbody;
    Transform _transform;

    private float forwardSpeed = 10;


    public PlayerMovement(Rigidbody rigidbody, Transform transform)
    {
        _rigidbody = rigidbody;
        _transform = transform;
    }

    public void Move()
    {
        _rigidbody.velocity = _transform.forward * forwardSpeed + new Vector3(0, _rigidbody.velocity.y, 0);
    }

    public void Fall()
    {
        _rigidbody.useGravity = true;
    }
    public void Finish()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.useGravity = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }
}
