<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EQU_operate.aspx.cs" Inherits="ZCZJ_DPF.ESM.equipment_operate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <base id="goDownload" target="_self" />
    <title>PMS��Ŀ����ϵͳ</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="pragma" content="no-cache"/>
    <meta http-equiv="cache-control" content="no-cache"/>
    <meta http-equiv="expires" content="0"/>
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="form1" runat="server">
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                ����豸(��<span class="Error">*</span>�ŵ�Ϊ������)
                            </td>
                        </tr>
                    </table>
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
                        <td align="right" style="width: 150px">
                            ��Ƭ��ţ�
                        </td>
                        <td>
                            <asp:TextBox ID="CNum" runat="server" Width="200px"></asp:TextBox><span class="Error">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="����������"
                                ControlToValidate="CNum"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                      <tr>
                        <td align="right" style="width: 150px">
                            ��ʼʹ�����ڣ�
                        </td>
                        <td>
                            <asp:TextBox ID="SDate" runat="server" Width="200px"></asp:TextBox><label class="Error">���ڸ�ʽ��1900.01.01</label>
<%--                                <asp:CompareValidator
                                    ID="CompareValidator1" runat="server" ErrorMessage="��������ȷ���ڸ�ʽ" ControlToValidate ="SDate"></asp:CompareValidator>--%>
                                
                        </td>
                    </tr>
                      <tr>
                        <td align="right" style="width: 150px">
                            ʹ�����ޣ��£���
                        </td>
                        <td>
                            <asp:TextBox ID="Dmonth" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           ԭֵ��
                        </td>
                        <td>
                            <asp:TextBox ID="Oval" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           �̶��ʲ���ţ�
                        </td>
                        <td width="200">
                            <asp:TextBox ID="ANum" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           ����ֵ�ʣ�
                        </td>
                        <td>
                            <asp:TextBox ID="ReRate" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           �ۼ��۾ɣ�
                        </td>
                        <td width="200px">
                            <asp:TextBox ID="AcDep" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           ʹ�ò��ţ�
                        </td>
                        <td>
                            <asp:TextBox ID="UsDe" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           ������ƣ�
                        </td>
                        <td>
                            <asp:TextBox ID="CName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           �̶��ʲ����ƣ�
                        </td>
                        <td width="200px">
                            <asp:TextBox ID="AName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           ��ֵ��
                        </td>
                        <td>
                            <asp:TextBox ID="NetVal" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td align="right" style="width: 150px">
                           ����ͺţ�
                        </td>
                        <td>
                            <asp:TextBox ID="Spec" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           �۾ɷ�����
                        </td>
                        <td>
                            <asp:TextBox ID="Depre" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           ��������λ��
                        </td>
                        <td>
                            <asp:TextBox ID="Unit" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           ��ŵص㣺
                        </td>
                        <td>
                            <asp:TextBox ID="Stor" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px">
                           ��Ӧ�۾ɿ�Ŀ��
                        </td>
                        <td>
                            <asp:TextBox ID="CorAccount" runat="server" Width="200px"></asp:TextBox>
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