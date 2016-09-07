<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NavGroupEdit.aspx.cs" Inherits="DRP.WEB.Module.Om.NavGroupEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../Scripts/Plugin/zTree/zTreeStyle.css" rel="stylesheet" />
    <script src="../../Scripts/Plugin/zTree/jquery.ztree.core-3.5.js"></script>
    <script src="../../Scripts/Plugin/zTree/jquery.ztree.excheck-3.5.js"></script>
    <script src="Script/NavGroupEdit.js" type="text/javascript"></script>
    <title>导航组管理</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel_100"><span class="red">*</span>导航组名称：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="NavGroup" data-options="required:true,validType:'chinese'"></asp:TextBox>
                    </td>
                    <td rowspan="3" valign="top">
                        <div style="width: 250px; margin-left: 10px; padding: 0px 10px 0px 10px;">
                            <div class="line">
                                功能菜单选择  
                                <a style="font-weight:normal; font-size:12px; display:inline-block; margin-left:80px;" href="javascript:;" id="btnSelectAll">全选</a>
                                <a style="font-weight:normal; font-size:12px; display:inline-block; margin-left:5px;" href="javascript:;" id="btnUnSelect">反选</a>
                            </div>
                            <div style="height: 280px; overflow: auto;">
                                <ul style="clear: both;" id="treeData" class="ztree">
                                </ul>
                            </div>
                        </div>

                    </td>
                </tr>
                <tr>
                    <td class="rowlabel_100">备注说明：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="Comment" EnableTheming="false"  TextMode="MultiLine" Style="width: 250px; height: 250px; overflow: auto;" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel_100">排序号：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="OrderIndex" CssClass="checkInt textbox" EnableTheming="false" Style="width: 40px; height:26px;" ClientIDMode="Static"></asp:TextBox>
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
