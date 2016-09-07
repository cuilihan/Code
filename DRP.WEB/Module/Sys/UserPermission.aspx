<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPermission.aspx.cs" Inherits="DRP.WEB.Module.Sys.UserPermission" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户的权限查询</title>
    <script src="../../Scripts/Plugin/zTree/jquery.ztree.core-3.5.js"></script>
    <link href="../../Scripts/Plugin/zTree/zTreeStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            p.queryNavPermission();
            p.queryDataPermisson();
            p.queryOrderPermisson();
        });

        var p = {
            serverURL: "Service/User.ashx?uid=" + request("id") + "&r=" + getRand(),
            queryNavPermission: function (userID) {
                var setting = {
                    data: {
                        simpleData: {
                            enable: true
                        }
                    }
                };
                var u = p.serverURL + "&action=4";
                dataService.ajaxGet(u, function (data) {
                    if (data != "") {
                        var zNodes = [];
                        $(eval(data)).each(function () {
                            var node = { id: this.ID, pId: this.ParentID, name: this.NavName };
                            zNodes.push(node);
                        });
                        $.fn.zTree.init($("#treeData"), setting, zNodes);
                    }
                });
            },
            queryDataPermisson: function (userID) {
                var u = p.serverURL + "&action=5";
                dataService.ajaxGet(u, function (data) {
                    $("#DataPermission").html(data);
                });
            },
            queryOrderPermisson: function (userID) {
                var u = p.serverURL + "&action=6";
                dataService.ajaxGet(u, function (data) {
                    $("#OrderPermisson").html(data);
                });
            }
        };

    </script>
</head>
<body>
    <form id="form1" runat="server" style="padding: 6px;">
        <table class="tblEdit">
            <tr>
                <th colspan="3">所属角色：<asp:Literal ID="RoleName" runat="server"></asp:Literal>
                </th>
            </tr>
            <tr>
                <th style="width: 200px;">功能栏目权限
                </th>
                <th>数据操作权限
                </th>
                <th>订单查询权限（目的地）
                </th>
            </tr>
            <tr>
                <td valign="top" id="NavPermission">
                    <ul style="clear: both;" id="treeData" class="ztree">
                        <li>正在查询...</li>
                    </ul>
                </td>
                <td valign="top" style="padding: 10px; color: Red; font-weight: bold;" id="DataPermission">正在查询...
                </td>
                <td valign="top" id="OrderPermisson">正在查询...
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
