using System.Data.Entity;

namespace MvcTienda.Infrastructura.Data
{
    public class AppDbInitalizer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {

        protected override void Seed(AppDbContext context)
        {
            context.Estados.Add(new Domain.Entities.Estado { Nombre = "Disponible" });
            context.Estados.Add(new Domain.Entities.Estado { Nombre = "No Disponible" });
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
