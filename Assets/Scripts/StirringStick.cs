using UnityEngine;
using UnityEngine.UI;

public class StirringStick : MonoBehaviour
{
    private Cauldron _cauldron;

    [Header("Настройки помешивания")]
    [SerializeField] private Slider _stirSlider;
    [SerializeField] private float _stirSpeed = 2.2f; 
    [SerializeField] private float _decaySpeed = 0.3f; 
    [SerializeField] private float _requiredStirLevel = 0.8f;

    [Header("Аудио")]
    [SerializeField] private AudioClip _stirringSound;

    private AudioSource _audioSource;
    private bool _isDragging;
    private Vector3 _lastPosition;
    [SerializeField] private float _minMovementThreshold = 0.2f;

    private void Awake()
    {
        _cauldron = GameObject.FindGameObjectWithTag("Cauldron").GetComponent<Cauldron>();
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
        _audioSource.clip = _stirringSound;
        _audioSource.loop = true;
        _audioSource.playOnAwake = false;
    }

    private void Update()
    {
        if (!_isDragging)
        {
            _stirSlider.value -= _decaySpeed * Time.deltaTime;
            _stirSlider.value = Mathf.Max(_stirSlider.value, 0);
        }
    }

    private void OnMouseDown()
    {
        _isDragging = true;
        _lastPosition = transform.position;
        _audioSource.Play();
    }

    private void OnMouseDrag()
    {
        
        if (!_isDragging)
        {
            return;
        }

        float distanceMoved = Vector3.Distance(transform.position, _lastPosition);

        if (distanceMoved > _minMovementThreshold)
        {
            _stirSlider.value += _stirSpeed * Time.deltaTime;
            _lastPosition = transform.position;            
        }
    }

    private void OnMouseUp()
    {
        _isDragging = false;

        _audioSource.Stop();

        if (_stirSlider.value >= _requiredStirLevel)
        {
            _cauldron.CheckPlayerAction(null, 0, true);
        }
        else
        {
            _cauldron.CheckPlayerAction(null, 0, false);
        }
    }
}
