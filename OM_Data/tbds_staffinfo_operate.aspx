<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tbds_staffinfo_operate.aspx.cs" Inherits="testpage.WebForm2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="pragma" content="no-cache">
    <meta http-equiv="cache-control" content="no-cache">
    <meta http-equiv="expires" content="0">
    <base id="goDownload" target="_self" />
    <title>PMS��Ŀ����ϵͳ</title>
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
        ���Ա��(��<span class="Error">*</span>�ŵ�Ϊ������)</div>
    <asp:Panel ID="Panel1" runat="server" Enabled="False">
        <div class="box-wrapper1" style="text-align:center">
            <div class="box-outer">
                <table cellpadding="4" cellspacing="1" style="background: #EEF7FD; white-space: nowrap;text-align:left"
                    class="grid" border="1">
                    <tr>
                        <td align="right" width="100px">
                            ������
                        </td>
                        <td width="250px">
                            <asp:TextBox ID="ST_NAME" runat="server"></asp:TextBox><span class="Error">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="������������"
                                Font-Size="Small" ControlToValidate="ST_NAME"></asp:RequiredFieldValidator>
                        </td>
                        <td rowspan="7" style="vertical-align: top" align="center">
                            <asp:Image ID="showImage" runat="server" Width="100px" Height="100px" />
                            <asp:Panel ID="Panel2" runat="server" CssClass="dispaly">
                                <asp:FileUpload ID="FileUploadupdate" runat="server" Width="150px" CssClass="" />
                                <br />
                                <asp:Button ID="btnupload2" runat="server" CausesValidation="false" OnClick="btnupload2_Click"
                                    Text="�ϴ�" />
                                <asp:Label ID="lblupload2" runat="server"></asp:Label>
                                <br />
                                &nbsp;
                                <asp:Label ID="lblupdate" runat="server"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            �Ա�
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownListgender" runat="server">
                                <asp:ListItem Selected="True" Text="��" Value="��"></asp:ListItem>
                                <asp:ListItem Text="Ů" Value="Ů"></asp:ListItem>
                            </asp:DropDownList>
                            <span class="Error">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            ��ţ�
                        </td>
                        <td width="300px">
                            <asp:TextBox ID="ST_CODE" runat="server" Enabled="False"></asp:TextBox><span class="Error">
                                *</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="��д���"
                                Font-Size="Small" ControlToValidate="ST_CODE"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            �������ţ�
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
                            ά���ˣ�
                        </td>
                        <td>
                            <asp:TextBox ID="ST_MANCLERK" runat="server" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            ��д���ڣ�
                        </td>
                        <td>
                            <asp:TextBox ID="ST_FILLDATE" runat="server" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            ��ע��
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
                <asp:TabPanel ID="Tab_SGHZ" runat="server" HeaderText="��Ա��Ϣ����">
                    <HeaderTemplate>
                        ��Ϣ����
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:Panel ID="Panel3" runat="server">
                            <table>
                                <tr>
                                    <td align="right" style="width: 80px">
                                        ְλ��
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_POSITION" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px">
                                        ���У�
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_SEQUEN" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px">
                                        ���֤�ţ�
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_IDCARD" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 80px">
                                        ��ϵ�绰��
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_TELE" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px">
                                        �������ͣ�
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_REGIST" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px">
                                        ���
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_MARRY" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 80px">
                                        ������ò��
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_POLITICAL" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px">
                                        ���䣺
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_AGE" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="Tab_SGYTJ" runat="server" Width="100%" HeaderText="��Ա��������" TabIndex="1">
                    <HeaderTemplate>
                        ѧ����Ϣ
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:Panel ID="Panel4" runat="server">
                            <table>
                                <tr>
                                    <td align="right" style="width: 80px">
                                        ѧ����
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_XUELI" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px">
                                        ѧλ��
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_XUEWEI" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px">
                                        ��ҵԺУ��
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_BIYE" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 80px">
                                        ��ѧרҵ��
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_ZHUANYE" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px">
                                        ��ҵʱ�䣺
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_BIYETIME" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px">
                                        �൱ѧ����
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_EQXUELI" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 80px">
                                        �൱ְ�ƣ�
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_EQZHICH" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 80px">
                                        ְ�ƣ�
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
                <asp:Button ID="btnupdate" runat="server" Text="�޸�" OnClick="btnupdate_Click" />
                <input type="button" id="btn_Back" value="����" onclick="window.close()" />
            </div>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
