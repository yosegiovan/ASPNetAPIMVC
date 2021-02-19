namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tb_M_Item",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        Quantity = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemID);
            
            CreateTable(
                "dbo.Tb_M_Supplier",
                c => new
                    {
                        SupplierID = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.SupplierID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tb_M_Supplier");
            DropTable("dbo.Tb_M_Item");
        }
    }
}
