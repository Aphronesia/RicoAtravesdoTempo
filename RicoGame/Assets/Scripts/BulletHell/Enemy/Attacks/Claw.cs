using UnityEngine;

namespace BulletHell.Enemy.Attacks
{
    public class Claw : AttacksSystem
    {
        [Header("Configurações da Garra")]
        [SerializeField, 
         Tooltip("Delay para desaparecer")] 
        private float delay;
        
        [SerializeField] 
        private Vector3[] positions;
        
        [Header("Configurações do Arranho")] 
        [SerializeField, 
         Tooltip("prefab do arranho deixado pela garra")] 
        GameObject scratch;
        
        private void Start()
        {
            StartSelfDestruct(delay);
            RandomPosition();
            SpawnScratch();
        }

        private void RandomPosition()
        {
            int index = Random.Range(0, 3);
            transform.position = positions[index];
        }
        
        /// <summary>
        /// Garra vira filha do arranho para não apagar o arranho junto em StartSelfDestruct.
        /// </summary>
        private void SpawnScratch()
        {
            scratch = Instantiate(scratch, transform.position, Quaternion.identity);
            transform.SetParent(scratch.transform);
        }
    }
}
