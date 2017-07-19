<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tbds_staffinfo_operate.aspx.cs" Inherits="testpage.WebForm2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="pragma" content="no-cache">
    <meta http-equiv="cache-control" content="no-cache">
    <meta http-equiv="expires" content="0">
    <base id="goDownload" target="_self" />
    <title>PMS项目管理系统</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 88px;
        }
        .dispaly
        {
            display: none;
        }
        .fileup
        {
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        添加员工(带<span class="Error">*</span>号的为必填项)</div>
    <asp:Panel ID="Panel1" runat="server" Enabled="False">
        <div class="box-wrapper1" style="text-align:center">
            <div class="box-outer">
                <table cellpadding="4" cellspacing="1" style="background: #EEF7FD; white-space: nowrap;text-align:left"
                    class="grid" border="1">
                    <tr>
                        <td align="right" width="100px">
                            姓名：
                        </td>
                        <td width="250px">
                            <asp:TextBox ID="ST_NAME" runat="server"></asp:TextBox><span class="Error">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="必须输入姓名"
                                Font-Size="Small" ControlToValidate="ST_NAME"></asp:RequiredFieldValidator>
                        </td>
                        <td rowspan="7" style="vertical-align: top" align="center">
                            <asp:Image ID="showImage" runat="server" Width="100px" Height="100px" />
                            <asp:Panel ID="Panel2" runat="server" CssClass="dispaly">
                                <asp:FileUpload ID="FileUploadupdate" runat="server" Width="150px" CssClass="" />
                                <br />
                                <asp:Button ID="btnupload2" runat="server" CausesValidation="false" OnClick="btnupload2_Click"
                                    Text="上传" />
                                <asp:Label ID="lblupload2" runat="server"></asp:Label>
                                <br />
                                &nbsp;
                                <asp:Label ID="lblupdate" runat="server"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            性别：
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownListgender" runat="server">
                                <asp:ListItem Selected="True" Text="男" Value="男"></asp:ListItem>
                                <asp:ListItem Text="女" Value="女"></asp:ListItem>
                            </asp:DropDownList>
                            <span class="Error">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            编号：
                        </td>
                        <td width="300px">
                            <asp:TextBox ID="ST_CODE" runat="server" Enabled="False"></asp:TextBox><span class="Error">
                                *</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="填写编号"
                                Font-Size="Small" ControlToValidate="ST_CODE"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            所属部门：
                        </td>
                        <td width="300px">
                            <asp:TextBox ID="TextBoxdept" runat="server" Width="100px"></asp:TextBox>
                            <asp:DropDownList ID="DropDownListdept" runat="server" OnSelectedIndexChanged="DropDownListdept_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                            <span class="Error">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            维护人：
                        </td>
                        <td>
                            <asp:TextBox ID="ST_MANCLERK" runat="server" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            填写日期：
                        </td>
                        <td>
                            <asp:TextBox ID="ST_FILLDATE" runat="server" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            备注：
                        </td>
                        <td>
                            <asp:TextBox ID="ST_NOTE" runat="server" TextMode="MultiLine" Width="250px" Height="100px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div>
            <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                ActiveTabIndex="0">
                <asp:TabPanel ID="Tab_SGHZ" runat="server" HeaderText="人员信息管理">
                    <HeaderTemplate>
                        信息管理
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:Panel ID="Panel3" runat="server">
                            <table>
                                <tr>
                                    <td align="right" style="width: 80px">
                                        职位：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_POSITION" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px">
                                        序列：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_SEQUEN" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px">
                                        身份证号：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_IDCARD" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 80px">
                                        联系电话：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_TELE" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px">
                                        户口类型：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_REGIST" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px">
                                        婚否：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_MARRY" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 80px">
                                        政治面貌：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_POLITICAL" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px">
                                        年龄：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_AGE" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="Tab_SGYTJ" runat="server" Width="100%" HeaderText="人员档案管理" TabIndex="1">
                    <HeaderTemplate>
                        学历信息
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:Panel ID="Panel4" runat="server">
                            <table>
                                <tr>
                                    <td align="right" style="width: 80px">
                                        学历：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_XUELI" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px">
                                        学位：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_XUEWEI" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px">
                                        毕业院校：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_BIYE" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 80px">
                                        所学专业：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_ZHUANYE" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px">
                                        毕业时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_BIYETIME" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px">
                                        相当学历：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_EQXUELI" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 80px">
                                        相当职称：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_EQZHICH" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px">
                                        职称：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_ZHICH" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
            <div align="center">
                <asp:Button ID="btnupdate" runat="server" Text="修改" OnClick="btnupdate_Click" />
                <input type="button" id="btn_Back" value="返回" onclick="window.close()" />
            </div>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
