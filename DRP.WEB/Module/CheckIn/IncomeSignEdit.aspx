<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IncomeSignEdit.aspx.cs" Inherits="DRP.WEB.Module.CheckIn.IncomeSignEdit" %>

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
                    <td class="rowlabel"><span class="red">*</span>收入金额：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="IncomeAmt" data-options="required:true,validType:'float'"></asp:TextBox>
                    </td>

                    <td class="rowlabel"><span class="red">*</span>收入日期：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" onclick="WdatePicker()" ID="IncomeDate" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>收入类型：
                    </td>
                    <td>
                        <asp:DropDownList ID="IncomeTypeID" data-options="required:true" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="rowlabel"><span class="red">*</span>收入部门：
                    </td>
                    <td>
                        <asp:DropDownList ID="DeptID" data-options="required:true" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>收入方式：
                    </td>
                    <td>
                        <asp:DropDownList ID="IncomeMethod" data-options="required:true" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="rowlabel">经办人：</td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="Operator"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td class="rowlabel">收入来源：
                    </td>
                    <td colspan="3">
                        <asp:TextBox runat="server" Width="80%" ClientIDMode="Static" ID="IncomeSource"></asp:TextBox>
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
