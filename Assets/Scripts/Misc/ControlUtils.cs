using UnityEngine;

public class ControlUtils
{
    public static Vector2 SnapToDir(Vector2 input, float directions)
    {
        if (input == Vector2.zero) return Vector2.zero;

        float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        float step = 360 / directions;
        angle = Mathf.Round(angle/step) * step;

        float rad = angle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        if (dir.x < 0.1f && dir.x > -0.1f) dir.x = 0;
        if (dir.y < 0.1f && dir.y > -0.1f) dir.y = 0;
        return dir;
    }
    public static Vector2 DeadzoneCheck(Vector2 input)
    {
        if (input.magnitude < PlayerData.Instance.deadzone) return Vector2.zero;
        return input;
    }
}
