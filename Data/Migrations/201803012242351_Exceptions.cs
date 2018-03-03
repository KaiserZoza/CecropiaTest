namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Exceptions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExceptionRecords",
                c => new
                    {
                        ExceptionRecordId = c.Int(nullable: false, identity: true),
                        ExceptionRecordDate = c.DateTime(nullable: false),
                        ExceptionMessage = c.String(),
                        ExceptionStackTrace = c.String(),
                        Source = c.String(),
                        InnerExceptionMessage = c.String(),
                        InnerExceptionStackTrace = c.String(),
                        InnerExceptionSource = c.String(),
                        ExceptionTypeName = c.String(),
                    })
                .PrimaryKey(t => t.ExceptionRecordId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExceptionRecords");
        }
    }
}
