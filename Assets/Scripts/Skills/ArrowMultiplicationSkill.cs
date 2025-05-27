using UnityEngine;
using System.Collections;

namespace Archero
{
    public class ArrowMultiplicationSkill : ISkill
    {
        public bool IsActive { get; private set; } = false;
        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        private int _arrowCount = 2;

        public void Apply(Projectile projectile, bool rageMode = false)
        {
            if (!IsActive) return;

            int _count = rageMode ? _arrowCount * 2 : _arrowCount;
            Vector3 _baseVelocity = projectile.GetVelocity();
            Vector3 _spawnPosition = projectile.transform.position;

            projectile.StartCoroutine(SpawnAdditionalArrows(projectile, _baseVelocity, _spawnPosition, _count, rageMode));
        }

        private IEnumerator SpawnAdditionalArrows(Projectile original, Vector3 baseVelocity, Vector3 basePosition, int count, bool rageMode)
        {
            float _delayBetweenArrows = 0.1f;
            float _spreadOffset = 0.5f;

            for (int i = 1; i < count; i++)
            {
                yield return new WaitForSeconds(_delayBetweenArrows);

                GameObject clone = Pool.Instance.GetObject(PoolType.Projectile);

                Vector3 offset = original.transform.right * (i * _spreadOffset - (count - 1) * _spreadOffset / 2);
                clone.transform.position = basePosition + offset;
                clone.transform.rotation = Quaternion.LookRotation(baseVelocity);

                var projClone = clone.GetComponent<Projectile>();
                projClone.Initialize(baseVelocity);
                SkillManager.Instance.ApplySkillsExcept<ArrowMultiplicationSkill>(projClone, rageMode);

            }
        }
    }
}
