using Libraries.Game.Controllers;
using UnityEngine;

namespace Libraries.Sparky
{
    [DisallowMultipleComponent]
    public class SparkyScaffold : MonoBehaviour
    {
        // Fields
        // Private
        private SparkySideNav _sparkySideNav;
        private SparkyPages _sparkyPages;
        private SparkyHeader _sparkyHeader;
        [SerializeField] private GameObject _sparkyBody;

        // Properties
        private SparkySideNav SparkySideNav => 
            _sparkySideNav ??= GetComponentInChildren<SparkySideNav>(true);

        private SparkyPages SparkyPages =>
            _sparkyPages ??= GetComponentInChildren<SparkyPages>(true);

        private SparkyHeader SparkyHeader =>
            _sparkyHeader ??= GetComponentInChildren<SparkyHeader>(true);

        // Methods
        // Private Methods
        private void Build()
        {
            GameController.OnStart();
            SparkySideNav.Build(SparkyPages);
            SparkyPages.Build();
            SparkyHeader.Build();
        }
        
        // Public Methods
        public void Start()
        {
            Build();
            
            SparkySideNav.OnStart();
            SparkyPages.OnStart();
            SparkyHeader.OnStart();
        }

        public void Update()
        {
            if (!DataController.DataLoaded) return;
            
            SparkySideNav.OnUpdate();
            SparkyPages.OnUpdate();
            
            SparkySideNav.OnRun();
            SparkyPages.OnRun();
        }
    }
}