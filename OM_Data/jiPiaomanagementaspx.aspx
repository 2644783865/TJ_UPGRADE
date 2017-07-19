<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jiPiaomanagementaspx.aspx.cs"
    Inherits="ZCZJ_DPF.OM_Data.jiPiaomanagementaspx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>��Ʊ����</title>
    <base id="goDownload" target="_self" />
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />

    <script src="../JS/jquery/jquery-1.4.2.js" type="text/javascript"></script>

    <script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>

    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>

    <link href="../JS/EasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../JS/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script src="../JS/EasyUICommon.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="height: 25px">
        </div>
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <div class="box-wrapper">
            <div class="box-outer">
                <table>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="audittitle" runat="server" Text="��Ʊ����"></asp:Label><%--�������ͣ�ÿ������µ�����ʱ�������޸�--%>
                            &nbsp;&nbsp;&nbsp;<strong>���ţ�</strong>&nbsp;
                            <asp:DropDownList ID="ddl_Depart" runat="server" Width="100px" AutoPostBack="true"
                                OnSelectedIndexChanged="dplbm_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;<strong>�Ƶ��ˣ�</strong><asp:TextBox ID="txtName" runat="server" Width="90px"></asp:TextBox>
                            �������ڴӣ�<input type="text" id="startdate" style="width: 100px" onfocus="this.blur()"
                                runat="server" class="easyui-datebox" />&nbsp;&nbsp;&nbsp; ����<input type="text" id="enddate"
                                    onfocus="this.blur()" runat="server" style="width: 100px" class="easyui-datebox" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btncx" runat="server" Text="��ѯ" OnClick="btncx_click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="right">
                            <asp:HyperLink ID="HyperLinkAdd" runat="server" Target="_blank" NavigateUrl="~/OM_Data/jiPiaodetail.aspx?action=add&auditno=">
                                <asp:Image ID="ImageAdd" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                ���</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="35%">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:RadioButtonList ID="shstate" runat="server" OnSelectedIndexChanged="shstate_CheckedChanged"
                                        RepeatDirection="Horizontal" AutoPostBack="true">
                                        <asp:ListItem Value="" Selected="True">ȫ��</asp:ListItem>
                                        <asp:ListItem Value="0">��ʼ��</asp:ListItem>
                                        <asp:ListItem Value="1">�����</asp:ListItem>
                                        <asp:ListItem Value="2">��ͨ��</asp:ListItem>
                                        <asp:ListItem Value="3">�Ѳ���</asp:ListItem>
                                    </asp:RadioButtonList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:RadioButtonList ID="task" runat="server" OnSelectedIndexChanged="shstate_CheckedChanged"
                                        RepeatDirection="Horizontal" AutoPostBack="true">
                                        <asp:ListItem Value="">ȫ��</asp:ListItem>
                                        <asp:ListItem Value="0" Selected="True">�ҵ�����</asp:ListItem>
                                    </asp:RadioButtonList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_Depart" />
                <asp:AsyncPostBackTrigger ControlID="btncx" />
                <asp:AsyncPostBackTrigger ControlID="shstate" />
                <asp:AsyncPostBackTrigger ControlID="task" />
            </Triggers>
            <ContentTemplate>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                            <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                                border="1" width="100%">
                                <asp:Repeater ID="rptjipiao" runat="server">
                                    <HeaderTemplate>
                                        <tr align="center" style="background-color: #B9D3EE;">
                                            <td align="center">
                                                ���
                                            </td>
                                            <td align="center">
                                                ����
                                            </td>
                                            <td align="center">
                                                ����
                                            </td>
                                            <td align="center">
                                                �Ƶ���
                                            </td>
                                            <td align="center">
                                                �Ƶ�ʱ��
                                            </td>
                                            <td>
                                                ���״̬
                                            </td>
                                            <td>
                                                ����״̬
                                            </td>
                                            <td align="center">
                                                �༭
                                            </td>
                                            <td align="center">
                                                ɾ��
                                            </td>
                                            <td align="center">
                                                �鿴
                                            </td>
                                            <td>
                                                ���
                                            </td>
                                            <td>
                                                �Ƶ��˷���
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="baseGadget" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                            ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                            <td align="center">
                                                <asp:CheckBox ID="cbxSelect" CssClass="checkBoxCss" runat="server" Onclick="checkme(this)" />
                                                <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="auditno" runat="server" Text='<%#Eval("auditno")%>'></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="sqdepartmentname" runat="server" Text='<%#Eval("sqdepartmentname")%>'></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="addpername" runat="server" Text='<%#Eval("addpername")%>'></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="addtime" runat="server" Text='<%#Eval("addtime")%>'></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="totalstate" runat="server" Text='<%#Eval("totalstate").ToString()=="0"?"��ʼ��":Eval("totalstate").ToString()=="1"?"�����":Eval("totalstate").ToString()=="2"?"��ͨ��":"����"%>'></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="fankuistate" runat="server" Text='<%#Eval("fankuistate").ToString()=="0"?"δ����":"�ѷ���"%>'></asp:Label>
                                                <asp:Label ID="fankui" runat="server" Visible="false" Text='<%#Eval("fankui")%>'></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:HyperLink ID="HyperLinkXG" Target="_blank" NavigateUrl='<%#"~/OM_Data/jiPiaodetail.aspx?action=edit&auditno="+Eval("auditno") %>'
                                                    runat="server">
                                                    <asp:Image ID="Image_binaji" ImageUrl="~/assets/images/res.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />�༭</asp:HyperLink>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="HyperLinkSC" OnClick="hlDelete_OnClick" CommandArgument='<%# Eval("auditno")%>'  OnClientClick="return confirm('ȷ��ɾ����?')" runat="server">
                                                <asp:Image ID="Image_shanchu" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                    runat="server" />ɾ��</asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="HyperLinkCK" Target="_blank" NavigateUrl='<%#"~/OM_Data/jiPiaodetail.aspx?action=view&auditno="+Eval("auditno") %>'
                                                    runat="server">
                                                    <asp:Image ID="Image_chakan" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />�鿴</asp:HyperLink>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="HyperLinkSH" Target="_blank" NavigateUrl='<%#"~/OM_Data/jiPiaodetail.aspx?action=audit&auditno="+Eval("auditno") %>'
                                                    runat="server">
                                                    <asp:Image ID="Image_shenhe" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />���</asp:HyperLink>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="HyperLinkFK" Target="_blank" NavigateUrl='<%#"~/OM_Data/jiPiaodetail.aspx?action=fankui&auditno="+Eval("auditno") %>'
                                                    runat="server">
                                                    <asp:Image ID="Image_fankui" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />����</asp:HyperLink>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                                û�м�¼!<br />
                                <br />
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
