using System;

[System.Serializable]
public class ItemData
{
    public string name;
    public string description;
    public float price;
}

[Serializable]
public class Products
{
    public ItemData[] products;
}