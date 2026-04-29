using Reqnroll;
using Reqnroll.BoDi;

namespace Despesas.BddTests.Support;

[Binding]
public class Hooks(IObjectContainer container)
{
    [BeforeScenario]
    public void StartApi()
    {
        var factory = new DespesasApiFactory();
        var client = factory.CreateClient();

        container.RegisterInstanceAs(factory);
        container.RegisterInstanceAs(client);
    }

    [AfterScenario]
    public void StopApi()
    {
        var factory = container.Resolve<DespesasApiFactory>();
        factory.Dispose();
    }
}
