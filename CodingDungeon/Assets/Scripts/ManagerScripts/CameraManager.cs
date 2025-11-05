using UnityEngine;
using System.Collections;

public class CameraManager : SceneSingleMono<CameraManager>
{
    //플레이어 겜오브젝트는 디버그용입니다 실제 개발시엔 SetTarget를 써주십시요
    [SerializeField] private GameObject player;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0, 0, -10);
    [SerializeField] private float _followSpeed = 5f;
    [SerializeField] private float _shakeIntensity = 0.3f;
    
    private Coroutine _followCoroutine;
    private Coroutine _shakeCoroutine;

    //디버그용
    /*private void Start()
    {
        SetTarget(player.transform);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartFollow(3);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StopFollow();
            StartShake(0.5f,0.1f);
        }
    }*/
    
    public void SetTarget(Transform newTarget)
    {
        _target = newTarget;
    }
    
    public void StartFollow(float? speed = null)
    {
        if (_followCoroutine != null)
        {
            StopCoroutine(_followCoroutine);
        }
        float followSpeed = speed ?? _followSpeed;
        _followCoroutine = StartCoroutine(FollowTargetFlow(followSpeed));
    }
    
    public void StopFollow()
    {
        if (_followCoroutine != null)
        {
            StopCoroutine(_followCoroutine);
            _followCoroutine = null;
        }
    }
    
    public void StartShake(float duration = 0.5f, float? intensity = null)
    {
        if (_shakeCoroutine != null)
        {
            StopCoroutine(_shakeCoroutine);
        }
        float shakeIntensity = intensity ?? _shakeIntensity;
        _shakeCoroutine = StartCoroutine(ShakeCameraFlow(duration, shakeIntensity));
    }
    
    public void StopShake()
    {
        if (_shakeCoroutine != null)
        {
            StopCoroutine(_shakeCoroutine);
            _shakeCoroutine = null;
        }
    }
    
    private IEnumerator FollowTargetFlow(float followSpeed)
    {
        while (_target != null)
        {
            Vector3 targetPosition = _target.position + _offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            yield return null;
        }
        _followCoroutine = null;
    }
    
    private IEnumerator ShakeCameraFlow(float duration, float shakeIntensity)
    {
        Vector3 originalPosition = transform.position;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * shakeIntensity;
            float y = Random.Range(-1f, 1f) * shakeIntensity;
            
            transform.position = new Vector3(
                originalPosition.x + x,
                originalPosition.y + y,
                originalPosition.z
            );
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        transform.position = originalPosition;
        _shakeCoroutine = null;
    }
}