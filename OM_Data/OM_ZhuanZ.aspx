<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_ZhuanZ.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_ZhuanZ" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    ��Աת����Ϣ
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                ActiveTabIndex="0">
                <asp:TabPanel ID="Tab" runat="server" TabIndex="0" HeaderText="��Աת����Ϣ">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td width="20%">
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rblSer" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                        OnSelectedIndexChanged="rblSer_SelectedIndexChanged">
                                        <asp:ListItem Text="ȫ��" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="δ��ְ" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="����ְ" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="������" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="right" width="200px">
                                    �������鿴��
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" Width="150px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" Text="ȷ ��" OnClick="rblSer_SelectedIndexChanged" />
                                </td>
                            </tr>
                        </table>
                        <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1"
                            style="cursor: pointer">
                            <asp:Repeater ID="rep_ZZ" runat="server" OnItemDataBound="rep_ZZ_ItemDataBound">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle headcolor">
                                        <td width="55px">
                                            <strong>���</strong>
                                        </td>
                                        <td width="100px">
                                            <strong>����</strong>
                                        </td>
                                        <td width="100px">
                                            <strong>����</strong>
                                        </td>
                                        <td width="150px">
                                            <strong>����</strong>
                                        </td>
                                        <td>
                                            <strong>ְλ</strong>
                                        </td>
                                        <td>
                                            <strong>��ְʱ��</strong>
                                        </td>
                                        <td>
                                            <strong>ת��ʱ��</strong>
                                        </td>
                                        <td>
                                            <strong>��ϵ�绰</strong>
                                        </td>
                                        <td>
                                            <strong>�Ƿ�ת��</strong>
                                        </td>
                                        <td>
                                            <strong>�༭</strong>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class='baseGadget' onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'"
                                        ondblclick="javascript:window.showModalDialog('Sta_StaffEdit.aspx?action=View&ST_ID=<%#Eval("ST_ID")%>','','DialogWidth=1020px;DialogHeight=600px')">
                                        <asp:Label ID="ST_ID" runat="server" Visible="false" Text='<%#Eval("ST_ID")%>'></asp:Label>
                                        <td>
                                            <%#Eval("ID_Num")%>
                                            <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="ST_NAME" runat="server" Text='<%#Eval("ST_NAME") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <%#Eval("ST_WORKNO") %>
                                        </td>
                                        <td>
                                            <%#Eval("DEP_NAME") %>
                                        </td>
                                        <td>
                                            <%#Eval("DEP_POSITION")%>
                                        </td>
                                        <td>
                                            <%#Eval("ST_INTIME") %>
                                        </td>
                                        <td>
                                            <%#Eval("ST_ZHENG") %>
                                        </td>
                                        <td>
                                            <%#Eval("ST_TELE") %>
                                        </td>
                                        <td>
                                            <%#(Eval("ST_ZHUANZ").ToString()=="0"?"δת��":"��ת��")%>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# Edit(Eval("ST_ID").ToString()) %>'
                                                runat="server" ToolTip='�༭' Width="100px">
                                                <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                                    runat="server" />�༭</asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                            û�м�¼!</asp:Panel>
                        <asp:UCPaging ID="UCPaging1" runat="server" />
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="Tab1" runat="server" TabIndex="1" HeaderText="��ͬ��Ϣ">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td width="20%">
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rblHeTong" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                        OnSelectedIndexChanged="rblHeTong_SelectedIndexChanged">
                                        <asp:ListItem Text="ȫ��" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="δ����" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="������" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="�ѹ���" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="right" width="200px">
                                    �������鿴��
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName1" runat="server" Width="150px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnConfirm1" runat="server" Text="ȷ ��" OnClick="rblHeTong_SelectedIndexChanged" />
                                </td>
                            </tr>
                        </table>
                        <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1"
                            style="cursor: pointer">
                            <asp:Repeater ID="rep_HTong" runat="server" OnItemDataBound="rep_HTong_ItemDataBound">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle headcolor">
                                        <td width="55px">
                                            <strong>���</strong>
                                        </td>
                                        <td width="100px">
                                            <strong>����</strong>
                                        </td>
                                        <td width="100px">
                                            <strong>����</strong>
                                        </td>
                                        <td width="120px">
                                            <strong>����</strong>
                                        </td>
                                        <td>
                                            <strong>ְλ</strong>
                                        </td>
                                        <td>
                                            <strong>��ͬ����</strong>
                                        </td>
                                        <td>
                                            <strong>��ͬ����</strong>
                                        </td>
                                        <td>
                                            <strong>��ͬ��ʼʱ��</strong>
                                        </td>
                                        <td>
                                            <strong>��ͬ��ֹʱ��</strong>
                                        </td>
                                        <td>
                                            <strong>����</strong>
                                        </td>
                                        <td>
                                            <strong>�༭</strong>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class='baseGadget' onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'"
                                        ondblclick="javascript:window.showModalDialog('Sta_StaffEdit.aspx?action=View&ST_ID=<%#Eval("ST_ID")%>','','DialogWidth=1020px;DialogHeight=600px')">
                                        <asp:Label ID="ST_ID" runat="server" Visible="false" Text='<%#Eval("ST_ID")%>'></asp:Label>
                                        <td>
                                            <%#Eval("ID_Num")%>
                                            <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="ST_NAME" runat="server" Text='<%#Eval("ST_NAME") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <%#Eval("ST_WORKNO") %>
                                        </td>
                                        <td>
                                            <%#Eval("DEP_NAME") %>
                                        </td>
                                        <td>
                                            <%#Eval("DEP_POSITION")%>
                                        </td>
                                        <td>
                                            <%#Eval("ST_CONTR")%>
                                        </td>
                                        <td>
                                            <%#Eval("ST_CONTRTIME")%>
                                        </td>
                                        <td>
                                            <%#Eval("ST_CONTRSTART")%>
                                        </td>
                                        <td>
                                            <%#Eval("ST_CONTREND")%>
                                        </td>
                                        <td>
                                            <%#Eval("ST_COUNT")%>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# Show(Eval("ST_ID").ToString()) %>'
                                                runat="server" ToolTip='�༭' Width="100px">
                                                <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                                    runat="server" />�༭</asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <asp:Panel ID="NoDataPane2" runat="server" HorizontalAlign="Center" ForeColor="Red">
                            û�м�¼!</asp:Panel>
                        <asp:UCPaging ID="UCPaging2" runat="server" />
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
