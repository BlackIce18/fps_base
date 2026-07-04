using UnityEngine;

public class GlobalStates : MonoBehaviour
{
    private static bool _isDebugging = false;
    
    public static bool IsDebugging
    {
        get => _isDebugging;
    }
}
