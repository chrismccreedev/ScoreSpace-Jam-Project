using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScoreSpace
{
    public class BackgroundRotator : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;
        private float _zAngle = 0;

        void Update()
        {
            _zAngle += _rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0,0,_zAngle);
        }
    }
}
