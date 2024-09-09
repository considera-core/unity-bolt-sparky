using System.Linq;

namespace Libraries.Sparky.Configs.Research
{
    public static class ResearchConfigs
    {
        private static readonly ResearchStatementsConfig[] StatementsConfigs = 
        {
            new()
            {
                Statements = new[]
                {
                    new ResearchStatementConfig(
                        statement: "W", 
                        description: "You are welcomed!"),
                        /*
                        ,requirements: new Dictionary<ResourceType, double>
                        {
                            { ResourceType.Water, 1 }
                        })*/
                    new ResearchStatementConfig(
                        statement: "T1",
                        description: "Theory 1 is unlocked.",
                        dependencies: new[] { "W" })
                }
            },
            new()
            {
                // todo, maybe turn statements into a dictionary with statement as the key
                Statements = new[]
                {
                    new ResearchStatementConfig(
                        statement: "W", // do we want an enum for these? one per theory? or all in one?
                        description: "Water is unlocked."),
                    new ResearchStatementConfig(
                        statement: "A",
                        description: "Extaction is automated.",
                        dependencies: new[] { "D", "W" }),
                    new ResearchStatementConfig(
                        statement: "B",
                        description: "Water is not clean.",
                        dependencies: new[] { "W" }),
                    new ResearchStatementConfig(
                        statement: "C",
                        description: "There is a way to store extract."),
                    new ResearchStatementConfig(
                        statement: "D",
                        description: "Water is extractable."),
                    new ResearchStatementConfig(
                        statement: "SRC",
                        description: "Water Sources are unlocked.",
                        dependencies: new[] { "W", "A", "C", "D" }),
                    new ResearchStatementConfig(
                        statement: "WEX",
                        description: "Water can be extracted.",
                        dependencies: new[] { "SRC", "B" }),
                    new ResearchStatementConfig(
                        statement: "PEX",
                        description: "Polluted Water is extracted as a byproduct.",
                        dependencies: new[] { "SRC", "B" }),
                    new ResearchStatementConfig(
                        statement: "T2",
                        description: "Theory 2 is unlocked.",
                        dependencies: new[] { "SRC", "B" })
                }
            }
        };
        
        private static readonly ResearchTheoremsConfig[] TheoremsConfigs = 
        {
            new()
            {
                Theorems = new[]
                {
                    new ResearchTheoremConfig(
                        logic: "W -> T1", 
                        translation: "If W, then T1",
                        statements: new[] {"W", "T1"})
                }
            },
            new()
            {
                Theorems = new[]
                {
                    new ResearchTheoremConfig(
                        logic: "Z = D <-> W", 
                        translation: "Z is: D if and only if W.",
                        statements: new[] {"D", "W"}), // note, this is true by default since D and W are both false.
                    new ResearchTheoremConfig(
                        logic: "Z -> A", 
                        translation: "If Z, then A.",
                        statements: new[] {"D", "W", "A"}),
                    new ResearchTheoremConfig(
                        logic: "Y = (Z -> A) & C", 
                        translation: "Y is: If Z, then A; and C.",
                        statements: new[] {"D", "W", "A", "C"}),
                    new ResearchTheoremConfig(
                        logic: "Y -> SRC", 
                        translation: "If Y, then SRC.",
                        statements: new[] {"D", "W", "A", "C", "SRC"}),
                    new ResearchTheoremConfig(
                        logic: "X = WEX & PEX & T2", 
                        translation: "X is: WEX, PEX, and T2.",
                        statements: new[] {"D", "W", "A", "C", "SRC", "B", "WEX", "PEX", "T2"}),
                    new ResearchTheoremConfig(
                        logic: "SRC & B -> X", 
                        translation: "If SRC and B, then X.",
                        statements: new[] {"D", "W", "A", "C", "SRC", "B", "WEX", "PEX", "T2"}),
                    new ResearchTheoremConfig(
                        logic: "SRC & !B -> WEX", 
                        translation: "If SRC and not B, then WEX.",
                        statements: new[] {"D", "W", "A", "C", "SRC", "B", "WEX"}), // unlocks pure water
                }
            }
        };
        
        public static readonly ResearchTheoriesConfig[] TheoriesConfigs = 
        {
            new()
            {
                TheoryId = 0,
                Name = "T0: Intro",
                StatementsConfig = StatementsConfigs[0],
                TheoremsConfig = TheoremsConfigs[0],
                DescriptionConfig = new ResearchDescriptionConfig(description: @"
Welcome to Idle Research 2! Theories in this page allows you to learn more about the environment you are in.

Theories are a group of statements where some can already be assumed to be true, and some that you need to prove based on provided requirements.

Think of it like cooking a nice pot of soup; you need ingrediants such as veggies, but also ingrediants for making the broth!

To get started with your first theory, you need to prove W in order to prove T1. W has no requirements, so you can hit that Prove W button! Basically, when it has been proven, the statement is true.

To unlock the first real theory, you need to prove T1, which requires W to be true.

<color=green>Confused? Just make sure every statement's Proven is TRUE!"),
                ProvingConfig = new ResearchProvingConfig()
            },
            new()
            {
                TheoryId = 1,
                Name = "T1: Water",
                StatementsConfig = StatementsConfigs[1],
                TheoremsConfig = TheoremsConfigs[1],
                DescriptionConfig = new ResearchDescriptionConfig(description: @"
Combined Theorems:
((D <-> W) -> A) & C -> SRC
SRC & B -> WEX & PEX & T2
SRC & !B -> WEX"),
                ProvingConfig = new ResearchProvingConfig()
            }
        };

        public static void AssignIds()
        {
            var a = 0;
            TheoriesConfigs
                .ToList()
                .ForEach(x =>
                {
                    var b = 0;
                    var c = 0;
                    x.TheoryId = a++;
                    x.StatementsConfig.TheoryId = x.TheoryId;
                    x.TheoremsConfig.TheoryId = x.TheoryId;
                    
                    x.StatementsConfig.Statements
                        .ToList()
                        .ForEach(y =>
                        {
                            y.TheoryId = x.TheoryId;
                            y.StatementId = b++;
                        });
                    
                    x.TheoremsConfig.Theorems
                        .ToList()
                        .ForEach(y =>
                        {
                            y.TheoryId = x.TheoryId;
                            y.TheoremId = c++;
                            y.AssignStatements(x.StatementsConfig.Statements
                                .Where(z => y.Statements.Contains(z.Statement))
                                .ToList());
                        });
                });
        }
    }
}