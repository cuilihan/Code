<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VenueEdit.aspx.cs" Inherits="DRP.WEB.Module.Pro.VenueEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>集合地点设置</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer">
            <table class="tblEdit">  
                <tr>
                    <td class="rowlabel"><span class="red">*</span>出发地：
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="DepartureID" AppendDataBoundItems="true" data-options="required:true">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                 <tr>
                    <td class="rowlabel">线路类型：
                    </td>
                    <td>
                        <asp:CheckBoxList ID="RouteTypeID" RepeatDirection="Horizontal" data-options="required:true" runat="server"></asp:CheckBoxList> 
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>集合地点：
                    </td>
                    <td>
                        <asp:TextBox runat="server" Width="250" ClientIDMode="Static" ID="Name" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td class="rowlabel">集合时间：
                    </td>
                    <td>
                        <asp:TextBox runat="server" Width="250" ClientIDMode="Static" ID="MeetTime" ></asp:TextBox>
                    </td>
                </tr>
               
                <tr>
                    <td class="rowlabel">接加价：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="PickAmt" CssClass="checkInt" data-options="validType:'int'" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">送加价：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="SendAmt" CssClass="checkInt" data-options="validType:'int'" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
                
                <tr>
                    <td class="rowlabel">排序号：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="OrderIndex" CssClass="checkInt textbox" EnableTheming="false" Style="width: 40px; height: 26px;" ClientIDMode="Static"></asp:TextBox>
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
