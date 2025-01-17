namespace IAAI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IAAI.Models.DbModel.DbConnect>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "IAAI.Models.DbModel.DbConnect";
        }

        protected override void Seed(IAAI.Models.DbModel.DbConnect context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
