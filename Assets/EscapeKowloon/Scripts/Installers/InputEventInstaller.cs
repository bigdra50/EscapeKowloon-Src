using EscapeKowloon.Scripts.Inputs;
using Zenject;

namespace EscapeKowloon.Scripts.Installers
{
    public class InputEventInstaller : MonoInstaller<InputEventInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IEscaperInputEventProvider>()
                .To<EscaperInputEventProvider>()
                .AsSingle();
        }
    }
}