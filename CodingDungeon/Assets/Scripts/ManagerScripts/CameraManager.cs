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
    [SerializeField] private float _defaultZoom = 5f;
    [SerializeField] private float _zoomSpeed = 2f;
    
    private Coroutine _followCoroutine;
    private Coroutine _shakeCoroutine;
    private Coroutine _zoomCoroutine;
    private Camera _camera;
    private float _currentZoom;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _currentZoom = _camera.orthographicSize;
    }

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
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ZoomIn(3f, 1f);
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            ZoomOut(7f, 1f);
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetZoom(0.5f);
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
    
    public void ZoomIn(float targetZoom, float duration = 0.5f)
    {
        SetZoom(targetZoom, duration);
    }
    
    public void ZoomOut(float targetZoom, float duration = 0.5f)
    {
        SetZoom(targetZoom, duration);
    }
    
    public void ResetZoom(float duration = 0.5f)
    {
        SetZoom(_defaultZoom, duration);
    }
    
    private void SetZoom(float targetZoom, float duration)
    {
        if (_zoomCoroutine != null)
        {
            StopCoroutine(_zoomCoroutine);
        }
        _zoomCoroutine = StartCoroutine(ZoomFlow(targetZoom, duration));
    }
    
    public void SetZoomImmediate(float zoomLevel)
    {
        if (_zoomCoroutine != null)
        {
            StopCoroutine(_zoomCoroutine);
            _zoomCoroutine = null;
        }
        _camera.orthographicSize = zoomLevel;
        _currentZoom = zoomLevel;
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
    
    private IEnumerator ZoomFlow(float targetZoom, float duration)
    {
        float startZoom = _camera.orthographicSize;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            _camera.orthographicSize = Mathf.Lerp(startZoom, targetZoom, t);
            yield return null;
        }
        
        _camera.orthographicSize = targetZoom;
        _currentZoom = targetZoom;
        _zoomCoroutine = null;
    }
}