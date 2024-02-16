using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxEffect;
    [SerializeField] private float offset;
    private float _length;
    private float _startPos;
    
    void Start()
    {
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    
    void FixedUpdate()
    {
        if (cam == null)
            return;

        var temp = -(cam.transform.position.x * (1-parallaxEffect));
        var dist = -(cam.transform.position.x * parallaxEffect);
 
        transform.position = new Vector3(_startPos + dist + offset, transform.position.y, transform.position.z);

        if(temp > _startPos + _length) _startPos += _length;
        else if (temp < _startPos - _length) _startPos -= _length;
    }
}
