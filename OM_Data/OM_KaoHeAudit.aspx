<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_KaoHeAudit.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_KaoHeAudit" %>
<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
考核任务审核
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
 <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
   <div class="box-wrapper">
                <div class="box-outer">
                    <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                        <asp:Repeater ID="rep_Kaohe" runat="server">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle headcolor">
                                    <td>
                                        <strong>序号</strong>
                                    </td>
                                    <td>
                                        <strong>姓名</strong>
                                    </td>
                                    <td>
                                        <strong>考核类型</strong>
                                    </td>
                                    <td>
                                        <strong>部门</strong>
                                    </td>
                                    <td>
                                        <strong>职位</strong>
                                    </td>
                                    <td>
                                        <strong>考核时间</strong>
                                    </td>
                                    <td>
                                        <strong>考核分数</strong>
                                    </td>
                                    <td>
                                        <strong>添加人</strong>
                                    </td>
                                    
                                    <td>
                                        <strong>考核</strong>
                                    </td>
                                    <td>
                                        <strong>审核</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class='baseGadget' onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                    <td>
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
                                        <%#Eval("kh_Time")%>
                                    </td>
                                    <td>
                                        <%#Eval("kh_Score")%>
                                    </td>
                                    <td>
                                        <%#Eval("kh_Add")%>
                                    </td>
                                   
                                    <td>
                                        <asp:HyperLink NavigateUrl='<%#"~/OM_Data/OM_KaoHe.aspx?action=audit&id="+Eval("kh_Context")%>'
                                            runat="server" ID="HyperLink3" Visible='<%#Eval("Kh_State").ToString()=="7"?false:true %>'>
                                            <asp:Image ID="Image3" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                                runat="server" />评价
                                        </asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink NavigateUrl='<%#"~/OM_Data/OM_KaoHe.aspx?action=auditsh&id="+Eval("kh_Context")%>'
                                            runat="server" ID="HyperLink1" Visible='<%#Eval("Kh_State").ToString()=="7"?true:Eval("Kh_State").ToString()=="6"?true:false %>'>
                                            <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                                runat="server" />审核
                                        </asp:HyperLink>
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
</asp:Content>
