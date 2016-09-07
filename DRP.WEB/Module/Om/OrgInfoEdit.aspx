<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrgInfoEdit.aspx.cs" Inherits="DRP.WEB.Module.Om.OrgInfoEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script type="text/ecmascript">
        $(function () {
            initComboTree();
            var v = $("#AreaID").val();
            if (v != "") {
                $('#AreaName').combotree('setValue', v);
            }

            $("#btnSave").click(function () {
                var val = $('#AreaName').combotree('getValue');
                if (val == "") {
                    Alert("请选择区域");
                    return false;
                }
                $("#AreaID").val(val);
                return $('#form1').form('validate');  
            }); 
        });
        function initComboTree() {
            $('#AreaName').combotree({
                url: "Service/OmArea.ashx?action=3&r=" + getRand()                 
            }); 
        }        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel"><span class="red">*</span>机构名称：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="Name" Width="250" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
                  <tr>
                    <td class="rowlabel">品牌或简称：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="Brand" Width="250"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">产品名称：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="ProName" Width="250"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">机构性质：
                    </td>
                    <td>
                       <asp:TextBox runat="server" ClientIDMode="Static" Width="250" ID="OrgProp"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>发布网址：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" Width="250" ID="ProDomain" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>机构区域：
                    </td>
                    <td>
                        <asp:HiddenField ID="AreaID" runat="server" />
                        <asp:TextBox runat="server" Height="26" Width="250" ClientIDMode="Static" ID="AreaName"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>使用有效期：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" onclick="WdatePicker()" EnableTheming="false" CssClass="textbox Wdate" Style="width: 110px; height:26px;" ID="OpenDate" data-options="required:true"></asp:TextBox>
                        ~
                         <asp:TextBox runat="server" ClientIDMode="Static" onclick="WdatePicker()" EnableTheming="false" CssClass="textbox Wdate" Style="width: 110px; height:26px;" ID="ExpiryDate" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>使用状态：
                    </td>
                    <td>
                        <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="DataStatus">
                            <asp:ListItem Text="启用" Selected="True" Value="1"></asp:ListItem>
                            <asp:ListItem Text="禁用" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>应用导航组：
                    </td>
                    <td>
                        <asp:DropDownList ID="NavGroupID" runat="server" Width="150" AppendDataBoundItems="true" data-options="required:true">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>机构联系人：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="OrgContact" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>联系电话：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="ContactPhone" data-options="required:true"></asp:TextBox>
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
