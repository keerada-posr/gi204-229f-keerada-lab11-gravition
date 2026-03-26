using UnityEngine;

public class Gravity : MonoBehaviour
{
    public static List<Gravity> otherObj;
    private Rigidbody rb;
    const float G = 6.67f; // ปรับตามต้องการโดยใส่ 0.00 

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (otherObj == null)
        {
            // หา Class Gravitation ในวัตถุอื่นๆ และเก็บใน List
            otherObj = new List<Gravity>();
        }
        otherObj.Add(this);
    }
    void FixedUpdate()
    {
        foreach (Gravity obj in otherObj)
        {
            if (obj != this) // ป้องกันไม่ให้วัตถุโดนแรงดึงดูดตัวเอง
            {
                Attract(obj);
            }
        }
    }
    void Attract(Gravity other)
    {
        Rigidbody otherRb = other._rb;
        Vector3 direction = _rb.position - otherRb.position;

        float distance = direction.magnitude;
        if (distance == 0f) return;

        float forceMagnitude = G * (_rb.mass * otherRb.mass) / Mathf.Pow(distance, 2);
        Vector3 gravityForce = forceMagnitude * direction.normalized;
        otherRb.AddForce(gravityForce);
    }
}
