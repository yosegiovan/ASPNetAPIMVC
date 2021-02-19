namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_M_Item", "SupplierID", c => c.Int(nullable: false));
            CreateIndex("dbo.Tb_M_Item", "SupplierID");
            AddForeignKey("dbo.Tb_M_Item", "SupplierID", "dbo.Tb_M_Supplier", "SupplierID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tb_M_Item", "SupplierID", "dbo.Tb_M_Supplier");
            DropIndex("dbo.Tb_M_Item", new[] { "SupplierID" });
            DropColumn("dbo.Tb_M_Item", "SupplierID");
        }
    }
}
