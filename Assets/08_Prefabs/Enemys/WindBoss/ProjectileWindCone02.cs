<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWindCone02 : MonoBehaviour
{
    public float _speed;
    public int _damage;
    public GameObject _muzzlePrefab, _hitPrefab;

    public float _timeToChangePosX, _timeToChangePosXMax, _posMaxX, _coefSpeedMoveX;
    public bool _boolSensX;
    public Vector3 _posStartWorld, _posStartLocal;

    private void Start()
    {
        if (_muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(_muzzlePrefab, transform.position, transform.rotation);
            //muzzleVFX.transform.forward = gameObject.transform.forward;

            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
            {
                Destroy(muzzleVFX, psMuzzle.main.duration);
            }
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        _posStartWorld = transform.position;
        _posStartLocal = transform.localPosition;

        Destroy(gameObject, 10);
    }

    private void Update()
    {
        if (_speed != 0)
        {
            transform.position += transform.up * (_speed * Time.deltaTime);

            if (_timeToChangePosX < _timeToChangePosXMax)
            {
                _timeToChangePosX += Time.deltaTime;
            }
            else
            {
                _boolSensX = !_boolSensX;
                _timeToChangePosX = 0;
            }

            if(_boolSensX)
            {
                transform.localPosition += new Vector3(Time.deltaTime * _coefSpeedMoveX, 0, 0);
            }
            else
            {
                transform.localPosition -= new Vector3(Time.deltaTime * _coefSpeedMoveX, 0, 0);
            }
        }
        else
        {
            Debug.Log("No Speed");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ici" + other.name);
        if (other.gameObject.layer == 8 && other.gameObject.tag == "PlayerColl")
        {
            Debug.Log("ici" + other.name);
            other.GetComponentInChildren<PlayerEntity>().LessLife(_damage);

            _speed = 0;

            if (_hitPrefab != null)
            {
                var hitVFX = Instantiate(_hitPrefab, transform.position, transform.rotation);

                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }

            Destroy(gameObject);
        }

        if (other.gameObject.layer == 31)
        {
            Debug.Log("ici" + other.name);
            _speed = 0;

            if (_hitPrefab != null)
            {
                var hitVFX = Instantiate(_hitPrefab, transform.position, transform.rotation);

                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }

            Destroy(gameObject);
        }
    }
}
=======
<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWindCone02 : MonoBehaviour
{
    public float _speed;
    public int _damage;
    public GameObject _muzzlePrefab, _hitPrefab;

    public float _timeToChangePosX, _timeToChangePosXMax, _posMaxX, _coefSpeedMoveX;
    public bool _boolSensX;
    public Vector3 _posStartWorld, _posStartLocal;

    private void Start()
    {
        if (_muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(_muzzlePrefab, transform.position, transform.rotation);
            //muzzleVFX.transform.forward = gameObject.transform.forward;

            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
            {
                Destroy(muzzleVFX, psMuzzle.main.duration);
            }
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        _posStartWorld = transform.position;
        _posStartLocal = transform.localPosition;

        Destroy(gameObject, 10);
    }

    private void Update()
    {
        if (_speed != 0)
        {
            transform.position += transform.up * (_speed * Time.deltaTime);

            if (_timeToChangePosX < _timeToChangePosXMax)
            {
                _timeToChangePosX += Time.deltaTime;
            }
            else
            {
                _boolSensX = !_boolSensX;
                _timeToChangePosX = 0;
            }

            if(_boolSensX)
            {
                transform.localPosition += new Vector3(Time.deltaTime * _coefSpeedMoveX, 0, 0);
            }
            else
            {
                transform.localPosition -= new Vector3(Time.deltaTime * _coefSpeedMoveX, 0, 0);
            }
        }
        else
        {
            Debug.Log("No Speed");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ici" + other.name);
        if (other.gameObject.layer == 8 && other.gameObject.tag == "PlayerColl")
        {
            Debug.Log("ici" + other.name);
            other.GetComponentInChildren<PlayerEntity>().LessLife(_damage);

            _speed = 0;

            if (_hitPrefab != null)
            {
                var hitVFX = Instantiate(_hitPrefab, transform.position, transform.rotation);

                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }

            Destroy(gameObject);
        }

        if (other.gameObject.layer == 31)
        {
            Debug.Log("ici" + other.name);
            _speed = 0;

            if (_hitPrefab != null)
            {
                var hitVFX = Instantiate(_hitPrefab, transform.position, transform.rotation);

                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }

            Destroy(gameObject);
        }
    }
}
=======
<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWindCone02 : MonoBehaviour
{
    public float _speed;
    public int _damage;
    public GameObject _muzzlePrefab, _hitPrefab;

    public float _timeToChangePosX, _timeToChangePosXMax, _posMaxX, _coefSpeedMoveX;
    public bool _boolSensX;
    public Vector3 _posStartWorld, _posStartLocal;

    private void Start()
    {
        if (_muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(_muzzlePrefab, transform.position, transform.rotation);
            //muzzleVFX.transform.forward = gameObject.transform.forward;

            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
            {
                Destroy(muzzleVFX, psMuzzle.main.duration);
            }
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        _posStartWorld = transform.position;
        _posStartLocal = transform.localPosition;

        Destroy(gameObject, 10);
    }

    private void Update()
    {
        if (_speed != 0)
        {
            transform.position += transform.up * (_speed * Time.deltaTime);

            if (_timeToChangePosX < _timeToChangePosXMax)
            {
                _timeToChangePosX += Time.deltaTime;
            }
            else
            {
                _boolSensX = !_boolSensX;
                _timeToChangePosX = 0;
            }

            if(_boolSensX)
            {
                transform.localPosition += new Vector3(Time.deltaTime * _coefSpeedMoveX, 0, 0);
            }
            else
            {
                transform.localPosition -= new Vector3(Time.deltaTime * _coefSpeedMoveX, 0, 0);
            }
        }
        else
        {
            Debug.Log("No Speed");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ici" + other.name);
        if (other.gameObject.layer == 8 && other.gameObject.tag == "PlayerColl")
        {
            Debug.Log("ici" + other.name);
            other.GetComponentInChildren<PlayerEntity>().LessLife(_damage);

            _speed = 0;

            if (_hitPrefab != null)
            {
                var hitVFX = Instantiate(_hitPrefab, transform.position, transform.rotation);

                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }

            Destroy(gameObject);
        }

        if (other.gameObject.layer == 31)
        {
            Debug.Log("ici" + other.name);
            _speed = 0;

            if (_hitPrefab != null)
            {
                var hitVFX = Instantiate(_hitPrefab, transform.position, transform.rotation);

                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }

            Destroy(gameObject);
        }
    }
}
=======
<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWindCone02 : MonoBehaviour
{
    public float _speed;
    public int _damage;
    public GameObject _muzzlePrefab, _hitPrefab;

    public float _timeToChangePosX, _timeToChangePosXMax, _posMaxX, _coefSpeedMoveX;
    public bool _boolSensX;
    public Vector3 _posStartWorld, _posStartLocal;

    private void Start()
    {
        if (_muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(_muzzlePrefab, transform.position, transform.rotation);
            //muzzleVFX.transform.forward = gameObject.transform.forward;

            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
            {
                Destroy(muzzleVFX, psMuzzle.main.duration);
            }
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        _posStartWorld = transform.position;
        _posStartLocal = transform.localPosition;

        Destroy(gameObject, 10);
    }

    private void Update()
    {
        if (_speed != 0)
        {
            transform.position += transform.up * (_speed * Time.deltaTime);

            if (_timeToChangePosX < _timeToChangePosXMax)
            {
                _timeToChangePosX += Time.deltaTime;
            }
            else
            {
                _boolSensX = !_boolSensX;
                _timeToChangePosX = 0;
            }

            if(_boolSensX)
            {
                transform.localPosition += new Vector3(Time.deltaTime * _coefSpeedMoveX, 0, 0);
            }
            else
            {
                transform.localPosition -= new Vector3(Time.deltaTime * _coefSpeedMoveX, 0, 0);
            }
        }
        else
        {
            Debug.Log("No Speed");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ici" + other.name);
        if (other.gameObject.layer == 8 && other.gameObject.tag == "PlayerColl")
        {
            Debug.Log("ici" + other.name);
            other.GetComponentInChildren<PlayerEntity>().LessLife(_damage);

            _speed = 0;

            if (_hitPrefab != null)
            {
                var hitVFX = Instantiate(_hitPrefab, transform.position, transform.rotation);

                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }

            Destroy(gameObject);
        }

        if (other.gameObject.layer == 31)
        {
            Debug.Log("ici" + other.name);
            _speed = 0;

            if (_hitPrefab != null)
            {
                var hitVFX = Instantiate(_hitPrefab, transform.position, transform.rotation);

                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }

            Destroy(gameObject);
        }
    }
}
=======
<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWindCone02 : MonoBehaviour
{
    public float _speed;
    public int _damage;
    public GameObject _muzzlePrefab, _hitPrefab;

    public float _timeToChangePosX, _timeToChangePosXMax, _posMaxX, _coefSpeedMoveX;
    public bool _boolSensX;
    public Vector3 _posStartWorld, _posStartLocal;

    private void Start()
    {
        if (_muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(_muzzlePrefab, transform.position, transform.rotation);
            //muzzleVFX.transform.forward = gameObject.transform.forward;

            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
            {
                Destroy(muzzleVFX, psMuzzle.main.duration);
            }
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        _posStartWorld = transform.position;
        _posStartLocal = transform.localPosition;

        Destroy(gameObject, 10);
    }

    private void Update()
    {
        if (_speed != 0)
        {
            transform.position += transform.up * (_speed * Time.deltaTime);

            if (_timeToChangePosX < _timeToChangePosXMax)
            {
                _timeToChangePosX += Time.deltaTime;
            }
            else
            {
                _boolSensX = !_boolSensX;
                _timeToChangePosX = 0;
            }

            if(_boolSensX)
            {
                transform.localPosition += new Vector3(Time.deltaTime * _coefSpeedMoveX, 0, 0);
            }
            else
            {
                transform.localPosition -= new Vector3(Time.deltaTime * _coefSpeedMoveX, 0, 0);
            }
        }
        else
        {
            Debug.Log("No Speed");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ici" + other.name);
        if (other.gameObject.layer == 8 && other.gameObject.tag == "PlayerColl")
        {
            Debug.Log("ici" + other.name);
            other.GetComponentInChildren<PlayerEntity>().LessLife(_damage);

            _speed = 0;

            if (_hitPrefab != null)
            {
                var hitVFX = Instantiate(_hitPrefab, transform.position, transform.rotation);

                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }

            Destroy(gameObject);
        }

        if (other.gameObject.layer == 31)
        {
            Debug.Log("ici" + other.name);
            _speed = 0;

            if (_hitPrefab != null)
            {
                var hitVFX = Instantiate(_hitPrefab, transform.position, transform.rotation);

                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }

            Destroy(gameObject);
        }
    }
}
=======
<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWindCone02 : MonoBehaviour
{
    public float _speed;
    public int _damage;
    public GameObject _muzzlePrefab, _hitPrefab;

    public float _timeToChangePosX, _timeToChangePosXMax, _posMaxX, _coefSpeedMoveX;
    public bool _boolSensX;
    public Vector3 _posStartWorld, _posStartLocal;

    private void Start()
    {
        if (_muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(_muzzlePrefab, transform.position, transform.rotation);
            //muzzleVFX.transform.forward = gameObject.transform.forward;

            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
            {
                Destroy(muzzleVFX, psMuzzle.main.duration);
            }
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        _posStartWorld = transform.position;
        _posStartLocal = transform.localPosition;

        Destroy(gameObject, 10);
    }

    private void Update()
    {
        if (_speed != 0)
        {
            transform.position += transform.up * (_speed * Time.deltaTime);

            if (_timeToChangePosX < _timeToChangePosXMax)
            {
                _timeToChangePosX += Time.deltaTime;
            }
            else
            {
                _boolSensX = !_boolSensX;
                _timeToChangePosX = 0;
            }

            if(_boolSensX)
            {
                transform.localPosition += new Vector3(Time.deltaTime * _coefSpeedMoveX, 0, 0);
            }
            else
            {
                transform.localPosition -= new Vector3(Time.deltaTime * _coefSpeedMoveX, 0, 0);
            }
        }
        else
        {
            Debug.Log("No Speed");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ici" + other.name);
        if (other.gameObject.layer == 8 && other.gameObject.tag == "PlayerColl")
        {
            Debug.Log("ici" + other.name);
            other.GetComponentInChildren<PlayerEntity>().LessLife(_damage);

            _speed = 0;

            if (_hitPrefab != null)
            {
                var hitVFX = Instantiate(_hitPrefab, transform.position, transform.rotation);

                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }

            Destroy(gameObject);
        }

        if (other.gameObject.layer == 31)
        {
            Debug.Log("ici" + other.name);
            _speed = 0;

            if (_hitPrefab != null)
            {
                var hitVFX = Instantiate(_hitPrefab, transform.position, transform.rotation);

                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }

            Destroy(gameObject);
        }
    }
}
=======
<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWindCone02 : MonoBehaviour
{
    public float _speed;
    public int _damage;
    public GameObject _muzzlePrefab, _hitPrefab;

    public float _timeToChangePosX, _timeToChangePosXMax, _posMaxX, _coefSpeedMoveX;
    public bool _boolSensX;
    public Vector3 _posStartWorld, _posStartLocal;

    private void Start()
    {
        if (_muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(_muzzlePrefab, transform.position, transform.rotation);
            //muzzleVFX.transform.forward = gameObject.transform.forward;

            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
            {
                Destroy(muzzleVFX, psMuzzle.main.duration);
            }
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        _posStartWorld = transform.position;
        _posStartLocal = transform.localPosition;

        Destroy(gameObject, 10);
    }

    private void Update()
    {
        if (_speed != 0)
        {
            transform.position += transform.up * (_speed * Time.deltaTime);

            if (_timeToChangePosX < _timeToChangePosXMax)
            {
                _timeToChangePosX += Time.deltaTime;
            }
            else
            {
                _boolSensX = !_boolSensX;
                _timeToChangePosX = 0;
            }

            if(_boolSensX)
            {
                transform.localPosition += new Vector3(Time.deltaTime * _coefSpeedMoveX, 0, 0);
            }
            else
            {
                transform.localPosition -= new Vector3(Time.deltaTime * _coefSpeedMoveX, 0, 0);
            }
        }
        else
        {
            Debug.Log("No Speed");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ici" + other.name);
        if (other.gameObject.layer == 8 && other.gameObject.tag == "PlayerColl")
        {
            Debug.Log("ici" + other.name);
            other.GetComponentInChildren<PlayerEntity>().LessLife(_damage);

            _speed = 0;

            if (_hitPrefab != null)
            {
                var hitVFX = Instantiate(_hitPrefab, transform.position, transform.rotation);

                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }

            Destroy(gameObject);
        }

        if (other.gameObject.layer == 31)
        {
            Debug.Log("ici" + other.name);
            _speed = 0;

            if (_hitPrefab != null)
            {
                var hitVFX = Instantiate(_hitPrefab, transform.position, transform.rotation);

                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }

            Destroy(gameObject);
        }
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWindCone02 : MonoBehaviour
{
    public float _speed;
    public int _damage;
    public GameObject _muzzlePrefab, _hitPrefab;

    public float _timeToChangePosX, _timeToChangePosXMax, _posMaxX, _coefSpeedMoveX;
    public bool _boolSensX;
    public Vector3 _posStartWorld, _posStartLocal;

    private void Start()
    {
        if (_muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(_muzzlePrefab, transform.position, transform.rotation);
            //muzzleVFX.transform.forward = gameObject.transform.forward;

            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
            {
                Destroy(muzzleVFX, psMuzzle.main.duration);
            }
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        _posStartWorld = transform.position;
        _posStartLocal = transform.localPosition;

        Destroy(gameObject, 10);
    }

    private void Update()
    {
        if (_speed != 0)
        {
            transform.position += transform.up * (_speed * Time.deltaTime);

            if (_timeToChangePosX < _timeToChangePosXMax)
            {
                _timeToChangePosX += Time.deltaTime;
            }
            else
            {
                _boolSensX = !_boolSensX;
                _timeToChangePosX = 0;
            }

            if(_boolSensX)
            {
                transform.localPosition += new Vector3(Time.deltaTime * _coefSpeedMoveX, 0, 0);
            }
            else
            {
                transform.localPosition -= new Vector3(Time.deltaTime * _coefSpeedMoveX, 0, 0);
            }
        }
        else
        {
            Debug.Log("No Speed");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ici" + other.name);
        if (other.gameObject.layer == 8 && other.gameObject.tag == "PlayerColl")
        {
            Debug.Log("ici" + other.name);
            other.GetComponentInChildren<PlayerEntity>().LessLife(_damage);

            _speed = 0;

            if (_hitPrefab != null)
            {
                var hitVFX = Instantiate(_hitPrefab, transform.position, transform.rotation);

                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }

            Destroy(gameObject);
        }

        if (other.gameObject.layer == 31)
        {
            Debug.Log("ici" + other.name);
            _speed = 0;

            if (_hitPrefab != null)
            {
                var hitVFX = Instantiate(_hitPrefab, transform.position, transform.rotation);

                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }

            Destroy(gameObject);
        }
    }
}
>>>>>>> Release
>>>>>>> Release
>>>>>>> Release
>>>>>>> Release
>>>>>>> Release
>>>>>>> Release
>>>>>>> Release
