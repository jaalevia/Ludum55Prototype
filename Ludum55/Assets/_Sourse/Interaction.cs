using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    [SerializeField] bool _isInRange = false;
    [SerializeField] KeyCode _interactionKeyCode;
    [SerializeField] KeyCode _destroyKeyCode;
    [SerializeField] int _playerLayerNumber;
    [SerializeField] private GameObject[] _objectToSpawn = new GameObject[1];
    [SerializeField] Transform _cameraTransform;
    [SerializeField] bool isPicked = false;
    [SerializeField] PlayerMovement _playerMovementScript;
    [SerializeField] LoseTestScript _loseTestScript;

    //Переменная которая отвечает за тип предмета(Обычный предмет - 0, Проклятый предмет - 1)
    public int TypeOfObject;

    public UnityEvent InteractAction;
    void Start()
    {
        TypeOfObject = Random.Range(0, 1);
    }

    void Update()
    {
        if(_isInRange)
        {
            if (Input.GetKeyDown(_interactionKeyCode) & isPicked == false)
            {
                Instantiate(_objectToSpawn[TypeOfObject], _cameraTransform.transform.position, Quaternion.identity);
                _playerMovementScript._speed = 0;
            }
            else if (Input.GetKeyDown(_interactionKeyCode) & isPicked == true)
            {
                Destroy(_objectToSpawn[TypeOfObject]);
                _playerMovementScript._speed = 5;
            }
            else if (Input.GetKeyDown(_destroyKeyCode))
            {
                if (TypeOfObject == 1)
                {
                    _loseTestScript.LosePoints += 1;
                }
                else
                { 
                    _loseTestScript.WinPoints += 1;
                }
                Destroy(_objectToSpawn[TypeOfObject]);
                isPicked = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _playerLayerNumber)
            { 
            _isInRange = true;
            }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _playerLayerNumber)
        {
            _isInRange = false;
        }
    }
}
