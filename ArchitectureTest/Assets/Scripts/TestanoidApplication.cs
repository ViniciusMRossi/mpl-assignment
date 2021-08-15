using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class TestanoidApplication
{
    private static TestanoidApplication _instance;

    public static TestanoidApplication Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TestanoidApplication();
            }

            return _instance;
        }
    }
    private TestanoidApplication()
    {
        CurrentLevel = 0;
    }

    public int CurrentLevel
    {
        get;
        private set;
    }
}
