using System.Data.Entity;

namespace MvcTienda.Infrastructura.Data
{
    internal class AppDbInitalizer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {

        protected override void Seed(AppDbContext context)
        {

            base.Seed(context);
        }
    }
}
