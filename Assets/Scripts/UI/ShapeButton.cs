using UnityEngine;

public enum ShapeType
{
    Cube, Star, Sphere, Hexagon
}

public class ShapeButton : MonoBehaviour
{
    ShapeShifter shapeShifter;
    [SerializeField] ShapeType shapeType;

    void Start()
    {
        shapeShifter = FindObjectOfType<ShapeShifter>();
    }

    public void OnClick()
    {
        shapeShifter.ShapeShift(shapeType);
    }
}
