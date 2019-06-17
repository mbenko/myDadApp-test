using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace myDadApp.Migrations
{
    public partial class ChoresView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW [dbo].[v_Chores] 
                AS 
                with myChores_CTE (Id, Owner, ParentId, Title, Description, createdAt, CompleteAt, Depth, Sort) as
                (
	                select Id, Owner, ParentId, convert(nvarchar(255),Title), Description, createdAt, CompleteAt, 0 as Depth, Convert(nvarchar(255), Title) as Sort
	                from chore s
	                where ParentId is null
	                UNION ALL
	                select e.Id, e.Owner, e.ParentId, 
		                convert(nvarchar(255), Replicate('>  ', depth+1) + e.Title), e.Description, e.createdAt, e.CompleteAt, m.Depth + 1, 
		                Convert(nvarchar(255), rtrim(sort) + ('>  ' + e.Title))
	                from chore e
		                INNER JOIN myChores_CTE m 
		                on e.ParentId = m.Id
                )
                select * from myChores_CTE
            ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "v_Chores");
        }
    }
}
