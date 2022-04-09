using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ClassSQLServer;
namespace WebInventarioParte1
{
    public partial class WebFormRAM : System.Web.UI.Page
    {
        UsaSQLServer objRam;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                objRam = (UsaSQLServer)Session["sesion"];
            }
            else
            {
                objRam = new UsaSQLServer();
                objRam.cadenaConexion = @"Data Source=DESKTOP-LUHL6NJ; " +
                    "Initial Catalog=BD_InventarioPWA; " + "Integrated Security=true";
                Session["sesion"] = objRam;
                CargarTipo();
                CargarRam();
            }
        }
        public void CargarTipo()
        {
            SqlDataReader container = null;
            SqlConnection conexion = null;
            string msj = "";
            conexion = objRam.AbrirConexion(ref msj);
            container = objRam.EjecutaConsultaDR(conexion, "select id_tipoRam,tipo from TipoRam;", ref msj);
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
        public void CargarRam()
        {
            SqlDataReader container = null;
            SqlConnection conexion = null;
            string msj = "";
            conexion = objRam.AbrirConexion(ref msj);
            container = objRam.EjecutaConsultaDR(conexion, "select id_Ram,capacidad,velocidad,tipo from RAM " +
                "inner join TipoRam on RAM.f_TipoR=TipoRam.id_tipoRam;", ref msj);
            DropDownList2.Items.Clear();
            if (container != null)
            {
                while (container.Read())
                {
                    DropDownList2.Items.Add(
                        new ListItem(container[1].ToString()+" "+container[2].ToString()+" " +container[3].ToString()
                        , container[0].ToString()));
                }
                conexion.Close();
                conexion.Dispose();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string msj = "";
            SqlConnection conexion;
            List<SqlParameter> lista = new List<SqlParameter>();
            SqlParameter temp = null;
            conexion = objRam.AbrirConexion(ref msj);
            string sentencia = "insert into RAM(Capacidad,Velocidad,F_tipoR) values(@cap,@vel,@tipo);";
            temp = new SqlParameter()
            {
                ParameterName = "cap",
                SqlDbType = SqlDbType.SmallInt,
                Value = txtCapacidad.Text
            };
            lista.Add(temp);
            temp = new SqlParameter()
            {
                ParameterName = "vel",
                SqlDbType = SqlDbType.VarChar,
                Size = 15,
                Value = txtVelocidad.Text
            };
            lista.Add(temp);
            temp = new SqlParameter()
            {
                ParameterName = "tipo",
                SqlDbType = SqlDbType.Int,
                Value = int.Parse(DropDownList1.SelectedValue)
            };
            lista.Add(temp);
            Boolean salida = objRam.ModificarBDParametros(sentencia, conexion, ref msj, lista);
            if (salida)
            {
                txtResultado.Text = msj;
            }
            txtCapacidad.Text = "";
            txtVelocidad.Text = "";
            CargarRam();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string msj = "";
            SqlConnection conexion;
            List<SqlParameter> lista = new List<SqlParameter>();
            SqlParameter temp = null;
            conexion = objRam.AbrirConexion(ref msj);
            string sentencia = "Update RAM set Capacidad=@cap,Velocidad=@vel,F_TipoR=@tipo where id_RAM=@id;";
            temp = new SqlParameter()
            {
                ParameterName = "cap",
                SqlDbType = SqlDbType.SmallInt,
                Value = txtCapacidad.Text
            };
            lista.Add(temp);
            temp = new SqlParameter()
            {
                ParameterName = "vel",
                SqlDbType = SqlDbType.VarChar,
                Size = 15,
                Value = txtVelocidad.Text
            };
            lista.Add(temp);
            temp = new SqlParameter()
            {
                ParameterName = "tipo",
                SqlDbType = SqlDbType.Int,
                Value = DropDownList1.Items[DropDownList1.SelectedIndex].Value
            };
            lista.Add(temp);
            temp = new SqlParameter()
            {
                ParameterName = "id",
                SqlDbType = SqlDbType.Int,
                Value = DropDownList2.Items[DropDownList2.SelectedIndex].Value
            };
            lista.Add(temp);
            Boolean salida = objRam.ModificarBDParametros(sentencia, conexion, ref msj, lista);
            if (salida)
            {
                txtResultado.Text = msj;
            }
            else
                txtResultado.Text = msj;
            txtCapacidad.Text = "";
            txtVelocidad.Text = "";
            CargarRam();
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader container = null;
            SqlConnection conexion = null;
            string msj = "";
            int id = int.Parse(DropDownList2.Items[DropDownList2.SelectedIndex].Value);
            conexion = objRam.AbrirConexion(ref msj);
            container = objRam.EjecutaConsultaDR(conexion, "select Capacidad,Velocidad from RAM where id_Ram="+id+";", ref msj);
            if (container != null)
            {
                container.Read();
                txtCapacidad.Text = container[0].ToString();
                txtVelocidad.Text = container[1].ToString();
            }
            else
            {
                txtResultado.Text = msj;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = null;
            string msj = "";
            int id = int.Parse(DropDownList2.Items[DropDownList2.SelectedIndex].Value);
            conexion = objRam.AbrirConexion(ref msj);
            Boolean resul = objRam.ModificarBD("delete from RAM where id_Ram=" + id + ";", conexion, ref msj);
            if (resul)
            {
                txtResultado.Text = "Gabinete eliminado";
            }
            else
                txtResultado.Text = msj;

            txtCapacidad.Text = "";
            txtVelocidad.Text = "";
            CargarRam();
        }

        

        protected void btnMostrar_Click1(object sender, EventArgs e)
        {
            string msj = "";
            SqlConnection conexion;
            SqlDataReader container = null;
            DataTable datos = null;
            conexion = objRam.AbrirConexion(ref msj);
            container = objRam.EjecutaConsultaDR(conexion, "select capacidad,velocidad,tipo from RAM " +
                "inner join TipoRam on RAM.f_TipoR=TipoRam.id_tipoRam;", ref msj);
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
    }
}