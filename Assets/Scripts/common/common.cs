using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace common
{
    public static class Raycast
    {
        public static RaycastResult? CheckIfTransparencyHit(RaycastResult clicked, Vector2 screenSpaceCoords)
        {
            Vector3 point = clicked.gameObject.transform.worldToLocalMatrix.MultiplyPoint(screenSpaceCoords);
            RectTransform rect = clicked.gameObject.GetComponent<RectTransform>();
            Texture2D pic = clicked.gameObject.transform.GetComponent<Image>().sprite.texture;
        
            Vector2 uv = new Vector2((point.x/rect.sizeDelta.x) +.5f , (point.y/rect.sizeDelta.y) +.5f);
        
            float alpha = pic.GetPixel((int) (uv.x * pic.width), (int) (uv.y * pic.height)).a;

            if (alpha >= .5f)
                return  clicked;

            return null;
        }
    }
}
