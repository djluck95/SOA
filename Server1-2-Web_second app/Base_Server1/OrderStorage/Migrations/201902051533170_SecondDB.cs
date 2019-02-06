namespace OrderStorage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        quantity = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Stocks");
        }
    }
}
