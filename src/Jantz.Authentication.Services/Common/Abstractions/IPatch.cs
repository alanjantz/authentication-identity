namespace Jantz.Authentication.Services.Common.Abstractions
{
    public interface IPatch
    {
        bool CanApply();
        void Apply();
    }
}
