using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.DesignPatterns;

public class BulletTemplate : MonoBehaviour, IPool
{
    public event OnModifyPool DeactivateObject;
    public event OnModifyPool ActivateObject;
    public Vector3 Position { get; set; }
    public bool IsAvailable { get ; set ; }

    public float timeToLive;
    public float bulletSpeed = 15f;

    private void Awake() {
        IsAvailable = true;
    }
    public virtual void Initialize()
    {
        //This is important because the gun is Initialized two times. One whe its unavailable (taken as a chosen object)
        //And other when the gun system behaviour trigger the OnBulletDone event.
        //Whe this happens, the bullet needs to activate it's corroutine, but can't clear it's position again.
        //This is another greate use of the IsAvailable variable. It tells you if the gun is beign acativate a secod time by some system.
        //Remember that the IPool object is never supposed to change its availability state.

        
        if(IsAvailable)
        {
            this.transform.localPosition = Vector3.zero;
            this.transform.localRotation = transform.parent.localRotation;
        }
        else
        {
            StartCoroutine(DestroyingAfterTime());
            this.transform.parent = null;
        }

        
    }

    private IEnumerator DestroyingAfterTime()
    {
        yield return new WaitForSeconds(timeToLive);
        DeactivateObject(this.gameObject);
    }

    private void Update() 
    {
        transform.position += transform.forward * Time.deltaTime * bulletSpeed;
    }
}
