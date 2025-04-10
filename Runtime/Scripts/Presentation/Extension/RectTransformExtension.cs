using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JABARACdesign.Base.Presentation.Extension
{
    public static class RectTransformExtension
    {
        private static Vector2 _vector2;
        
        #region SetAnchoredPosition
        
        public static void SetAnchoredPositionX(this RectTransform rectTransform, float x)
        {
            _vector2 = rectTransform.anchoredPosition;
            _vector2.x = x;
            rectTransform.anchoredPosition = _vector2;
        }
        
        public static void SetAnchoredPositionY(this RectTransform rectTransform, float y)
        {
            _vector2 = rectTransform.anchoredPosition;
            _vector2.y = y;
            rectTransform.anchoredPosition = _vector2;
        }
        
        public static void SetAnchoredPosition(this RectTransform rectTransform, float x, float y)
        {
            _vector2 = rectTransform.anchoredPosition;
            _vector2.x = x;
            _vector2.y = y;
            rectTransform.anchoredPosition = _vector2;
        }
        
        #endregion SetAnchoredPosition
        
        #region AddAnchoredPosition
        
        public static void AddAnchoredPositionX(this RectTransform rectTransform, float x)
        {
            _vector2 = rectTransform.anchoredPosition;
            _vector2.x += x;
            rectTransform.anchoredPosition = _vector2;
        }
        
        public static void AddAnchoredPositionY(this RectTransform rectTransform, float y)
        {
            _vector2 = rectTransform.anchoredPosition;
            _vector2.y += y;
            rectTransform.anchoredPosition = _vector2;
        }
        
        public static void AddAnchoredPosition(this RectTransform rectTransform, float x, float y)
        {
            _vector2 = rectTransform.anchoredPosition;
            _vector2.x += x;
            _vector2.y += y;
            rectTransform.anchoredPosition = _vector2;
        }
        
        #endregion AddAnchoredPosition
        
        #region SetSize
        
        public static void SetSizeX(this RectTransform rectTransform, float x)
        {
            _vector2 = rectTransform.sizeDelta;
            _vector2.x = x;
            rectTransform.sizeDelta = _vector2;
        }
        
        public static void SetSizeY(this RectTransform rectTransform, float y)
        {
            _vector2 = rectTransform.sizeDelta;
            _vector2.y = y;
            rectTransform.sizeDelta = _vector2;
        }
        
        public static void SetSize(this RectTransform rectTransform, float x, float y)
        {
            _vector2 = rectTransform.sizeDelta;
            _vector2.x = x;
            _vector2.y = y;
            rectTransform.sizeDelta = _vector2;
        }
        
        #endregion SetSize
        
        #region SetScale
        
        public static void SetLocalScaleX(this RectTransform rectTransform, float x)
        {
            _vector2 = rectTransform.localScale;
            _vector2.x = x;
            rectTransform.localScale = _vector2;
        }
        
        public static void SetLocalScaleY(this RectTransform rectTransform, float y)
        {
            _vector2 = rectTransform.localScale;
            _vector2.y = y;
            rectTransform.localScale = _vector2;
        }
        
        public static void SetLocalScale(this RectTransform rectTransform, float x, float y)
        {
            _vector2 = rectTransform.localScale;
            _vector2.x = x;
            _vector2.y = y;
            rectTransform.localScale = _vector2;
        }
        
        #endregion
        
        #region SetOffset
        
        public static void SetOffsetLeft(this RectTransform rectTransform, float left)
        {
            _vector2 = rectTransform.offsetMin;
            _vector2.x = left;
            rectTransform.offsetMin = _vector2;
        }
        
        public static void SetOffsetRight(this RectTransform rectTransform, float right)
        {
            _vector2 = rectTransform.offsetMax;
            _vector2.x = -right;
            rectTransform.offsetMax = _vector2;
        }
        
        public static void SetOffsetTop(this RectTransform rectTransform, float top)
        {
            _vector2 = rectTransform.offsetMax;
            _vector2.y = -top;
            rectTransform.offsetMax = _vector2;
        }
        
        public static void SetOffsetBottom(this RectTransform rectTransform, float bottom)
        {
            _vector2 = rectTransform.offsetMin;
            _vector2.y = bottom;
            rectTransform.offsetMin = _vector2;
        }
        
        public static void SetOffsetMinX(this RectTransform rectTransform, float x)
        {
            _vector2 = rectTransform.offsetMin;
            _vector2.x = x;
            rectTransform.offsetMin = _vector2;
        }
        
        public static void SetOffsetMinY(this RectTransform rectTransform, float y)
        {
            _vector2 = rectTransform.offsetMin;
            _vector2.y = y;
            rectTransform.offsetMin = _vector2;
        }
        
        public static void SetOffsetMin(this RectTransform rectTransform, float x, float y)
        {
            _vector2 = rectTransform.offsetMin;
            _vector2.x = x;
            _vector2.y = y;
            rectTransform.offsetMin = _vector2;
        }
        
        public static void SetOffsetMaxX(this RectTransform rectTransform, float x)
        {
            _vector2 = rectTransform.offsetMax;
            _vector2.x = x;
            rectTransform.offsetMax = _vector2;
        }
        
        public static void SetOffsetMaxY(this RectTransform rectTransform, float y)
        {
            _vector2 = rectTransform.offsetMax;
            _vector2.y = y;
            rectTransform.offsetMax = _vector2;
        }
        
        public static void SetOffsetMax(this RectTransform rectTransform, float x, float y)
        {
            _vector2 = rectTransform.offsetMax;
            _vector2.x = x;
            _vector2.y = y;
            rectTransform.offsetMax = _vector2;
        }
        
        #endregion SetOffset
        
        #region Children
        
        public static int GetChildrenCount(this RectTransform rectTransform)
        {
            return rectTransform.childCount;
        }
        
        public static float GetChildrenTotalWidth(this RectTransform rectTransform)
        {
            float width = 0;
            foreach (RectTransform child in rectTransform)
            {
                width += child.rect.width;
            }
            
            return width;
        }
        
        public static float GetChildrenTotalHeight(this RectTransform rectTransform)
        {
            float height = 0;
            foreach (RectTransform child in rectTransform)
            {
                height += child.rect.height;
            }
            
            return height;
        }
        
        public static float GetMaxChildrenWidth(this RectTransform rectTransform)
        {
            float width = 0;
            foreach (RectTransform child in rectTransform)
            {
                if (child.rect.width > width)
                {
                    width = child.rect.width;
                }
            }
            
            return width;
        }
        
        public static float GetMaxChildrenHeight(this RectTransform rectTransform)
        {
            float height = 0;
            foreach (RectTransform child in rectTransform)
            {
                if (child.rect.height > height)
                {
                    height = child.rect.height;
                }
            }
            
            return height;
        }
        
        public static List<RectTransform> GetChildren(this RectTransform rectTransform)
        {
            List<RectTransform> children = new();
            foreach (RectTransform child in rectTransform)
            {
                children.Add(item: child);
            }
            
            return children;
        }
        
        public static void DeleteAllChildren(this RectTransform rectTransform, List<GameObject> ignoreObjects = null)
        {
            foreach (RectTransform child in rectTransform)
            {
                if (ignoreObjects == null || !ignoreObjects.Contains(item: child.gameObject))
                {
                    Object.Destroy(obj: child.gameObject);
                }
            }
        }
        
        #if UNITY_EDITOR
        
        public static void DeleteAllChildrenImmediateOnEditor(
            this RectTransform rectTransform,
            List<GameObject> ignoreObjects = null)
        {
            foreach (RectTransform child in rectTransform)
            {
                if (ignoreObjects == null || !ignoreObjects.Contains(item: child.gameObject))
                {
                    EditorApplication.delayCall += () =>
                        Object.DestroyImmediate(obj: child.gameObject);
                }
            }
        }
        
        #endif
        
        #endregion Children
    }
}