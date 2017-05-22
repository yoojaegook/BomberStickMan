using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CForceAt : MonoBehaviour
{

    public int _index;
    private string _name;
    private float _forcePower;
    private string _direction;
    private float _range;
    [RangeAttribute(0, 400.0f)]
    public float _force = 85f;
    private float _forceSpeed;
    Vector2 _dir = Vector2.zero;
    List<Rigidbody2D> _rbody2D = new List<Rigidbody2D>();
    PolygonCollider2D _pcol;
    public GameObject _doll;
    Rigidbody2D rbody;
    public LayerMask _mask;
    public Transform _bombPosition;
    public void Init(DatasData data)
    {
        _index = data.INDEX;
        _name = data.NAME;
        _forcePower = data.FORCEPOWER;
        _direction = data.DIRECTION;
        _range = data.RANGE/5;
        _forceSpeed = _forcePower * _force;
    }
    // Use this for initialization
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        _pcol = GetComponent<PolygonCollider2D>();
        _doll = GameObject.FindWithTag("Player").gameObject;
        _rbody2D = _doll.GetComponentsInChildren<Rigidbody2D>().ToList();
    }
    public void AddObj(Rigidbody2D rbd2D)
    {
        if (!_rbody2D.Contains(rbd2D))
            _rbody2D.Add(rbd2D);
    }
    public void DelObj(Rigidbody2D rbd2D)
    {
        if (_rbody2D.Contains(rbd2D))
            _rbody2D.Remove(rbd2D);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }
    public void Boom()
    {
        if (_direction.Equals("U"))
        {
            ShootTheBall(_bombPosition.transform.position - transform.position);
        }
        else
        {
            ShootTheBall(_doll.transform.position - transform.position);
        }

    }
    void ShootTheBall(Vector2 dir)
    {
		float dist = Vector3.Distance(Vector3.zero, dir);
		Debug.Log(dist);
		Debug.Log(_range);
        if (dist > _range)
        { 

		}
        else
        {
            if (!CBombManager.Instance.IsBombed())
            {

                CBombManager.Instance.TurnOnBomb();
                foreach (Rigidbody2D r in _rbody2D)
                {
                    r.simulated = true;
                    r.AddForceAtPosition(dir.normalized * _forceSpeed, transform.position);
                }
            }
            else
            {
                foreach (Rigidbody2D r in _rbody2D)
                {
                    r.velocity += dir.normalized * _forceSpeed * 0.03f;
                }
            }
        }
		SoundPlay();
        GameObject impact = Instantiate(CEffectManager.Instance.GetEffectObj(_index), _bombPosition.position, transform.rotation) as GameObject;
        Destroy(impact, impact.GetComponent<ParticleSystem>().main.duration);
        Destroy(this.gameObject, impact.GetComponent<ParticleSystem>().main.duration);
    }

	void SoundPlay()
	{
		switch (_index)
		{
			case 1: AppSound.instance.SE_EXPLOSION_1.Play();
			Debug.Log("play1");
			break;
			case 2: AppSound.instance.SE_EXPLOSION_2.Play();
			Debug.Log("play2");
			break;
			case 3: AppSound.instance.SE_EXPLOSION_3.Play();
			Debug.Log("play3");
			break;
			case 4: AppSound.instance.SE_EXPLOSION_4.Play();
			Debug.Log("play4");
			break;
			case 5: AppSound.instance.SE_EXPLOSION_5.Play();
			Debug.Log("play5");
			break;
			case 6: AppSound.instance.SE_EXPLOSION_6.Play();
			Debug.Log("play6");
			break;
			default:
			break;
		}
	}
}
