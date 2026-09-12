using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ControlLimiter
{
    public static Vector2 SnapTo8Dir(Vector2 input)
    {
        if (input == Vector2.zero) return Vector2.zero;

        float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        float step = 360 / 8;
        angle = Mathf.Round(angle/step) * step;

        float rad = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }
}
