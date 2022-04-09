<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormGabinete.aspx.cs" Inherits="WebInventarioParte1.WebFormGabinete" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- CSS only -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous"/>
    <!-- JavaScript Bundle with Popper -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
    <title>Gabinetes</title>
</head>
<body>
    <ul class="nav nav-tabs" id="myTab" role="tablist" style="background:#b6b6b6">
        <li class="nav-item" role="presentation">
            <a class="nav-link active"  href="WebFormGabinete.aspx" type="button" role="tab"  aria-selected="true">Gabinete</a>
        </li>
        <li class="nav-item" role="presentation">
            <a class="nav-link"  href="WebFormRAM.aspx" type="button" role="tab"  aria-selected="false">RAM</a>
        </li>
        <li class="nav-item" role="presentation">
            <a class="nav-link"  href="WebFormCPU_Generico.aspx" role="tab" aria-selected="false">CPU Generico</a>
        </li>
    </ul>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row align-items-center">
                <div class="col">
                    <br />
                    <br />
                    <p>Modelo</p>       
        
                    <asp:TextBox ID="txtModeloGabinete" runat="server"></asp:TextBox>
       
                    <p>Tipo de Forma</p>
       
                    <asp:TextBox ID="txtForma" runat="server"></asp:TextBox>
       
                    <p>Marca</p>
                    
                    <asp:DropDownList ID="DropDownList1" runat="server" Height="40px" Width="128px">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Button ID="btnInsertar" class="btn btn-dark" runat="server" OnClick="btnInsertar_Click" Text="Insertar Gabinete" Width="149px" />
                </div>
                <div class="col">
                    <p>Seleccione un Gabinete para modificar o eliminar</p>            

                    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" Height="40px" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" Width="216px">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Button ID="btnModificar" class="btn btn-dark" runat="server" OnClick="btnModificar_Click" Text="Modificar" Width="91px" />
                    <asp:Button ID="btnEliminar" class="btn btn-dark" runat="server" Text="Eliminar" Width="93px" OnClick="btnEliminar_Click" />

                </div>
            </div>
            <div class="row align-items-center">
                <div class="col">
                    <br />
                    <br />
                    <asp:GridView ID="GridView1" runat="server" Width="340px" CellPadding="4" ForeColor="#333333" GridLines="None" Height="239px">
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

                    <asp:Button ID="btnMostrar" class="btn btn-dark" runat="server" OnClick="btnMostrar_Click" Text="Mostrar" Width="202px" />
                    <br />
                    <br />
                    <asp:TextBox ID="txtResultado" runat="server" Width="455px"></asp:TextBox>
                </div>
            </div>
        </div>
    </form>
   
</body>
</html>
