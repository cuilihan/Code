<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShoppingEdit.aspx.cs" Inherits="DRP.WEB.Module.Res.ShoppingEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>购物店维护</title>
    <script src="Script/ResourceUtility.js" type="text/javascript"></script>
    <script src="Script/ShoppingEdit.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer" Style="padding: 10px;">
            <table class="tblInfo">
                <tr>
                    <td class="rowlabel"><span class="red">*</span>购物店地区：</td>
                    <td colspan="5">
                        <asp:HiddenField ID="hfRouteTypeID" ClientIDMode="Static" runat="server" />
                        <asp:HiddenField ID="hfDestinationID" ClientIDMode="Static" runat="server" />
                        <input class="easyui-combotree" id="RouteTypeID" style="width: 100px; height: 26px;" />
                        <input class="easyui-combotree" id="DestinationID" style="width: 150px; height: 26px;" />
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>购物店名称：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="Name" Width="300" ClientIDMode="Static" runat="server" data-options="required:true"></asp:TextBox>
                    </td>

                    <td class="rowlabel">状态：
                    </td>
                    <td>
                        <asp:DropDownList ID="IsEnable" ClientIDMode="Static" runat="server">
                            <asp:ListItem Text="启用" Value="1"></asp:ListItem>
                            <asp:ListItem Text="禁用" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">负责人：
                    </td>
                    <td>
                        <asp:TextBox ID="Contact" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>

                    <td class="rowlabel">手机号码：
                    </td>
                    <td>
                        <asp:TextBox ID="Mobile" runat="server" ClientIDMode="Static"></asp:TextBox>
                    </td>
                    <td class="rowlabel">联系电话：
                    </td>
                    <td>
                        <asp:TextBox ID="Phone" runat="server" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>


                <tr>
                    <td class="rowlabel">地址：
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="Addr" Width="90%" runat="server" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">备注说明：
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="Remark" TextMode="MultiLine" EnableTheming="false" CssClass="textbox" Height="60" Width="90%" runat="server" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="padding: 10px;">
                        <ul class="ul_tabs" id="ul_tabs">
                            <li class="thistab"><a>业务联系人</a></li>
                        </ul>
                        <div style="padding: 10px;">
                            <table class="tblItem">
                                <tr>
                                    <th style="width: 25px;">序
                                    </th>
                                    <th style="width: 100px;">姓名
                                    </th>
                                    <th style="width: 150px;">电话</th>
                                    <th style="width: 150px;">传真</th>
                                    <th>备注</th>
                                    <th style="width: 50px;"></th>
                                </tr>
                                
                                <tbody id="tblItem">
                                    <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:Literal ID="lblNo" runat="server"></asp:Literal>
                                                </td>
                                                <td>
                                                    <input type="text" class="textbox" style="width: 90px; height: 26px;" value='<%# Eval("Name") %>' /></td>
                                                <td>
                                                    <input type="text" class="textbox" style="width: 140px; height: 26px;" value='<%# Eval("Phone") %>' /></td>
                                                <td>
                                                    <input type="text" class="textbox" style="width: 140px; height: 26px;" value='<%# Eval("Fax") %>' />
                                                </td>
                                                <td>
                                                    <input type="text" class="textbox" style="width: 98%; height: 26px;" value='<%# Eval("Remark") %>' /></td>

                                                <td style='text-align: center;'><a href='javascript:;' onclick="res.fnDeleteItem(this)">删除</a></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                                 <tr>
                                    <td colspan="6" style="text-align:right;">
                                        <a href="javascript:;" id="btnAddItem" class="easyui-linkbutton" iconcls="icon-add">添加一个业务联系人</a>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="7" style="text-align: center; margin: 20px;">
                        <a href="javascript:;" class="easyui-linkbutton" id="btnSave" iconcls="icon-save">保存</a>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
