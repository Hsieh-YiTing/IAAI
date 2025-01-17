using IAAI.Models.BaseEntityModel;
using IAAI.Models.EFModel;
using IAAI.Models.EFModel.AssnEF;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace IAAI.Models.DbModel
{
    public class DbConnect : DbContext
    {
        // 您的內容已設定為使用應用程式組態檔 (App.config 或 Web.config)
        // 中的 'DbConnect' 連接字串。根據預設，這個連接字串的目標是
        // 您的 LocalDb 執行個體上的 'IAAI.Models.DbModel.DbConnect' 資料庫。
        // 
        // 如果您的目標是其他資料庫和 (或) 提供者，請修改
        // 應用程式組態檔中的 'DbConnect' 連接字串。
        public DbConnect()
            : base("name=DbConnect")
        {
        }

        // 針對您要包含在模型中的每種實體類型新增 DbSet。如需有關設定和使用
        // Code First 模型的詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=390109。

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<AssnJobTitle> AssnJobTitle { get; set; }

        public virtual DbSet<AssnMember> AssnMembers { get; set; }

        // DbModelBuilder，EF自動提供給OnModelCreating方法的物件
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 直接設置所有BaseEntity類的屬性
            // 也就是不用每個子類都要寫modelBuilder.Entity<T>
            // 最後使用Ignore()，才不會在資料庫建立BaseEntity表資料表
            modelBuilder.Types<BaseEntity>()
                .Configure(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                });

            modelBuilder.Ignore<BaseEntity>();

            // 只是擴充方法，但是原有的方法邏輯還是需要，所以要呼叫base執行
            base.OnModelCreating(modelBuilder);
        }

        // 回傳值為int，表示成功的CRUD的數量
        public override int SaveChanges()
        {
            // 追蹤繼承BaseEntity類的實體
            var entries = ChangeTracker.Entries<BaseEntity>();

            // entry，物件紀錄，包含物件狀態以及實體
            // entry.State : 物件狀態，entry.Entity : 物件實體
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.UpdateTime = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdateTime = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}