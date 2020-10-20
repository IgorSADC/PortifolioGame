using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.DesignPatterns;

public class BulletTemplate : MonoBehaviour, IPool
{
    public event OnModifyPool DeactivateObject;
    public event OnModifyPool ActivateObject;
    public Vector3 Position { get; set; }
    public float timeToLive;

    public virtual void Initialize()
    {
        Debug.Log("Initialized called");
        this.transform.localPosition = Vector3.zero;
        this.transform.rotation = Quaternion.Euler(0,0,0);
        this.transform.parent = null;
        StartCoroutine(DestroyingAfterTime());
    }

    private IEnumerator DestroyingAfterTime(){
        yield return new WaitForSeconds(timeToLive);
        DeactivateObject(this.gameObject);
    }

    private void Update() {
        transform.position += transform.forward * Time.deltaTime * 10f;
    }
}
