using Microsoft.EntityFrameworkCore;
using ApiForecast.Models.Entities;
using ApiForescast.Models.Entities;

namespace ApiForecast.Data{

    public class ForecastContext : DbContext{

        public ForecastContext(DbContextOptions<ForecastContext> options) : base(options){

        }


        public DbSet<AccessoSucursales> AccesoSucursales { get; set;}
        public DbSet<Clientes> Clientes { get; set;}
        public DbSet<Compras> Compras { get; set;}
        public DbSet<Folios> Folios { get; set;}
        public DbSet<Grupos> Grupos { get; set;}
        public DbSet<Impresoras> Impresoras { get; set;}
        public DbSet<Parametros> Parametros { get; set;}
        public DbSet<Permisos> Permisos { get; set;}
        public DbSet<Productos> Productos { get; set;}
        public DbSet<Proveedores> Proveedores { get; set;}
        public DbSet<Puntos> Puntos { get; set;}
        public DbSet<Reportes> Reportes { get; set;}
        public DbSet<Roles> Roles { get; set;}
        public DbSet<Sucursales> Sucursales { get; set;}
        public DbSet<UserRoles> User_Roles { get; set;}
        public DbSet<Usuarios> Usuarios { get; set;}
        public DbSet<Vendedores> Vendedores { get; set;}
        public DbSet<Ventas> Ventas { get; set;}

        override protected void OnModelCreating(ModelBuilder modelBuilder){

            //Relacion Acceso Sucursales
            modelBuilder.Entity<AccessoSucursales>()
                .HasOne(x => x.Usuario)
                .WithMany(x => x.AccessoSucursales)
                .HasForeignKey(x => x.User_id);
            modelBuilder.Entity<AccessoSucursales>()
                .HasOne(x => x.Sucursal)
                .WithMany(x => x.AccessoSucursales)
                .HasForeignKey(x => x.Sucursal_id);    
            //Relacion Compras
            modelBuilder.Entity<Compras>()
                .HasOne(x => x.Product)
                .WithMany(x => x.Compras)
                .HasForeignKey(x => x.Product_id);
            modelBuilder.Entity<Compras>()
                .HasOne(x=>x.Proveedor)
                .WithMany(x=>x.Compras)
                .HasForeignKey(x=>x.Provider_id); 
            //Relacion Folios
            modelBuilder.Entity<Folios>()
                .HasOne(x=>x.Sucursales)
                .WithMany(x=>x.Folios)
                .HasForeignKey(x=>x.Sucursal_id);       
            //Relacion Impresoras
            modelBuilder.Entity<Impresoras>()
                .HasOne(x=>x.Sucursales)
                .WithMany(x=>x.Impresoras)
                .HasForeignKey(x=>x.Sucursal_id);
             //Relacion Parametros
            modelBuilder.Entity<Parametros>()
                .HasOne(x=>x.Sucursal)
                .WithMany(x=>x.Parametros)
                .HasForeignKey(x=>x.Sucursal_id);
            //Relacion Permisos
            modelBuilder.Entity<Permisos>()
                .HasOne(x=>x.User)
                .WithMany(x=>x.Permisos)
                .HasForeignKey(x=>x.User_id);  
            //Relacion Productos
            modelBuilder.Entity<Productos>()
                .HasOne(x=>x.Grupos)
                .WithMany(x=>x.Productos)
                .HasForeignKey(x=>x.Group_Id);      
            //Relacion Puntos
            modelBuilder.Entity<Puntos>()
                .HasOne(x=>x.Sucursales)
                .WithMany(x=>x.Puntos)
                .HasForeignKey(x=>x.Sucursal_id);
            //Relacion UserRoles
            modelBuilder.Entity<UserRoles>()
                .HasOne(x=>x.User)
                .WithMany(x=>x.UserRoles)
                .HasForeignKey(x=>x.User_Id);
            modelBuilder.Entity<UserRoles>()
                .HasOne(x=>x.Role)
                .WithMany(x=>x.UserRole)
                .HasForeignKey(x=>x.Role_Id);  
            modelBuilder.Entity<UserRoles>()
                .HasOne(x=>x.Sucursal)
                .WithMany(x=>x.UserRoles)
                .HasForeignKey(x=>x.Sucursal_id);    
            //Relacion Ventas

            modelBuilder.Entity<Ventas>()
                .HasOne(x=>x.Productos)
                .WithMany(x=>x.Ventas)
                .HasForeignKey(x=>x.Product_Id);   
            modelBuilder.Entity<Ventas>()
                .HasOne(x=>x.Cliente)
                .WithMany(x=>x.Ventas)
                .HasForeignKey(x=>x.Client_id);
            modelBuilder.Entity<Ventas>()
                .HasOne(x=>x.Vendedores)
                .WithMany(x=>x.Ventas)
                .HasForeignKey(x=>x.Vendor_Id);    



                
        }
    }
}