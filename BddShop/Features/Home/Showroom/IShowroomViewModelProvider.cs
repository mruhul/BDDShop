namespace BddShop.Features.Home.Showroom
{
    public interface IShowroomViewModelProvider
    {
        string[] ForTenants { get; }
        ShowroomViewModel Get();
    }
}