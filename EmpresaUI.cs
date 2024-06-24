using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudEmpresasPOO
{
    internal class EmpresaUI
    {
        const int ANCHO = 50;
        private EmpresaCRUD crud;
        private MensajeUI mesajeUI = new MensajeUI(50);

        public EmpresaUI()
        {
            crud = new EmpresaCRUD();
        }


        public void MostrarMenuPrincipal()
        {
            int opcion = 0;
            while (opcion != 5)
            {
                Console.Clear();
                this.mesajeUI.mostrarTitulo("CRUD DE EMPRESAS");
                Console.WriteLine(@"
                    [1] REGISTRAR EMPRESA
                    [2] MOSTRAR EMPRESAS
                    [3] ACTUALIZAR EMPRESA
                    [4] ELIMINAR EMPRESA
                    [5] SALIR
                ");
                this.mesajeUI.mostrarTitulo("INGRESE UNA OPCIÓN DEL MENU: ");
                opcion = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (opcion)
                {
                    case 1:
                        crud.RegistrarEmpresa();
                        break;
                    case 2:
                        crud.MostrarEmpresas();
                        Console.WriteLine("PRESIONE ENTER PARA CONTINUAR...");
                        Console.ReadKey();
                        break;
                    case 3:
                        crud.ActualizarEmpresa();
                        break;
                    case 4:
                        crud.EliminarEmpresa();
                        break;
                    case 5:
                        Console.WriteLine("SALIENDO DEL SISTEMA ... ");
                        crud.GuardarEmpresas();
                        break;
                    default:
                        this.mesajeUI.mostrarMensaje("OPCIÓN INVALIDA!!!");
                        break;
                }
            }
        }
    }
}
