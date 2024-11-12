using UnityEngine;

public interface IItem
{
    string Name { get; set; }
    string Description { get; set; }
    float Price { get; set; }
    void Init(string name, string description, float price);
    void PositionItem(Vector3 position);
}