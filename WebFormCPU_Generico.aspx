<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormCPU_Generico.aspx.cs" Inherits="WebInventarioParte1.WebFormCPU_Generico" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <!-- CSS only -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous"/>
    <!-- JavaScript Bundle with Popper -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
    <title>CPU GENERICO</title>
</head>
<body>    
    <ul class="nav nav-tabs" id="myTab" role="tablist" style="background:#b6b6b6">
        <li class="nav-item active" role="presentation">
            <a class="nav-link"  href="WebFormGabinete.aspx" type="button" role="tab"  aria-selected="true">Gabinete</a>
        </li>
        <li class="nav-item" role="presentation">
            <a class="nav-link"  href="WebFormRAM.aspx" type="button" role="tab"  aria-selected="false">RAM</a>
        </li>
        <li class="nav-item" role="presentation">
            <a class="nav-link active"  href="WebFormCPU_Generico.aspx" role="tab" aria-selected="false">CPU Generico</a>
        </li>
    </ul>

    <form id="form1" runat="server">
        <div class="container">
            <div class="row align-items-center">
                <div class="col">
                    <br />
                    <br />
                    <p>Tipo cpu </p>
                    <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Height="30px" Width="170px">
                    </asp:DropDownList>
                   

                    <br />
                    <p>Marca Cpu</p>

                    <asp:DropDownList ID="DropDownList2" runat="server" Height="31px" Width="175px">
                    </asp:DropDownList>
                    <br />

                    <p>Modelo</p>
                   
                        <asp:TextBox ID="txtModelo" runat="server" Width="165px"></asp:TextBox>
         
                    <p>Descripcion</p>
                    
                        <asp:TextBox ID="txtDescripcion" runat="server" Width="164px"></asp:TextBox>
                  
                    <p>Tipo Ram</p>
                    
                        <asp:DropDownList ID="DropDownList3" runat="server" Height="30px" Width="187px">
                        </asp:DropDownList>
                    
                    <p>Gabinete</p>

                    <asp:DropDownList ID="DropDownList4" runat="server" Height="31px" Width="191px">
                    </asp:DropDownList>


                    <br />
                    <br />
                    <asp:Button ID="btnInsertar" class="btn btn-dark" runat="server" Text="Insertar CPU Generico" OnClick="btnInsertar_Click" />


                </div>
                <div class="col">
                    <p>&nbsp;</p>
                    <p>Selecciona un Cpu Generico para modificar o elimnar</p>
                    

                    <asp:DropDownList ID="DropDownList5" runat="server" Height="31px" Width="321px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Button ID="btnModificar" class="btn btn-dark" runat="server" Text="Modificar" Width="100px" OnClick="btnModificar_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnEliminar" class="btn btn-dark" runat="server" Text="Eliminar" Width="126px" OnClick="btnEliminar_Click" />
                    <br />             
                </div>
            </div>
            <div class="row align-items-center">
                <div class="col">

                    <br />
                    <asp:Button ID="btnMostrar" class="btn btn-dark" runat="server" Text="Mostrar" Width="324px" OnClick="btnMostrar_Click" />
                    <br />
                    <br />
                    <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Height="159px" Width="780px">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <br />
                    <br />
                    <asp:TextBox ID="txtResultado" runat="server" Width="594px"></asp:TextBox>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
