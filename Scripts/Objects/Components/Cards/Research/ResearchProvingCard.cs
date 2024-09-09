using System.Collections.Generic;
using System.Linq;
using Libraries.Bolt.Configs.Components.Paginator;
using Libraries.Bolt.Extensions.UI.Cards;
using Libraries.Sparky.Configs.Research;
using Libraries.Sparky.Objects.Components.Paginators;
using Libraries.Sparky.Objects.Groups.Research;
using UnityEngine;

namespace Libraries.Sparky.Objects.Components.Cards.Theories
{
    public class ResearchProvingCard : CardExtension
    {
        // Constants
        // Private
        private const int PAGE_SIZE = 2;
        
        // Fields
        // Private
        private ResearchTheoriesConfig _config;
        private List<ResearchProvingGroup> _provingGroups;
        // Private Serialized
        [SerializeField] private Transform _provingGroupsContainer;
        [SerializeField] private ResearchProvingGroup _provingGroupPrefab;
        [SerializeField] private ResearchProvingPaginator _paginator;
        
        // Properties
        // Private
        private IEnumerable<ResearchStatementConfig> _statements => 
            _config.StatementsConfig.Statements;
        
        // Methods
        // Base
        public void Build(ResearchTheoriesConfig config)
        {
            _config = config;
            _provingGroups?.ToList().ForEach(x => Destroy(x.gameObject));
            _provingGroups = new List<ResearchProvingGroup>();
            _statements.ToList().ForEach(x =>
            {
                var group = Instantiate(_provingGroupPrefab, _provingGroupsContainer);
                group.Build(x);
                _provingGroups.Add(group);
            });
            _paginator.Build(new PaginatorConfig<ResearchProvingGroup>(PAGE_SIZE, _provingGroups));
        }

        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            _provingGroups.ForEach(x => x.OnUpdate());

            return true;
        }

        public override void OnRun() =>
            _provingGroups.ForEach(x => x.OnRun());
    }
}