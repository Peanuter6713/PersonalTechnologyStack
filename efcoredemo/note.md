# 搭建EFCore环境

1.   SqlServer的使用，安装以下包，其自动依赖于EFCore

   ```
   Microsoft.EntityFrameworkCore.SqlServer
   ```

2.  创建实现了 IEntityTypeConfiguration接口的实体配置类，配置实体类和数据库表的对应关系

3. 安装  Microsoft.EntityFrameworkCore.Tools 

# EFCore 命令的使用

1.   修改model時，运行以下命令，查看生成的Migration 类，是否和想还要修改的一致。

   ```
   Add-Migration "MigrationName"  
   
   // 尤其是修改列名，合并列时可以使用Sql语句进行。
   migrationBuilder.Sql(
   @"
       UPDATE Customer
       SET FullName = FirstName + ' ' + LastName;
   ");
   ```

2.  确保生成的Migration类无误后，进行数据库更新

   ```
   Update-Databse // 相当于 dbContext.SaveChanges()
   ```

3.  删除上一次的迁移, 上一次迁移未保存（即未执行Update-Database）

   ```
   Remove-Migration 
   ```

4.  获取所有的Migration

   ```
   Get-Migration  // 数据库中有张管理Migration的Table
   ```

5.  将修改的数据库生成SQL脚本

   ```
   Script-Migration  
   Script-Migration "MigrationName" // 只针对某次你迁移生成SQL脚本
   ```

   

# EFCore 优化之AsNoTracking

1. 如果确认获取的数据只显示不更新，则可使用AsNoTracking方法，取消对数据的监视。降低对内存的占用。

   ```
   dbContext.Books.AsNoTracking().ToList();
   dbContext.Entry(item[0]).State  // 查看监视状态
   ```

# 全局过滤筛选器

1.  在实现IEntityTypeConfiguration接口的配置类中配置filter

   ```
   builder.HasQueryFilter(a => a.IsDeleted == false)
   ```

   

 