<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TravalAgencyEdit.aspx.cs" Inherits="DRP.WEB.Module.Res.TravalAgencyEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>综合供应商维护</title>
    <script src="Script/ResourceUtility.js" type="text/javascript"></script>
    <script src="Script/TravelAgencyEdit.js" type="text/javascript"></script>
    <script src="../Order/Script/OrderUtility.js" type="text/javascript"></script>
    <script src="../../Scripts/Plugin/Uploadify/jquery.uploadify-3.1.min.js" type="text/javascript"></script>
    <link href="../../Scripts/Plugin/Uploadify/uploadify.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer" Style="padding: 10px;">
            <table class="tblInfo">
                <tr>
                    <td class="rowlabel"><span class="red">*</span>区域：</td>
                    <td colspan="3">
                        <asp:HiddenField ID="hfRouteTypeID" ClientIDMode="Static" runat="server" />
                        <asp:HiddenField ID="hfDestinationID" ClientIDMode="Static" runat="server" />
                        <input class="easyui-combotree" id="RouteTypeID" style="width: 100px; height: 26px;" />
                        <input class="easyui-combotree" id="DestinationID" style="width: 150px; height: 26px;" />
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
                    <td class="rowlabel"><span class="red">*</span>供应商名称：
                    </td>
                    <td>
                        <asp:TextBox ID="Name" ClientIDMode="Static" runat="server" data-options="required:true"></asp:TextBox>
                    </td>
                    <td class="rowlabel">品牌或简称：
                    </td>
                    <td>
                        <asp:TextBox ID="Brand" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                    <td class="rowlabel"><span class="red">*</span>供应商类型：
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="BizType" ClientIDMode="Static">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                            <asp:ListItem Text="地接社" Value="地接社"></asp:ListItem>
                            <asp:ListItem Text="批发商" Value="批发商"></asp:ListItem>
                            <asp:ListItem Text="电商平台" Value="电商平台"></asp:ListItem>
                            <asp:ListItem Text="其他" Value="其他"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">负责人：
                    </td>
                    <td>
                        <asp:TextBox ID="Contact" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                    <td class="rowlabel">职务：
                    </td>
                    <td>
                        <asp:TextBox ID="Title" runat="server" ClientIDMode="Static"></asp:TextBox>

                    </td>
                    <td class="rowlabel">手机号码：
                    </td>
                    <td>
                        <asp:TextBox ID="Mobile" runat="server" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">联系电话：
                    </td>
                    <td>
                        <asp:TextBox ID="Phone" runat="server" ClientIDMode="Static"></asp:TextBox>

                    </td>
                    <td class="rowlabel">传真号码：
                    </td>
                    <td>
                        <asp:TextBox ID="Fax" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                    <td class="rowlabel">QQ：
                    </td>
                    <td>
                        <asp:TextBox ID="QQ" runat="server" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">开户行：
                    </td>
                    <td>
                        <asp:TextBox ID="BankName" runat="server" ClientIDMode="Static"></asp:TextBox>

                    </td>
                    <td class="rowlabel">开户账号：
                    </td>
                    <td>
                        <asp:TextBox ID="BankAcct" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                    <td class="rowlabel">联系地址：
                    </td>
                    <td>
                        <asp:TextBox ID="Addr" runat="server" ClientIDMode="Static"></asp:TextBox>
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
                                    <td colspan="6" style="text-align: right;">
                                        <a href="javascript:;" id="btnAddItem" class="easyui-linkbutton" iconcls="icon-add">添加一个业务联系人</a>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">上传附件：
                    </td>
                    <td colspan="5" style="padding: 10px;">
                        <div>
                            <input type="file" name="uploadify" id="uploadify" runat="server" />
                        </div>
                        <table class="tblEdit" cellpadding="1" cellspacing="1">
                            <tr>
                                <th>文件名称
                                </th>
                                <th style="width: 90px;">文件类型
                                </th>
                                <th style="width: 90px;">文件大小
                                </th>
                                <th style="width: 120px;">上传日期
                                </th>
                                <th style="width: 80px;">上传人
                                </th>
                                <th style="width: 45px;"></th>
                            </tr>
                            <tbody id="tblFile">
                                <asp:Repeater runat="server" ID="rptFile">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <input type="hidden" value='<%# Eval("ID") %>' />
                                                <a href='<%# Eval("FilePath") %>' target='_blank'>
                                                    <%# Eval("FileName") %></a>
                                            </td>
                                            <td style="text-align: center;">
                                                <%# Eval("FileType") %>
                                            </td>
                                            <td style="text-align: center;">
                                                <%# Eval("FileSize") %>
                                            </td>
                                            <td style="text-align: center;">
                                                <%# Eval("CreateDate") %>
                                            </td>
                                            <td style="text-align: center;">
                                                <%# Eval("CreateUserName") %>
                                            </td>
                                            <td style="text-align: center;">
                                                <a href="javascript:;" onclick="comm.fnDeleteFile(this)">删除</a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
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
