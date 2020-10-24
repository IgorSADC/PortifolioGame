using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay.GunSystem.Controller
{
    
    public class GunController : MonoBehaviour
    {

        public GunSO TESTGUN;   
        private Dictionary<GunType, Gun> enumMapping;
        private GunType selectedGun;
    

        private void Awake() 
        {
            enumMapping = new Dictionary<GunType, Gun>();
            FindHolders();
        }

        public void ChangeGun(GunSO newGun)
        {
            enumMapping[newGun.Gun].ChangeGun(newGun);
        }

        public void AddBullets(int nBullets)
        {
            if(selectedGun == GunType.Front)
                enumMapping[selectedGun].AddBullets(nBullets);
            else 
            {
                var bQ = (int) nBullets/2;
                enumMapping[GunType.Right].AddBullets(bQ);
                enumMapping[GunType.Left].AddBullets(bQ);
            }
        }
        

        private void FindHolders()
        {
            var holders = GetComponentsInChildren<Gun>();
            foreach (var holder in holders)
            {
                enumMapping[holder.HolderType] = holder;
            }
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.Space))
                ChangeGun(TESTGUN);
            
        }

    }
}