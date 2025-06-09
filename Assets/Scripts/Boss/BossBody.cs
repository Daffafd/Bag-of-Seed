using UnityEngine;

public class BossBody : MonoBehaviour
{
    [SerializeField] private int _length;

    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private Vector3[] _segmentPos;

    [SerializeField] private Transform _targetDir;
    [SerializeField] private float _targetDist;
     private Vector3[] _segmentV;
    [SerializeField] private float _smoothSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer.positionCount = _length;
        _segmentPos = new Vector3[_length];
        _segmentV = new Vector3[_length];
        ResetPos();
    }

    // Update is called once per frame
    void Update()
    {
        _segmentPos[0] = _targetDir.position;
        for (int i = 1; i < _segmentPos.Length; i++)
        {
            _segmentPos[i] = Vector3.SmoothDamp(_segmentPos[i], _segmentPos[i - 1] + _targetDir.right * _targetDist,
                ref _segmentV[i], _smoothSpeed);
            
        }
        _lineRenderer.SetPositions(_segmentPos);
    }

    private void ResetPos()
    {
        _segmentPos[0] = _targetDir.position;
        for (int i = 1; i < _segmentPos.Length; i++)
        {
            _segmentPos[i] = _segmentPos[i - 1] + _targetDir.right * _targetDist;

        }
        _lineRenderer.SetPositions(_segmentPos);
    }
}
