using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Clases
{
    class CSLcsv
    {
        public void imprimirConsulta(DataTable dt,string d1, string d2, string d3, string d4)
        {
            int acumulador = 0;
            string contenido = $"NIT;NOMBRE;PEDIDO;PRECIO_COMPRA;\n{d1};{d2};{d3};{d4}";
            foreach (DataRow Datos in dt.Rows)
            {
                contenido += ($"{d1};{d2};{d3};{d4};\n");
                acumulador++;
            }

            File.WriteAllText($@"C:\Users\alumno\Desktop\Nueva carpeta\ProyectoFinal\archivo{acumulador}.csv", contenido);
        }


    }
}
