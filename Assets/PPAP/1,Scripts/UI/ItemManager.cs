using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Pattern;


public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance=null;
    public Datas _bombdatas;
    public DatasData choosedData{
        get{
            return _choosedData;
        }
        set{
            if (_choosedData != value)
            {
                _choosedData = value;
                ChangeSprite(spriteDic[_choosedData.SPRITENAME]);
                _image.transform.localRotation = Quaternion.identity;
            }
        }
    }
    Dictionary<int,bool> itemTurn = new Dictionary<int,bool>();
    [SerializeField]
    private DatasData _choosedData;
    public GameObject _efObj;
    public Image _image;
    public List<Sprite> _sps = new List<Sprite>();
    public List<GameObject> buttons = new List<GameObject>();
    Dictionary<string,Sprite> spriteDic = new Dictionary<string,Sprite>();

    bool next = false;
 //ItemManager
    public CFirstGetItem firstGetItme;

    public Text rankM;
    public Text gameNum;

    public Text totalM;
    public Transform player;

    int oriX = 0;

    int score = 0;
    void Awake()
    {
        Instance = this;
        FadeFilter.instance.FadeIn(Color.black, 0.5f);
        spriteDic = _sps.ToDictionary(k=>k.name,v=>v);
       
    }
    
    void Start()
    {
        Debug.Log(CDataManager.instance.unlock[0]);
        itemTurn.Add(1, CDataManager.instance.unlock[0]);
        itemTurn.Add(2, CDataManager.instance.unlock[1]);
        itemTurn.Add(3, CDataManager.instance.unlock[2]);
        itemTurn.Add(4, CDataManager.instance.unlock[3]);
        itemTurn.Add(5, CDataManager.instance.unlock[4]);
        itemTurn.Add(6, CDataManager.instance.unlock[5]);

        for (int i = 0; i < buttons.Count; i++)
        {
            if (itemTurn[i + 1])
            {
                buttons[i].SendMessage("Unlock");
            }
            else
            {
                buttons[i].SendMessage("Onlock");
            }

        }
        CDataManager.instance.ReSetCountUp();
        GameNumSetting();
        oriX = (int) player.position.x;
        rankM.text = "0M";
        totalM.text = "Total " + CDataManager.instance.score.ToString() + "M";

        Debug.Log("실행실행!!!!" + CDataManager.instance.score.ToString());
    }
    
    void Update()
    {
        MSetting2((int)player.position.x);
    }
    public void CheckItem(int index)
    {
        if(itemTurn[index])
        {
            firstGetItme.FirstGetItem(GetSprite(index));
        }
    }

    public void SetItemActive(int index)
    {
        itemTurn[index] = true;
        //Debug.Log(buttons.Count + " 버튼 갯수");
        buttons[index-1].SendMessage("Unlock");
        CDataManager.instance.unlock[index-1] = true;
    }
    DatasData GetBombData(int index)
    {
        return _bombdatas.dataArray.Where(a=>a.INDEX.Equals(index)).FirstOrDefault();
    }
    public bool IsSelected()
    {
        // 갯수 처리
        // 떨궜을 때 NONE으로 변경
        return choosedData == null ? false : true;
    }
    public void ChangeSprite(Sprite spr)
    {
        _image.sprite = spr;
    }
    public void SetObj(int index)
    {
        Debug.Log(index);
        if(index != 0)
        {
            DatasData getedData = GetBombData(index);
            Debug.Log(getedData);
            choosedData = getedData;
        }
    }
    public void RotatePicture()
    {
        if(_image.sprite ==null)return;
        if (_efObj != null)
        {
            Debug.Log("notnull");
            Destroy(_efObj);
        }
        _image.transform.Rotate(new Vector3(0,0,-45));
        Vector3 bombPosition = choosedData.DIRECTION.Equals("U")?
                                (_image.transform.FindChild("upPosition").transform.position)
                                :_image.transform.position;

        
        GameObject insObj= Instantiate(CEffectManager.Instance.GetEffectObj(_choosedData.INDEX),bombPosition,transform.rotation)as GameObject;
        Destroy(insObj,2f);
    }
    public Sprite GetSprite(int index)
    {
        return _sps[index-1];
    }
    public void ResetScene()
    {
        
        //FadeFilter.instance.FadeOut(Color.black,0.15f);
        if(next) return;
            next = true;
            CDataManager.instance.AddScore(score);
            totalM.text = "Total " + CDataManager.instance.score.ToString() + "M";
            if(CDataManager.instance.GetReSetCount() >= 5)
            {
                CDataManager.instance.ResetReSetCount();
                SceneManager.LoadScene("Title");
                return;
            }
        
        SceneManager.LoadScene("Shoot");
    }

    
    public void MSetting2(int x)
    {
         rankM.text = (x - oriX).ToString() + "M";
         score = (x - oriX);
         Debug.Log("업데이트 실행" + x + " " + oriX);
    }
    public void GameNumSetting()
    {
        gameNum.text = CDataManager.instance.GetReSetCount().ToString() + "/" + "5"; 
    }
}
