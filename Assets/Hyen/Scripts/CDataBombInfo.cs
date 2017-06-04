public class CDataBombInfo{

    bool bombUnLock = false;
    int bombNumber = 0;
    CDataBomb dataBomb;
    public bool BombUnLock { get { return bombUnLock; } set { bombUnLock = value; } }
    public int BombNumber { get { return bombNumber; } set { bombNumber = value; } }
    public CDataBomb DataBomb { get { return dataBomb; } set { dataBomb = value; } }

    public CDataBombInfo(bool bombUnLock, int bombNuber, CDataBomb dataBomb)
    {
        BombUnLock = bombUnLock;
        BombNumber = bombNuber;
        DataBomb = dataBomb;
    }
}
