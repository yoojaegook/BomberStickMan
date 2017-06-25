using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDataBomb {

    public enum PowerDirection
    {
        U,
        D,
        A,
        LR,
        L,
        R,
    }
    int number = 0;
    string name = "";
    int unLockingCost = 0;
    int cost = 0;
    int power = 0;
    int row = 0;
    int col = 0;
    PowerDirection powerDir = PowerDirection.U;
    Sprite img = null;

    public CDataBomb(int number, string name, int unLockingCost, int cost, int power,
        PowerDirection powerDir, int row, int col, Sprite img)
    {
        this.number = number;
        this.name = name;
        this.unLockingCost = unLockingCost;
        this.cost = cost;
        this.power = power;
        this.row = row;
        this.col = col;
        this.powerDir = powerDir;
        this.img = img;
    }
    public int GetNumber()
    {
        return number;
    }

    public string GetName()
    {
        return name;
    }
    public int GetUnLockingCost()
    {
        return unLockingCost;
    }
    public int GetCost()
    {
        return cost;
    }
    public int GetPower()
    {
        return power;
    }
    public int GetRow()
    {
        return row;
    }
    public int GetCol()
    {
        return col;
    }
    public PowerDirection GetPowerDir()
    {
        return powerDir;
    }
    public Sprite GetImg()
    {
        return img;
    }
}
