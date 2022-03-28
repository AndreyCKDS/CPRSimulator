using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatObject : MonoBehaviour
{
    public bool CanBePressed;
    public KeyCode KeyToPress;
    private SpriteRenderer SR;
    private bool Hit;
    private float Depth = 5;
    private AudioSource AudioSource;
    public AudioClip BeatSound;

    public GameObject NormalEffect, GreatEffect, PerfectEffect, MissEffect;

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyToPress))
        {
            if(CanBePressed)
            {
                Hit = true;
                if (Mathf.Abs(transform.position.x) > 1)
                {
                    BeatScroller.instance.BeatHit(1);
                    Instantiate(NormalEffect, new Vector3(0, -8, 0), transform.rotation);
                    SR.color = new Color(0.2f, 0.6f, 0.99f);
                    GetComponent<BoxCollider2D>().enabled = false;
                }
                else if (Mathf.Abs(transform.position.x) > 0.25)
                {
                    BeatScroller.instance.BeatHit(2);
                    SR.color = new Color(0.4f, 1f, 0.4f);
                    Instantiate(GreatEffect, new Vector3(0, -8, 0), transform.rotation);
                    GetComponent<BoxCollider2D>().enabled = false;
                }
                else
                {
                    BeatScroller.instance.BeatHit(3);
                    SR.color = new Color(1f, 1f, 0.4f);
                    Instantiate(PerfectEffect, new Vector3(0, -8, 0), transform.rotation);
                    GetComponent<BoxCollider2D>().enabled = false;
                }
                BeatScroller.instance.DepthScore(Depth);
                //AudioSource.PlayOneShot(BeatSound, 1);
            }
            else
            {

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            CanBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            CanBePressed = false;
            if(!Hit)
            {
                BeatScroller.instance.BeatMiss();
                SR.color = new Color(1f, 0.42f, 0.42f);
                Instantiate(MissEffect, new Vector3(0, -8, 0), transform.rotation);
                GetComponent<BoxCollider2D>().enabled = false;
            }
            
        }
    }
}
