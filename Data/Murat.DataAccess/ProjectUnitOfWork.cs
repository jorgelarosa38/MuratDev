using Microsoft.Extensions.Configuration;
using Murat.Repository;
using Murat.UnitOfWork;

namespace Murat.DataAccess
{
    public class ProjectUnitOfWork : IUnitOfWork
    {
        public ISecurityRepository Security { get; private set; }
        public IPublicadoRepository Publicado { get; private set; }
        public IMuratServiceRepository Murat { get; private set; }
        public ICommonRepository Common { get; private set; }
        public ISliderRepository Slider { get; private set; }
        public IMarcaRepository Marca { get; private set; }
        public IProductoRepository Producto { get; private set; }
        public IUserRepository User { get; private set; }
        public ITablaMaestraRepository TablaMaestra { get; private set; }
        public IComboRepository Combo { get; private set; }

        public ProjectUnitOfWork(string connectionString, IConfiguration _configuration)
        {
            Security = new SecurityRepository(connectionString, _configuration);
            Publicado = new PublicadoRepository(connectionString);
            Murat = new MuratServiceRepository(connectionString);
            Common = new CommonRepository(connectionString);
            Slider = new SliderRepository(connectionString);
            User = new UserRepository(connectionString);
            TablaMaestra = new TablaMaestraRepository(connectionString);
            Combo = new ComboRepository(connectionString);
            Producto = new ProductoRepository(connectionString);
            Marca = new MarcaRepository(connectionString);
        }


    }
}
