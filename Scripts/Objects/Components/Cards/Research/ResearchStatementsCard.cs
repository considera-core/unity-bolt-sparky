using System.Collections.Generic;
using System.Linq;
using Libraries.Bolt.Extensions.UI.Cards;
using Libraries.Bolt.Objects;
using Libraries.Bolt.Objects.Components.Buttons.Images;
using Libraries.Bolt.Objects.Components.Table;
using Libraries.Game.Controllers;
using Libraries.Game.Models.Data.Research;
using Libraries.Sparky.Configs.Research;
using UnityEngine;
using UnityEngine.Serialization;

namespace Libraries.Sparky.Objects.Components.Cards.Research
{
    public class ResearchStatementsCard : CardExtension
    {
        // Fields
        // Private
        private Table _table;
        private ResearchStatementsConfig _config;
        [FormerlySerializedAs("actionOldButton")] [SerializeField] private BaseImageButton _actionButton;
        // Private Static
        private static readonly Color Valid = new(0.486f, 0.757f, 0);
        private static readonly Color Invalid = new(0.757f, 0.486f, 0);

        // Properties
        // Private
        private Table Table => _table ??= GetComponentInChildren<Table>(true);
        private List<ResearchTheoryStatementData> Statements => 
            DataController.CoreData.Research.Theories[_config.TheoryId].Statements;
        
        // Methods
        // Base
        public IObject Build(ResearchStatementsConfig config)
        {
            _config = config;
            _actionButton.SetText("Prove");
            Card.Build(title: $"Statements ({config.Statements.Length})"); // todo, title will be (completed/total)
            Table.Build(ResearchTheoriesConfig.ResearchStatementTableConfig(config));

            return this;
        }
        
        public override void OnStart() => 
            StartTable();

        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            UpdateTable();
            
            return true;
        }

        // Private
        private RectTransform GetAdjustedActionButtonRect()
        {
            var rect = _actionButton.Rect;
            rect.anchoredPosition = new Vector2(0, 0);
            rect.anchorMin = new Vector2(0.0f, 0.0f);
            rect.anchorMax = new Vector2(1.0f, 1.0f);
            rect.offsetMin = new Vector2(4, 4);
            rect.offsetMax = new Vector2(4, 4);
            return rect;
        }
        
        private void StartTable()
        {
            var row = 0;
            _config.Statements.ToList().ForEach(x =>
            {
                Table.UpdateBody(0, row, x.Statement);
                Table.UpdateBody(1, row, x.Description);
                Table.UpdateBody(3, row, GetAdjustedActionButtonRect());
                row++;
            });
            UpdateTable();
        }
        
        private void UpdateTable()
        {
            var row = 0;
            _config.Statements.ToList().ForEach(x =>
            {
                if (Statements[row].Proven) // can prove
                    _actionButton.SetColor(Valid);
                else
                    _actionButton.SetColor(Invalid);
                Table.UpdateBody(2, row,  Statements[row].Proven ? "<color=green>TRUE</color>" : "<color=red>FALSE</color>");
                Table.UpdateBody(3, row,  Statements[row].Proven ? "<color=green>TRUE</color>" : "<color=red>FALSE</color>");
                row++;
            });
            
        }
    }
}