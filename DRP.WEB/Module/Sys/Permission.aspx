<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="Permission.aspx.cs" Inherits="DRP.WEB.Module.Sys.Permission" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style type="text/css">
        #chkDataPermissionID label {
            display: inline-block;
            margin-left: 6px;
            height: 20px;
        }

        #chkDataPermissionID input {
            border: 1px solid #ccc;
        }
    </style>
    <title>权限管理</title>
    <link href="../../Scripts/Plugin/zTree/zTreeStyle.css" rel="stylesheet" />
    <script src="../../Scripts/Plugin/zTree/jquery.ztree.core-3.5.js"></script>
    <script src="../../Scripts/Plugin/zTree/jquery.ztree.excheck-3.5.js"></script>
    <script src="Script/Permission.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="easyui-panel" iconcls="icon-reload" title="权限管理" style="padding: 10px;">
            <div style="padding-left: 20px;">
                <a href="javascript:;" id="btnSave" class="easyui-linkbutton" iconcls="icon-save">保存权限</a>
            </div>
            <table>
                <tr>
                    <td valign="top" style="width: 160px; padding-right: 20px; padding-left: 10px;">
                        <div class="line" style="width: 150px;">
                            角色选择
                        </div>
                        <div id="rdRoleID">
                            <asp:RadioButtonList Width="200" Style="line-height: 26px;" runat="server" ID="rdRole" CssClass="textbox">
                            </asp:RadioButtonList>
                        </div>

                    </td>
                    <td valign="top" style="width: 200px; padding-right: 20px; padding-left: 10px;">
                        <div class="line">
                            数据权限设定(<span style="color: red;">默认私有权限</span>)
                        </div>
                        <div id="rdDataPermissionID">
                            <asp:RadioButtonList runat="server" Style="line-height: 26px;" Width="200" ID="rdDataPermission" CssClass="textbox">
                                <asp:ListItem Text="私有数据权限" Selected="True" Value="0"></asp:ListItem>
                                <asp:ListItem Text="部门数据权限" Value="1"></asp:ListItem>
                                <asp:ListItem Text="所有数据权限" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="line" style="margin-top: 20px;">
                            客户数据权限(<span style="color: red;">默认不具备操作权限</span>)
                        </div>
                        <div id="chkCrmPermissionID">
                            <asp:CheckBoxList runat="server" Style="line-height: 26px;" Width="200" ID="chkCrmPermission" CssClass="textbox"> 
                                <asp:ListItem Text="导出数据" Value="2"></asp:ListItem>
                                <asp:ListItem Text="删除数据" Value="4"></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                        <div class="line" style="margin-top: 20px;">
                            订单权限设定
                        </div>
                        <div id="chkDataPermissionID">
                            <asp:CheckBoxList runat="server" Width="200" ID="chkOrderPermission" CssClass="textbox">
                            </asp:CheckBoxList>
                        </div>
                        <div class="line" style="margin-top: 20px;">
                            首页数据模块显示设定
                        </div>
                        <div id="chkDataModuleID">
                            <asp:CheckBoxList runat="server" Style="line-height: 26px;" Width="200" ID="chkDataModule" CssClass="textbox"> 
                                <asp:ListItem Text="首页数据模块显示" Value="9"></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </td>
                    <td valign="top" style="padding-left: 20px;">
                        <div class="line">
                            功能权限设定
                            <div style="float: right; font-weight: normal;">
                                <a href="javascript:;" id="btnSelectAll">全选</a>
                                &nbsp;&nbsp;
                                 <a href="javascript:;" id="btnUnSelectAll">反选</a>
                                &nbsp;&nbsp;
                                <a href="javascript:;" id="btnCollapse">折叠</a>
                                &nbsp;&nbsp;
                                <a href="javascript:;" id="btnExpand">展开</a>
                            </div>
                        </div>
                        <div id="navPermissionID" style="border: 1px solid #ccc; overflow: auto; width: 260px;">
                            <ul style="clear: both;" id="treeData" class="ztree">
                            </ul>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
