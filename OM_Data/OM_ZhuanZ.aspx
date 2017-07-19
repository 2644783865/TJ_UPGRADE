<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_ZhuanZ.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_ZhuanZ" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    人员转正信息
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                ActiveTabIndex="0">
                <asp:TabPanel ID="Tab" runat="server" TabIndex="0" HeaderText="人员转正信息">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td width="20%">
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rblSer" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                        OnSelectedIndexChanged="rblSer_SelectedIndexChanged">
                                        <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="未入职" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="已入职" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="待处理" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="right" width="200px">
                                    按姓名查看：
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" Width="150px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" Text="确 定" OnClick="rblSer_SelectedIndexChanged" />
                                </td>
                            </tr>
                        </table>
                        <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1"
                            style="cursor: pointer">
                            <asp:Repeater ID="rep_ZZ" runat="server" OnItemDataBound="rep_ZZ_ItemDataBound">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle headcolor">
                                        <td width="55px">
                                            <strong>序号</strong>
                                        </td>
                                        <td width="100px">
                                            <strong>姓名</strong>
                                        </td>
                                        <td width="100px">
                                            <strong>工号</strong>
                                        </td>
                                        <td width="150px">
                                            <strong>部门</strong>
                                        </td>
                                        <td>
                                            <strong>职位</strong>
                                        </td>
                                        <td>
                                            <strong>入职时间</strong>
                                        </td>
                                        <td>
                                            <strong>转正时间</strong>
                                        </td>
                                        <td>
                                            <strong>联系电话</strong>
                                        </td>
                                        <td>
                                            <strong>是否转正</strong>
                                        </td>
                                        <td>
                                            <strong>编辑</strong>
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
                                            <%#(Eval("ST_ZHUANZ").ToString()=="0"?"未转正":"已转正")%>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# Edit(Eval("ST_ID").ToString()) %>'
                                                runat="server" ToolTip='编辑' Width="100px">
                                                <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                                    runat="server" />编辑</asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                            没有记录!</asp:Panel>
                        <asp:UCPaging ID="UCPaging1" runat="server" />
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="Tab1" runat="server" TabIndex="1" HeaderText="合同信息">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td width="20%">
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rblHeTong" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                        OnSelectedIndexChanged="rblHeTong_SelectedIndexChanged">
                                        <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="未到期" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="待处理" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="已过期" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="right" width="200px">
                                    按姓名查看：
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName1" runat="server" Width="150px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnConfirm1" runat="server" Text="确 定" OnClick="rblHeTong_SelectedIndexChanged" />
                                </td>
                            </tr>
                        </table>
                        <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1"
                            style="cursor: pointer">
                            <asp:Repeater ID="rep_HTong" runat="server" OnItemDataBound="rep_HTong_ItemDataBound">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle headcolor">
                                        <td width="55px">
                                            <strong>序号</strong>
                                        </td>
                                        <td width="100px">
                                            <strong>姓名</strong>
                                        </td>
                                        <td width="100px">
                                            <strong>工号</strong>
                                        </td>
                                        <td width="120px">
                                            <strong>部门</strong>
                                        </td>
                                        <td>
                                            <strong>职位</strong>
                                        </td>
                                        <td>
                                            <strong>合同主体</strong>
                                        </td>
                                        <td>
                                            <strong>合同期限</strong>
                                        </td>
                                        <td>
                                            <strong>合同起始时间</strong>
                                        </td>
                                        <td>
                                            <strong>合同终止时间</strong>
                                        </td>
                                        <td>
                                            <strong>次数</strong>
                                        </td>
                                        <td>
                                            <strong>编辑</strong>
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
                                                runat="server" ToolTip='编辑' Width="100px">
                                                <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                                    runat="server" />编辑</asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <asp:Panel ID="NoDataPane2" runat="server" HorizontalAlign="Center" ForeColor="Red">
                            没有记录!</asp:Panel>
                        <asp:UCPaging ID="UCPaging2" runat="server" />
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
