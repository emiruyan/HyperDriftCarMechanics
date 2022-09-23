using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   [SerializeField] private Transform carTransform;
   public Vector3 _offset;


   private void Update()
   {
      transform.position = Vector3.Lerp(transform.position, carTransform.position + _offset, Time.deltaTime *  5);
   }
}
