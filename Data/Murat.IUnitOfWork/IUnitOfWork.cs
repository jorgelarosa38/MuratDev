using Murat.Repository;

namespace Murat.UnitOfWork
{
    public interface IUnitOfWork
    {
        ISecurityRepository Security { get; }
        IPublicadoRepository Publicado { get; }
        ICommonRepository Common { get; }
        ISliderRepository Slider { get; }
        IMarcaRepository Marca { get; }
        IProductoRepository Producto { get; }
        IUserRepository User { get; }
        ITablaMaestraRepository TablaMaestra { get; }
        IComboRepository Combo { get; }
        IMuratServiceRepository Murat { get; }
    }
}
