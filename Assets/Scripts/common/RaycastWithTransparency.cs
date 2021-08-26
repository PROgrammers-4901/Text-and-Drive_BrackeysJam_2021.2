using System.Linq;
using UnityEngine;

namespace common
{
    public static class RaycastWithTransparency
    {
        public static RaycastHit? Raycast(Ray ray)
        {
            var res = Physics.RaycastAll(ray, float.MaxValue).ToList().OrderBy(h => h.distance);
            foreach (var h in res)
            {
                var col = h.collider;
                Renderer rend = h.transform.GetComponent<Renderer>();
                Texture2D tex = rend.material.mainTexture as Texture2D;
                var xInTex = (int) (h.textureCoord.x*tex.width);
                var yInTex = (int) (h.textureCoord.y*tex.height);
                var pix = tex.GetPixel(xInTex, yInTex);
                if (pix.a > 0)
                {
                    Debug.Log("You hit: " + col.name + " position " + h.textureCoord.x + " , " + h.textureCoord.y);
                    return h;
                 
                }
            }
            return null;
        }
    }
}
