using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private const int _extinguish = 2;

    [SerializeField] private float _speedRotation;
    [SerializeField] private float _borderRotateY = 50;
    [SerializeField] private float _borderRotateX = 30;
    [SerializeField] private Car _car;

    private bool _isMove = true;
    private float _yRotation = 0;
    private float _xRotation = 0;
    private Vector3 _startCarRotation;
    private float _offsetY;

    private void Start()
    {
        _startCarRotation = _car.transform.rotation.eulerAngles;
    }

    private void LateUpdate()
    {
        _offsetY = (_car.transform.rotation.eulerAngles.y > 180) ? (_car.transform.rotation.eulerAngles.y - _startCarRotation.y - 360) : _car.transform.rotation.eulerAngles.y - _startCarRotation.y;
        _offsetY /= _extinguish;

        if (Input.GetMouseButton(0) && _isMove)
        {
            float angleX = _speedRotation * Input.GetAxis("Mouse X") * Time.deltaTime;
            float angleY = _speedRotation * Input.GetAxis("Mouse Y") * Time.deltaTime;

            _yRotation -= angleX;
            _xRotation -= angleY;

            //transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
        }
        _yRotation = Mathf.Clamp(_yRotation, -_borderRotateY - _offsetY, _borderRotateY - _offsetY);
        _xRotation = Mathf.Clamp(_xRotation, -_borderRotateX, _borderRotateX);

        var newROtation = new Vector3(_xRotation, -_yRotation - _offsetY, 0);
        transform.localRotation = Quaternion.Euler(newROtation);
    }

    public void ChangeState(bool flag) => _isMove = flag;
}
