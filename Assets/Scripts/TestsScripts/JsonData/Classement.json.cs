[System.Serializable]
public class NamesData
{
    public FirstNamesData premier;
    public SecondNamesData deuxieme;
    public LostNamesData perdu;
}

[System.Serializable]
public class FirstNamesData
{
    public string[] prenoms;
}

[System.Serializable]
public class SecondNamesData
{
    public string[] prenoms;
}

[System.Serializable]
public class LostNamesData
{
    public string[] prenoms;
}
