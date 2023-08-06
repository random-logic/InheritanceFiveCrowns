using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{
    
    public class ShaderPropAnimator : MonoBehaviour
    {

        private Renderer _mRenderer;
        private Material _mMaterial;

        public AnimationCurve GlowCurve;

        public float MFrame;

        void Awake()
        {
            // Cache a reference to object's renderer
            _mRenderer = GetComponent<Renderer>();

            // Cache a reference to object's material and create an instance by doing so.
            _mMaterial = _mRenderer.material;
        }

        void Start()
        {
            StartCoroutine(AnimateProperties());
        }

        IEnumerator AnimateProperties()
        {
            //float lightAngle;
            float glowPower;
            MFrame = Random.Range(0f, 1f);

            while (true)
            {
                //lightAngle = (m_Material.GetFloat(ShaderPropertyIDs.ID_LightAngle) + Time.deltaTime) % 6.2831853f;
                //m_Material.SetFloat(ShaderPropertyIDs.ID_LightAngle, lightAngle);

                glowPower = GlowCurve.Evaluate(MFrame);
                _mMaterial.SetFloat(ShaderUtilities.ID_GlowPower, glowPower);

                MFrame += Time.deltaTime * Random.Range(0.2f, 0.3f);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
