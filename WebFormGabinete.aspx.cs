using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassSQLServer;
using System.Data;
using System.Data.SqlClient;
namespace WebInventarioParte1
{
    public partial class WebFormGabinete : System.Web.UI.Page
    {
        UsaSQLServer obj;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                obj = (UsaSQLServer)Session["sesion"];
            }
            else
            {
                obj = new UsaSQLServer();
                obj.cadenaConexion = @"Data Source=DESKTOP-LUHL6NJ; " +
                    "Initial Catalog=BD_InventarioPWA; " + "Integrated Security=true";
                Session["sesion"] = obj;
                CargarMarcasGabinete();
                CargarGabinetes();
            }
        }
        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            string msj = "";
            SqlConnection conexion;
            List<SqlParameter> lista = new List<SqlParameter>();
            SqlParameter temp = null;
            conexion = obj.AbrirConexion(ref msj);
            string sentencia = "insert into Gabinete(Modelo,TipoForma,F_marca) values(@model,@form,@marc);";
            temp = new SqlParameter()
            {
                ParameterName = "model",
                SqlDbType = SqlDbType.VarChar,
                Size = 10,
                Value = txtModeloGabinete.Text
            };
            lista.Add(temp);
            temp = new SqlParameter()
            {
                ParameterName = "form",
                SqlDbType = SqlDbType.VarChar,
                Size = 30,
                Value = txtForma.Text
            };
            lista.Add(temp);
            temp = new SqlParameter()
            {
                ParameterName = "marc",
                SqlDbType = SqlDbType.Int,
                Value = int.Parse(DropDownList1.SelectedValue)
            };
            lista.Add(temp);
            Boolean salida= obj.ModificarBDParametros(sentencia, conexion, ref msj, lista);
            if (salida)
            {
                txtResultado.Text = msj;
            }
            txtModeloGabinete.Text = "";
            txtForma.Text = "";
            DropDownList1.SelectedIndex = -1;
            CargarGabinetes();
        }
        public void CargarMarcasGabinete()
        {
            SqlDataReader container = null;
            SqlConnection conexion = null;
            string msj = "";
            conexion = obj.AbrirConexion(ref msj);
            container = obj.EjecutaConsultaDR(conexion, "select Id_Marca,Marca from Marca" +
                " Where Id_componente=4;", ref msj);            
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
        public void CargarGabinetes()
        {
            SqlDataReader container = null;
            SqlConnection conexion = null;
            string msj = "";
            conexion = obj.AbrirConexion(ref msj);
            container = obj.EjecutaConsultaDR(conexion, "select * from Gabinete", ref msj);
            DropDownList2.Items.Clear();
            if (container != null)
            {
                while (container.Read())
                {
                    DropDownList2.Items.Add(new ListItem(container[1].ToString() +" "+ container[2].ToString(),
                        container[0].ToString()));
                }
                conexion.Close();
                conexion.Dispose();
            }
        }
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string msj = "";
            SqlConnection conexion;
            List<SqlParameter> lista = new List<SqlParameter>();
            SqlParameter temp = null;
            conexion = obj.AbrirConexion(ref msj);
            string sentencia = "Update Gabinete set Modelo=@model, TipoForma=@form where id_Gabinete=@id;";
            temp = new SqlParameter()
            {
                ParameterName = "model",
                SqlDbType = SqlDbType.VarChar,
                Size = 10,
                Value = txtModeloGabinete.Text
            };
            lista.Add(temp);
            temp = new SqlParameter()
            {
                ParameterName = "form",
                SqlDbType = SqlDbType.VarChar,
                Size = 30,
                Value = txtForma.Text
            };
            lista.Add(temp);
            temp = new SqlParameter()
            {
                ParameterName = "id",
                SqlDbType = SqlDbType.Int,
                Value = DropDownList2.Items[DropDownList2.SelectedIndex].Value
            };
            lista.Add(temp);
            Boolean salida = obj.ModificarBDParametros(sentencia, conexion, ref msj, lista);
            if (salida)
            {
                txtResultado.Text = msj;
            }
            txtForma.Text = "";
            txtModeloGabinete.Text = "";
            DropDownList1.SelectedIndex = -1;
            CargarGabinetes();
        }

        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            string msj = "";
            SqlConnection conexion;
            SqlDataReader container = null;
            DataTable datos = null;
            conexion = obj.AbrirConexion(ref msj);
            container = obj.EjecutaConsultaDR(conexion,"select modelo,TipoForma,Marca " +
                "from Gabinete inner join Marca on Gabinete.F_Marca=Marca.Id_Marca", ref msj);
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

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader container = null;
            SqlConnection conexion = null;
            string msj = "";
            int id = int.Parse(DropDownList2.Items[DropDownList2.SelectedIndex].Value);
            conexion = obj.AbrirConexion(ref msj);
            container = obj.EjecutaConsultaDR(conexion, "select Modelo,TipoForma from Gabinete where id_Gabinete="+id+";", ref msj);
            if (container!=null)
            {
                container.Read();
                txtModeloGabinete.Text = container[0].ToString();
                txtForma.Text = container[1].ToString();
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
            conexion = obj.AbrirConexion(ref msj);
            Boolean resul = obj.ModificarBD("delete from Gabinete where id_Gabinete=" + id + ";",conexion, ref msj);
            if (resul)
            {
                txtResultado.Text = "Gabinete eliminado";
            }
            else
                txtResultado.Text = msj;

            txtForma.Text = "";
            txtModeloGabinete.Text = "";
            CargarGabinetes();
        }

        protected void btnIrRam_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebFormRAM.aspx");
        }
    }
}