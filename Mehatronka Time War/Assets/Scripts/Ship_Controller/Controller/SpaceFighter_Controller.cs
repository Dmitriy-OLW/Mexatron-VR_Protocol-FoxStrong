using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class SpaceFighter_Controller : MonoBehaviour
{
    public float invers_speed = -1;
    
    public GameObject general_Rupulsor_torbin_left;
    public GameObject general_Rupulsor_torbin_right;
    
    public VSX.UniversalVehicleCombat.Thruster_Controller trust_pln;
    public VSX.UniversalVehicleCombat.Thruster_Controller trust_plv;
    public VSX.UniversalVehicleCombat.Thruster_Controller trust_prn;
    public VSX.UniversalVehicleCombat.Thruster_Controller trust_prv;
    public VSX.UniversalVehicleCombat.Thruster_Controller trust_zln;
    public VSX.UniversalVehicleCombat.Thruster_Controller trust_zlv;
    public VSX.UniversalVehicleCombat.Thruster_Controller trust_zrn;
    public VSX.UniversalVehicleCombat.Thruster_Controller trust_zrv;

    public bool inversivnoe_ypravlenie = false;

    protected Input_Map_SpaceFighter input;

    [SerializeField] private GameObject[] Engine_SFX;
    
    //Spedometr
    [SerializeField] private TextMeshProUGUI _Text_speed;
    [SerializeField] private Image _Speed_Barr;
    
    //trust_level
    [SerializeField] private float Trust_Index_Level;

    //Speed
    [SerializeField] private float _speed_Forward_Back = 1000;
    [SerializeField] private float _speed_To_V = 1000;
    [SerializeField] private float _speed_Y = 1000;
    [SerializeField] private float _speed_Righ_Left = 1000;
    [SerializeField] private float _speed_Turbo = 3000;
    [SerializeField] private float _speed_Rotate = 1000000;

    private float Spedometr_Speed;
    
    private float Value_Position_X;
    private float Value_Position_Y;
    private float Value_Position_Z;
    private float Value_Rotation_X;
    private float Value_Rotation_Y;
    private float Value_Rotation_Z;

    private float _trust_Value_Position_X;
    private float _trust_Value_Position_Y;
    private float _trust_Value_Position_Z;
    private float _trust_Value_Rotation_X;
    private float _trust_Value_Rotation_Y;
    private float _trust_Value_Rotation_Z;
    private float _trust_Turbo;

    private Rigidbody _rigidbody;

    protected virtual void OnEnable()
    {
        input.Enable();
    }


    protected virtual void OnDisable()
    {
        input.Disable();
    }

    protected void Awake()
    {

        input = new Input_Map_SpaceFighter();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.maxAngularVelocity = Mathf.Infinity;
        if (inversivnoe_ypravlenie)
        {
            invers_speed = 1;
        }
        else
        {
            invers_speed = -1;
        }

        general_Rupulsor_torbin_left.gameObject.transform.localScale = new Vector3(4f, 4f, 0f);
        general_Rupulsor_torbin_right.gameObject.transform.localScale = new Vector3(4f, 4f, 0f);
        foreach (GameObject Obj in Engine_SFX)
        {
            Obj.SetActive(false);
        }
    }


    private void FixedUpdate()
    {
        /*//PC
        Value_Position_Z = (input._SpaceFighter_Controls_.Boost.ReadValue<float>()* _speed_Turbo)+(input._SpaceFighter_Controls_.Move.ReadValue<Vector3>().z * _speed_Forward_Back);
        if (Value_Position_Z > 0)
        {
            Value_Position_Z += _speed_To_V;
        }
        Value_Position_X = _speed_Righ_Left*(input._SpaceFighter_Controls_.Move.ReadValue<Vector3>().x);
        Value_Position_Y = _speed_Y*(input._SpaceFighter_Controls_.Move.ReadValue<Vector3>().y);
        
        Value_Rotation_X = _speed_Rotate*input._SpaceFighter_Controls_.Rotate.ReadValue<Vector3>().x;
        Value_Rotation_Y = _speed_Rotate*input._SpaceFighter_Controls_.Rotate.ReadValue<Vector3>().y;
        Value_Rotation_Z = _speed_Rotate/10*input._SpaceFighter_Controls_.Rotate.ReadValue<Vector3>().z;

        Debug.Log(input._SpaceFighter_Controls_.Exit_Menu.ReadValue<Vector3>());*/


        //XR
        _trust_Value_Position_X = input._SpaceFighter_Controls_XR.Move_FLRB.ReadValue<Vector2>().x;
        _trust_Value_Position_Y = input._SpaceFighter_Controls_XR.Move_Up_Down.ReadValue<float>();
        _trust_Value_Position_Z = input._SpaceFighter_Controls_XR.Move_FLRB.ReadValue<Vector2>().y;
        _trust_Value_Rotation_X = input._SpaceFighter_Controls_XR.Rotate_FLRB.ReadValue<Vector2>().y;
        _trust_Value_Rotation_Y = input._SpaceFighter_Controls_XR.Rotate_Left_Right.ReadValue<float>();
        _trust_Value_Rotation_Z = -input._SpaceFighter_Controls_XR.Rotate_FLRB.ReadValue<Vector2>().x;
        _trust_Turbo = input._SpaceFighter_Controls_XR.Boost.ReadValue<float>();


        Value_Position_Z = (input._SpaceFighter_Controls_XR.Boost.ReadValue<float>() * _speed_Turbo) +
                           (input._SpaceFighter_Controls_XR.Move_FLRB.ReadValue<Vector2>().y * _speed_Forward_Back);
        if (Value_Position_Z > 0)
        {
            Value_Position_Z += _speed_To_V;
        }

        if (input._SpaceFighter_Controls_XR.Boost.ReadValue<float>() == 0 &&
            (input._SpaceFighter_Controls_XR.Boost_Extra_Lalue_Left.ReadValue<float>() > 0 &&
             input._SpaceFighter_Controls_XR.Boost_Extra_Lalue_Right.ReadValue<float>() > 0))
        {
            Value_Position_Z += _speed_Turbo *
                                (input._SpaceFighter_Controls_XR.Boost_Extra_Lalue_Left.ReadValue<float>() *
                                 input._SpaceFighter_Controls_XR.Boost_Extra_Lalue_Right.ReadValue<float>());
            _trust_Turbo = input._SpaceFighter_Controls_XR.Boost_Extra_Lalue_Left.ReadValue<float>() *
                           input._SpaceFighter_Controls_XR.Boost_Extra_Lalue_Right.ReadValue<float>();
            
        }


        Value_Position_X = _speed_Righ_Left * (input._SpaceFighter_Controls_XR.Move_FLRB.ReadValue<Vector2>().x);
        Value_Position_Y = _speed_Y * (input._SpaceFighter_Controls_XR.Move_Up_Down.ReadValue<float>());

        Value_Rotation_Y = _speed_Rotate * input._SpaceFighter_Controls_XR.Rotate_Left_Right.ReadValue<float>();

        Value_Rotation_X = invers_speed * _speed_Rotate *
                           input._SpaceFighter_Controls_XR.Rotate_FLRB.ReadValue<Vector2>().y;
        Value_Rotation_Z = _speed_Rotate / 10 * -input._SpaceFighter_Controls_XR.Rotate_FLRB.ReadValue<Vector2>().x;

        
        //_rigidbody.velocity = transform.rotation * new Vector3(Value_Position_X, Value_Position_Y, Value_Position_Z);
        _rigidbody.AddRelativeForce(Value_Position_X, Value_Position_Y, Value_Position_Z);
        _rigidbody.AddRelativeTorque(Value_Rotation_X, Value_Rotation_Y, Value_Rotation_Z);
        TrustUpdate();
        SpedometrUpdate();
    }

    private void SFX_Update()
    {
        
    }

    private void SpedometrUpdate()
    {
        
        Spedometr_Speed = _rigidbody.velocity.magnitude;
        _Text_speed.text = Convert.ToString((int)Spedometr_Speed);
        _Speed_Barr.fillAmount = Spedometr_Speed/1000;
    }


    private void TrustUpdate()
    {
        trust_pln.trust_Control_Level = 0;
        trust_plv.trust_Control_Level = 0;
        trust_prn.trust_Control_Level = 0;
        trust_prv.trust_Control_Level = 0;
        trust_zln.trust_Control_Level = 0;
        trust_zlv.trust_Control_Level = 0;
        trust_zrn.trust_Control_Level = 0;
        trust_zrv.trust_Control_Level = 0;
        general_Rupulsor_torbin_left.gameObject.transform.localScale = new Vector3(4f, 4f, 0f);
        general_Rupulsor_torbin_right.gameObject.transform.localScale = new Vector3(4f, 4f, 0f);

        if (_trust_Value_Position_X != 0 || _trust_Value_Position_Y != 0 || _trust_Value_Position_Z != 0 ||
            _trust_Value_Rotation_X != 0 || _trust_Value_Rotation_Y != 0 || _trust_Value_Rotation_Z != 0)
        {
            try
            {
                Engine_SFX[0].SetActive(true);
                Engine_SFX[0].gameObject.GetComponent<AudioSource>().volume = Spedometr_Speed / 150;
            }
            catch
            {
            }
        }
        else
        {
            Engine_SFX[0].gameObject.GetComponent<AudioSource>().volume = Spedometr_Speed / 150;
            if (Spedometr_Speed <= 5)
            {
                Engine_SFX[0].SetActive(false);
            }
            
        }


        if (_trust_Value_Rotation_X * -invers_speed > 0)
        {
            trust_pln.trust_Control_Level = _trust_Value_Rotation_X * -invers_speed;
            trust_prn.trust_Control_Level = _trust_Value_Rotation_X * -invers_speed;
            trust_zlv.trust_Control_Level = _trust_Value_Rotation_X * -invers_speed;
            trust_zrv.trust_Control_Level = _trust_Value_Rotation_X * -invers_speed;
        }

        if (_trust_Value_Rotation_X * -invers_speed < 0)
        {
            trust_plv.trust_Control_Level = _trust_Value_Rotation_X * invers_speed;
            trust_prv.trust_Control_Level = _trust_Value_Rotation_X * invers_speed;
            trust_zln.trust_Control_Level = _trust_Value_Rotation_X * invers_speed;
            trust_zrn.trust_Control_Level = _trust_Value_Rotation_X * invers_speed;
        }


        if (_trust_Value_Rotation_Z > 0)
        {
            trust_prn.trust_Control_Level = _trust_Value_Rotation_Z;
            trust_plv.trust_Control_Level = _trust_Value_Rotation_Z;
            trust_zrn.trust_Control_Level = _trust_Value_Rotation_Z;
            trust_zlv.trust_Control_Level = _trust_Value_Rotation_Z;
        }

        if (_trust_Value_Rotation_Z < 0)
        {
            trust_pln.trust_Control_Level = -_trust_Value_Rotation_Z;
            trust_prv.trust_Control_Level = -_trust_Value_Rotation_Z;
            trust_zln.trust_Control_Level = -_trust_Value_Rotation_Z;
            trust_zrv.trust_Control_Level = -_trust_Value_Rotation_Z;
        }

        if (_trust_Value_Rotation_Y > 0)
        {
            general_Rupulsor_torbin_left.gameObject.transform.localScale = new Vector3(4f, 4f, _trust_Value_Rotation_Y*4f);
            trust_pln.trust_Control_Level = _trust_Value_Rotation_Y;
            trust_plv.trust_Control_Level = _trust_Value_Rotation_Y;
            trust_zrn.trust_Control_Level = _trust_Value_Rotation_Y;
            trust_zrv.trust_Control_Level = _trust_Value_Rotation_Y;
        }

        if (_trust_Value_Rotation_Y < 0)
        {
            
            general_Rupulsor_torbin_right.gameObject.transform.localScale = new Vector3(4f, 4f, _trust_Value_Rotation_Y*4f);
            trust_prn.trust_Control_Level = -_trust_Value_Rotation_Y;
            trust_prv.trust_Control_Level = -_trust_Value_Rotation_Y;
            trust_zln.trust_Control_Level = -_trust_Value_Rotation_Y;
            trust_zlv.trust_Control_Level = -_trust_Value_Rotation_Y;
        }

        if (_trust_Value_Position_X > 0)
        {
            trust_pln.trust_Control_Level = _trust_Value_Position_X;
            trust_plv.trust_Control_Level = _trust_Value_Position_X;
            trust_zln.trust_Control_Level = _trust_Value_Position_X;
            trust_zlv.trust_Control_Level = _trust_Value_Position_X;
        }

        if (_trust_Value_Position_X < 0)
        {
            trust_prn.trust_Control_Level = -_trust_Value_Position_X;
            trust_prv.trust_Control_Level = -_trust_Value_Position_X;
            trust_zrn.trust_Control_Level = -_trust_Value_Position_X;
            trust_zrv.trust_Control_Level = -_trust_Value_Position_X;
        }

        if (_trust_Value_Position_Y > 0)
        {
            trust_pln.trust_Control_Level = _trust_Value_Position_Y;
            trust_prn.trust_Control_Level = _trust_Value_Position_Y;
            trust_zln.trust_Control_Level = _trust_Value_Position_Y;
            trust_zrn.trust_Control_Level = _trust_Value_Position_Y;
        }

        if (_trust_Value_Position_Y < 0)
        {
            trust_plv.trust_Control_Level = -_trust_Value_Position_Y;
            trust_prv.trust_Control_Level = -_trust_Value_Position_Y;
            trust_zlv.trust_Control_Level = -_trust_Value_Position_Y;
            trust_zrv.trust_Control_Level = -_trust_Value_Position_Y;
        }

        if (_trust_Value_Position_Z < 0)
        {
            trust_pln.trust_Control_Level = -_trust_Value_Position_Z;
            trust_plv.trust_Control_Level = -_trust_Value_Position_Z;
            trust_prn.trust_Control_Level = -_trust_Value_Position_Z;
            trust_prv.trust_Control_Level = -_trust_Value_Position_Z;
        }

        if (_trust_Value_Position_Z > 0)
        {
            general_Rupulsor_torbin_left.gameObject.transform.localScale = new Vector3(4f, 4f, _trust_Value_Position_Z*6f);
            general_Rupulsor_torbin_right.gameObject.transform.localScale = new Vector3(4f, 4f, _trust_Value_Position_Z*6f);
        }

        if (_trust_Turbo > 0)
        {
            general_Rupulsor_torbin_left.gameObject.transform.localScale = new Vector3(4f, 4f, _trust_Turbo* 20f);
            general_Rupulsor_torbin_right.gameObject.transform.localScale = new Vector3(4f, 4f, _trust_Turbo* 20f);
            try
            {
                Engine_SFX[0].SetActive(true);
                Engine_SFX[0].gameObject.GetComponent<AudioSource>().volume = Spedometr_Speed / 150;
                Engine_SFX[1].SetActive(true);
                Engine_SFX[1].gameObject.GetComponent<AudioSource>().volume = Spedometr_Speed / 300;
            }
            catch
            {
            }
        }
        else
        {
            
            Engine_SFX[1].gameObject.GetComponent<AudioSource>().volume = Spedometr_Speed / 300;
            Engine_SFX[0].gameObject.GetComponent<AudioSource>().volume = Spedometr_Speed / 150;
            if (Spedometr_Speed <= 5)
            {
                Engine_SFX[0].SetActive(false);
                Engine_SFX[1].SetActive(false);
            }
            
        }

    }
}






















/*


trust_pln.trust_Control_Level = Trust_Index_Level * _trust_Value_Position_X * _trust_Value_Position_Y *
                    _trust_Value_Position_Z * -_trust_Value_Rotation_X *
                    _trust_Value_Rotation_Y * _trust_Value_Rotation_Z;

trust_plv.trust_Control_Level = Trust_Index_Level * _trust_Value_Position_X * _trust_Value_Position_Y *
                    _trust_Value_Position_Z * -_trust_Value_Rotation_X *
                    _trust_Value_Rotation_Y * _trust_Value_Rotation_Z;

trust_prn.trust_Control_Level = Trust_Index_Level * _trust_Value_Position_X * _trust_Value_Position_Y *
                    _trust_Value_Position_Z * -_trust_Value_Rotation_X *
                    _trust_Value_Rotation_Y * _trust_Value_Rotation_Z;

trust_prv.trust_Control_Level = Trust_Index_Level * _trust_Value_Position_X * _trust_Value_Position_Y *
                    _trust_Value_Position_Z * -_trust_Value_Rotation_X *
                    _trust_Value_Rotation_Y * _trust_Value_Rotation_Z;

trust_zln.trust_Control_Level = Trust_Index_Level * _trust_Value_Position_X * _trust_Value_Position_Y *
                    _trust_Value_Position_Z * -_trust_Value_Rotation_X *
                    _trust_Value_Rotation_Y * _trust_Value_Rotation_Z;

trust_zlv.trust_Control_Level = Trust_Index_Level * _trust_Value_Position_X * _trust_Value_Position_Y *
                    _trust_Value_Position_Z * -_trust_Value_Rotation_X *
                    _trust_Value_Rotation_Y * _trust_Value_Rotation_Z;

trust_zrn.trust_Control_Level = Trust_Index_Level * _trust_Value_Position_X * _trust_Value_Position_Y *
                    _trust_Value_Position_Z * -_trust_Value_Rotation_X *
                    _trust_Value_Rotation_Y * _trust_Value_Rotation_Z;

trust_zrv.trust_Control_Level = Trust_Index_Level * _trust_Value_Position_X * _trust_Value_Position_Y *
                    _trust_Value_Position_Z * -_trust_Value_Rotation_X *
                    _trust_Value_Rotation_Y * _trust_Value_Rotation_Z;*/
        











/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceFighter_Controller : MonoBehaviour
{
    //Подумать над идее сделать акселератор на вперёд назад
    
        protected Input_Map_SpaceFighter input;
        
        //Speed
        [SerializeField]private float _speed_Forward = 2000;
        [SerializeField]private float _speed_Back = 1000;
        [SerializeField]private float _speed_Y = 1000;
        [SerializeField]private float _speed_Righ_Left = 1000;
        [SerializeField]private float _speed_Turbo = 3000;
        [SerializeField]private float speed_Rotate = 1000;
        
        private float Value_Position_X;
        private float Value_Position_Y;
        private float Value_Position_Z;
        private float Value_Rotation_X;
        private Rigidbody _rigidbody;
        protected void Awake()
        {

            input = new Input_Map_SpaceFighter();

        }
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        

        private void Update()
        {
            Debug.Log(input._SpaceFighter_Controls_.Move.ReadValue<Vector3>());
            Debug.Log(input._SpaceFighter_Controls_.Rotate.ReadValue<Vector3>());
            Debug.Log(input._SpaceFighter_Controls_.Move.ReadValue<Vector3>().x);
            //Value_Position_Z = (input._SpaceFighter_Controls_.Boost.ReadValue<float>()* _speed_Turbo)+(input._SpaceFighter_Controls_.Move_Forward.ReadValue<float>() * _speed_Forward) + (input._SpaceFighter_Controls_.Move_Back.ReadValue<float>() * _speed_Back * -1);
            //Value_Position_X = _speed_Righ_Left*(move_right.action.ReadValue<float>() + move_left.action.ReadValue<float>());
            //Value_Position_Y = _speed_Y*(move_up.action.ReadValue<float>() + move_down.action.ReadValue<float>());
            _rigidbody.velocity = transform.rotation * new Vector3(0f, 0f, 0f);
        }
        
        





}
*/



















    /*
    /*public bool PC = true;
    public bool AR = false;#1#
    /*
    //for PC
    public InputActionProperty move_forward;
    public InputActionProperty move_back;
    public InputActionProperty move_right;
    public InputActionProperty move_left;
    public InputActionProperty move_up;
    public InputActionProperty move_down;
    
    public InputActionProperty rotate_right;
    public InputActionProperty rotate_left;
    public InputActionProperty rotate_axis_right;
    public InputActionProperty rotate_axis_left;
    public InputActionProperty rotate_horisont_up;
    public InputActionProperty rotate_horisont_down;

    public InputActionProperty turbo;
    
    
    
    //for AR
    public InputActionProperty move_X;
    public InputActionProperty move_Y;
    public InputActionProperty move_up_Y;
    public InputActionProperty move_down_X;
    
    public InputActionProperty rotate_right_Grip;
    public InputActionProperty rotate_left_Grip;
    public InputActionProperty rotate_axis;
    public InputActionProperty rotate_horisont;

    public InputActionProperty turbo_A;
    
    //Speed
    [SerializeField]private float _speed_Forward = 2000;
    [SerializeField]private float _speed_Back = 1000;
    [SerializeField]private float _speed_Y = 1000;
    [SerializeField]private float _speed_Righ_Left = 1000;
    [SerializeField]private float _speed_Turbo = 3000;
    [SerializeField]private float speed_Rotate_X = 10;
    [SerializeField]private float speed_Rotate_Y = 10;
    [SerializeField]private float speed_Rotate_Z = 10;#1#

    private float Value_Position_X;
    private float Value_Position_Y;
    private float Value_Position_Z;
    private float Value_Rotation_X;
    private float Value_Rotation_Y;
    private float Value_Rotation_Z;
    
    private Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //for PC
        /*Value_Position_Z = (move_forward.action.ReadValue<float>() * _speed_Forward) + (move_back.action.ReadValue<float>() * _speed_Back);
        Value_Position_X = _speed_Righ_Left*(move_right.action.ReadValue<float>() + move_left.action.ReadValue<float>());
        Value_Position_Y = _speed_Y*(move_up.action.ReadValue<float>() + move_down.action.ReadValue<float>());
        
        Value_Rotation_Y = rotate_right.action.ReadValue<float>() + rotate_left.action.ReadValue<float>();
        Value_Rotation_Z = rotate_axis_right.action.ReadValue<float>() + rotate_axis_left.action.ReadValue<float>();
        Value_Rotation_X = rotate_horisont_up.action.ReadValue<float>() + rotate_horisont_down.action.ReadValue<float>();
        #1#

        Debug.Log(move_forward.action.ReadValue<float>());
    }

    private void FixedUpdate()
    {
        //_rigidbody.velocity = new Vector3(Value_Position_X +(_speed_Turbo*turbo.action.ReadValue<float>()), Value_Position_Y, Value_Position_Z);
    }
}
*/








    /*
    /*public bool PC = true;
    public bool AR = false;#1#
    /*
    //for PC
    public InputActionProperty move_forward;
    public InputActionProperty move_back;
    public InputActionProperty move_right;
    public InputActionProperty move_left;
    public InputActionProperty move_up;
    public InputActionProperty move_down;
    
    public InputActionProperty rotate_right;
    public InputActionProperty rotate_left;
    public InputActionProperty rotate_axis_right;
    public InputActionProperty rotate_axis_left;
    public InputActionProperty rotate_horisont_up;
    public InputActionProperty rotate_horisont_down;

    public InputActionProperty turbo;
    
    
    
    //for AR
    public InputActionProperty move_X;
    public InputActionProperty move_Y;
    public InputActionProperty move_up_Y;
    public InputActionProperty move_down_X;
    
    public InputActionProperty rotate_right_Grip;
    public InputActionProperty rotate_left_Grip;
    public InputActionProperty rotate_axis;
    public InputActionProperty rotate_horisont;

    public InputActionProperty turbo_A;
    
    //Speed
    [SerializeField]private float _speed_Forward = 2000;
    [SerializeField]private float _speed_Back = 1000;
    [SerializeField]private float _speed_Y = 1000;
    [SerializeField]private float _speed_Righ_Left = 1000;
    [SerializeField]private float _speed_Turbo = 3000;
    [SerializeField]private float speed_Rotate_X = 10;
    [SerializeField]private float speed_Rotate_Y = 10;
    [SerializeField]private float speed_Rotate_Z = 10;#1#

    private float Value_Position_X;
    private float Value_Position_Y;
    private float Value_Position_Z;
    private float Value_Rotation_X;
    private float Value_Rotation_Y;
    private float Value_Rotation_Z;
    
    private Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //for PC
        /*Value_Position_Z = (move_forward.action.ReadValue<float>() * _speed_Forward) + (move_back.action.ReadValue<float>() * _speed_Back);
        Value_Position_X = _speed_Righ_Left*(move_right.action.ReadValue<float>() + move_left.action.ReadValue<float>());
        Value_Position_Y = _speed_Y*(move_up.action.ReadValue<float>() + move_down.action.ReadValue<float>());
        
        Value_Rotation_Y = rotate_right.action.ReadValue<float>() + rotate_left.action.ReadValue<float>();
        Value_Rotation_Z = rotate_axis_right.action.ReadValue<float>() + rotate_axis_left.action.ReadValue<float>();
        Value_Rotation_X = rotate_horisont_up.action.ReadValue<float>() + rotate_horisont_down.action.ReadValue<float>();
        #1#

        Debug.Log(move_forward.action.ReadValue<float>());
    }

    private void FixedUpdate()
    {
        //_rigidbody.velocity = new Vector3(Value_Position_X +(_speed_Turbo*turbo.action.ReadValue<float>()), Value_Position_Y, Value_Position_Z);
    }
}
*/
