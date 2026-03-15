using UnityEngine;

public class MegaBrick : Brick
{
    void Awake()
    {
        PointValue = 50;
    }

    // POLYMORPHISM
    protected override void SetColor()
    {
        var renderer = GetComponentInChildren<Renderer>();
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        block.SetColor("_BaseColor", Color.magenta);
        renderer.SetPropertyBlock(block);
    }
    protected override void OnCollisionEnter(Collision other)
    {
        Debug.Log("MegaBrick destroyed! Bonus points!");
        base.OnCollisionEnter(other);
    }
}