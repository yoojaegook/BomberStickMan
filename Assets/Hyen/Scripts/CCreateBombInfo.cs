using UnityEngine;

public class CCreateBombInfo {

    int bombNumber;
    CBomb.BombDir bombDir;
    Vector2 bombPos;

	public CCreateBombInfo(int bombNumber, CBomb.BombDir bombDir, Vector2 bombPos)
    {
        this.bombNumber = bombNumber;
        this.bombDir = bombDir;
        this.bombPos = bombPos;
    }

    public int GetBombNumber()
    {
        return bombNumber;
    }
    public CBomb.BombDir GetBombDir()
    {
        return bombDir;
    }
    public Vector2 GetBombPos()
    {
        return bombPos;
    }
}
