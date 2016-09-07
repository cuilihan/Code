<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerEdit.aspx.cs" Inherits="DRP.WEB.Module.Crm.CustomerEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>客户基本资料维护</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script src="Script/CustomerEdit.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            navTab("ul_tabs", "tab_conbox");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" class="formedit">
        <div class="tips_wrap hide" id="tipWraper">当前客户的手机号码已存在，客户手机号须唯一！</div>
        <asp:Panel runat="server" ID="pnlContainer" Style="padding: 10px;">
            <table class="tblInfo">
                <tr>
                    <td class="rowlabel"><span class="red">*</span>客户名称：
                    </td>
                    <td>
                        <asp:TextBox ID="Name" ClientIDMode="Static" runat="server" data-options="required:true"></asp:TextBox>
                    </td>
                    <td class="rowlabel">英文名称：
                    </td>
                    <td>
                        <asp:TextBox ID="EngName" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                    <td class="rowlabel">性别：
                    </td>
                    <td>
                        <asp:RadioButtonList ID="Sex" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal" Width="150">
                            <asp:ListItem Text="男" Value="男"></asp:ListItem>
                            <asp:ListItem Text="女" Value="女"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>手机号码：
                    </td>
                    <td>
                        <asp:TextBox ID="Mobile" runat="server" ClientIDMode="Static" data-options="validType:'mobile'"></asp:TextBox>&nbsp;&nbsp<asp:CheckBox ID="haveMobile" runat="server" />无手机号&nbsp;&nbsp;<span class="red">（注：若不填写手机号，在订单添加客户中将不显示该客户）</span>

                    </td>
                    <td class="rowlabel">办公电话：
                    </td>
                    <td>
                        <asp:TextBox ID="Phone" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                    <td class="rowlabel">传真号码：
                    </td>
                    <td>
                        <asp:TextBox ID="Fax" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">身份证号：
                    </td>
                    <td>
                        <asp:TextBox ID="IDNum" runat="server" ClientIDMode="Static" data-options="validType:'idcard'"></asp:TextBox>
                    </td>
                    <td class="rowlabel">客户类型：
                    </td>
                    <td>
                        <asp:DropDownList ID="CustomerType" ClientIDMode="Static" AppendDataBoundItems="true" EnableTheming="false" Style="width: 150px; height: 26px;" CssClass="textbox" runat="server">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="rowlabel">QQ：
                    </td>
                    <td>
                        <asp:TextBox ID="QQ" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">公司名称：
                    </td>
                    <td>
                        <asp:TextBox ID="Company" ClientIDMode="Static" Width="300" runat="server"></asp:TextBox>   
                    </td>
                    <td class="rowlabel">客户生日：
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="BirthDay" ClientIDMode="Static" runat="server"  onclick="WdatePicker()" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="padding: 10px;">
                        <ul class="ul_tabs" id="ul_tabs">
                            <li><a>销售线索</a></li>
                            <li><a>其他信息</a></li>
                        </ul>
                        <ul id="tab_conbox">
                            <li class="tab_con">
                                <table class="tblItem">
                                    <tr>
                                        <th style="width: 25px;">序
                                        </th>
                                        <th style="width: 150px;">销售线索名称
                                        </th>
                                        <th style="width: 80px;">类型</th>
                                        <th style="width: 100px;">联系人</th>
                                        <th style="width: 100px;">预计成交日期</th>
                                        <th>备注</th>
                                        <th style="width: 70px">创建人</th>
                                        <th style="width: 50px;"></th>
                                    </tr>
                                    <tbody id="tblItem">
                                        <asp:Repeater runat="server" ID="rptTrace" OnItemDataBound="rptTrace_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center;">
                                                        <asp:Literal ID="lblNo" runat="server"></asp:Literal>
                                                        <input type="hidden" value='<%# Eval("ID") %>' />
                                                    </td>
                                                    <td>
                                                        <input type="text" class="textbox" style="width: 140px; height: 26px;" value='<%# Eval("ItemName") %>' /></td>
                                                    <td>
                                                        <asp:Literal ID="lblItemType" runat="server"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <input type="text" class="textbox" style="width: 90px; height: 26px;" value='<%# Eval("Contact") %>' /></td>
                                                    <td>
                                                        <asp:Literal ID="lblTradeDate" runat="server"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <input type="text" class="textbox" style="width: 98%; height: 26px;" value='<%# Eval("Comment") %>' /></td>
                                                    <td style="text-align: center;">
                                                        <%# Eval("CreateUserName") %> </td>
                                                    <td style='text-align: center;'><a href='javascript:;' onclick="c.fnTraceDelete('<%# Eval("ID") %>',this)">删除</a></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <tr>
                                        <td colspan="8" style="text-align: right;">
                                            <a href="javascript:;" id="btnAddItem" class="easyui-linkbutton" iconcls="icon-add">添加一个销售线索</a>
                                        </td>
                                    </tr>
                                </table>
                            </li>
                            <li class="tab_con">
                                <table class="tblEdit">
                                    <tr>
                                        <th style="width: 200px;">证件类型
                                        </th>
                                        <th>证件号码</th>
                                        <th style="width: 40px;"></th>
                                    </tr>
                                    <tbody id="tblIDInfo">
                                        <asp:Repeater runat="server" ID="rptCertificate">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <input type='text' style='height: 26px; width: 190px;' class='textbox' value='<%# Eval("ItemType") %>' /></td>
                                                    <td>
                                                        <input type='text' style='height: 26px; width: 90%;' class='textbox' value='<%# Eval("ItemVal") %>' /></td>
                                                    <td><a href='javascript:;' onclick="c.fnIDDelete(this)">删除</a></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <tr>
                                        <td colspan="3" style="text-align: right; padding-right: 10px;">
                                            <a href="javascript:;" id="btnIDAdd" class="easyui-linkbutton" iconcls="icon-add">增加证件类型</a>
                                        </td>
                                    </tr>
                                </table>
                                <p>
                                    地址：  
                                <asp:TextBox ID="Addr" ClientIDMode="Static" Width="100%" Height="26" EnableTheming="false" CssClass="textbox" runat="server"></asp:TextBox>
                                </p>
                                <p>
                                    备注：   
                                <asp:TextBox ID="Comment" ClientIDMode="Static" Style="overflow: auto;" TextMode="MultiLine" Width="100%" Height="120" EnableTheming="false" CssClass="textbox" runat="server"></asp:TextBox>
                                </p>
                            </li>
                        </ul>
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
