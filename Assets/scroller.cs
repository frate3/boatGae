using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroller : MonoBehaviour
{
    public float scrollSpeedX;
    public float scrollSpeedY;
    public SpriteRenderer texture;
    private 
    // Start is called before the first frame update
    void Start()
    {
        //texture = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        print(Time.realtimeSinceStartup * scrollSpeedX);
        texture.material.mainTextureOffset = new Vector2(Time.realtimeSinceStartup * scrollSpeedX, Time.realtimeSinceStartup * scrollSpeedY);
    }
}
