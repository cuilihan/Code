<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="DRP.WEB.Module.Sys.UserEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户信息维护</title>
    <script type="text/ecmascript">
        $(function () {
            initDept();
            setDept();

            $("#btnSave").click(function () {
                var val = $('#DeptID').combotree('getValue');
                if (val == "") {
                    Alert("请选择部门");
                    return false;
                }
                else {
                    $("#hfDeptID").val(val);
                }
                var pDept = $("#PartDeptID").combotree("getValue");
                if (pDept != "") {
                    $("#hfPartDeptID").val(pDept);
                }
                return $('#form1').form('validate');
            });
        });
        function initDept() {
            var url = "Service/User.ashx?action=3";
            $("#DeptID").combotree({ "url": url });
            $("#PartDeptID").combotree({ "url": url });
        }
        function setDept() {
            var v = $("#hfDeptID").val();
            if (v != "") {
                $('#DeptID').combotree('setValue', v);
            }
            v = $("#hfPartDeptID").val();
            if (v != "") {
                $('#PartDeptID').combotree('setValue', v);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer">
            <table class="tblEdit">
                <tr>
                    <td colspan="4">1.基本信息
                        <span style="float: right;">
                            <asp:Literal runat="server" ID="UserCountInfo"></asp:Literal>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>用户姓名：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="Name" data-options="required:true"></asp:TextBox>
                    </td>

                    <td class="rowlabel">身份证号：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="IDNo" data-options="validType:'idcard'"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>用户部门：
                    </td>
                    <td>
                        <asp:TextBox runat="server" Height="26" ClientIDMode="Static" ID="DeptID"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="hfDeptID" ClientIDMode="Static" />
                    </td>

                    <td class="rowlabel">兼职部门：
                    </td>
                    <td>
                        <asp:TextBox runat="server" Height="26" ClientIDMode="Static" ID="PartDeptID"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="hfPartDeptID" ClientIDMode="Static" />
                    </td>
                </tr>

                <tr>
                    <td class="rowlabel"><span class="red">*</span>手机号：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="Mobile" data-options="required:true,validType:'mobile'"></asp:TextBox>
                    </td>

                    <td class="rowlabel">电子邮件：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="Email" data-options="validType:'email'"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">QQ：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="QQ"></asp:TextBox>
                    </td>
                    <td class="rowlabel"></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="4">2.账号信息
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>用户账号：
                    </td>
                    <td>

                        <asp:TextBox runat="server" ClientIDMode="Static" ID="AcctID" data-options="required:true"></asp:TextBox>
                    </td>

                    <td class="rowlabel">账号密码：
                    </td>
                    <td>
                        <asp:HiddenField runat="server" ID="AcctPwd" ClientIDMode="Static" />
                        <asp:TextBox runat="server" ClientIDMode="Static" TextMode="Password" ID="txtAcctPwd"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span> 账号状态：
                    </td>
                    <td>
                        <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="DataStatus">
                            <asp:ListItem Text="启用" Selected="True" Value="1"></asp:ListItem>
                            <asp:ListItem Text="禁用" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td colspan="2" style="padding-left: 55px;">
                        <asp:Literal runat="server" ID="lbTips" Text="更新用户信息且密码为空时，系统不更新用户密码。" Visible="false"></asp:Literal>
                    </td>
                </tr>

            </table>
        </asp:Panel>
        <div style="text-align: center;">
            <asp:LinkButton runat="server" ID="btnSave" CssClass="easyui-linkbutton" iconCls="icon-save" Text="保存" ClientIDMode="Static" OnClick="btnSave_Click" OnClientClick="return $('#form1').form('validate');">
            </asp:LinkButton>
            <div style="color: red; padding-top: 5px;">
                <asp:Label runat="server" ID="lblTips" Text=""></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
