using Gameplay.GunSystem;
using Gameplay.GunSystem.Controller;
using UnityEngine;
using Utils.DesignPatterns;

public class DestructableBox : MonoBehaviour, IBulleted
{
    public GameObject colletactable; 

    public void TakeShot(GameObject bullet)
    {
        colletactable.SetActive(true);
        gameObject.SetActive(false);
    }
}
