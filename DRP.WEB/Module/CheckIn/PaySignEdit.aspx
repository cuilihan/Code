<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaySignEdit.aspx.cs" Inherits="DRP.WEB.Module.CheckIn.PaySignEdit" %>

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
                    <td class="rowlabel"><span class="red">*</span>支出金额：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="PayAmt" data-options="required:true,validType:'float'"></asp:TextBox>
                    </td>

                    <td class="rowlabel"><span class="red">*</span>支出日期：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" onclick="WdatePicker()" ID="PayDate" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>支出类型：
                    </td>
                    <td>
                        <asp:DropDownList ID="PayTypeID" data-options="required:true" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="rowlabel"><span class="red">*</span>支出部门：
                    </td>
                    <td>
                        <asp:DropDownList ID="DeptID" data-options="required:true" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>支出方式：
                    </td>
                    <td>
                        <asp:DropDownList ID="PayMethod" data-options="required:true" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="rowlabel">经办人：</td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="Operator"></asp:TextBox>
                    </td>
                </tr>
 
                <tr>
                    <td class="rowlabel">备注：
                    </td>
                    <td colspan="3">
                        <asp:TextBox runat="server" Width="80%" ClientIDMode="Static" ID="Comment" EnableTheming="false" TextMode="MultiLine" CssClass="textbox" Height="60"></asp:TextBox>
                    </td>
                </tr>

            </table>
        </asp:Panel>
        <div style="text-align: center;">
            <asp:LinkButton runat="server" ID="btnSave" CssClass="easyui-linkbutton" iconCls="icon-save" Text="保存" ClientIDMode="Static" OnClick="btnSave_Click" OnClientClick="return $('#form1').form('validate');">
            </asp:LinkButton>
        </div>
    </form>
</body>
</html>
