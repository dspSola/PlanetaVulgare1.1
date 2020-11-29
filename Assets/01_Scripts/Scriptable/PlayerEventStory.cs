using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu]
public class PlayerEventStory : ScriptableObject
{
    [SerializeField] private bool _gameExist, _playerPassPortal;
    [SerializeField] private bool _bossFire, _bossWind, _bossWater, _bossEarth, _ultimeBoss;
    [SerializeField] private bool _totemFire, _totemWind, _totemWater, _totemEarth;
    [SerializeField] private int _cptBossWin;
    [SerializeField] private Vector3 _posSave, _posCheckPointDie;

    [SerializeField] private EntityData _playerEntityData;

    public void Init()
    {
        _totemEarth = false;
        _totemFire = false;
        _totemWater = false;
        _totemWind = false;
        _bossFire = false;
        _bossWind = false;
        _bossWater = false;
        _bossEarth = false;
        _ultimeBoss = false;

        _cptBossWin = 0;

        _posSave = Vector3.zero;
        _posCheckPointDie = Vector3.zero;

        _playerPassPortal = false;

        _playerEntityData.LifeMax = 100;

        SaveAllIntoTheText();
        ReadTheText();
    }

    public void SaveAllIntoTheText()
    {
        //Pass the filepath and filename to the StreamWriter Constructor
        StreamWriter sw = new StreamWriter("Assets/07_Datas/SaveData.txt");
        //Write a line of text
        sw.WriteLine("_gameExist=" + _gameExist);
        sw.WriteLine("_playerPassPortal=" + _playerPassPortal);
        sw.WriteLine("_totemEarth=" + _totemEarth);
        sw.WriteLine("_totemFire=" + _totemFire);
        sw.WriteLine("_totemWater=" + _totemWater);
        sw.WriteLine("_totemWind=" + _totemWind);
        sw.WriteLine("_bossFire=" + _bossFire);
        sw.WriteLine("_bossWind=" + _bossWind);
        sw.WriteLine("_bossWater=" + _bossWater);
        sw.WriteLine("_bossEarth=" + _bossEarth);
        sw.WriteLine("_ultimeBoss=" + _ultimeBoss);

        sw.WriteLine("_cptBossWin=" + _cptBossWin);

        sw.WriteLine("_posSave=" + _posSave);
        sw.WriteLine("_posCheckPointDie=" + _posCheckPointDie);
        //Close the file
        sw.Close();
    }

    public void TakeSaveData()
    {
        string line;
        //Pass the file path and file name to the StreamReader constructor
        StreamReader sr = new StreamReader("Assets/07_Datas/SaveData.txt");
        //Read the first line of text
        line = sr.ReadLine();

        int i = 0;

        //Continue to read until you reach end of file
        while (line != null)
        {
            //write the lie to console window
            Debug.Log(line);

            // using the method 
            string[] splitArray = line.Split(char.Parse("="));

            foreach (string s in splitArray)
            {
                Debug.Log(s);
            }

            if (i == 0)
            {
                Debug.Log("ICI" + splitArray[1]);
                if (splitArray[1] == "True")
                {
                    _gameExist = true;
                }
                else
                {
                    _gameExist = false;
                }
            }
            if (i == 1)
            {
                Debug.Log("ICI" + splitArray[1]);
                if (splitArray[1] == "True")
                {
                    _playerPassPortal = true;
                }
                else
                {
                    _playerPassPortal = false;
                }
            }
            if (i == 2)
            {
                Debug.Log("ICI" + splitArray[1]);
                if (splitArray[1] == "True")
                {
                    _totemEarth = true;
                }
                else
                {
                    _totemEarth = false;
                }
            }
            if (i == 3)
            {
                Debug.Log("ICI" + splitArray[1]);
                if (splitArray[1] == "True")
                {
                    _totemFire = true;
                }
                else
                {
                    _totemFire = false;
                }
            }
            if (i == 4)
            {
                Debug.Log("ICI" + splitArray[1]);
                if (splitArray[1] == "True")
                {
                    _totemWater = true;
                }
                else
                {
                    _totemWater = false;
                }
            }
            if (i == 5)
            {
                Debug.Log("ICI" + splitArray[1]);
                if (splitArray[1] == "True")
                {
                    _totemWind = true;
                }
                else
                {
                    _totemWind = false;
                }
            }
            if (i == 6)
            {
                Debug.Log("ICI" + splitArray[1]);
                if (splitArray[1] == "True")
                {
                    _bossFire = true;
                }
                else
                {
                    _bossFire = false;
                }
            }
            if (i == 7)
            {
                Debug.Log("ICI" + splitArray[1]);
                if (splitArray[1] == "True")
                {
                    _bossWind = true;
                }
                else
                {
                    _bossWind = false;
                }
            }
            if (i == 8)
            {
                Debug.Log("ICI" + splitArray[1]);
                if (splitArray[1] == "True")
                {
                    _bossWater = true;
                }
                else
                {
                    _bossWater = false;
                }
            }
            if (i == 9)
            {
                Debug.Log("ICI" + splitArray[1]);
                if (splitArray[1] == "True")
                {
                    _bossEarth = true;
                }
                else
                {
                    _bossEarth = false;
                }
            }
            if (i == 10)
            {
                Debug.Log("ICI" + splitArray[1]);
                if (splitArray[1] == "True")
                {
                    _ultimeBoss = true;
                }
                else
                {
                    _ultimeBoss = false;
                }
            }
            if (i == 11)
            {
                Debug.Log("ICI" + splitArray[1]);
                if (splitArray[1] != "0")
                {
                    _cptBossWin = int.Parse(splitArray[1]);
                }
                else
                {
                    _cptBossWin = 0;
                }
            }
            if (i == 12)
            {
                if (splitArray[1] != "(0.0, 0.0, 0.0)")
                {
                    char[] separators = { '(', ',', ')' };
                    string[] splitVector = splitArray[1].Split(separators);
                    int y = 0;
                    foreach (string s in splitVector)
                    {
                        Debug.Log(y + " = " + s);
                        y++;
                    }
                    float test = float.Parse("41.00027357629127", System.Globalization.CultureInfo.InvariantCulture);
                    Debug.Log(test);
                    _posSave = new Vector3(float.Parse(splitVector[1], System.Globalization.CultureInfo.InvariantCulture), float.Parse(splitVector[2], System.Globalization.CultureInfo.InvariantCulture), float.Parse(splitVector[3], System.Globalization.CultureInfo.InvariantCulture));
                }
                else
                {
                    _posSave = Vector3.zero;
                }
            }
            if (i == 13)
            {
                if (splitArray[1] != "(0.0, 0.0, 0.0)")
                {
                    char[] separators = { '(', ',', ')' };
                    string[] splitVector = splitArray[1].Split(separators);
                    foreach (string s in splitVector)
                    {
                        Debug.Log(s);
                    }
                    _posCheckPointDie = new Vector3(float.Parse(splitVector[1], System.Globalization.CultureInfo.InvariantCulture), float.Parse(splitVector[2], System.Globalization.CultureInfo.InvariantCulture), float.Parse(splitVector[3], System.Globalization.CultureInfo.InvariantCulture));
                }
                else
                {
                    _posCheckPointDie = Vector3.zero;
                }
            }

            //Read the next line
            line = sr.ReadLine();
            i++;
        }
        //close the file
        sr.Close();
    }

    public void WritOnText()
    {
        //Pass the filepath and filename to the StreamWriter Constructor
        StreamWriter sw = new StreamWriter("Assets/07_Datas/SaveData.txt");
        //Write a line of text
        sw.WriteLine("Hello World!!");
        //Write a second line of text
        sw.WriteLine("From the StreamWriter class");
        //Close the file
        sw.Close();
    }

    public void ReadTheText()
    {
        string line;
        //Pass the file path and file name to the StreamReader constructor
        StreamReader sr = new StreamReader("Assets/07_Datas/SaveData.txt");
        //Read the first line of text
        line = sr.ReadLine();
        //Continue to read until you reach end of file
        while (line != null)
        {
            //write the lie to console window
            Debug.Log(line);
            //Read the next line
            line = sr.ReadLine();
        }
        //close the file
        sr.Close();
    }

    public void NewGame()
    {
        _gameExist = true;
        Init();
    }

    public void Continue()
    {
        _posCheckPointDie = Vector3.zero;
    }

    public void SavePos(Vector3 posPlayer)
    {
        _posSave = posPlayer;
    }

    public void AddTotemFire()
    {
        _totemFire = true;
    }
    public void AddTotemWind()
    {
        _totemWind = true;
    }
    public void AddTotemWater()
    {
        _totemWater = true;
    }
    public void AddTotemEarth()
    {
        _totemEarth = true;
    }

    public void WinBossFire()
    {
        _bossFire = true;
        _cptBossWin++;
    }
    public void WinBossWind()
    {
        _bossWind = true;
        _cptBossWin++;
    }
    public void WinBossWater()
    {
        _bossWater = true;
        _cptBossWin++;
    }
    public void WinBossEarth()
    {
        _bossEarth = true;
        _cptBossWin++;
    }
    public void WinBossUltime()
    {
        _ultimeBoss = true;
        _cptBossWin++;
    }

    public void ReloadScene()
    {
        if (_totemFire && !_bossFire)
        {
            _totemFire = false;
        }
        if (_totemEarth && !_bossEarth)
        {
            _totemEarth = false;
        }
        if (_totemWater && !_bossWater)
        {
            _totemWater = false;
        }
        if (_totemWind && !_bossWind)
        {
            _totemWind = false;
        }
    }

    public Vector3 PosCheckPointDie { get => _posCheckPointDie; set => _posCheckPointDie = value; }
    public bool TotemFire { get => _totemFire; set => _totemFire = value; }
    public bool TotemWind { get => _totemWind; set => _totemWind = value; }
    public bool TotemWater { get => _totemWater; set => _totemWater = value; }
    public bool TotemEarth { get => _totemEarth; set => _totemEarth = value; }
    public bool BossFire { get => _bossFire; set => _bossFire = value; }
    public bool BossWind { get => _bossWind; set => _bossWind = value; }
    public bool BossWater { get => _bossWater; set => _bossWater = value; }
    public bool BossEarth { get => _bossEarth; set => _bossEarth = value; }
    public int CptBossWin { get => _cptBossWin; set => _cptBossWin = value; }
    public bool GameExist { get => _gameExist; set => _gameExist = value; }
    public Vector3 PosSave { get => _posSave; set => _posSave = value; }
    public bool PlayerPassPortal { get => _playerPassPortal; set => _playerPassPortal = value; }
}
