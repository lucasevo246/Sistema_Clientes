namespace Sistema_Clientes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Required : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clientes", "Nome", c => c.String(nullable: false));
            AlterColumn("dbo.Clientes", "Documento", c => c.String(nullable: false));
            AlterColumn("dbo.Clientes", "Tel", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clientes", "Tel", c => c.String());
            AlterColumn("dbo.Clientes", "Documento", c => c.String());
            AlterColumn("dbo.Clientes", "Nome", c => c.String());
        }
    }
}
