using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Extensions
{
    public static class Vector2Extension 
    {
        public static Vector2 Rotate(this Vector2 vector, float degress)
        {
            float sin = (Mathf.Sin(degress * Mathf.Deg2Rad));
            float cos = (Mathf.Cos(degress * Mathf.Deg2Rad));

            var magX = vector.x;
            var magY = vector.y;

            vector.x = (cos * magX) - (sin * magY);
            vector.y = (sin * magX) + (cos * magY);

            return vector;
        
        }

        
    }

    public static class CameraExtensions
    {
        public static Vector3 GetMousePosition(this Camera cam)
        {
            var mousePos = Input.mousePosition;
            return cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        }
    }

}

