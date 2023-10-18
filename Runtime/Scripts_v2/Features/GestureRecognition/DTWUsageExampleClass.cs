using System;
using System.Collections;
//using DTWot;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class DTWUsageExampleClass : MonoBehaviour
{

    #region Gestures declaration


    ///////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////// TENNIS ////////////////////////////////////////////////

    #region Deprecared orientations

    List<Quaternion> FH_TOPSPIN_ORIENTATION = new List<Quaternion>
{
    new Quaternion(-0.3006f, -0.51611f, 0.69873f, -0.3938f),
    new Quaternion(-0.31573f, -0.49323f, 0.71503f, -0.38184f),
    new Quaternion(-0.43237f, -0.28912f, 0.81799f, -0.24567f),
    new Quaternion(-0.43237f, -0.28912f, 0.81799f, -0.24567f),
    new Quaternion(-0.48224f, -0.05713f, 0.87036f, -0.08148f),
    new Quaternion(-0.50586f, 0.04761f, 0.86133f, -0.00079f),
    new Quaternion(-0.51068f, 0.06604f, 0.85712f, 0.01392f),
    new Quaternion(-0.52612f, 0.1947f, 0.81927f, 0.11853f),
    new Quaternion(-0.48938f, 0.24341f, 0.82434f, 0.14752f),
    new Quaternion(-0.46545f, 0.25342f, 0.84784f, 0.01666f),
    new Quaternion(-0.43195f, 0.23785f, 0.86389f, -0.10272f),
    new Quaternion(-0.37659f, 0.2074f, 0.86841f, -0.24719f),
    new Quaternion(-0.27594f, 0.16522f, 0.83917f, -0.4386f),
    new Quaternion(-0.14038f, 0.10065f, 0.75671f, -0.63055f),
    new Quaternion(0.01172f, 0.00159f, 0.62891f, -0.7774f),
    new Quaternion(0.29181f, -0.15393f, 0.36481f, -0.87067f),
    new Quaternion(0.55414f, -0.25531f, 0.09436f, -0.78668f),
    new Quaternion(0.80273f, -0.25336f, -0.28357f, -0.45935f),
    new Quaternion(0.86993f, -0.18481f, -0.41425f, -0.19354f),
    new Quaternion(0.86993f, -0.18481f, -0.41425f, -0.19354f),
};

    List<Quaternion> FH_FLAT_ORIENTATION = new List<Quaternion>

{
    new Quaternion(-0.43213f, -0.47662f, 0.69861f, -0.31317f),
    new Quaternion(-0.42511f, -0.40924f, 0.76642f, -0.25372f),
    new Quaternion(-0.39465f, -0.30603f, 0.84937f, -0.17096f),
    new Quaternion(-0.35742f, -0.21405f, 0.90387f, -0.09711f),
    new Quaternion(-0.30823f, -0.09393f, 0.94666f, 0.0047f),
    new Quaternion(-0.24023f, 0.02863f, 0.96722f, 0.07715f),
    new Quaternion(-0.22113f, 0.09644f, 0.97046f, 0.0f),
    new Quaternion(-0.19983f, 0.11371f, 0.9671f, -0.10919f),
    new Quaternion(-0.14276f, 0.14014f, 0.93555f, -0.29102f),
    new Quaternion(-0.00653f, 0.25226f, 0.80121f, -0.5426f),
    new Quaternion(0.03955f, 0.26398f, 0.7066f, -0.65533f),
    new Quaternion(0.0918f, 0.26837f, 0.60419f, -0.74463f),
    new Quaternion(0.12305f, 0.25958f, 0.54236f, -0.78949f),
    new Quaternion(0.14313f, 0.2558f, 0.48438f, -0.82428f),
    new Quaternion(0.14722f, 0.23792f, 0.4624f, -0.84137f),
    new Quaternion(0.14722f, 0.23792f, 0.4624f, -0.84137f),
    new Quaternion(0.13947f, 0.23413f, 0.45233f, -0.84918f),
};

    List<Quaternion> LOB_ORIENTATION = new List<Quaternion>
{
    new Quaternion(-0.66675f, -0.22791f, -0.24335f, -0.66656f),
    new Quaternion(-0.67303f, -0.23822f, -0.23157f, -0.66083f),
    new Quaternion(-0.6745f, -0.24023f, -0.22626f, -0.6604f),
    new Quaternion(-0.6507f, -0.22144f, -0.18195f, -0.70319f),
    new Quaternion(-0.59167f, -0.18787f, -0.1842f, -0.76202f),
    new Quaternion(-0.49921f, -0.11829f, -0.17688f, -0.83997f),
    new Quaternion(-0.40399f, -0.04846f, -0.18225f, -0.89508f),
    new Quaternion(-0.27307f, 0.03925f, -0.19623f, -0.94092f),
    new Quaternion(-0.16858f, 0.08643f, -0.21411f, -0.95825f),
    new Quaternion(-0.05402f, 0.0954f, -0.23608f, -0.96552f),
    new Quaternion(0.00446f, 0.09863f, -0.24048f, -0.96564f),
    new Quaternion(0.02545f, 0.09967f, -0.24158f, -0.9649f),
};



    List<Quaternion> BH_FLAT_ORIENTATION = new List<Quaternion>
{
    new Quaternion(-0.13214f, -0.19104f, 0.93799f, -0.25739f),
    new Quaternion(-0.12079f, -0.17731f, 0.94055f, -0.26331f),
    new Quaternion(-0.12079f, -0.17731f, 0.94055f, -0.26331f),
    new Quaternion(-0.12079f, -0.17731f, 0.94055f, -0.26331f),
    new Quaternion(-0.18561f, 0.00232f, 0.96533f, -0.18335f),
    new Quaternion(-0.21375f, 0.07294f, 0.97064f, -0.08295f),
    new Quaternion(-0.21375f, 0.07294f, 0.97064f, -0.08295f),
    new Quaternion(-0.29797f, 0.10498f, 0.91608f, 0.24701f),
    new Quaternion(-0.3031f, -0.04901f, 0.68188f, 0.66388f),
};

    List<Quaternion> FH_BACKSPIN_ORIENTATION = new List<Quaternion>
{
    new Quaternion(0.52301f, 0.13354f, 0.31067f, -0.78235f),
    new Quaternion(0.56171f, 0.09467f, 0.34381f, -0.74652f),
    new Quaternion(0.57977f, 0.0755f, 0.36017f, -0.72699f),
    new Quaternion(0.63196f, 0.02106f, 0.42511f, -0.64764f),
    new Quaternion(0.66455f, 0.02484f, 0.46753f, -0.5824f),
    new Quaternion(0.67847f, 0.07751f, 0.49512f, -0.53717f),
    new Quaternion(0.66492f, 0.13837f, 0.50128f, -0.53619f),
    new Quaternion(0.63568f, 0.21399f, 0.50262f, -0.54541f),
    new Quaternion(0.55286f, 0.31616f, 0.49506f, -0.59106f),
    new Quaternion(0.41479f, 0.44275f, 0.43457f, -0.66565f),
    new Quaternion(0.31708f, 0.50586f, 0.36926f, -0.71222f),
    new Quaternion(-0.14026f, 0.53357f, 0.11798f, -0.82562f),
    new Quaternion(-0.17688f, 0.49335f, 0.0321f, -0.85101f),
    new Quaternion(-0.18433f, 0.49689f, 0.01929f, -0.84778f),
    new Quaternion(-0.26001f, 0.51648f, -0.00964f, -0.81586f),
    new Quaternion(-0.26001f, 0.51648f, -0.00964f, -0.81586f),
};

    List<Quaternion> BH_BACKSPIN_ORIENTATION = new List<Quaternion>
{
    new Quaternion(0.61359f, 0.27722f, -0.68835f, -0.26984f),
    new Quaternion(0.63831f, 0.34259f, -0.64838f, -0.23401f),
    new Quaternion(0.64392f, 0.35065f, -0.63837f, -0.23413f),
    new Quaternion(0.64844f, 0.34424f, -0.63196f, -0.24817f),
    new Quaternion(0.64386f, 0.32581f, -0.63489f, -0.27606f),
    new Quaternion(0.64386f, 0.32581f, -0.63489f, -0.27606f),
    new Quaternion(0.63409f, 0.28754f, -0.6438f, -0.31738f),
    new Quaternion(0.52008f, 0.05316f, -0.68866f, -0.50244f),
    new Quaternion(0.37634f, -0.12811f, -0.67072f, -0.62616f),
    new Quaternion(0.25433f, -0.25494f, -0.62457f, -0.69299f),
    new Quaternion(-0.10083f, -0.52008f, -0.38245f, -0.75702f),
    new Quaternion(-0.2348f, -0.5752f, -0.22467f, -0.75073f),
    new Quaternion(-0.24854f, -0.57709f, -0.21899f, -0.74652f),
    new Quaternion(-0.3006f, -0.5813f, -0.16174f, -0.73865f),
    new Quaternion(-0.3313f, -0.578f, -0.14935f, -0.73065f),
    new Quaternion(-0.3291f, -0.55835f, -0.13763f, -0.74896f),
};

    List<Quaternion> BH_TOPSPIN_ORIENTATION = new List<Quaternion>
{
    new Quaternion(-0.38324f, -0.09833f, -0.90845f, -0.13464f),
    new Quaternion(-0.39703f, -0.10883f, -0.9054f, -0.10394f),
    new Quaternion(-0.4057f, -0.11603f, -0.90338f, -0.07605f),
    new Quaternion(-0.44537f, -0.13715f, -0.8847f, -0.00922f),
    new Quaternion(-0.45477f, -0.14276f, -0.87903f, 0.00732f),
    new Quaternion(-0.45917f, -0.14612f, -0.8761f, 0.01605f),
    new Quaternion(-0.48212f, -0.1853f, -0.84918f, 0.11035f),
    new Quaternion(-0.47351f, -0.19189f, -0.849f, 0.13464f),
    new Quaternion(-0.4541f, -0.20648f, -0.85461f, 0.14417f),
    new Quaternion(-0.43469f, -0.21686f, -0.86206f, 0.14429f),
    new Quaternion(-0.41656f, -0.23224f, -0.87079f, 0.11932f),
    new Quaternion(-0.41656f, -0.23224f, -0.87079f, 0.11932f),
    new Quaternion(-0.36926f, -0.25494f, -0.89349f, 0.0174f),
    new Quaternion(-0.14056f, -0.11316f, -0.86591f, -0.46649f),
    new Quaternion(0.06091f, 0.16553f, -0.67255f, -0.71875f),
    new Quaternion(0.17773f, 0.41187f, -0.4436f, -0.77588f),
    new Quaternion(0.2868f, 0.53192f, -0.27167f, -0.74902f),
    new Quaternion(0.2868f, 0.53192f, -0.27167f, -0.74902f),
    new Quaternion(0.42609f, 0.49762f, -0.23578f, -0.71777f),
    new Quaternion(0.47003f, 0.42706f, -0.25739f, -0.72833f),
    new Quaternion(0.47076f, 0.3811f, -0.27307f, -0.74738f),
};
    #endregion

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
    List<Vector3> FH_TOPSPIN_ANGULARVELOCITY  = new List<Vector3>
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
    List<Vector3> LOB_ACCELERATION  = new List<Vector3>
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

    #endregion
    [SerializeField] TextMeshProUGUI foundGestureNameTxt;

    // Gesture recording flags
    private bool isTriggerButtonPressed = false;
    private bool hasLoggedData = true;

    private Quaternion consoleOrientation;
    private Vector3 consoleAcceleration;
    private Vector3 consoleAngularVelocity;

    private List<Vector3> accelerations = new List<Vector3>();
    private List<Vector3> angularVelocities = new List<Vector3>();
    private List<Quaternion> orientations = new List<Quaternion>();

    [SerializeField]
    public bool considerAcceleration = true;
    [SerializeField]
    public bool considerAngularVelocity = true;
    [SerializeField]
    public bool considerYaw = true;
    [SerializeField]
    public bool considerPitch = true;
    [SerializeField]
    public bool considerRoll = true;
    [SerializeField]
    private string LogPrefix = "DTW Recognition Results";

    private DTW4 dtw = DTW4.Instance;

    // Constants
    private readonly double RecognitionThreshold = 15;

    

   private void Start()
    {
        dtw.Initialize(considerAcceleration, considerAngularVelocity, considerYaw, considerPitch, considerRoll, LogPrefix);

        GestureData fh_topspin = new GestureData("FH_TOPSPIN");
        fh_topspin.Acceleration = FH_TOPSPIN_ACCELERATION;
        fh_topspin.AngularVelocity = FH_TOPSPIN_ANGULARVELOCITY;
        dtw.AddGesture(fh_topspin);

        GestureData fh_flat = new GestureData("FH_FLAT");
        fh_flat.Acceleration = FH_FLAT_ACCELERATION;
        fh_flat.AngularVelocity = FH_FLAT_ANGULARVELOCITY;
        dtw.AddGesture(fh_flat);

        GestureData lob = new GestureData("LOB");
        lob.Acceleration = LOB_ACCELERATION;
        lob.AngularVelocity = LOB_ANGULARVELOCITY;
        dtw.AddGesture(lob);

        GestureData bh_flat = new GestureData("BH_FLAT");
        bh_flat.Acceleration = BH_FLAT_ACCELERATION;
        bh_flat.AngularVelocity = BH_FLAT_ANGULARVELOCITY;
        dtw.AddGesture(bh_flat);

        GestureData bh_topspin = new GestureData("BH_TOPSPIN");
        bh_topspin.Acceleration = BH_TOPSPIN_ACCELERATION;
        bh_topspin.AngularVelocity = BH_TOPSPIN_ANGULARVELOCITY;
        dtw.AddGesture(bh_topspin);

        GestureData fh_backspin = new GestureData("FH_BACKSPIN");
        fh_backspin.Acceleration = FH_BACKSPIN_ACCELERATION;
        fh_backspin.AngularVelocity = FH_BACKSPIN_ANGULARVELOCITY;
        dtw.AddGesture(fh_backspin);

        GestureData bh_backspin = new GestureData("BH_BACKSPIN");
        bh_backspin.Acceleration = BH_BACKSPIN_ACCELERATION;
        bh_backspin.AngularVelocity = BH_BACKSPIN_ANGULARVELOCITY;
        dtw.AddGesture(bh_backspin);

        /*GestureData backSlashGesture = new GestureData("BACK_SLASH_GESTURE");
        backSlashGesture.Acceleration = ControlGestures.BACK_SLASH_ACCELERATION;
        backSlashGesture.AngularVelocity = ControlGestures.BACK_SLASH_ANGULAR_VELOCITY;
        dtw.AddGesture(backSlashGesture);

        GestureData stabGesture = new GestureData("STAB_GESTURE");
        stabGesture.Acceleration = ControlGestures.STAB_ACCELERATION;
        stabGesture.AngularVelocity = ControlGestures.STAB_ANGULAR_VELOCITY;
        dtw.AddGesture(stabGesture);

        GestureData forwardSlashGesture = new GestureData("FORWARD_SLASH_GESTURE");
        forwardSlashGesture.Acceleration = ControlGestures.FORWARD_SLASH_ACCELERATION;
        forwardSlashGesture.AngularVelocity = ControlGestures.FORWARD_SLASH_ANGULAR_VELOCITY;
        dtw.AddGesture(forwardSlashGesture);

        GestureData dunkGesture = new GestureData("DUNK_GESTURE");
        dunkGesture.Acceleration = ControlGestures.DUNK_ACCELERATION;
        dunkGesture.AngularVelocity = ControlGestures.DUNK_ANGULAR_VELOCITY;
        dtw.AddGesture(dunkGesture);

        GestureData uppercutGesture = new GestureData("UPPERCUT_GESTURE");
        uppercutGesture.Acceleration = ControlGestures.UPPERCUT_ACCELERATION;
        uppercutGesture.AngularVelocity = ControlGestures.UPPERCUT_ANGULAR_VELOCITY;
        dtw.AddGesture(uppercutGesture);*/

        SubscribeToBLEEvents();
    }

    private void Update()
    {
        HandleGestureData();
        //CheckForKeyboardInput();
    }

    private void OnDestroy()
    {
        UnsubscribeFromBLEEvents();
    }

    private void HandleGestureData()
    {
        if (isTriggerButtonPressed)
        {
            ListenToGestureData();
        }
        else if (!hasLoggedData)
        {
            RecognitionResult result = dtw.RecognizeGesture(orientations, accelerations, angularVelocities, RecognitionThreshold);
            double distance;
            result.distancesToOthers.TryGetValue(result.gestureName, out distance);
            foundGestureNameTxt.SetText($"N: {result.gestureName} D: {distance}");
            dtw.LogGestureData(LogPrefix, accelerations, orientations, angularVelocities);
            InvokeAnimation(result.gestureName);
            ClearGestureData();
            hasLoggedData = true;
        }
    }

    // Events
    public event Action<string> OnGestureRecognized;
    private void InvokeAnimation(string recognizedGestureName)
    {
        OnGestureRecognized?.Invoke(recognizedGestureName);
    }

    private void ListenToGestureData()
    {
        consoleOrientation = UDUGetters.GetOrientation();
        consoleAcceleration = UDUGetters.GetAcceleration().normalized;
        consoleAngularVelocity = UDUGetters.GetAngularVelocity();
        accelerations.Add(consoleAcceleration);
        orientations.Add(consoleOrientation);
        angularVelocities.Add(consoleAngularVelocity);
        hasLoggedData = false;
    }

    private void ClearGestureData()
    {
        accelerations.Clear();
        orientations.Clear();
        angularVelocities.Clear();
    }

    private void OnConsoleTriggerButtonPress()
    {
        isTriggerButtonPressed = true;
    }

    private void OnConsoleTriggerButtonRelease()
    {
        isTriggerButtonPressed = false;
    }

    private void SubscribeToBLEEvents()
    {
        EventsSystemHandler.Instance.onTriggerPressTriggerButton += OnConsoleTriggerButtonPress;
        EventsSystemHandler.Instance.onTriggerReleaseTriggerButton += OnConsoleTriggerButtonRelease;
    }

    private void UnsubscribeFromBLEEvents()
    {
        EventsSystemHandler.Instance.onTriggerPressTriggerButton -= OnConsoleTriggerButtonPress;
        EventsSystemHandler.Instance.onTriggerReleaseTriggerButton -= OnConsoleTriggerButtonRelease;
    }

    // FOR TESTING ONLY
    // Code just to test gesture animations with keyboard
   /* private Dictionary<KeyCode, string> keyToGestureMapping = new Dictionary<KeyCode, string>
    {
        {KeyCode.Alpha1, "BACK_SLASH_GESTURE"},
        {KeyCode.Alpha2, "STAB_GESTURE"},
        {KeyCode.Alpha3, "FORWARD_SLASH_GESTURE"},
        {KeyCode.Alpha4, "DUNK_GESTURE"},
        {KeyCode.Alpha5, "UPPERCUT_GESTURE"}
    };

    private void CheckForKeyboardInput()
    {

        foreach (var keyGesturePair in keyToGestureMapping)
        {
            if (Input.GetKeyDown(keyGesturePair.Key))
            {
                recognizedGestureName = keyGesturePair.Value;
                OnGestureRecognized?.Invoke(recognizedGestureName);
                recognizedGestureName = "";  // reset the recognized gesture
                break;  // Exit once a key is detected
            }
        }
    }*/

}
