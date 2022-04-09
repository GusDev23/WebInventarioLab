using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace ClassSQLServer
{
    public class UsaSQLServer
    {
        public string cadenaConexion { get; set; }
        public SqlConnection AbrirConexion(ref string mensaje)
        {
            SqlConnection carret = new SqlConnection();
            carret.ConnectionString = cadenaConexion;
            try
            {
                carret.Open();
                mensaje = "todo correcto";
            }
            catch (Exception m)
            {
                carret = null;
                mensaje = "Error" + m.Message;
            }
            return carret;
        }
        public SqlDataReader EjecutaConsultaDR(SqlConnection cnab, string consulta, ref string msj)
        {
            SqlDataReader salidacaja = null;
            SqlCommand car = null;
            if (cnab != null)
            {
                car = new SqlCommand();
                car.CommandText = consulta;
                car.Connection = cnab;
                try
                {
                    salidacaja = car.ExecuteReader();
                    msj = "Consulta Correcta";
                }
                catch (Exception x)
                {
                    salidacaja = null;
                    msj = "Error" + x.Message;
                }
            }
            else
            {
                msj = "No hay Conexion a la BD";
            }
            return salidacaja;
        }
        public DataSet ConsultaDataSet(string query1, SqlConnection cn_ab, ref string msj)
        {
            SqlCommand carritotemp = null;
            SqlDataAdapter trailer = null;
            DataSet contenedo = null;

            if (cn_ab != null)
            {
                carritotemp = new SqlCommand();
                carritotemp.CommandText = query1;
                carritotemp.Connection = cn_ab;
                trailer = new SqlDataAdapter();
                trailer.SelectCommand = carritotemp;
                contenedo = new DataSet();
                try
                {
                    trailer.Fill(contenedo);
                    msj = "dataset ya tiene datos";
                }
                catch (Exception w)
                {
                    contenedo = null;
                    msj = "Error" + w.Message;
                }
                cn_ab.Close();
                cn_ab.Dispose();
            }
            else
            {
                msj = "No hay conexion";
            }
            return contenedo;
        }
        public Boolean ModificarBD(string sentenciaSQL,SqlConnection conexion,ref string msj)
        {
            SqlCommand carrito = null;
            Boolean salida = false;
            if (conexion != null)
            {
                carrito = new SqlCommand();
                carrito.CommandText = sentenciaSQL;
                carrito.Connection = conexion;
                try
                {
                    carrito.ExecuteNonQuery();
                    msj = "Insercion correcta";
                    salida = true;
                }
                catch (Exception r)
                {
                    salida = false;
                    msj = r.ToString();
                }
                conexion.Close();
                conexion.Dispose();
            }
            else
                msj = "No hay conexion con BD";
            return salida;
        }
        public Boolean InserccionSeguraMonitor(string idmarc, string conect, string t, SqlConnection conexion, ref string msj)
        {//recibir un arreglo de parametros
            string sentencia = "insert into Monitor(f_marcam,conectores,tamano) values(@marc,@con,@tam);";
            Boolean salida = false;
            SqlParameter m = new SqlParameter()
            {
                ParameterName = "marc",
                SqlDbType = SqlDbType.Int,
                Value = idmarc
            };
            SqlParameter con = new SqlParameter()
            {
                ParameterName = "con",
                SqlDbType = SqlDbType.VarChar,
                Size = 64,
                Value = conect
            };
            SqlCommand medio = new SqlCommand();
            medio.CommandText = sentencia;
            medio.Connection = conexion;
            medio.Parameters.Add(m);
            medio.Parameters.Add(con);
            medio.Parameters.Add(new SqlParameter()
            {
                ParameterName = "tam",
                SqlDbType = SqlDbType.VarChar,
                Size = 64,
                Value = t
            });
            try
            {
                medio.ExecuteNonQuery();
                msj = "Inserccion Correcta";
                salida = true;
            }
            catch (Exception e)
            {
                msj = "ERROR: " + e;
                salida = false;
            }
            conexion.Close();
            conexion.Dispose();
            return salida;
        }
        public Boolean ModificarBDParametros(string sentenciaSQL, SqlConnection conexion, ref string msj,
            List<SqlParameter> parametros)
        {//METODO DE ACTIVIDAD 1
            SqlCommand carrito = null;
            Boolean salida = false;
            if (conexion != null)
            {
                carrito = new SqlCommand();
                carrito.CommandText = sentenciaSQL;
                carrito.Connection = conexion;
                foreach (var item in parametros)
                {
                    carrito.Parameters.Add(item);
                }
                try
                {
                    carrito.ExecuteNonQuery();
                    msj = "Operacion correcta";
                    salida = true;
                }
                catch (Exception r)
                {
                    salida = false;
                    msj = r.ToString();
                }
                conexion.Close();
                conexion.Dispose();
            }
            else
                msj = "No hay conexion con BD";
            return salida;
        }
        public SqlDataReader EjecutaConsultaDRParametros(SqlConnection cnab, string consulta, ref string msj, List<SqlParameter> parametros)
        {//METODO ACTIVIAD 1
            SqlDataReader salidacaja = null;
            SqlCommand car = null;
            if (cnab != null)
            {
                car = new SqlCommand();
                car.CommandText = consulta;
                car.Connection = cnab;
                foreach (var item in parametros)
                {
                    car.Parameters.Add(item);
                }
                try
                {
                    salidacaja = car.ExecuteReader();
                    msj = "Consulta Correcta";
                }
                catch (Exception x)
                {
                    salidacaja = null;
                    msj = "Error" + x.Message;
                }
            }
            else
            {
                msj = "No hay Conexion a la BD";
            }
            return salidacaja;
        }
    }
}
