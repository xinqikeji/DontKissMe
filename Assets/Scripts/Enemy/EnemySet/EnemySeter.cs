using Dreamteck.Splines;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy.EnemySet
{
    [RequireComponent(typeof(SplineComputer))]
    public class EnemySeter : MonoBehaviour
    {
        [SerializeField] private Transform _enemyCase;
        [SerializeField] private SplineComputer _splineComputer;
        [SerializeField] private float _offsetX = 5;

        private Transform[] _transforms;
        private List<SplinePositioner> _splinePositioners;

        public float Distant { set; get; }
        public bool SetEvenly { set; get; }

        public void Init()
        {
            if (_enemyCase == null)
                return;
            _transforms = new Transform[_enemyCase.childCount];

            for (int i = 0; i < _enemyCase.childCount; i++)
                _transforms[i] = _enemyCase.GetChild(i);
            _splinePositioners = new List<SplinePositioner>();

            for (int i = 0; i < _transforms.Length; i++)
            {
                if (_transforms[i].TryGetComponent(out SplinePositioner splinePositioner))
                {
                    _splinePositioners.Add(splinePositioner);
                    splinePositioner.spline = _splineComputer;
                }
            }
        }

        public void SetPosition()
        {
            float distant = CountPersentDistant(_transforms.Length);

            for (int i = 0; i < _splinePositioners.Count; i++)
            {
                float tempDistant = (SetEvenly ? Distant : distant) * i;
                if (tempDistant == 0)
                    tempDistant = 0.1f;

                _splinePositioners[i].SetDistance(tempDistant);
            }
        }

        public void SetOffcet()
        {
            for (int i = 0; i < _splinePositioners.Count; i++)
            {
                int plusOrMinus = (Random.Range(0, 2) * 2) - 1;
                _splinePositioners[i].motion.offset = new Vector2(_offsetX * plusOrMinus, 0);
            }
        }
#if UNITY_EDITOR
        public void Save() => _splinePositioners.ForEach(x => UnityEditor.EditorUtility.SetDirty(x));
#endif

        private float CountPersentDistant(int count) => _splineComputer.CalculateLength() / count;

        private void OnValidate() => Init();

    }
}