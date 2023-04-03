using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Input")]
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    [Header("Other Properties")]
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float _walkSpeed;

    //components
    private Rigidbody2D _rigidbody;

    //other
    private bool _isGrounded;

    public void Jump(float y)
    {
        _rigidbody.AddForce(Vector3.up * y, ForceMode2D.Impulse);
    }

    public void Walk(Vector3 dir)
    {
        _rigidbody.AddForce(dir* _walkSpeed);
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        this.UpdateAsObservable().Where(_ => Input.GetKeyDown(up) && _isGrounded)
            .Subscribe(_ =>
            {
                Jump(_jumpForce);
            });

        this.UpdateAsObservable().Where(_ => Input.GetKey(left))
           .Subscribe(_ =>
           {
               Walk(Vector3.left);
           });

        this.UpdateAsObservable().Where(_ => Input.GetKey(right))
          .Subscribe(_ =>
          {
              Walk(Vector3.right);
          });

        this.OnCollisionStay2DAsObservable()            .Where(col => col.contacts[0].normal.y > 0.7)
            .Subscribe(_ => { _isGrounded = true;  });

        this.OnCollisionExit2DAsObservable()
            .Where(_=>_isGrounded)
            .Subscribe(_ => { _isGrounded = false;  });
    }
}
