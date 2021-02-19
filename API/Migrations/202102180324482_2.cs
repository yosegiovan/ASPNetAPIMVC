namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tb_M_Item");
        }
    }
}
