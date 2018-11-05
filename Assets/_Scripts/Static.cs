using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text;
using System.Collections.Generic;

public static class Static
{
    private static Vector3 vectorZero = Vector3.zero;
    public static Vector3 VectorZero { get { return vectorZero; } }

    private static Vector3 vectorOne = Vector3.one;
    public static Vector3 VectorOne { get { return vectorOne; } }
    
    public static float targetTimeScale = 1.0f;
    public static readonly string mouseXAxis = "Mouse X", mouseYAxis = "Mouse Y",
        horizontalAxis = "Horizontal", verticalAxis = "Vertical";

    public static void SetTimePause(bool paused)
    {
        Time.timeScale = paused ? 0 : targetTimeScale;
    }

    public static void SetAlpha(this MaskableGraphic graphics, float alpha)
    {
        Color color = graphics.color;
        color.a = alpha;
        graphics.color = color;
    }


    public static bool IsPrefab(this GameObject a_Object)
    {
        return a_Object.scene.rootCount == 0;
    }

    public static string AddSpaces(string text)
    {
        if (text == null || text.Length == 0)
            return string.Empty;
        StringBuilder newText = new StringBuilder(text.Length * 2);
        newText.Append(text[0]);
        for (int i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                newText.Append(' ');
            newText.Append(text[i]);
        }
        return newText.ToString();
    }

    public static bool ContainsLayer(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    public static Color SetAlpha(Color color, float alpha)
    {
        Color temp = color;
        temp.a = alpha;
        return temp;
    }

    public static bool TouchedOverUI
    {
        get { return EventSystem.current.IsPointerOverGameObject() || EventSystem.current.currentSelectedGameObject != null; }
    }

    public static void Shuffle<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int idx = Random.Range(i, array.Length);

            //swap elements
            T tmp = array[i];
            array[i] = array[idx];
            array[idx] = tmp;
        }
    }
    public static T GetRandom<T>(this T[] array)
    {
        if (array.Length == 0)
        {
            Debug.Log("Warning! Getting random value from empty array!");
            return default(T);
        }
        return array[Random.Range(0, array.Length)];
    }

    public static float AngleTo(this Vector3 this_, Vector3 to)
    {
        Vector2 direction = new Vector2(to.x, to.y) - new Vector2(this_.x, this_.y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < 0f) angle += 360f;
        return angle;
    }

    public static float VectorSqrDistance(Vector3 a, Vector3 b)
    {
        return (b - a).sqrMagnitude;
    }
    public static T GetComponentInChildrenOfAsset<T>(this GameObject go) where T : Component
    {
        List<Transform> tfs = new List<Transform>();
        CollectChildren(tfs, go.transform);
        for (int i = 0; i < tfs.Count; i++)
        {
            T temp = tfs[i].GetComponent<T>();

            if (temp)
                return temp;
        }
        return null;
    }
    public static T[] GetComponentsInChildrenOfAsset<T>(this GameObject go) where T : Component
    {
        List<Transform> tfs = new List<Transform>();
        CollectChildren(tfs, go.transform);
        List<T> all = new List<T>();
        for (int i = 0; i < tfs.Count; i++)
            all.AddRange(tfs[i].gameObject.GetComponents<T>());
        return all.ToArray();
    }

    static void CollectChildren(List<Transform> transforms, Transform tf)
    {
        transforms.Add(tf);
        foreach (Transform child in tf)
        {
            CollectChildren(transforms, child);
        }
    }

    public static void Swap<T>(this IList<T> list, int indexA, int indexB)
    {
        T tmp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = tmp;
    }

    public static void Populate<T>(this T[] arr, T value)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = value;
        }
    }
}
