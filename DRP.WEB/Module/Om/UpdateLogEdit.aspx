<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateLogEdit.aspx.cs" Inherits="DRP.WEB.Module.Om.UpdateLogEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel"><span class="red">*</span>更新内容：
                    </td>
                    <td>
                        <asp:TextBox runat="server" EnableTheming="false" CssClass="textbox" Height="150" ClientIDMode="Static" ID="Summary" TextMode="MultiLine" Width="96%" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td class="rowlabel"><span class="red">*</span>更新类型：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="xType" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>更新日期：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" onclick="WdatePicker()" ID="CreateDate" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td class="rowlabel"><span class="red">*</span>更新人：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="CreateUserName" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div style="text-align: center;">
            <asp:LinkButton runat="server" ID="btnSave" CssClass="easyui-linkbutton" iconCls="icon-save" Text="保存" ClientIDMode="Static" OnClick="btnSave_Click">
            </asp:LinkButton>
        </div>
    </form>
</body>
</html>
