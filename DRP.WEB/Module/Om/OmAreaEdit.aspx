<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OmAreaEdit.aspx.cs" Inherits="DRP.WEB.Module.Om.OmAreaEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>行政区域维护</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel">上级区域名称：
                    </td>
                    <td>
                        <asp:Literal ID="lblNavName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>区域名称：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="AreaName" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">排序号：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="OrderIndex" CssClass="checkInt textbox" EnableTheming="false" Style="width: 40px;height:26px;" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div class="statusbar">
            <asp:LinkButton runat="server" ID="btnSave" CssClass="easyui-linkbutton" iconCls="icon-save" Text="保存" ClientIDMode="Static" OnClick="btnSave_Click" OnClientClick="return $('#form1').form('validate');">
            </asp:LinkButton>
        </div>
    </form>
</body>
</html>
