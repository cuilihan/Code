<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleEdit.aspx.cs" Inherits="DRP.WEB.Module.Sys.RoleEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑角色</title>
    <script src="Script/RoleEdit.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel_100"><span class="red">*</span>角色名称：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" Width="250" ID="RoleName" data-options="required:true"></asp:TextBox>
                    </td>
                    <td rowspan="3" valign="top">
                        <div style="width: 320px; border-left: 1px solid #ccc; margin-left: 10px; padding: 0px 10px 0px 10px;">
                            <div style="padding-bottom: 5px; border-bottom: 1px solid #ccc; font-weight: bold;">
                                按部门选择用户  
                                <input id="txtDept" style="height:25px; width:200px;" class="textbox" />
                            </div>
                            <div style="overflow: auto;">
                                <table>
                                    <tr>
                                        <td valign="top">
                                            <div style="text-align: center;margin-bottom:3px;">待选择用户</div>
                                            <asp:ListBox runat="server" ID="SrcUser" Style="height: 275px; padding-top:5px; overflow:auto; width: 120px;" ClientIDMode="Static" EnableTheming="false" CssClass="textbox"></asp:ListBox>
                                        </td>
                                        <td style="width: 20px;">
                                            <a class="lst_toright_single" title="选择用户"></a>
                                            <a class="lst_toright_all" title="选择全部用户"></a>
                                          
                                            <a class="lst_toleft_single" title="选择用户" style="margin-top:20px;"></a>
                                            <a class="lst_toleft_all" title="选择用户"></a>
                                            
                                        </td>
                                        <td valign="top">
                                            <div style="text-align: center;margin-bottom:3px;">已选择用户</div>
                                            <asp:ListBox runat="server" ID="DesUser" Style="height: 275px; padding-top:5px; overflow:auto; width: 120px;" ClientIDMode="Static" EnableTheming="false" CssClass="textbox"></asp:ListBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel_100">备注说明：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="Comment" EnableTheming="false" TextMode="MultiLine" Style="width: 250px; height: 300px; overflow:auto; " ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>

            </table>
        </asp:Panel>
        <div style="text-align: center;">
            <a href="javascript:;" class="easyui-linkbutton" iconcls="icon-save" id="btnSave">保存</a>
        </div>
    </form>
</body>
</html>
