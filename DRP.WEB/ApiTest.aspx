<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApiTest.aspx.cs" Inherits="DRP.WEB.ApiTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
            <tr>
                <td>账号：
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                <td>密码：
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                <td>APPID：
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                <td>APPKey：
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Sign：
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td>
                <td>expires：
                    <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
                <td>url：
                    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox></td>
                <td>app：
                 <asp:DropDownList ID="DropDownList1" runat="server">
                     <asp:ListItem Value="PackageBuyerStoreDetail" Text="门店详情接口"></asp:ListItem>
                     <asp:ListItem Value="PackageDestinationList" Text="目的地接口"></asp:ListItem>
                     <asp:ListItem Value="PackageBuyerStoreUserList" Text="10	门店用户接口"></asp:ListItem>
                 </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>时间戳的必填参数：
                    <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox></td>
                <td>其他参数：
                    <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox></td>
                <td colspan="2">
                    <asp:Button ID="Button1" runat="server" Text="执行" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>

        <asp:Literal ID="lblData" runat="server"></asp:Literal>
        <div id="resData" runat="server" style="border: 1px solid #808080; background: #f0f0f0; padding: 50px; margin: 20px">
        </div>
        <div style="margin-top:30px;">
            <fieldset>
                <legend>接收订单接口测试(使用上述的AppId与主账号)</legend>
                Api地址：
                <asp:TextBox runat="server" ID="ApiHost" Width="100%" Text="http://localhost:8002/33ly/ordersync"></asp:TextBox>
                订单XML：
                <asp:TextBox runat="server" ID="XmlData" TextMode="MultiLine" Width="96%" Height="100"></asp:TextBox>
                <asp:LinkButton runat="server" ID="btnSyncOrder" OnClick="btnSyncOrder_Click" Text="执行"></asp:LinkButton>
            </fieldset>
        </div>
    </form>
</body>
</html>
