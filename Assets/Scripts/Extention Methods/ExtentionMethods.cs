using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public static class ExtentionMethods
{
    public static float GetWorldXPositionOfScreenEnd() => Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

    public static void ResetTransform(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.rotation = Quaternion.identity;
        trans.localScale = Vector3.one;
    }

    public static T GetRandomElement<T>(this List<T> list)
    {
        int RandomIndex = Random.Range(0, list.Count);
        return list[RandomIndex];
    }

    public static T GetRandomElement<T>(this T[] list)
    {
        int RandomIndex = Random.Range(0, list.Length);
        return list[RandomIndex];
    }

    public static string ToStringPrice(this int num)
    {
        if (num >= 100000000)
        {
            return ((num >= 10050000 ? num - 500000 : num) / 1000000D).ToString("#M");
        }
        if (num >= 10000000)
        {
            return ((num >= 10500000 ? num - 50000 : num) / 1000000D).ToString("0.#M");
        }
        if (num >= 1000000)
        {
            return ((num >= 1005000 ? num - 5000 : num) / 1000000D).ToString("0.##M");
        }
        if (num >= 100000)
        {
            return ((num >= 100500 ? num - 500 : num) / 1000D).ToString("0.k");
        }
        if (num >= 10000)
        {
            return ((num >= 10550 ? num - 50 : num) / 1000D).ToString("0.#k");
        }

        return num >= 1000 ? ((num >= 1005 ? num - 5 : num) / 1000D).ToString("0.##k") : num.ToString("#,0");
    }
    public static void PlaceInCircle()
    {
        /* float angleStep = 360f / wheelItems.Length;

         for (int i = 0; i < wheelItems.Length; i++)
         {
             // Calculate the position and rotation for each item
             Vector3 position = wheelTransform.position + Quaternion.Euler(0f, 0f, angleStep * i) * Vector3.up * distanceFromCenter;
             Quaternion rotation = Quaternion.Euler(0f, 0f, angleStep * i);
         }*/
    }

    public static bool IsTouchInside(this RectTransform rect, RenderMode canvasRenderMode = RenderMode.ScreenSpaceOverlay)
    {
        Vector2 localMousePosition = Vector2.right;
        switch (canvasRenderMode)
        {
            case RenderMode.ScreenSpaceOverlay:
                localMousePosition = rect.InverseTransformPoint(Input.mousePosition);
                break;
            case RenderMode.ScreenSpaceCamera:
                RectTransformUtility.ScreenPointToLocalPointInRectangle
                (
                 rect,
                 Input.mousePosition,
                 Camera.main,
                 out localMousePosition
                );
                break;
            case RenderMode.WorldSpace:
                RectTransformUtility.ScreenPointToLocalPointInRectangle
               (
                rect,
                Input.mousePosition,
                Camera.main,
                out localMousePosition
               );
                break;
            default:
                break;
        }
        return rect.rect.Contains(localMousePosition);
    }
}
