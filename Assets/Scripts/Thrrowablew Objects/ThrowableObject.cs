using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    Rigidbody2D rb;

    public float objectWeight;
    public int baseThrowForce;
    public int baseGravityScale;

    bool isThrown;
    public PlayerStun PM;
    GameObject owner;

    Vector3 throwArc;

    ObjectSpawnPosition spawnPos;
    ObjectSpawner objectSpawner;
    ScreenBounds screenBounds;

    public bool player;

    public Transform spawnerPos;
    public SpriteRenderer sr;
    public Sprite[] sprites;
    public Sprite baseSprite;

    public float stunTime;

    public ObjectFaling faling;
    [Header("octopus only")]
    public bool octopus;
    public Animator anim;
    private void Awake()
    {
        screenBounds = GameData.scrennBounds;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        //throwArc = new Vector3(baseThrowForce / objectWeight, baseThrowForce / objectWeight, 0);
    }
    private void Update()
    {
        if (!player)
        {
            if (screenBounds != null)
            {
                //destoing outside of screen
                if (screenBounds.AmIOutOfBounds(transform.localPosition))
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    public void Throw(bool right, GameObject parent)
    {
        isThrown = true;
        owner = parent;
        rb.simulated = true;
        transform.parent = spawnerPos;
        throwArc = new Vector3(baseThrowForce / objectWeight, (baseThrowForce / objectWeight/5*3), 0);
        if (right)
        {
            rb.AddForce(throwArc);
        }else
        {
            rb.AddForce(new Vector3(-throwArc.x,throwArc.y,throwArc.z));
        }
        rb.gravityScale = baseGravityScale * objectWeight;
        if (octopus)
        {
            anim.Play("Idle");
            return;
        }
        sr.sprite = baseSprite;
    }
    public void PickUp(Transform player,Character character)
    {
        faling.Stop();
        transform.position = player.position;
        transform.position += Vector3.up * 0.2f;
        transform.parent = player;
        spawnPos.Release();
        rb.simulated = false;
        if (octopus)
        {
            ChangeAnimations(character);
            return;
        }
        ChangeSprite(character);
    }
    public void Init(ObjectSpawnPosition spawnPosition,ScreenBounds bounds,ObjectSpawner spawner)
    {
        spawnPos = spawnPosition;
        screenBounds = bounds;
        objectSpawner = spawner;
    }
    private void OnDestroy()
    {
        objectSpawner.DestroiedObject();
    }
    public void ThrowDown(GameObject parent)
    {
        isThrown = true;
        owner = parent;
        rb.simulated = true;
        transform.parent = spawnerPos;
        rb.AddForce(Vector3.down * 3f);
        rb.gravityScale = baseGravityScale * objectWeight * 4;
        if (octopus)
        {
            anim.Play("Idle");
            return;
        }
        sr.sprite = baseSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("colision");
        if (collision.gameObject.tag == "Player" && isThrown)
        {
            //Debug.Log("object is thrown and touching player");
            PM = collision.GetComponent<PlayerStun>();
            if (owner != PM.gameObject)
            {
                int id = owner.transform.parent.GetComponent<InputHandler>().playerId;
                //Debug.Log("stuning player");
                PM.StunPlayer(id, stunTime);
            }
        }
    }
    public void Release()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        owner = null;
        rb.simulated = false;
        transform.parent = spawnerPos;
        sr.sprite = baseSprite;
        faling.Fall();
    }
    void ChangeSprite(Character typ)
    {
        switch (typ)
        {
            case Character.butcher:
                sr.sprite = sprites[0];
                break;
            case Character.detective:
                sr.sprite = sprites[1];
                break;
            case Character.nobleman:
                sr.sprite = sprites[2];
                break;
            case Character.ocultist:
                sr.sprite = sprites[3];
                break;
        }
    }
    void ChangeAnimations(Character typ)
    {
        switch (typ)
        {
            case Character.butcher:
                anim.Play("Butcher");
                break;
            case Character.detective:
                anim.Play("Detective");
                break;
            case Character.nobleman:
                anim.Play("Nobleman");
                break;
            case Character.ocultist:
                anim.Play("Occultist");
                break;

        }
    }

}
