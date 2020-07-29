using System.Threading.Tasks;

namespace Enigma.Client.WPF.Services.Navigation
{
    public interface IActivable
    {
        Task ActivateAsync(object parameter);
    }
}
