<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FavoritesEdit.aspx.cs" Inherits="DRP.WEB.Module.My.FavoritesEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel"><span class="red">*</span>链接名称：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="Name" Width="250" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>链接地址：
                    </td>
                    <td>
                        <asp:TextBox runat="server" Text="http://www." ClientIDMode="Static" ID="URL" Width="250" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">排序：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="OrderIndex"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div style="text-align: center;">
            <asp:LinkButton runat="server" ID="btnSave" OnClientClick="return $('#form1').form('validate');" CssClass="easyui-linkbutton" iconCls="icon-save" Text="保存" ClientIDMode="Static" OnClick="btnSave_Click">
            </asp:LinkButton>
        </div>
    </form>
</body>
</html>
