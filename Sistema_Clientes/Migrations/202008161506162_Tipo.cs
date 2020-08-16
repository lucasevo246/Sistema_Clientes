namespace Sistema_Clientes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tipo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clientes", "Tipo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clientes", "Tipo", c => c.String());
        }
    }
}
