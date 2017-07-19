<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_KaoHeList.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_KaoHeList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    Ա����Ч���˼�¼
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <table width="100%">
        <tr width="100%">
            <td style="width: 300px" align="left">
                ʱ�䣺
                <asp:DropDownList ID="dplYear" runat="server">
                </asp:DropDownList>
                &nbsp;��&nbsp;
                <asp:DropDownList ID="dplMoth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Year_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;��&nbsp;&nbsp;&nbsp;&nbsp;
                ���ţ�
                <asp:DropDownList ID="ddl_Depart" runat="server" Width="100px" AutoPostBack="true"
                    OnSelectedIndexChanged="ddl_Year_SelectedIndexChanged">
                </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                �������ͣ�
                <asp:DropDownList ID="ddlType" runat="server" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddl_Year_SelectedIndexChanged">
                    <asp:ListItem Text="��ѡ��" Value="00"></asp:ListItem>
                    <asp:ListItem Text="��Ա�¶ȿ���" Value="��Ա�¶ȿ���"></asp:ListItem>
                    <asp:ListItem Text="��Ա��ȿ���" Value="��Ա��ȿ���"></asp:ListItem>
                    <asp:ListItem Text="��ְת������" Value="��ְת������"></asp:ListItem>
                    <asp:ListItem Text="��ͬ��������" Value="��ͬ��������"></asp:ListItem>
                    <asp:ListItem Text="��ͬ����ת�ƿ���" Value="��ͬ����ת�ƿ���"></asp:ListItem>
                    <asp:ListItem Text="ʵϰ��ʵϰ�ڿ���" Value="ʵϰ��ʵϰ�ڿ���"></asp:ListItem>
                    <asp:ListItem Text="Ա����λ��������" Value="Ա����λ��������"></asp:ListItem>
                </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                ������
                <asp:TextBox ID="txtName" runat="server" Width="90px"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="�� ѯ" OnClick="ddl_Year_SelectedIndexChanged" />
            </td>
            
        </tr>
    </table>
    <table width="100%">
        <tr width="100%">
            <td>
                ����״̬��
            </td>
            <td>
                <asp:RadioButtonList runat="server" ID="rblState" OnSelectedIndexChanged="ddl_Year_SelectedIndexChanged"
                    RepeatColumns="5" AutoPostBack="true">
                    <asp:ListItem Text="ȫ��" Value="" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="δ����" Value="0"></asp:ListItem>
                    <asp:ListItem Text="������" Value="1"></asp:ListItem>
                    <asp:ListItem Text="������" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                ���״̬��
            </td>
            <td>
                <asp:RadioButtonList runat="server" ID="rblshstate" OnSelectedIndexChanged="rblshstate_SelectedIndexChanged"
                    RepeatColumns="5" AutoPostBack="true">
                    <asp:ListItem Text="ȫ��" Value="" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="�����" Value="1"></asp:ListItem>
                    <asp:ListItem Text="��ͨ��" Value="2"></asp:ListItem>
                    <asp:ListItem Text="�Ѳ���" Value="3"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            
            <td align="right">
                <asp:HyperLink ID="HyperLink4" NavigateUrl="OM_KaoHe_Deadline.aspx?action=add" runat="server">
                    <asp:Image ID="Image4" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                        align="absmiddle" runat="server" />
                    ��������ȷ��</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="hpTask" NavigateUrl="OM_KaoHe.aspx?action=add" runat="server">
                    <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                        align="absmiddle" runat="server" />
                    �����Ա����</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="HyperLink3" NavigateUrl="OM_KaoHeAddPiLiang.aspx?action=add" runat="server">
                    <asp:Image ID="Image3" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                        align="absmiddle" runat="server" />
                    ���������Ա����</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDelete" ForeColor="Red" runat="server" Text="ɾ��" OnClick="btnDelete_OnClick" Visible="false" />
                &nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
        <div class="box-wrapper">
            <div class="box-outer">
                <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                    <asp:Repeater ID="rep_Kaohe" runat="server">
                        <HeaderTemplate>
                            <tr align="center" class="tableTitle headcolor">
                                <td>
                                    <strong>���</strong>
                                </td>
                                <td>
                                    <strong>����</strong>
                                </td>
                                <td>
                                    <strong>��������</strong>
                                </td>
                                <td>
                                    <strong>����</strong>
                                </td>
                                <td>
                                    <strong>ְλ</strong>
                                </td>
                                <td>
                                    <strong>��������</strong>
                                </td>
                                <td>
                                    <strong>����ʱ��</strong>
                                </td>
                                <td>
                                    <strong>���˷���</strong>
                                </td>
                                <td>
                                    <strong>����״̬</strong>
                                </td>
                                <td>
                                    <strong>���״̬</strong>
                                </td>
                                <td>
                                    <strong>�����</strong>
                                </td>
                                <td>
                                    <strong>�鿴</strong>
                                </td>
                                <td>
                                    <strong>�༭</strong>
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class='baseGadget' onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                <td>
                                    <asp:Label runat="server" ID="lbkh_Context" Visible="false" Text='<%#Eval("kh_Context")%>'></asp:Label>
                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" runat="server" Checked="false" Onclick="checkme(this)" />
                                    <%#Eval("ID_Num") %>
                                </td>
                                <td>
                                    <%#Eval("ST_NAME") %>
                                </td>
                                <td>
                                    <%#Eval("kh_type")%>
                                </td>
                                <td>
                                    <%#Eval("DEP_NAME")%>
                                </td>
                                <td>
                                    <%#Eval("DEP_POSITION")%>
                                </td>
                                <td>
                                    <%#Eval("KhYearMonth")%>
                                </td>
                                <td>
                                    <%#Eval("kh_Time")%>
                                </td>
                                <td>
                                    <%#Eval("kh_Score")%>
                                </td>
                                <td>
                                    <%#Eval("kh_State").ToString().Contains("0") ? "δ����" : Eval("kh_State").ToString().Contains("7") || Eval("kh_State").ToString().Contains("6")?"������":"������"%>
                                </td>
                                <td>
                                    <%#Eval("Kh_shtoltalstate").ToString().Contains("2") ? "�����" : Eval("Kh_shtoltalstate").ToString().Contains("3")? "�Ѳ���" : "�����"%>
                                </td>
                                <td>
                                    <%#Eval("kh_Add")%>
                                </td>
                                <td>
                                    <asp:HyperLink NavigateUrl='<%#"~/OM_Data/OM_KaoHe.aspx?action=view&id="+Eval("kh_Context")%>'
                                        runat="server" ID="HyperLink1">
                                        <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                            runat="server" />�鿴
                                    </asp:HyperLink>
                                </td>
                                <td width="100px">
                                    <asp:HyperLink NavigateUrl='<%#"~/OM_Data/OM_KaoHe.aspx?action=edit&id="+Eval("kh_Context")%>'
                                        runat="server" ID="HyperLink2">
                                        <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                            runat="server" />�༭
                                    </asp:HyperLink>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                    û�м�¼!</asp:Panel>
                <asp:UCPaging ID="UCPaging1" runat="server" />
            </div>
        </div>
</asp:Content>
