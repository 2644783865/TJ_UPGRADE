<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EQU_tzsbop.aspx.cs" Inherits="ZCZJ_DPF.ESM.EQU_tzsbop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <base id="goDownload" target="_self" />
    <title>�����豸����</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="pragma" content="no-cache"/>
    <meta http-equiv="cache-control" content="no-cache"/>
    <meta http-equiv="expires" content="0"/>
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                ��������豸(��<span class="Error">*</span>�ŵ�Ϊ������)
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="box-wrapper">
            <div class="box-outer">
                <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                <tr>
                        <td align="right" style="width: 100px">
                            �ڲ���ţ�
                        </td>
                        <td>
                            <asp:TextBox ID="Code" runat="server"></asp:TextBox><span class="Error">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="���������ڲ����"
                                ControlToValidate="Code"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100px">
                            �豸���ƣ�
                        </td>
                        <td>
                            <asp:TextBox ID="Name" runat="server"></asp:TextBox><span class="Error">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="���������豸����"
                                ControlToValidate="Name"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100px">
                            �豸���
                        </td>
                        <td>
                            <asp:TextBox ID="Type" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100px">
                            �ͺŹ��
                        </td>
                        <td>
                            <asp:TextBox ID="Specification" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100px">
                            ������ţ�
                        </td>
                        <td>
                            <asp:TextBox ID="Ocode" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100px">
                            ע���ţ�
                        </td>
                        <td>
                            <asp:TextBox ID="Rcode" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100px">
                            ʹ��֤�ţ�
                        </td>
                        <td>
                            <asp:TextBox ID="Ucode" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100px">
                            �����̣�
                        </td>
                        <td>
                            <asp:TextBox ID="Manufa" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100px">
                            ��װλ�ã�
                        </td>
                        <td>
                            <asp:TextBox ID="Position" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100px">
                            ʹ��״̬��
                        </td>
                        <td>
                            <asp:TextBox ID="Ustate" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100px">
                            �ټ����ڣ�
                        </td>
                        <td>
                            <asp:TextBox  ID="Redate" runat="server"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator1" ValueToCompare="1900-01-01" Operator="GreaterThan" runat="server"  ControlToValidate="Redate"  Type="Date"  Text="�������ڸ�ʽ����ȷ����1900-01-01"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100px">
                            ��ע��
                        </td>
                        <td>
                            <asp:TextBox  ID="Remark" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                        <asp:Button ID="btnupdate" runat="server" Text="�޸�" OnClick="btnupdate_Click" />
                        <input type="button" id="btn_Back" value="����" onclick="window.close()" />
                        </td>
                     </tr>
                     </table>
                     </div>
                     </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
