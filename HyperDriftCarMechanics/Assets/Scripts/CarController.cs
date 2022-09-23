using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float carSpeed;//Arabanın hızı
    [SerializeField] private float maxSpeed;//Arabanın çıkabileceği maximum hız
    [SerializeField] private float Traction;//Çekiş gücü

    [SerializeField] private float steerAngle;

    private float dragAmount=0.99f;//Sürtünme kuvveti 

    private Vector3 _moveVec;
    private Vector3 _rotVec;

    public Transform lw, rw;//Ön tekerleklerin transformu
    


    private void Update()
    {
        CarInputandMovement();
    }

    private void CarInputandMovement()
    {
        // if (Input.GetMouseButton(0))//Mouse sol click ile hareket ettirme işlemini yaptık
        // {
        //     _moveVec += transform.forward * carSpeed * Time.deltaTime;
        //     transform.position += _moveVec * Time.deltaTime;
        // }
        
        //Arabayı ileri doğru sürekli hareket ettirmek için değerler oluşturduk ve _moveVec'e atadık
        _moveVec += transform.forward * carSpeed * Time.deltaTime;
        transform.position += _moveVec * Time.deltaTime;//_moveVec'i deltaTime ile çarpıp transform'a atadık

        _rotVec += new Vector3(0, Input.GetAxis("Horizontal"), 0);
        
        transform.Rotate(Vector3.up * Input.GetAxis("Horizontal")* steerAngle * Time.deltaTime *_moveVec.magnitude);

        _moveVec *= dragAmount;
        _moveVec = Vector3.ClampMagnitude(_moveVec, maxSpeed);//Arabanın hızını sınırladık
        _moveVec = Vector3.Lerp(_moveVec.normalized, transform.forward, Traction * Time.deltaTime) * _moveVec.magnitude; 
        _rotVec = Vector3.ClampMagnitude(_rotVec, steerAngle);//Tekerlerin  dönüşünü sınırladık
        
        //localRotation girme sebebimiz tekerlerin arabanın içinde ayrı bir rotation'a sahip olması
        lw.localRotation = Quaternion.Euler(_rotVec);
        rw.localRotation = Quaternion.Euler(_rotVec);


    }
}
