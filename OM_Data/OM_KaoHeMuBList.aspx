<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_KaoHeMuBList.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_KaoHeMuBList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    考核模板
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
            <table>
                <tr width="100%">
                    <td style="width: 20%;">
                        &nbsp;&nbsp;<strong>条件选择</strong>
                    </td>
                    <td align="right" width="150px">
                        部门：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_Depart" runat="server" Width="100px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddl_Position_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
            
                    <td align="right" width="30%">
                        <asp:HyperLink ID="hpMuBAdd" NavigateUrl="OM_KaoHeMuBan.aspx?action=add" runat="server">
                            <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                                align="absmiddle" runat="server" />
                            添加考核模板</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            <div class="box-wrapper">
                <div class="box-outer">
                    <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                        <asp:Repeater ID="rep_Kaohe" runat="server" OnItemDataBound="rep_Kaohe_ItemDataBound" >
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle headcolor">
                                    <td style="width: 50px">
                                        <strong>序号</strong>
                                    </td>
                                    <td>
                                        <strong>部门</strong>
                                    </td>
                                    <td>
                                        <strong>类型</strong>
                                    </td>
                                    <td>
                                        <strong>模板名称</strong>
                                    </td>
                                    <td>
                                        <strong>添加时间</strong>
                                    </td>
                                    <td>
                                        <strong>状态</strong>
                                    </td>
                                    <td>
                                        <strong>添加人</strong>
                                    </td>
                                    <td>
                                        <strong>编辑</strong>
                                    </td>
                                     <td>
                                        <strong>查看</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class='baseGadget' onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                    <td>
                                        <%#Eval("ID_Num") %>
                                        <input type="hidden" id="hidAddPer" runat="server"  value='<%#Eval("kh_AddId") %>'/>
                                    </td>
                                    <td>
                                        <%#Eval("kh_Dep").ToString().Contains("100") ? "通用模板" : Eval("DEP_NAME")%>
                                    </td>
                                    <td>
                                        <%#Eval("kh_Type1")%>
                                    </td>
                                    <td>
                                        <%#Eval("kh_Name")%>
                                    </td>
                                    <td>
                                        <%#Eval("kh_Time")%>
                                    </td>
                                    <td>
                                        <%#Eval("kh_State").ToString()=="0"?"在 用":"未 用"%>
                                    </td>
                                    <td>
                                        <%#Eval("ST_NAME")%>
                                    </td>
                                    <td width="100px">
                                        <asp:HyperLink ID="hpMuBEdit" NavigateUrl='<%#"OM_KaoHeMuBan.aspx?action=edit&fkey="+Eval("kh_Fkey") %>'
                                            runat="server">
                                            <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                                runat="server" />编辑</asp:HyperLink>
                                    </td>
                                      <td width="100px">
                                        <asp:HyperLink ID="hpMuBView" NavigateUrl='<%#"OM_KaoHeMuBan.aspx?action=view&fkey="+Eval("kh_Fkey") %>'
                                            runat="server">
                                            <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                                runat="server" />查看</asp:HyperLink>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                        没有记录!</asp:Panel>
                    <asp:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
