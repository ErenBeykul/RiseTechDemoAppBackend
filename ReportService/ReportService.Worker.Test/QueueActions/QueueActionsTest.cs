using Lamar;
using RiseTechDemoApp.Domain.Enums;
using ReportService.Worker.Test.DI;

namespace ReportService.Worker.Test
{
    public class QueueActionsTest: IClassFixture<LamarContainerFactory>
    {
        readonly IContainer _container;
        readonly IQueueActions _actions;

        public QueueActionsTest(LamarContainerFactory factory)
        {
            _container = factory.Container;
            _actions = _container.GetInstance<IQueueActions>();
        }

        [Fact]
        public void Send()
        {
            QueueActions.Send(QueueName.Reports.ToString(), new Guid().ToString());
        }

        [Fact]
        public void GenerateReportIfAsked()
        {
            _actions.GenerateReportIfAsked();
        }
    }
}