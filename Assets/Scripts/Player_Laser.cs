using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Laser : MonoBehaviour
{
    //[SerializeField] string name;
    [SerializeField] float RotationInSec = 10f;
    [SerializeField] int bulletSplit_Amount = 0;
    [SerializeField] float bulletSpeed = 1f;
    [SerializeField] float bulletFireSpeed = 5f;
    [SerializeField] bool destroyable = true;

    /*  public string GetName()
      {
          return name;
      }
    */

    void Start()
    {
        StartCoroutine(RotateMe(Vector3.back, RotationInSec));
    }

    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        /*     while (RotationInSec > 0) { 
             for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
             {
                 var fromAngle = transform.rotation;
                 var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);

                 transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
                 yield return null;
             }
             } */
        while (RotationInSec > 0)
        {
            transform.Rotate(Vector3.back * RotationInSec);
            yield return null;
        }

    }

    public float GetbulletSpeed()
    {
        return bulletSpeed;
    }

    public int GetBulletSplit()
    {
            return bulletSplit_Amount;
    }

    public float GetBulletFireSpeed() 
    {
        return bulletFireSpeed;
    }

    public bool GetDestroyable()
    {
        return destroyable;
    }

}
