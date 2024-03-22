using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotJEM.Json.Validation.Results;
using NUnit.Framework;

namespace DotJEM.Json.Validation.Test.Results
{
    public class OptimizeTest
    {
        [Test]
        public void Optimize_NestedOrResultsWithFieldResults_FlattensToSingleLevelWith21FieldResults()
        {
            OrResult or = new OrResult(
                new OrResult(
                    new FieldResult(null, new FakeResult(), new FakeResult()),
                    new FieldResult(null, new FakeResult(), new FakeResult()),
                    new FieldResult(null, new FakeResult(), new FakeResult())
                ),
                new OrResult(
                    new OrResult(
                        new FieldResult(null, new FakeResult(), new FakeResult()),
                        new FieldResult(null, new FakeResult(), new FakeResult()),
                        new FieldResult(null, new FakeResult(), new FakeResult())
                    ),
                    new OrResult(
                        new OrResult(
                            new FieldResult(null, new FakeResult(), new FakeResult()),
                            new FieldResult(null, new FakeResult(), new FakeResult()),
                            new FieldResult(null, new FakeResult(), new FakeResult())
                        ),
                        new OrResult(
                            new FieldResult(null, new FakeResult(), new FakeResult()),
                            new FieldResult(null, new FakeResult(), new FakeResult()),
                            new FieldResult(null, new FakeResult(), new FakeResult())
                        ),
                        new OrResult(
                            new FieldResult(null, new FakeResult(), new FakeResult()),
                            new FieldResult(null, new FakeResult(), new FakeResult()),
                            new FieldResult(null, new FakeResult(), new FakeResult())
                        )
                    ),
                    new OrResult(
                        new FieldResult(null, new FakeResult(), new FakeResult()),
                        new FieldResult(null, new FakeResult(), new FakeResult()),
                        new FieldResult(null, new FakeResult(), new FakeResult())
                    )
                ),
                new OrResult(
                    new FieldResult(null, new FakeResult(), new FakeResult()),
                    new FieldResult(null, new FakeResult(), new FakeResult()),
                    new FieldResult(null, new FakeResult(), new FakeResult())
                )
            );

            OrResult result = (OrResult)or.Optimize();
            Assert.That(result.Results, Has.Count.EqualTo(21) & Has.All.Matches(Is.TypeOf<FieldResult>()));
        }

        [Test]
        public void Optimize_NestedAndResultsWithFieldResults_FlattensToSingleLevelWith21FieldResults()
        {
            AndResult or = new AndResult(
                new AndResult(
                    new FieldResult(null, new FakeResult(), new FakeResult()),
                    new FieldResult(null, new FakeResult(), new FakeResult()),
                    new FieldResult(null, new FakeResult(), new FakeResult())
                ),
                new AndResult(
                    new AndResult(
                        new FieldResult(null, new FakeResult(), new FakeResult()),
                        new FieldResult(null, new FakeResult(), new FakeResult()),
                        new FieldResult(null, new FakeResult(), new FakeResult())
                    ),
                    new AndResult(
                        new AndResult(
                            new FieldResult(null, new FakeResult(), new FakeResult()),
                            new FieldResult(null, new FakeResult(), new FakeResult()),
                            new FieldResult(null, new FakeResult(), new FakeResult())
                        ),
                        new AndResult(
                            new FieldResult(null, new FakeResult(), new FakeResult()),
                            new FieldResult(null, new FakeResult(), new FakeResult()),
                            new FieldResult(null, new FakeResult(), new FakeResult())
                        ),
                        new AndResult(
                            new FieldResult(null, new FakeResult(), new FakeResult()),
                            new FieldResult(null, new FakeResult(), new FakeResult()),
                            new FieldResult(null, new FakeResult(), new FakeResult())
                        )
                    ),
                    new AndResult(
                        new FieldResult(null, new FakeResult(), new FakeResult()),
                        new FieldResult(null, new FakeResult(), new FakeResult()),
                        new FieldResult(null, new FakeResult(), new FakeResult())
                    )
                ),
                new AndResult(
                    new FieldResult(null, new FakeResult(), new FakeResult()),
                    new FieldResult(null, new FakeResult(), new FakeResult()),
                    new FieldResult(null, new FakeResult(), new FakeResult())
                )
            );

            AndResult result = (AndResult)or.Optimize();
            Assert.That(result.Results, Has.Count.EqualTo(21) & Has.All.Matches(Is.TypeOf<FieldResult>()));
        }

        [Test]
        public void Optimize_MixedNestedAndOrResultsWithFieldResults_FlattensToSingleLevelWith21FieldResults()
        {
            AndResult or = new AndResult(
                new AndResult(
                    new FieldResult(null, new FakeResult(), new FakeResult()),
                    new FieldResult(null, new FakeResult(), new FakeResult()),
                    new FieldResult(null, new FakeResult(), new FakeResult())
                ),
                new AndResult(
                    new OrResult(
                        new FieldResult(null, new FakeResult(), new FakeResult()),
                        new FieldResult(null, new FakeResult(), new FakeResult()),
                        new FieldResult(null, new FakeResult(), new FakeResult())
                    ),
                    new AndResult(
                        new AndResult(
                            new FieldResult(null, new FakeResult(), new FakeResult()),
                            new FieldResult(null, new FakeResult(), new FakeResult()),
                            new FieldResult(null, new FakeResult(), new FakeResult())
                        ),
                        new AndResult(
                            new FieldResult(null, new FakeResult(), new FakeResult()),
                            new FieldResult(null, new FakeResult(), new FakeResult()),
                            new FieldResult(null, new FakeResult(), new FakeResult())
                        ),
                        new AndResult(
                            new FieldResult(null, new FakeResult(), new FakeResult()),
                            new FieldResult(null, new FakeResult(), new FakeResult()),
                            new FieldResult(null, new FakeResult(), new FakeResult())
                        )
                    ),
                    new OrResult(
                        new FieldResult(null, new FakeResult(), new FakeResult()),
                        new FieldResult(null, new FakeResult(), new FakeResult()),
                        new FieldResult(null, new FakeResult(), new FakeResult())
                    )
                ),
                new AndResult(
                    new FieldResult(null, new FakeResult(), new FakeResult()),
                    new FieldResult(null, new FakeResult(), new FakeResult()),
                    new FieldResult(null, new FakeResult(), new FakeResult())
                )
            );

            AndResult result = (AndResult)or.Optimize();
            Assert.That(result.Results, Has.Count.EqualTo(17) & Has.All
                .Matches(Is.TypeOf<FieldResult>() | Is.TypeOf<OrResult>()));
        }
    }

    public class FakeResult : Result
    {
        public override bool IsValid => true;
    }
}
