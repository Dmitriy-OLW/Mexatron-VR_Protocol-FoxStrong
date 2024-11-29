using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scolzenie_Menu : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private float _speed = 10;
    private Vector3 _offset = new Vector3(0, 0 ,0);
    
    void Update()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            _target.position + _offset,
            _speed * Time.deltaTime);
    }
}