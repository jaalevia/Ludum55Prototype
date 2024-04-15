using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotation : MonoBehaviour
{
    [SerializeField] private float _speed;
    public GameObject form;
    [SerializeField] private GameObject[] _3dObjectsToSpawn = new GameObject[5];
    
    void Start()
    {
        
    }


    void Update()
    {
        float XaxisRotation = Input.GetAxis("Mouse X") * _speed;
        float YaxisRotation = Input.GetAxis("Mouse Y") * _speed;

        form.transform.Rotate(Vector3.down, XaxisRotation);
        form.transform.Rotate(Vector3.right, YaxisRotation);
    }

    void ObjectToSpawn()
    { 
        
    }
    
}
