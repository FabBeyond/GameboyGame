using UnityEngine;

public class ControlUtils
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

    public static int DirToNum(Vector2 input)
    {
        if (input == Vector2.zero) return -1;

        float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 180;
        angle -= 45;

        if (angle <= 90) return 0;
        else if (angle > 90 && angle <= 180) return 1;
        else if (angle > 180 && angle <= 270) return 2;
        else if (angle > 270 && angle <= 360) return 3;

        return -1;
    }
}
