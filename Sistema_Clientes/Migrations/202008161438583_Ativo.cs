namespace Sistema_Clientes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ativo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clientes", "Ativo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clientes", "Ativo");
        }
    }
}
