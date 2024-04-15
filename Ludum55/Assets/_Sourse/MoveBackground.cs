using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _clampPosition;

    [SerializeField] private Vector3 _position;
    void Start()
    {
        _position = transform.position;
    }

    void FixedUpdate()
    {
        float _newPosition = Mathf.Repeat(Time.time * _speed, _clampPosition);
        transform.position = _position + Vector3.down * _newPosition;
    }
}
