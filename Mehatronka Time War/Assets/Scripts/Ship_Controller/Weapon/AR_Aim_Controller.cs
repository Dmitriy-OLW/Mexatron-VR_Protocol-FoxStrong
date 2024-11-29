using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using Color = System.Drawing.Color;

public class AR_Aim_Controller : MonoBehaviour
{
    
    protected Input_Map_SpaceFighter input;
    [SerializeField] private GameObject _aim_pivot; //Точка в которую бъёт оружие
    [SerializeField] private GameObject _cours_shooting_pivot; //Точка перед короблём в которую бъёт оружие при курсовом режиме
    [SerializeField] private GameObject _controller_pivot; //Точка перед кнтролером в которую бъёт оружие при контролируемым режиме
    [SerializeField] private GameObject _void_pivot; //Точка в пространстве в которую бъёт оружие при контролируемым режиме
    [SerializeField] private GameObject _Aim_Spher_HoloGram; //сама сфера в которую мы стреляем
    [SerializeField] private GameObject _ray_pivot;//точка начала луча

    [SerializeField] private GameObject Turell_Other_Controller;
    
    //Spectralka
    [SerializeField] private GameObject Spectral_Start_Point;
    [SerializeField] private GameObject Spectral_Spher;
    [SerializeField] private GameObject Spectral_CutScene;
    
    [SerializeField] private GameObject Laser_CutScene;
    
    [SerializeField] private GameObject EMP_CutScene;
    
    [SerializeField] private GameObject Refocusirator_Start_Point;
    [SerializeField] private GameObject Refocusirator_end_pivot_Point;
    [SerializeField] private GameObject Refocusirator_Spher;
    [SerializeField] private GameObject Refocusirator_End_Spher;
    [SerializeField] private GameObject Refocusirator_CutScene;
    [SerializeField] private LineRenderer _Refocusirator_Line;
    
    
    [SerializeField] private GameObject Weapon_UI;
    [SerializeField] private GameObject Spectral_UI;
    [SerializeField] private Image[] Select_Image_Weapon;
    [SerializeField] private Image _Zaryd_Barr;

    [SerializeField] private AudioSource _Audio_Spec_Select;
    [SerializeField] private GameObject _Audio_Zaryd_is_Done;
    
    [SerializeField] private int Num_Weapon_Select = 1;
    [SerializeField] private bool Spectral_Weapon = false;

    private bool Refocusiratot_Fire;
    private bool Button_Down;
    private float Timer_Pereclucenie;

    private float Timer_Zaryd_Spectrl_Gun;
    private float Timer_Fire_Zaryd;

    private Ray _Ray_Aim;
    private RaycastHit _Hit_Aim;
    private LineRenderer _Line_Rend;

    private GameObject Ref_newObj_1;
    private GameObject Ref_newObj_2;

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
        Num_Weapon_Select = 1;
        Spectral_Weapon = false;
        Button_Down = false;
        Timer_Pereclucenie = 0;
        Timer_Zaryd_Spectrl_Gun = 70;
        Timer_Fire_Zaryd = 0;
    }

    private void Start()
    {
        _Line_Rend = _ray_pivot.GetComponent<LineRenderer>();
        UI_Sprite_Update();
        Star_Spectral_Active();
        //_aim_pivot = GameObject.Find("_Aim_Pivot_for_Transform");
        //_cours_shooting_pivot = GameObject.Find("_Aim_Point_on_SpaceFighter");
        //_controller_pivot = GameObject.Find("_Aim_Point_on_Controller");
    }
    private void Star_Spectral_Active()
    {
       
        try
        {
            Spectral_CutScene.SetActive(false);
            Spectral_Spher.SetActive(false);
            Laser_CutScene.SetActive(false);
            EMP_CutScene.SetActive(false);
            
            Refocusiratot_Fire = false;
            Refocusirator_Spher.SetActive(false);
            Refocusirator_End_Spher.SetActive(false);
            Refocusirator_CutScene.SetActive(false);
            _Refocusirator_Line.enabled = false;
        }
        catch{}
    }

    private void Update()
    {
        Zaryd_Update();
        _aim_pivot.transform.position = _cours_shooting_pivot.transform.position;
        if (input._SpaceFighter_Controls_XR.Selector_HotBar_Spec_Right.ReadValue<float>() > 0 &&
            input._SpaceFighter_Controls_XR.Selector_HotBar_Spec_Left.ReadValue<float>() > 0)
        {
            Timer_Pereclucenie += Time.deltaTime;
            if (Timer_Pereclucenie > 1)
            {
                Button_Down = true;
                Turell_Other_Controller.SetActive(Spectral_Weapon);
                Spectral_Weapon = !Spectral_Weapon;
                Num_Weapon_Select = 1;
                Timer_Pereclucenie = 0;
                Timer_Fire_Zaryd = 0;
                UI_Sprite_Update();
                Effects_Update(0);
            }
            
            
        }
        if (Button_Down == false && input._SpaceFighter_Controls_XR.Selector_HotBar_Spec_Left.ReadValue<float>() > 0)
        {
            Button_Down = true;
            Num_Weapon_Select -= 1;
            if (Num_Weapon_Select <= 0)
            {
                Num_Weapon_Select = 4;
            }
            UI_Sprite_Update();
        }
        if (Button_Down == false && input._SpaceFighter_Controls_XR.Selector_HotBar_Spec_Right.ReadValue<float>() > 0)
        {
            Button_Down = true;
            Num_Weapon_Select += 1;
            if (Num_Weapon_Select >= 5)
            {
                Num_Weapon_Select = 1;
            }
            UI_Sprite_Update();
        }

        
        if (input._SpaceFighter_Controls_XR.Selector_HotBar_Spec_Right.ReadValue<float>() == 0 &&
            input._SpaceFighter_Controls_XR.Selector_HotBar_Spec_Left.ReadValue<float>() == 0)
        {
            Button_Down = false;
            
        }

        if (!Spectral_Weapon)
        {
            
            if(Num_Weapon_Select == 1)
            {
                _Aim_Spher_HoloGram.SetActive(false);
                Fire_Kurs_1();
            }
            else if (Num_Weapon_Select == 2)
            {
                _Aim_Spher_HoloGram.SetActive(true);
                Fire_Kontroller_2();
            }
            else if (Num_Weapon_Select == 3)
            {
                _Aim_Spher_HoloGram.SetActive(true);
                Fire_Point_3();
            }
            else if (Num_Weapon_Select == 4)
            {
                _Aim_Spher_HoloGram.SetActive(false);
                Fire_Ray_4();
            }
        }
        else if (Spectral_Weapon)
        {
            _Aim_Spher_HoloGram.SetActive(false);
            if(Num_Weapon_Select == 1)
            {   Fire_Spectral_5();
            }
            else if (Num_Weapon_Select == 2)
            {
                Fire_Laser_6();
            }
            else if (Num_Weapon_Select == 3)
            {
                Fire_Electromagnit_7();
            }
            else if (Num_Weapon_Select == 4)
            {
                Fire_Refocusirator_8();
            }
        }

        //Debug.Log(input._SpaceFighter_Controls_XR.Aim_Fire_Right.ReadValue<float>());
        //Debug.Log(input._SpaceFighter_Controls_XR.Select_Fire_Left.ReadValue<float>());
        /*if (input._SpaceFighter_Controls_XR.Aim_Fire_Right.ReadValue<float>() > 0.5f)
        {
            _aim_pivot.transform.position = _controller_pivot.transform.position;
        }
        else if (input._SpaceFighter_Controls_XR.Select_Fire_Left.ReadValue<float>() > 0.5f)
        {
            _aim_pivot.transform.position = _cours_shooting_pivot.transform.position;
        }
        else
        {
            
            //Debug.Log("Test");
        }*/
    }

    public void Select_Weapon(int num, bool spec)
    {
        Num_Weapon_Select = num;
        Spectral_Weapon = spec;
    }

    private void Zaryd_Update()
    {
        Timer_Zaryd_Spectrl_Gun += Time.deltaTime;
        _Zaryd_Barr.fillAmount = Timer_Zaryd_Spectrl_Gun/100;
        
        if (Timer_Zaryd_Spectrl_Gun < 99)
        {
            _Audio_Zaryd_is_Done.SetActive(false);
        }
        else if (Timer_Zaryd_Spectrl_Gun < 105 && Timer_Zaryd_Spectrl_Gun > 100)
        {
            _Audio_Zaryd_is_Done.SetActive(true);
        }
        else if (Timer_Zaryd_Spectrl_Gun > 105)
        {
            _Audio_Zaryd_is_Done.SetActive(false);
        }
        
    }

    private void UI_Sprite_Update()
    {
        _Line_Rend = _ray_pivot.GetComponent<LineRenderer>();
        foreach (Image img in Select_Image_Weapon)
        {
            img.color = new UnityEngine.Color(0,0,0,1);
        }

        if (Spectral_Weapon)
        {
            Weapon_UI.SetActive(false);
            Spectral_UI.SetActive(true);
        }
        else
        {
            Weapon_UI.SetActive(true);
            Spectral_UI.SetActive(false);
        }
    }

    private void UI_Select_Weapon(int num)
    {
        Select_Image_Weapon[num].color = new UnityEngine.Color(0, 0, 0.3f, 1);
    }
    
    //Fire methods
    private void Fire_Kurs_1()
    {
        UI_Select_Weapon(0);
        _aim_pivot.transform.position = _cours_shooting_pivot.transform.position;
        if (input._SpaceFighter_Controls_XR.Aim_Fire_Right.ReadValue<float>() > 0.5f || input._SpaceFighter_Controls_XR.Select_Fire_Left.ReadValue<float>() > 0.5f ) 
        {
            _aim_pivot.transform.position = _cours_shooting_pivot.transform.position;
        }
    }

    private void Fire_Kontroller_2()
    {
        UI_Select_Weapon(1);
        _aim_pivot.transform.position = _controller_pivot.transform.position;
        if (input._SpaceFighter_Controls_XR.Aim_Fire_Right.ReadValue<float>() > 0.5f || input._SpaceFighter_Controls_XR.Select_Fire_Left.ReadValue<float>() > 0.5f ) 
        {
            _aim_pivot.transform.position = _controller_pivot.transform.position;
        }
    }

    private void Fire_Point_3()
    {
        UI_Select_Weapon(2);
        _aim_pivot.transform.position = _void_pivot.transform.position;
        if (input._SpaceFighter_Controls_XR.Select_Fire_Left.ReadValue<float>() > 0.5f)
        {
            _void_pivot.transform.position = _controller_pivot.transform.position;
        }
        if (input._SpaceFighter_Controls_XR.Aim_Fire_Right.ReadValue<float>() > 0.5f ) 
        {
            _aim_pivot.transform.position = _void_pivot.transform.position;
        }
    }

    private void Fire_Ray_4()
    {
        UI_Select_Weapon(3);
        //Debug.Log("Test_ray");
        _Ray_Aim = new Ray();
        _Ray_Aim.origin = _ray_pivot.transform.position;
        _Ray_Aim.direction = _ray_pivot.transform.forward;
        if (input._SpaceFighter_Controls_XR.Select_Fire_Left.ReadValue<float>() > 0.5f)
        {
            _Aim_Spher_HoloGram.SetActive(true);
            if (Physics.Raycast(_Ray_Aim, out _Hit_Aim))
            {
                _void_pivot.transform.position = _Hit_Aim.point;
                _aim_pivot.transform.position = _void_pivot.transform.position;
                _Line_Rend.enabled = true;
                _Line_Rend.SetPosition(0, _ray_pivot.transform.position);
                _Line_Rend.SetPosition(1, _aim_pivot.transform.position);
            }
            else
            {
                _Line_Rend.enabled = false;
            }
            _aim_pivot.transform.position = _void_pivot.transform.position;
        }
        else
        {
            _aim_pivot.transform.position = _cours_shooting_pivot.transform.position;
            _void_pivot.transform.position = _cours_shooting_pivot.transform.position;
            _Line_Rend.enabled = false;
            _Aim_Spher_HoloGram.SetActive(false);
        }
        if (input._SpaceFighter_Controls_XR.Aim_Fire_Right.ReadValue<float>() > 0.5f ) 
        {
            _aim_pivot.transform.position = _void_pivot.transform.position;
        }
    }
    
    //Spectral Fire methods
    private void Fire_Spectral_5()
    {
        UI_Select_Weapon(4);
        if (Timer_Zaryd_Spectrl_Gun > 100)
        {
            if (Timer_Fire_Zaryd > 6 ||
                ((input._SpaceFighter_Controls_XR.Select_Fire_Left.ReadValue<float>() > 0.5f) &&
                 (input._SpaceFighter_Controls_XR.Aim_Fire_Right.ReadValue<float>() > 0.5f)))
            {
                Timer_Fire_Zaryd += Time.deltaTime;
                Spectral_CutScene.SetActive(true);
                if (Timer_Fire_Zaryd > 6.6f)
                {
                    GameObject newObj = Instantiate(Spectral_Spher, Spectral_Start_Point.transform.position,
                        Quaternion.Euler(0, 0, 0));
                    newObj.transform.rotation = Spectral_Start_Point.transform.rotation;
                    newObj.SetActive(true);
                    Rigidbody _rb_Spher = newObj.gameObject.GetComponent<Rigidbody>();
                    _rb_Spher.AddRelativeForce(0, 0, 200000);
                    Timer_Fire_Zaryd = 0;
                    Timer_Zaryd_Spectrl_Gun = 0;
                }
            }
            else
            {
                Timer_Fire_Zaryd = 0;
                try
                {
                    Spectral_CutScene.SetActive(false);
                }
                catch{ }
            }
        }
    }

    private void Fire_Laser_6()
    {
        UI_Select_Weapon(5);
        if (Timer_Zaryd_Spectrl_Gun > 100)
        {
            if (Timer_Fire_Zaryd > 12 ||
                ((input._SpaceFighter_Controls_XR.Select_Fire_Left.ReadValue<float>() > 0.5f) &&
                (input._SpaceFighter_Controls_XR.Aim_Fire_Right.ReadValue<float>() > 0.5f)))
            {
                Timer_Fire_Zaryd += Time.deltaTime;
                Laser_CutScene.SetActive(true);
                
                if (Timer_Fire_Zaryd > 29)
                {
                    Laser_CutScene.SetActive(false);
                    Timer_Fire_Zaryd = 0;
                    Timer_Zaryd_Spectrl_Gun = 0;
                }
            }
            else
            {
                Timer_Fire_Zaryd = 0;
                Laser_CutScene.SetActive(false);
            }
        }
    }

    private void Fire_Electromagnit_7()
    {
        UI_Select_Weapon(6);
        if (Timer_Zaryd_Spectrl_Gun > 100)
        {
            if (Timer_Fire_Zaryd > 1 ||
                ((input._SpaceFighter_Controls_XR.Select_Fire_Left.ReadValue<float>() > 0.5f) &&
                (input._SpaceFighter_Controls_XR.Aim_Fire_Right.ReadValue<float>() > 0.5f)))
            {
                Timer_Fire_Zaryd += Time.deltaTime;
                EMP_CutScene.SetActive(true);
                if (Timer_Fire_Zaryd > 11.5)
                {
                    EMP_CutScene.SetActive(false);
                    Timer_Fire_Zaryd = 0;
                    Timer_Zaryd_Spectrl_Gun = 0;
                }
            }
            else
            {
                Timer_Fire_Zaryd = 0;
                EMP_CutScene.SetActive(false);
            }
        }
    }

    private void Fire_Refocusirator_8()
    {
        UI_Select_Weapon(7);
        if (Timer_Zaryd_Spectrl_Gun > 100)
        {
            if (Timer_Fire_Zaryd > 6 ||
                ((input._SpaceFighter_Controls_XR.Select_Fire_Left.ReadValue<float>() > 0.5f) &&
                 (input._SpaceFighter_Controls_XR.Aim_Fire_Right.ReadValue<float>() > 0.5f)))
            {
                Timer_Fire_Zaryd += Time.deltaTime;
                Refocusirator_CutScene.SetActive(true);
                if (Refocusiratot_Fire == false && Timer_Fire_Zaryd > 7f)
                {
                    gameObject.GetComponent<SpaceFighter_Controller>().enabled = false;
                    Ref_newObj_1 = Instantiate(Refocusirator_Spher, Refocusirator_Start_Point.transform.position,
                        Quaternion.Euler(0, 0, 0));
                    Ref_newObj_1.transform.rotation = Refocusirator_Start_Point.transform.rotation;
                    _Refocusirator_Line.enabled = true;
                    _Refocusirator_Line.SetPosition(0, Refocusirator_end_pivot_Point.transform.position);
                    _Refocusirator_Line.SetPosition(1, Ref_newObj_1.transform.position);
                    Ref_newObj_1.SetActive(true);
                    Rigidbody _rb_Spher = Ref_newObj_1.gameObject.GetComponent<Rigidbody>();
                    _rb_Spher.AddRelativeForce(0, 0, 100000);
                    Refocusiratot_Fire = true;
                }

                if(_Refocusirator_Line.enabled == true)
                {
                    _Refocusirator_Line.SetPosition(1, Ref_newObj_1.transform.position);
                    _Refocusirator_Line.SetPosition(0, Refocusirator_end_pivot_Point.transform.position);
                }
                
                /*if (Refocusiratot_Fire == true && gameObject.GetComponent<SpaceFighter_Controller>().enabled == false &&  Timer_Fire_Zaryd > 12f)
                {
                    Ref_newObj_2 = Instantiate(Refocusirator_End_Spher, Refocusirator_Start_Point.transform.position,
                        Quaternion.Euler(0, 0, 0));
                    Ref_newObj_2.transform.rotation = Refocusirator_Start_Point.transform.rotation;
                    Ref_newObj_2.transform.position = Refocusirator_Start_Point.transform.position;
                    Ref_newObj_2.GetComponent<Refocusirator_End_Snaryd>().OnEnemy = true;
                    Ref_newObj_2.SetActive(true);
                    Rigidbody _rb_Spher = Ref_newObj_2.gameObject.GetComponent<Rigidbody>();
                    _rb_Spher.AddRelativeForce(0, 0, 100000);
                    gameObject.GetComponent<SpaceFighter_Controller>().enabled = true;
                }
                if (Timer_Fire_Zaryd > 12.5f)
                {
                    Vector3 position = Ref_newObj_1.transform.position;
                    Ref_newObj_2.transform.position = Vector3.MoveTowards(transform.position, position, 5000 * Time.deltaTime);
                }*/
                
                if (Timer_Fire_Zaryd > 17.6f)
                {
                    _Refocusirator_Line.enabled = false;
                    Timer_Fire_Zaryd = 0;
                    Timer_Zaryd_Spectrl_Gun = 0;
                    Refocusiratot_Fire = false;
                }
                
            }
            else
            {
                Timer_Fire_Zaryd = 0;
                _Refocusirator_Line.enabled = false;
                gameObject.GetComponent<SpaceFighter_Controller>().enabled = true;
                try
                {
                    Spectral_CutScene.SetActive(false);
                }
                catch{ }
            }
        }
    }

    private void Effects_Update(int scenarie)
    {
        if (scenarie == 0)
        {
            _Audio_Spec_Select.Play();
        }
        /*if (scenarie == 1)
        {
            _Audio_Zaryd_is_Done.Play();
        }*/
    }

    
    
}

