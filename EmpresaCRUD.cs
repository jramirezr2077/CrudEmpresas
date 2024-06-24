﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudEmpresasPOO
{
    internal class EmpresaCRUD
    {
        private List<Empresa> listaEmpresas = new List<Empresa>();
        private MensajeUI mensaje = new MensajeUI(50);
        private const string FilePath = "empresas.csv";

        public EmpresaCRUD()
        {
            this.CargarEmpresas();
        }

        public void MostrarEmpresas()
        {
            this.mensaje.mostrarTitulo("RELACIÓN DE EMPRESAS");
            foreach (var empresa in listaEmpresas)
            {
                Console.WriteLine(new string('*', 50));
                empresa.Mostrar();
            }
        }

        public void RegistrarEmpresa()
        {
            this.mensaje.mostrarTitulo("REGISTRO DE NUEVA EMPRESA");
            Console.Write("RUC: ");
            long ruc;
            bool resultado = false;
            while (!resultado)
            {
                Console.WriteLine("Ingrese el RUC (debe tener 11 dígitos):");
                resultado = long.TryParse(Console.ReadLine(), out ruc) && ((ruc >= 10000000000 && ruc <= 10999999999) || (ruc >= 20000000000 && ruc <= 20999999999));
                if (resultado)
                {
                    if ((ruc >= 10000000000 && ruc <= 10999999999) || (ruc >= 20000000000 && ruc <= 20999999999))
                    {
                        Console.WriteLine($"El RUC ingresado es: {ruc}");

                        Console.Write("RAZON SOCIAL: ");
                        string razon_social = Console.ReadLine();
                        Console.Write("DIRECCIÓN: ");
                        string direccion = Console.ReadLine();

                        Empresa nuevaEmpresa = new Empresa(ruc.ToString(), razon_social, direccion);
                        listaEmpresas.Add(nuevaEmpresa);
                        this.mensaje.mostrarMensaje("EMPRESA REGISTRADA CON EXITO!!!");
                    }
                    else
                    {
                        Console.WriteLine("El RUC ingresado no tiene 11 dígitos.");
                    }
                }
                else
                {
                    Console.WriteLine("Ingrese un RUC válido, debe tener 11 dígitos y comenzar con 10 para RUC personas y 20 para RUC empresas");
                }
            }
            

            
        }

        private Empresa buscarEmpresa(string opcion)
        {
            Console.WriteLine($" {opcion} EMPRESA");
            Console.WriteLine($"INGRESE EL RUC DE LA EMPRESA A {opcion}: ");
            string ruc = Console.ReadLine();

            Empresa empresa = listaEmpresas.Find(a => a.Ruc.Equals(ruc, StringComparison.OrdinalIgnoreCase));
            return empresa;
        }

        public void ActualizarEmpresa()
        {
            Empresa empresa = this.buscarEmpresa("ACTUALIZAR");
            if (empresa != null)
            {
                Console.Write("NUEVO RUC: ");
                string nuevoRuc = Console.ReadLine();
                Console.Write("NUEVA RAZON SOCIAL: ");
                string nuevoRazonSocial = Console.ReadLine();
                Console.Write("NUEVA DIRECCIÓN: ");
                string nuevaDireccion = Console.ReadLine();

                empresa.Ruc = nuevoRuc;
                empresa.Razon = nuevoRazonSocial;
                empresa.Direccion = nuevaDireccion;

                this.mensaje.mostrarMensaje("EMPRESA ACTUALIZADA CON EXITO!!!");
            }
            else
            {
                this.mensaje.mostrarMensaje("EMPRESA NO ENCONTRADA...");
            }
        }

        public void EliminarEmpresa()
        {
            Empresa empresa = this.buscarEmpresa("ELIMINAR");
            if (empresa != null)
            {
                listaEmpresas.Remove(empresa);
                this.mensaje.mostrarMensaje("EMPRESA ELIMINADA CON EXITO!!!");
            }
            else
            {
                this.mensaje.mostrarMensaje("NO SE ENCONTRO LA EMPRESA...");
            }
        }

        public void CargarEmpresas()
        {
            if (File.Exists(FilePath))
            {
                using (StreamReader sr = new StreamReader(FilePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        listaEmpresas.Add(Empresa.FromCsv(line));
                    }
                }
            }
        }

        public void GuardarEmpresas()
        {
            using (StreamWriter sr = new StreamWriter(FilePath))
            {
                foreach (var empresa in listaEmpresas)
                {
                    sr.WriteLine(empresa.ToCsv());
                }
            }
        }
    }
}
