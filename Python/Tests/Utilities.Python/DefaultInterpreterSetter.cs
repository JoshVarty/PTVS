using System;
using Microsoft.PythonTools;
using Microsoft.PythonTools.Interpreter;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudioTools.VSTestHost;

namespace TestUtilities.UI {
    public class DefaultInterpreterSetter : IDisposable {
        private readonly IComponentModel _model;
        public readonly IPythonInterpreterFactory OriginalInterpreter;
        private bool _isDisposed;

        public DefaultInterpreterSetter(IPythonInterpreterFactory factory, IServiceProvider site = null) {
            Assert.IsNotNull(factory, "Cannot set default to null");
            _model = (IComponentModel)(site ?? VSTestContext.ServiceProvider).GetService(typeof(SComponentModel));
            var interpreterService = _model.GetService<IInterpreterOptionsService>();
            Assert.IsNotNull(interpreterService);

            OriginalInterpreter = interpreterService.DefaultInterpreter;
            CurrentDefault = factory;
            interpreterService.DefaultInterpreter = factory;
        }

        public void SetDefault(IPythonInterpreterFactory factory) {
            Assert.IsNotNull(factory, "Cannot set default to null");
            var interpreterService = _model.GetService<IInterpreterOptionsService>();
            Assert.IsNotNull(interpreterService);

            CurrentDefault = factory;
            interpreterService.DefaultInterpreter = factory;
        }

        public IPythonInterpreterFactory CurrentDefault { get; private set; }


        public void Dispose() {
            if (!_isDisposed) {
                _isDisposed = true;

                var model = (IComponentModel)VSTestContext.ServiceProvider.GetService(typeof(SComponentModel));
                var interpreterService = model.GetService<IInterpreterOptionsService>();
                Assert.IsNotNull(interpreterService);
                interpreterService.DefaultInterpreter = OriginalInterpreter;
            }
        }
    }
}
