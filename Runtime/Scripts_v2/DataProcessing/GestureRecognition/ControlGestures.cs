using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGestures
{
    // Declaration of gestures
    public static readonly List<Vector3> STAB_ACCELERATION = new List<Vector3>()
    {
        new Vector3(0.06f, -0.06f, 1.0f),
        new Vector3(0.26f, -0.13f, 0.96f),
        new Vector3(0.26f, -0.13f, 0.96f),
        new Vector3(0.26f, -0.13f, 0.96f),
        new Vector3(0.2f, -0.46f, 0.86f),
        new Vector3(0.08f, -0.64f, 0.76f),
        new Vector3(-0.27f, -0.68f, 0.69f),
        new Vector3(-0.27f, -0.68f, 0.69f),
        new Vector3(-0.82f, -0.37f, 0.43f),
        new Vector3(-0.41f, 0.8f, 0.44f),
        new Vector3(-0.41f, 0.8f, 0.44f),
        new Vector3(-0.41f, 0.8f, 0.44f),
        new Vector3(0.1f, 0.99f, 0.06f),
        new Vector3(0.15f, 0.97f, 0.18f),
        new Vector3(-0.65f, 0.49f, 0.58f),
        new Vector3(-0.65f, 0.49f, 0.58f),
    };

    public static readonly List<Vector3> BACK_SLASH_ACCELERATION = new List<Vector3>()
    {
        new Vector3(-0.78f, -0.54f, -0.30f),
        new Vector3(-0.76f, -0.45f, -0.48f),
        new Vector3(-0.76f, -0.45f, -0.48f),
        new Vector3(-0.65f, -0.34f, -0.67f),
        new Vector3(-0.64f, -0.30f, -0.71f),
        new Vector3(-0.65f, -0.20f, -0.74f),
        new Vector3(-0.68f, 0.26f, -0.69f),
        new Vector3(-0.68f, 0.48f, -0.56f),
        new Vector3(-0.62f, 0.65f, -0.44f),
        new Vector3(-0.52f, 0.81f, -0.26f),
        new Vector3(-0.52f, 0.81f, -0.26f),
        new Vector3(-0.49f, 0.87f, -0.04f),
        new Vector3(-0.38f, 0.91f, 0.19f),
        new Vector3(-0.19f, 0.85f, 0.49f),
        new Vector3(-0.19f, 0.85f, 0.49f),
        new Vector3(0.03f, 0.66f, 0.75f),
        new Vector3(-0.19f, 0.53f, 0.83f)
    };

    public static readonly List<Vector3> FORWARD_SLASH_ACCELERATION = new List<Vector3>()
    {
        new Vector3(-0.18f, -0.96f, -0.22f),
        new Vector3(-0.18f, -0.96f, -0.22f),
        new Vector3(-0.18f, -0.96f, -0.22f),
        new Vector3(0.02f, -0.49f, -0.87f),
        new Vector3(0.16f, -0.32f, -0.93f),
        new Vector3(0.16f, -0.32f, -0.93f),
        new Vector3(0.16f, -0.32f, -0.93f),
        new Vector3(0.47f, 0.07f, -0.88f),
        new Vector3(0.53f, 0.22f, -0.82f),
        new Vector3(0.52f, 0.3f, -0.8f),
        new Vector3(0.52f, 0.36f, -0.78f),
        new Vector3(0.56f, 0.67f, -0.49f),
        new Vector3(0.49f, 0.68f, -0.54f),
        new Vector3(0.49f, 0.68f, -0.54f),
        new Vector3(0.38f, 0.87f, -0.31f),
        new Vector3(0.22f, 0.69f, 0.7f),
        new Vector3(0.01f, 0.67f, 0.74f),
        new Vector3(-0.31f, 0.58f, 0.75f),
        new Vector3(0.29f, 0.59f, 0.75f),
    };

    public static readonly List<Vector3> DUNK_ACCELERATION = new List<Vector3>()
    {
        new Vector3(-0.31f, -0.35f, -0.89f),
        new Vector3(-0.31f, -0.35f, -0.89f),
        new Vector3(-0.38f, -0.15f, -0.92f),
        new Vector3(-0.18f, -0.09f, -0.98f),
        new Vector3(-0.11f, -0.03f, -0.99f),
        new Vector3(-0.11f, -0.03f, -0.99f),
        new Vector3(-0.03f, 0.05f, -1.0f),
        new Vector3(0.03f, 0.27f, -0.96f),
        new Vector3(0.03f, 0.27f, -0.96f),
        new Vector3(0.03f, 0.27f, -0.96f),
        new Vector3(0.0f, 0.7f, -0.71f),
        new Vector3(-0.13f, 0.89f, -0.44f),
        new Vector3(-0.12f, 0.95f, -0.3f),
        new Vector3(0.04f, 0.95f, 0.31f),
        new Vector3(0.49f, 0.68f, 0.54f),
        new Vector3(0.19f, 0.61f, 0.77f),
    };

    public static readonly List<Vector3> UPPERCUT_ACCELERATION = new List<Vector3>
    {
        new Vector3(-0.36f, 0.91f, 0.22f),
        new Vector3(-0.36f, 0.91f, 0.22f),
        new Vector3(-0.36f, 0.91f, 0.22f),
        new Vector3(-0.39f, 0.79f, 0.48f),
        new Vector3(-0.33f, 0.74f, 0.59f),
        new Vector3(-0.33f, 0.74f, 0.59f),
        new Vector3(-0.33f, 0.74f, 0.59f),
        new Vector3(-0.32f, 0.75f, 0.58f),
        new Vector3(-0.21f, 0.77f, 0.6f),
        new Vector3(-0.4f, 0.91f, -0.12f),
        new Vector3(-0.4f, 0.91f, -0.12f),
        new Vector3(-0.45f, 0.86f, -0.24f),
        new Vector3(-0.58f, 0.64f, -0.5f),
        new Vector3(-0.58f, 0.64f, -0.5f),
        new Vector3(-0.58f, 0.64f, -0.5f),
    };

    public static readonly List<Quaternion> DUNK_ORIENTATION = new List<Quaternion>()
    {
        new Quaternion(-0.29724f, 0.88171f, -0.31274f, 0.19092f),
        new Quaternion(-0.30634f, 0.89691f, -0.27509f, 0.16132f),
        new Quaternion(-0.30634f, 0.89691f, -0.27509f, 0.16132f),
        new Quaternion(-0.32776f, 0.90649f, -0.22644f, 0.14001f),
        new Quaternion(-0.34436f, 0.90466f, -0.21118f, 0.13544f),
        new Quaternion(-0.35046f, 0.89648f, -0.23059f, 0.14258f),
        new Quaternion(-0.35046f, 0.89648f, -0.23059f, 0.14258f),
        new Quaternion(-0.35046f, 0.89648f, -0.23059f, 0.14258f),
        new Quaternion(-0.34747f, 0.87115f, -0.30524f, 0.16486f),
        new Quaternion(-0.28986f, 0.75171f, -0.54205f, 0.23901f),
        new Quaternion(-0.28986f, 0.75171f, -0.54205f, 0.23901f),
        new Quaternion(-0.23785f, 0.63965f, -0.67456f, 0.28143f),
        new Quaternion(-0.151f, 0.41705f, -0.82227f, 0.35657f),
        new Quaternion(-0.13574f, 0.29059f, -0.86395f, 0.38831f),
        new Quaternion(-0.10382f, 0.16339f, -0.89136f, 0.40991f),
        new Quaternion(0.04596f, -0.03015f, -0.8974f, 0.43787f),
        new Quaternion(0.0661f, -0.10199f, -0.89124f, 0.43695f),
        new Quaternion(0.0661f, -0.10199f, -0.89124f, 0.43695f),
        new Quaternion(0.04779f, -0.1853f, -0.87878f, 0.43713f),
    };

    public static readonly List<Quaternion> FORWARD_SLASH_ORIENTATION = new List<Quaternion>()
    {
        new Quaternion(-0.53259f, 0.4787f, -0.61292f, 0.33398f),
        new Quaternion(-0.54083f, 0.48431f, -0.60962f, 0.3183f),
        new Quaternion(-0.54974f, 0.48511f, -0.60657f, 0.30743f),
        new Quaternion(-0.56775f, 0.48389f, -0.59912f, 0.29083f),
        new Quaternion(-0.57892f, 0.47467f, -0.59943f, 0.28333f),
        new Quaternion(-0.57892f, 0.47467f, -0.59943f, 0.28333f),
        new Quaternion(-0.59729f, 0.44147f, -0.6004f, 0.29626f),
        new Quaternion(-0.61462f, 0.38947f, -0.60315f, 0.32678f),
        new Quaternion(-0.61505f, 0.2915f, -0.61395f, 0.39978f),
        new Quaternion(-0.61505f, 0.2915f, -0.61395f, 0.39978f),
        new Quaternion(-0.57111f, 0.16785f, -0.62378f, 0.50653f),
        new Quaternion(-0.4035f, -0.05292f, -0.60193f, 0.68707f),
        new Quaternion(-0.22626f, -0.19855f, -0.5592f, 0.77246f),
        new Quaternion(-0.22626f, -0.19855f, -0.5592f, 0.77246f),
        new Quaternion(-0.06219f, -0.27802f, -0.52551f, 0.8017f),
        new Quaternion(-0.06219f, -0.27802f, -0.52551f, 0.8017f),
    };

    public static readonly List<Quaternion> BACK_SLASH_ORIENTATION = new List<Quaternion>()
    {
        new Quaternion(-0.03735f, 0.87213f, -0.23645f, 0.4267f),
        new Quaternion(-0.04523f, 0.88239f, -0.21729f, 0.41492f),
        new Quaternion(-0.04767f, 0.88257f, -0.22131f, 0.41211f),
        new Quaternion(-0.04767f, 0.88257f, -0.22131f, 0.41211f),
        new Quaternion(-0.03668f, 0.87836f, -0.24072f, 0.41138f),
        new Quaternion(0.00635f, 0.85394f, -0.32404f, 0.4071f),
        new Quaternion(0.06152f, 0.81592f, -0.41095f, 0.40198f),
        new Quaternion(0.06152f, 0.81592f, -0.41095f, 0.40198f),
        new Quaternion(0.16852f, 0.72412f, -0.55005f, 0.38043f),
        new Quaternion(0.27374f, 0.60498f, -0.66241f, 0.3468f),
        new Quaternion(0.40466f, 0.39844f, -0.77338f, 0.2818f),
        new Quaternion(0.40466f, 0.39844f, -0.77338f, 0.2818f),
        new Quaternion(0.46356f, 0.20984f, -0.83221f, 0.22028f),
        new Quaternion(0.46356f, 0.20984f, -0.83221f, 0.22028f),
        new Quaternion(0.46356f, 0.20984f, -0.83221f, 0.22028f),
        new Quaternion(0.51434f, -0.08954f, -0.84796f, 0.09174f),
        new Quaternion(0.51202f, -0.10876f, -0.84796f, 0.08374f),
    };

    public static readonly List<Quaternion> STAB_ORIENTATION = new List<Quaternion>()
    {
        new Quaternion(0.0426f, -0.08942f, -0.85138f, 0.51514f),
        new Quaternion(0.04431f, -0.08722f, -0.84808f, 0.52069f),
        new Quaternion(0.04156f, -0.08484f, -0.84808f, 0.52136f),
        new Quaternion(0.04156f, -0.08484f, -0.84808f, 0.52136f),
        new Quaternion(0.04047f, -0.08398f, -0.84839f, 0.52112f),
        new Quaternion(0.03247f, -0.06592f, -0.84937f, 0.52271f),
        new Quaternion(0.01331f, -0.02472f, -0.86322f, 0.50409f),
        new Quaternion(0.01331f, -0.02472f, -0.86322f, 0.50409f),
        new Quaternion(0.00598f, -0.00629f, -0.87476f, 0.48444f),
        new Quaternion(-0.00958f, -0.01611f, -0.89465f, 0.44641f),
        new Quaternion(0.00311f, -0.02686f, -0.90466f, 0.42523f),
        new Quaternion(0.00311f, -0.02686f, -0.90466f, 0.42523f),
        new Quaternion(-0.00873f, -0.04333f, -0.93079f, 0.36292f),
        new Quaternion(-0.01178f, -0.05548f, -0.93921f, 0.33868f),
        new Quaternion(-0.01178f, -0.05548f, -0.93921f, 0.33868f),
        new Quaternion(-0.01178f, -0.05548f, -0.93921f, 0.33868f),
    };

    public static readonly List<Quaternion> UPPERCUT_ORIENTATION = new List<Quaternion>
    {
        new Quaternion(-0.21326f, 0.70795f, 0.66315f, 0.11633f),
        new Quaternion(-0.21326f, 0.70795f, 0.66315f, 0.11633f),
        new Quaternion(-0.21326f, 0.70795f, 0.66315f, 0.11633f),
        new Quaternion(-0.21344f, 0.75897f, 0.60669f, 0.10175f),
        new Quaternion(-0.22827f, 0.77094f, 0.59021f, 0.07196f),
        new Quaternion(-0.22827f, 0.77094f, 0.59021f, 0.07196f),
        new Quaternion(-0.22827f, 0.77094f, 0.59021f, 0.07196f),
        new Quaternion(-0.24103f, 0.77478f, 0.58185f, 0.05493f),
        new Quaternion(-0.27893f, 0.76422f, 0.58112f, 0.02197f),
        new Quaternion(-0.29871f, 0.27667f, 0.90924f, -0.08661f),
        new Quaternion(-0.29871f, 0.27667f, 0.90924f, -0.08661f),
        new Quaternion(-0.08752f, 0.00354f, 0.98236f, -0.16541f),
        new Quaternion(0.16876f, -0.31403f, 0.883f, -0.30536f),
        new Quaternion(0.16876f, -0.31403f, 0.883f, -0.30536f),
        new Quaternion(0.16876f, -0.31403f, 0.883f, -0.30536f),
    };



    ///////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////// TENNIS ////////////////////////////////////////////////

    #region BH_TOPSPIN
    List<Vector3> BH_TOPSPIN_ACCELERATION = new List<Vector3>
{
    new Vector3(0.45f, 0.65f, 0.61f),
    new Vector3(0.45f, 0.65f, 0.61f),
    new Vector3(0.45f, 0.65f, 0.61f),
    new Vector3(0.5f, 0.78f, 0.37f),
    new Vector3(0.18f, 0.79f, 0.58f),
    new Vector3(0.11f, 0.79f, 0.61f),
    new Vector3(0.11f, 0.79f, 0.61f),
    new Vector3(-0.06f, 0.81f, 0.58f),
    new Vector3(-0.19f, 0.87f, 0.45f),
    new Vector3(-0.16f, 0.89f, 0.43f),
    new Vector3(-0.16f, 0.89f, 0.43f),
    new Vector3(-0.06f, 0.94f, 0.35f),
    new Vector3(0.11f, 0.91f, 0.41f),
    new Vector3(0.11f, 0.91f, 0.41f),
    new Vector3(0.11f, 0.91f, 0.41f),
    new Vector3(0.15f, 0.8f, 0.59f),
    new Vector3(0.56f, 0.73f, 0.39f),
    new Vector3(0.54f, 0.74f, 0.4f),
    new Vector3(0.54f, 0.74f, 0.4f),
    new Vector3(0.6f, 0.57f, -0.56f),
    new Vector3(0.38f, 0.45f, -0.81f),
    new Vector3(0.33f, 0.08f, -0.94f),
    new Vector3(0.4f, -0.11f, -0.91f),
};
    List<Vector3> BH_TOPSPIN_ANGULARVELOCITY = new List<Vector3>
{
    new Vector3(0.1f, -0.01f, 0.17f),
    new Vector3(0.1f, -0.01f, 0.17f),
    new Vector3(0.1f, -0.01f, 0.17f),
    new Vector3(0.18f, 0.0f, 0.24f),
    new Vector3(0.11f, 0.09f, 0.23f),
    new Vector3(0.12f, 0.12f, 0.15f),
    new Vector3(0.12f, 0.12f, 0.15f),
    new Vector3(0.07f, 0.1f, 0.12f),
    new Vector3(0.07f, 0.09f, 0.06f),
    new Vector3(0.08f, 0.03f, -0.04f),
    new Vector3(0.08f, 0.03f, -0.04f),
    new Vector3(0.07f, -0.04f, -0.13f),
    new Vector3(0.07f, -0.07f, -0.34f),
    new Vector3(0.07f, -0.07f, -0.34f),
    new Vector3(0.07f, -0.07f, -0.34f),
    new Vector3(-0.5f, -0.1f, -0.79f),
    new Vector3(-0.73f, -0.04f, -0.98f),
    new Vector3(-0.89f, 0.03f, -1.04f),
    new Vector3(-0.89f, 0.03f, -1.04f),
    new Vector3(-0.41f, -0.06f, -0.28f),
    new Vector3(-0.17f, 0.19f, 0.11f),
    new Vector3(0.04f, 0.16f, 0.14f),
    new Vector3(0.07f, 0.18f, 0.13f),
};

    #endregion
    #region BH_BACKSPIN
    List<Vector3> BH_BACKSPIN_ACCELERATION = new List<Vector3>
{
    new Vector3(-0.98f, -0.18f, -0.12f),
    new Vector3(-0.98f, -0.18f, -0.12f),
    new Vector3(-0.89f, 0.01f, -0.45f),
    new Vector3(-0.85f, 0.03f, -0.53f),
    new Vector3(-0.78f, 0.26f, -0.56f),
    new Vector3(-0.78f, 0.26f, -0.56f),
    new Vector3(-0.58f, 0.51f, -0.64f),
    new Vector3(-0.37f, 0.65f, -0.67f),
    new Vector3(-0.32f, 0.82f, -0.48f),
    new Vector3(-0.32f, 0.82f, -0.48f),
    new Vector3(-0.31f, 0.85f, -0.42f),
    new Vector3(-0.34f, 0.88f, -0.32f),
    new Vector3(-0.46f, 0.87f, -0.17f),
    new Vector3(0.14f, 0.7f, 0.7f),
    new Vector3(-0.06f, 0.53f, 0.85f),
    new Vector3(0.1f, 0.58f, 0.81f),
    new Vector3(-0.51f, 0.51f, 0.7f),
    new Vector3(-0.02f, 0.52f, 0.86f),
};
    List<Vector3> BH_BACKSPIN_ANGULARVELOCITY = new List<Vector3>
{
    new Vector3(-0.16f, 0.05f, 0.05f),
    new Vector3(-0.16f, 0.05f, 0.05f),
    new Vector3(-0.11f, 0.01f, 0.02f),
    new Vector3(-0.04f, 0.01f, -0.01f),
    new Vector3(0.06f, 0.02f, -0.05f),
    new Vector3(0.06f, 0.02f, -0.05f),
    new Vector3(0.13f, 0.06f, -0.07f),
    new Vector3(0.33f, 0.1f, -0.09f),
    new Vector3(0.6f, 0.09f, -0.1f),
    new Vector3(0.6f, 0.09f, -0.1f),
    new Vector3(0.74f, 0.13f, -0.1f),
    new Vector3(0.82f, 0.16f, -0.09f),
    new Vector3(1.0f, 0.19f, -0.06f),
    new Vector3(0.65f, 0.01f, -0.08f),
    new Vector3(0.17f, -0.11f, 0.07f),
    new Vector3(0.12f, -0.11f, 0.06f),
    new Vector3(0.12f, -0.03f, 0.05f),
    new Vector3(0.14f, -0.13f, 0.05f),
};

    #endregion
    #region FH_BACKSPIN
    List<Vector3> FH_BACKSPIN_ACCELERATION = new List<Vector3>
{
    new Vector3(0.45f, -0.47f, 0.76f),
    new Vector3(0.53f, -0.48f, 0.7f),
    new Vector3(0.83f, -0.38f, -0.4f),
    new Vector3(0.83f, -0.38f, -0.4f),
    new Vector3(0.44f, -0.02f, -0.9f),
    new Vector3(0.41f, 0.23f, -0.89f),
    new Vector3(0.44f, 0.33f, -0.83f),
    new Vector3(0.44f, 0.33f, -0.83f),
    new Vector3(0.41f, 0.48f, -0.78f),
    new Vector3(0.53f, 0.6f, -0.6f),
    new Vector3(-0.32f, 0.95f, 0.0f),
    new Vector3(-0.32f, 0.95f, 0.0f),
    new Vector3(0.35f, 0.63f, 0.7f),
    new Vector3(-0.89f, 0.0f, 0.46f),
    new Vector3(-0.89f, 0.0f, 0.46f),
    new Vector3(-0.89f, 0.0f, 0.46f),
    new Vector3(0.1f, 0.49f, 0.87f),
};
    List<Vector3> FH_BACKSPIN_ANGULARVELOCITY = new List<Vector3>
{
    new Vector3(-0.19f, 0.07f, 0.0f),
    new Vector3(-0.2f, 0.07f, -0.03f),
    new Vector3(-0.19f, 0.06f, -0.05f),
    new Vector3(-0.19f, 0.06f, -0.05f),
    new Vector3(-0.19f, 0.02f, -0.1f),
    new Vector3(-0.14f, 0.0f, -0.17f),
    new Vector3(-0.05f, -0.03f, -0.22f),
    new Vector3(-0.05f, -0.03f, -0.22f),
    new Vector3(0.24f, -0.12f, -0.31f),
    new Vector3(0.81f, -0.06f, -0.1f),
    new Vector3(1.34f, 0.03f, 0.15f),
    new Vector3(1.34f, 0.03f, 0.15f),
    new Vector3(0.35f, 0.42f, -0.01f),
    new Vector3(0.08f, -0.01f, 0.09f),
    new Vector3(0.08f, -0.01f, 0.09f),
    new Vector3(0.08f, -0.01f, 0.09f),
    new Vector3(0.08f, 0.14f, 0.06f),
};

    #endregion
    #region BH_FLAT
    List<Vector3> BH_FLAT_ACCELERATION = new List<Vector3>
{
    new Vector3(-0.22f, 0.18f, 0.96f),
    new Vector3(-0.33f, 0.2f, 0.92f),
    new Vector3(-0.33f, 0.2f, 0.92f),
    new Vector3(-0.33f, 0.2f, 0.92f),
    new Vector3(-0.65f, 0.48f, 0.58f),
    new Vector3(-0.68f, 0.59f, 0.43f),
    new Vector3(-0.66f, 0.63f, 0.4f),
    new Vector3(-0.66f, 0.63f, 0.4f),
    new Vector3(-0.66f, 0.61f, 0.44f),
    new Vector3(-0.33f, 0.63f, 0.7f),
    new Vector3(-0.2f, 0.68f, 0.71f),
    new Vector3(-0.02f, 0.7f, 0.71f),
    new Vector3(0.65f, 0.69f, 0.33f),
    new Vector3(0.91f, 0.42f, -0.02f),
    new Vector3(0.91f, 0.42f, -0.02f),
    new Vector3(0.9f, 0.34f, -0.29f),
    new Vector3(0.9f, 0.34f, -0.29f),
    new Vector3(0.87f, 0.22f, -0.44f),
    new Vector3(0.87f, 0.22f, -0.44f),
};
    List<Vector3> BH_FLAT_ANGULARVELOCITY = new List<Vector3>
{
    new Vector3(-0.03f, 0.01f, 0.06f),
    new Vector3(-0.02f, 0.0f, 0.01f),
    new Vector3(-0.02f, 0.0f, 0.01f),
    new Vector3(-0.02f, 0.0f, 0.01f),
    new Vector3(0.0f, 0.02f, -0.04f),
    new Vector3(0.03f, 0.03f, -0.09f),
    new Vector3(0.07f, 0.03f, -0.14f),
    new Vector3(0.1f, 0.05f, -0.19f),
    new Vector3(0.18f, 0.17f, -0.33f),
    new Vector3(0.21f, 0.18f, -0.56f),
    new Vector3(0.15f, 0.15f, -0.63f),
    new Vector3(0.05f, 0.11f, -0.66f),
    new Vector3(-0.23f, -0.15f, -0.61f),
    new Vector3(-0.23f, 0.0f, -0.3f),
    new Vector3(-0.23f, 0.0f, -0.3f),
    new Vector3(-0.15f, 0.03f, -0.11f),
    new Vector3(-0.15f, 0.03f, -0.11f),
    new Vector3(-0.12f, 0.05f, -0.07f),
    new Vector3(-0.12f, 0.05f, -0.07f),
};
    #endregion
    #region FH_TOPSPIN
    List<Vector3> FH_TOPSPIN_ACCELERATION = new List<Vector3>
{
    new Vector3(-0.96f, -0.26f, -0.1f),
    new Vector3(-0.99f, 0.05f, 0.16f),
    new Vector3(-0.99f, 0.05f, 0.16f),
    new Vector3(-0.48f, 0.58f, 0.66f),
    new Vector3(-0.45f, 0.56f, 0.7f),
    new Vector3(0.07f, 0.65f, 0.76f),
    new Vector3(0.35f, 0.63f, 0.69f),
    new Vector3(-0.29f, 0.8f, 0.52f),
    new Vector3(-0.29f, 0.8f, 0.52f),
    new Vector3(-0.33f, 0.86f, 0.38f),
    new Vector3(-0.61f, 0.75f, 0.25f),
    new Vector3(-0.61f, 0.75f, 0.25f),
    new Vector3(-0.63f, 0.73f, 0.26f),
    new Vector3(-0.66f, 0.66f, 0.35f),
    new Vector3(-0.68f, 0.67f, 0.3f),
    new Vector3(-0.65f, 0.71f, 0.27f),
    new Vector3(-0.65f, 0.71f, 0.27f),
    new Vector3(-0.67f, 0.69f, -0.26f),
    new Vector3(-0.43f, 0.64f, -0.64f),
    new Vector3(-0.41f, 0.38f, -0.83f),
    new Vector3(-0.41f, 0.38f, -0.83f),
};
    List<Vector3> FH_TOPSPIN_ANGULARVELOCITY = new List<Vector3>
{
    new Vector3(0.25f, -0.01f, -0.03f),
    new Vector3(0.34f, -0.02f, -0.05f),
    new Vector3(0.34f, -0.02f, -0.05f),
    new Vector3(0.49f, 0.03f, -0.11f),
    new Vector3(0.5f, 0.03f, -0.12f),
    new Vector3(0.45f, -0.09f, -0.15f),
    new Vector3(0.13f, -0.12f, 0.1f),
    new Vector3(-0.17f, -0.06f, 0.28f),
    new Vector3(-0.17f, -0.06f, 0.28f),
    new Vector3(-0.18f, -0.15f, 0.39f),
    new Vector3(-0.21f, -0.2f, 0.51f),
    new Vector3(-0.21f, -0.2f, 0.51f),
    new Vector3(-0.21f, -0.2f, 0.54f),
    new Vector3(-0.58f, -0.09f, 0.73f),
    new Vector3(-0.85f, -0.11f, 0.72f),
    new Vector3(-0.97f, -0.18f, 0.69f),
    new Vector3(-0.97f, -0.18f, 0.69f),
    new Vector3(-0.99f, -0.04f, 0.57f),
    new Vector3(-0.69f, -0.2f, 0.28f),
    new Vector3(-0.19f, -0.09f, 0.08f),
    new Vector3(-0.19f, -0.09f, 0.08f),
};
    #endregion
    #region FH_FLAT
    List<Vector3> FH_FLAT_ACCELERATION = new List<Vector3>
{
    new Vector3(-0.88f, -0.38f, 0.27f),
    new Vector3(-0.86f, -0.38f, 0.34f),
    new Vector3(-0.87f, -0.37f, 0.32f),
    new Vector3(-0.87f, -0.37f, 0.32f),
    new Vector3(-0.69f, 0.22f, 0.69f),
    new Vector3(-0.42f, 0.62f, 0.66f),
    new Vector3(0.22f, 0.7f, 0.68f),
    new Vector3(0.22f, 0.7f, 0.68f),
    new Vector3(0.39f, 0.68f, 0.63f),
    new Vector3(-0.22f, 0.85f, 0.48f),
    new Vector3(-0.45f, 0.78f, 0.45f),
    new Vector3(-0.45f, 0.78f, 0.45f),
    new Vector3(-0.58f, 0.63f, 0.52f),
    new Vector3(-0.85f, 0.32f, 0.41f),
    new Vector3(-0.95f, -0.15f, 0.29f),
    new Vector3(-0.95f, -0.15f, 0.29f),
    new Vector3(-0.88f, -0.32f, 0.35f),
};

    List<Vector3> FH_FLAT_ANGULARVELOCITY = new List<Vector3>
{
    new Vector3(0.09f, -0.06f, -0.01f),
    new Vector3(0.09f, -0.06f, -0.02f),
    new Vector3(0.1f, -0.06f, -0.04f),
    new Vector3(0.1f, -0.06f, -0.04f),
    new Vector3(0.34f, -0.18f, -0.17f),
    new Vector3(0.44f, -0.13f, -0.22f),
    new Vector3(0.42f, -0.13f, -0.23f),
    new Vector3(0.42f, -0.13f, -0.23f),
    new Vector3(0.38f, -0.13f, -0.18f),
    new Vector3(-0.01f, -0.11f, 0.5f),
    new Vector3(0.08f, -0.22f, 0.56f),
    new Vector3(0.08f, -0.22f, 0.56f),
    new Vector3(0.1f, -0.22f, 0.57f),
    new Vector3(-0.02f, -0.07f, 0.48f),
    new Vector3(0.02f, -0.02f, 0.19f),
    new Vector3(0.02f, -0.02f, 0.19f),
    new Vector3(-0.01f, -0.03f, 0.1f),
};
    #endregion
    #region LOB
    List<Vector3> LOB_ACCELERATION = new List<Vector3>
{
    new Vector3(0.0f, 0.93f, 0.37f),
    new Vector3(-0.01f, 0.94f, 0.33f),
    new Vector3(0.05f, 0.92f, 0.39f),
    new Vector3(0.0f, 0.56f, 0.83f),
    new Vector3(0.15f, 0.46f, 0.87f),
    new Vector3(0.13f, 0.52f, 0.84f),
    new Vector3(-0.02f, 0.65f, 0.76f),
    new Vector3(-0.06f, 0.67f, 0.74f),
    new Vector3(-0.1f, 0.83f, 0.55f),
    new Vector3(-0.19f, 0.85f, 0.5f),
    new Vector3(-0.29f, 0.88f, 0.37f),
    new Vector3(-0.27f, 0.96f, -0.04f),
    new Vector3(-0.24f, 0.88f, -0.4f),
    new Vector3(-0.14f, 0.68f, -0.72f),
    new Vector3(-0.11f, 0.43f, -0.9f),
    new Vector3(-0.11f, 0.64f, -0.76f),
    new Vector3(-0.05f, 0.01f, -1.0f),
};
    List<Vector3> LOB_ANGULARVELOCITY = new List<Vector3>
{
    new Vector3(0.08f, -0.01f, -0.03f),
    new Vector3(0.09f, -0.02f, -0.03f),
    new Vector3(0.1f, -0.02f, -0.03f),
    new Vector3(0.07f, 0.05f, -0.03f),
    new Vector3(-0.05f, 0.04f, -0.02f),
    new Vector3(-0.15f, 0.05f, 0.02f),
    new Vector3(-0.24f, 0.03f, 0.03f),
    new Vector3(-0.29f, -0.01f, 0.03f),
    new Vector3(-0.39f, -0.12f, 0.06f),
    new Vector3(-0.46f, -0.16f, 0.11f),
    new Vector3(-0.48f, -0.17f, 0.1f),
    new Vector3(-0.49f, -0.14f, 0.11f),
    new Vector3(-0.41f, -0.11f, 0.09f),
    new Vector3(-0.36f, -0.08f, 0.09f),
    new Vector3(-0.31f, -0.02f, 0.1f),
    new Vector3(-0.1f, 0.05f, -0.01f),
    new Vector3(-0.04f, -0.02f, 0.0f),
};
    #endregion


    public List<List<Quaternion>> orientations { get; private set; }
    public List<List<Vector3>> accelerations { get; private set; }
    public List<List<Vector3>> angularVelocities { get; private set; }

    
    private static ControlGestures _instance;

    // Singleton
    public static ControlGestures Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ControlGestures();
            }
            return _instance;
        }
    }
    // Private constructor to prevent instantiation outside the class
    private ControlGestures()
    {
        orientations = new List<List<Quaternion>>();
        accelerations = new List<List<Vector3>>();
        angularVelocities = new List<List<Vector3>>();

        /* orientations.Add(STAB_ORIENTATION);
         orientations.Add(BACK_SLASH_ORIENTATION);
         orientations.Add(FORWARD_SLASH_ORIENTATION);
         orientations.Add(DUNK_ORIENTATION);
         orientations.Add(UPPERCUT_ORIENTATION);

         accelerations.Add(STAB_ACCELERATION);
         accelerations.Add(BACK_SLASH_ACCELERATION);
         accelerations.Add(FORWARD_SLASH_ACCELERATION);
         accelerations.Add(DUNK_ACCELERATION);
         accelerations.Add(UPPERCUT_ACCELERATION);

        orientations.Add(FH_TOPSPIN_ORIENTATION);
        orientations.Add(FH_FLAT_ORIENTATION);
        orientations.Add(LOB_ORIENTATION);
        orientations.Add(BH_TOPSPIN_ORIENTATION);*/
        //orientations.Add(BH_FLAT_ORIENTATION);


        accelerations.Add(FH_TOPSPIN_ACCELERATION);
        accelerations.Add(FH_FLAT_ACCELERATION);
        accelerations.Add(LOB_ACCELERATION);
        accelerations.Add(BH_TOPSPIN_ACCELERATION);
        accelerations.Add(BH_FLAT_ACCELERATION);
        accelerations.Add(FH_BACKSPIN_ACCELERATION);
        accelerations.Add(BH_BACKSPIN_ACCELERATION);

        
        angularVelocities.Add(FH_TOPSPIN_ANGULARVELOCITY);
        angularVelocities.Add(FH_FLAT_ANGULARVELOCITY);
        angularVelocities.Add(LOB_ANGULARVELOCITY);
        angularVelocities.Add(BH_TOPSPIN_ANGULARVELOCITY);
        angularVelocities.Add(BH_FLAT_ANGULARVELOCITY);
        angularVelocities.Add(FH_BACKSPIN_ANGULARVELOCITY);
        angularVelocities.Add(BH_BACKSPIN_ANGULARVELOCITY);

    }

    

}
