<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuideEdit.aspx.cs" Inherits="DRP.WEB.Module.Res.GuideEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>导游管理</title>
    <script src="../Order/Script/OrderUtility.js" type="text/javascript"></script>
    <script src="../../Scripts/Plugin/Uploadify/jquery.uploadify-3.1.min.js" type="text/javascript"></script>
    <link href="../../Scripts/Plugin/Uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            comm.fnUploadFile(); //上传附件
            $("#btnSave").click(function () {
                t.btnSave();
            });
        });

        var t = {
            btnSave: function () {
                var fileIDs = comm.fnGetFileID();
                $("#FileID").val(fileIDs);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel"><span class="red">*</span>导游名称：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="Name" data-options="required:true"></asp:TextBox>
                    </td>

                    <td class="rowlabel">地区：
                    </td>
                    <td>
                        <asp:DropDownList ID="DepartureID" runat="server" AppendDataBoundItems="true" data-options="required:true">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">性别：
                    </td>
                    <td>
                        <asp:DropDownList ID="Sex" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                            <asp:ListItem Text="男" Value="男"></asp:ListItem>
                            <asp:ListItem Text="女" Value="女"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="rowlabel">手机号：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="Mobile"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">导游等级：
                    </td>
                    <td>
                        <asp:DropDownList ID="GuideLevel" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="rowlabel">语种：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="Language"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td class="rowlabel">身份证号：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="IDNum"></asp:TextBox>
                    </td>
                    <td class="rowlabel">特长：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="Skill"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">邮箱：
                    </td>
                    <td>
                        <asp:TextBox ID="Mail" runat="server"></asp:TextBox>
                    </td>
                    <td class="rowlabel">QQ：
                    </td>
                    <td>
                        <asp:TextBox ID="QQ" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">导游证：
                    </td>
                    <td>
                        <asp:CheckBox ID="IsIDCard" Text="有" runat="server" />
                    </td>
                    <td class="rowlabel">导游证号：
                    </td>
                    <td>
                        <asp:TextBox ID="IDCardNum" runat="server"></asp:TextBox>
                    </td>
                </tr> 
                <tr>
                    <td class="rowlabel">领队证：
                    </td>
                    <td>
                        <asp:CheckBox ID="IsLeaderCard" Text="有" runat="server" />
                    </td>
                    <td class="rowlabel"><span class="red">*</span>状态：
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="IsEnable" runat="server">
                            <asp:ListItem Text="启用" Selected="True" Value="1"></asp:ListItem>
                            <asp:ListItem Text="禁用" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                 <tr>
                    <td class="rowlabel">开户行名：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="BankName"></asp:TextBox>
                    </td>
                    <td class="rowlabel">开户账号：
                    </td>
                    <td>
                        <asp:TextBox ID="BankAcct" runat="server"></asp:TextBox>
                    </td>
                </tr> 
                <tr>
                    <td class="rowlabel">备注：
                    </td>
                    <td colspan="3">
                        <asp:TextBox runat="server" EnableTheming="false" CssClass="textbox" Width="90%" Height="40" TextMode="MultiLine" ClientIDMode="Static" ID="Remark"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <asp:HiddenField ID="FileID" runat="server" />
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
            </table>
        </asp:Panel>
        <div style="text-align: center;">
            <asp:LinkButton runat="server" ID="btnSave" CssClass="easyui-linkbutton" iconCls="icon-save" Text="保存" ClientIDMode="Static" OnClick="btnSave_Click" OnClientClick="return $('#form1').form('validate');">
            </asp:LinkButton>
        </div>
    </form>
</body>
</html>
