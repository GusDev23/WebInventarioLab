using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using ClassSQLServer;
namespace WebInventarioParte1
{
    public partial class WebFormCPU_Generico : System.Web.UI.Page
    {
        UsaSQLServer objCpu;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                objCpu = (UsaSQLServer)Session["sesion"];
            }
            else
            {
                objCpu = new UsaSQLServer();
                objCpu.cadenaConexion = @"Data Source=DESKTOP-LUHL6NJ; " +
                    "Initial Catalog=BD_InventarioPWA; " + "Integrated Security=true";
                Session["sesion"] = objCpu;
                CargarTipoCPU();
                CargarMarcasCpu();
                CargarRam();
                CargarGabinete();
                CargarCpuGenerico();
            }
        }
        private void CargarTipoCPU()
        {
            SqlDataReader container = null;
            SqlConnection conexion = null;
            string msj = "";
            conexion = objCpu.AbrirConexion(ref msj);
            container = objCpu.EjecutaConsultaDR(conexion, "select id_Tcpu,Tipo from Tipo_CPU;", ref msj);
            DropDownList1.Items.Clear();
            if (container != null)
            {
                while (container.Read())
                {
                    DropDownList1.Items.Add(
                        new ListItem(container[1].ToString(), container[0].ToString()));
                }
                conexion.Close();
                conexion.Dispose();
            }
        }
        private void CargarMarcasCpu()
        {
            SqlDataReader container = null;
            SqlConnection conexion = null;
            string msj = "";
            conexion = objCpu.AbrirConexion(ref msj);
            container = objCpu.EjecutaConsultaDR(conexion, "select Id_Marca,Marca from Marca" +
                " Where Id_componente=1;", ref msj);
            DropDownList2.Items.Clear();
            if (container != null)
            {
                while (container.Read())
                {
                    DropDownList2.Items.Add(
                        new ListItem(container[1].ToString(), container[0].ToString()));
                }
                conexion.Close();
                conexion.Dispose();
            }
        }
        private void CargarRam()
        {
            SqlDataReader container = null;
            SqlConnection conexion = null;
            string msj = "";
            conexion = objCpu.AbrirConexion(ref msj);
            container = objCpu.EjecutaConsultaDR(conexion, "select id_Ram,capacidad,velocidad,tipo from RAM " +
                "inner join TipoRam on RAM.f_TipoR=TipoRam.id_tipoRam;", ref msj);
            DropDownList3.Items.Clear();
            if (container != null)
            {
                while (container.Read())
                {
                    DropDownList3.Items.Add(
                        new ListItem(container[1].ToString() + " " + container[2].ToString() + " " + container[3].ToString()
                        , container[0].ToString()));
                }
                conexion.Close();
                conexion.Dispose();
            }
        }
        private void CargarGabinete()
        {
            SqlDataReader container = null;
            SqlConnection conexion = null;
            string msj = "";
            conexion = objCpu.AbrirConexion(ref msj);
            container = objCpu.EjecutaConsultaDR(conexion, "select * from Gabinete", ref msj);
            DropDownList4.Items.Clear();
            if (container != null)
            {
                while (container.Read())
                {
                    DropDownList4.Items.Add(new ListItem(container[1].ToString() + " " + container[2].ToString(),
                        container[0].ToString()));
                }
                conexion.Close();
                conexion.Dispose();
            }
            else
                txtResultado.Text = msj;
        }
        private void CargarCpuGenerico()
        {
            SqlDataReader container = null;
            SqlConnection conexion = null;
            string msj = "";
            conexion = objCpu.AbrirConexion(ref msj);
            container = objCpu.EjecutaConsultaDR(conexion, "select id_CPU,CPU_Generico.Modelo,Descripcion,Gabinete.Modelo,RAM.Capacidad,RAM.Velocidad from CPU_Generico" +
                " inner join Gabinete on CPU_Generico.id_Gabinete=Gabinete.id_Gabinete inner join RAM on CPU_Generico.f_tipoRam=RAM.id_RAM", ref msj);
            DropDownList5.Items.Clear();
            if (container != null)
            {
                while (container.Read())
                {
                    DropDownList5.Items.Add(new ListItem(container[1] + " " + container[2] + " " + container[3] + " " + container[4] + " " + container[5],
                        container[0].ToString()));
                }
                conexion.Close();
                conexion.Dispose();
            }
            else
                txtResultado.Text = msj;
        }
        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            string msj = "";
            SqlConnection conexion;
            List<SqlParameter> lista = new List<SqlParameter>();
            SqlParameter temp = null;
            conexion = objCpu.AbrirConexion(ref msj);
            string sentencia = "insert into CPU_Generico(f_Tcpu,f_MarcaCpu,Modelo,Descripcion,f_tipoRam,id_Gabinete) " +
                "values(@cpu,@marc,@mod,@des,@tiporam,@gab);";
            temp = new SqlParameter()
            {
                ParameterName = "cpu",
                SqlDbType = SqlDbType.Int,
                Value = DropDownList1.Items[DropDownList1.SelectedIndex].Value
            };
            lista.Add(temp);
            temp = new SqlParameter()
            {
                ParameterName = "marc",
                SqlDbType = SqlDbType.Int,
                Value = DropDownList2.Items[DropDownList2.SelectedIndex].Value
            };
            lista.Add(temp);
            temp = new SqlParameter()
            {
                ParameterName = "mod",
                SqlDbType = SqlDbType.VarChar,
                Size=20,
                Value = txtModelo.Text
            };
            lista.Add(temp);
            temp = new SqlParameter()
            {
                ParameterName = "des",
                SqlDbType = SqlDbType.VarChar,
                Size = 40,
                Value = txtDescripcion.Text
            };
            lista.Add(temp);
            temp = new SqlParameter()
            {
                ParameterName = "tiporam",
                SqlDbType = SqlDbType.Int,
                Value = DropDownList3.Items[DropDownList3.SelectedIndex].Value
            };
            lista.Add(temp);
            temp = new SqlParameter()
            {
                ParameterName = "gab",
                SqlDbType = SqlDbType.Int,
                Value = DropDownList4.Items[DropDownList4.SelectedIndex].Value
            };
            lista.Add(temp);
            
            Boolean salida = objCpu.ModificarBDParametros(sentencia, conexion, ref msj, lista);
            if (salida)
            {
                txtResultado.Text = msj;
            }
            txtDescripcion.Text = "";
            txtModelo.Text = "";
            CargarCpuGenerico();
        }
        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            string msj = "";
            SqlConnection conexion;
            SqlDataReader container = null;
            DataTable datos = null;
            conexion = objCpu.AbrirConexion(ref msj);
            container = objCpu.EjecutaConsultaDR(conexion, "select Tipo_CPU.Tipo,Marca.Marca,CPU_Generico.Modelo," +
            "Tipo_CPU.Familia,Tipo_CPU.Velocidad,CPU_Generico.Descripcion,Gabinete.Modelo,Gabinete.TipoForma,RAM.Capacidad,RAM.Velocidad,TipoRAM.Tipo from TipoRAM " +
            "inner join RAM on TipoRAM.id_tipoRam = RAM.F_TipoR inner join CPU_Generico on "+
            "RAM.id_RAM = CPU_Generico.f_tipoRam inner join Tipo_CPU on CPU_Generico.f_Tcpu = Tipo_CPU.id_Tcpu "+
            "inner join Gabinete on CPU_Generico.id_Gabinete = Gabinete.id_Gabinete inner join Marca on "+
            "CPU_Generico.f_MarcaCpu = Marca.Id_Marca;", ref msj);
            if (container != null)
            {
                datos = new DataTable();
                datos.Load(container);
                GridView1.DataSource = datos;
                GridView1.DataBind();
            }
            else
                txtResultado.Text = msj;
        }
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string msj = "";
            SqlConnection conexion;
            List<SqlParameter> lista = new List<SqlParameter>();
            SqlParameter temp = null;
            conexion = objCpu.AbrirConexion(ref msj);
            string sentencia = "update CPU_Generico set f_Tcpu=@cpu,f_MarcaCpu=@marc,Modelo=@mod,Descripcion=@des,f_tipoRam=@tiporam,id_Gabinete=@gab" +
                " where id_CPU=@id";
            temp = new SqlParameter()
            {
                ParameterName = "cpu",
                SqlDbType = SqlDbType.Int,
                Value = DropDownList1.Items[DropDownList1.SelectedIndex].Value
            };
            lista.Add(temp);
            temp = new SqlParameter()
            {
                ParameterName = "marc",
                SqlDbType = SqlDbType.Int,
                Value = DropDownList2.Items[DropDownList2.SelectedIndex].Value
            };
            lista.Add(temp);
            temp = new SqlParameter()
            {
                ParameterName = "mod",
                SqlDbType = SqlDbType.VarChar,
                Size = 20,
                Value = txtModelo.Text
            };
            lista.Add(temp);
            temp = new SqlParameter()
            {
                ParameterName = "des",
                SqlDbType = SqlDbType.VarChar,
                Size = 40,
                Value = txtDescripcion.Text
            };
            lista.Add(temp);
            temp = new SqlParameter()
            {
                ParameterName = "tiporam",
                SqlDbType = SqlDbType.Int,
                Value = DropDownList3.Items[DropDownList3.SelectedIndex].Value
            };
            lista.Add(temp);
            temp = new SqlParameter()
            {
                ParameterName = "gab",
                SqlDbType = SqlDbType.Int,
                Value = DropDownList4.Items[DropDownList4.SelectedIndex].Value
            };
            lista.Add(temp);
            temp = new SqlParameter()
            {
                ParameterName = "id",
                SqlDbType = SqlDbType.Int,
                Value = DropDownList5.Items[DropDownList5.SelectedIndex].Value
            };
            lista.Add(temp);
            Boolean salida = objCpu.ModificarBDParametros(sentencia, conexion, ref msj, lista);
            if (salida)
            {
                txtResultado.Text = msj;
            }
            else
                txtResultado.Text = msj;

            txtDescripcion.Text = "";
            txtModelo.Text = "";            
            CargarCpuGenerico();
        }       
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = null;
            string msj = "";
            int id = int.Parse(DropDownList5.Items[DropDownList5.SelectedIndex].Value);
            conexion = objCpu.AbrirConexion(ref msj);
            Boolean resul = objCpu.ModificarBD("delete from CPU_Generico where id_CPU=" + id + ";", conexion, ref msj);
            if (resul)
            {
                txtResultado.Text = "CPU Generico eliminado";
            }
            else
                txtResultado.Text = msj;

            txtDescripcion.Text = "";
            txtModelo.Text = "";
            CargarCpuGenerico();
        }
        protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader container = null;
            SqlConnection conexion = null;
            string msj = "";
            int id = int.Parse(DropDownList5.Items[DropDownList5.SelectedIndex].Value);
            conexion = objCpu.AbrirConexion(ref msj);
            container = objCpu.EjecutaConsultaDR(conexion, "select Modelo,Descripcion from CPU_Generico where id_CPU=" + id + ";", ref msj);
            if (container != null)
            {
                container.Read();
                txtModelo.Text = container[0].ToString();
                txtDescripcion.Text = container[1].ToString();
            }
            else
            {
                txtResultado.Text = msj;
            }
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}