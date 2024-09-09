using System.Collections.Generic;
using Libraries.Bolt.Configs.Components.Chip;
using Libraries.Bolt.Extensions.UI.Cards;
using Libraries.Bolt.Objects;
using Libraries.Bolt.Objects.Components.Chip;
using UnityEngine;

namespace Libraries.Sparky.Objects.Components.Cards.Other
{
    public class RelevantResourcesCard : CardExtension
    {
        private List<ChipComponent> _resources;
        [SerializeField] private ChipComponent _chipPrefab;
        [SerializeField] private Transform _prefabTarget;
        
        public IObject Build(List<ChipConfig> configs)
        {
            _resources = new List<ChipComponent>();
            foreach (var config in configs)
            {
                var resource = Instantiate(_chipPrefab, _prefabTarget).GetComponent<ChipComponent>();
                resource.Build(config);
                _resources.Add(resource);
            }

            return this;
        }
        
        public void SetLabel(int index, string level) =>
            _resources[index].SetText(level);
    }
}