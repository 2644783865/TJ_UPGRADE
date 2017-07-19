<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="QC_External_Audit.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_External_Audit" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    外审
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
     
    </script>

    <div class="box-inner">
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
      <div class="box_right">
            <div class="box-title">
                <table style="width: 100%; height: 24px">
                    <tr>
                        <td style="width: 30%; height: 24px">
                            <b>中材(天津)重型机械有限公司外审</b>
                        </td>
                        <%--<td align="right">
                            按名称查询：
                            <asp:DropDownList ID="DropDownList2" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            <asp:Button ID="Button1" runat="server" Text="查询" />
                        </td>--%>
                        <td>
                            <asp:RadioButtonList ID="ddl_state" runat="server" OnSelectedIndexChanged="btn_search_Click"
                                AutoPostBack="true" RepeatColumns="8">
                                <asp:ListItem Text="全部" Value="%"></asp:ListItem>
                                <asp:ListItem Text="审批中" Value="1"></asp:ListItem>
                                <asp:ListItem Text="已通过" Value="2"></asp:ListItem>
                                <asp:ListItem Text="待处理" Value="3" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="已驳回" Value="4"></asp:ListItem>
                                <asp:ListItem Text="我的任务" Value="5"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="right">
                            <asp:HyperLink ID="HyperLink1" runat="server"  NavigateUrl="QC_External_Audit_Edit.aspx?action=add">
                                <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                            runat="server" />新增外审&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div>
        <table cellpadding="4" cellspacing="1" class="toptable grid" border="1">
            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Purordertotal_list_Repeater_ItemDataBound">
                <HeaderTemplate>
                    <tr align="center" class="tableTitle headcolor">
                        <td style="width: 10px">
                        </td>
                        <td>
                            <strong>文件号</strong>
                        </td>
                        <td>
                            <strong>年份</strong>
                        </td>
                        <td>
                            <strong>发往部门</strong>
                        </td>
                        <td>
                            <strong>主题</strong>
                        </td>
                        <td>
                            <strong>提出人</strong>
                        </td>
                        <td>
                            <strong>审核人</strong>
                        </td>
                        
                        <td>
                            <strong>查看</strong>
                        </td>
                        <td>
                            <strong>审核</strong>
                        </td>
                        <td>
                            <strong>删除</strong>
                        </td>
                         <td>
                            <strong>反审</strong>
                        </td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr >
                        <td>
                            <asp:CheckBox ID="cbx" runat="server" />
                        </td>
                        <td width="50px" align="center">
                            <%#Eval("PRO_ID") %>
                            <input type="hidden" id="hidId" value=' <%#Eval("Id") %>' />
                        </td>
                        <td width="50px" align="center">
                            <%#Eval("PRO_YEAR") %>
                        </td>
                        <td width="50px" align="center">
                            <%#Eval("DEP_NAME")%>
                        </td>
                        <td width="50px" align="center">
                            <%#Eval("PRO_THEME") %>
                        </td>
                        <td width="50px" align="center">
                            <%#Eval("PRO_SHYNM")%>
                        </td>
                        <td width="50px" align="center">
                            <%#Eval("PRO_SPRNM")%>
                        </td>
                      
                        <td width="80px" align="center">
                            <asp:HyperLink ID="HyperLinkDetail" runat="server" NavigateUrl='<%#"~/QC_Data/QC_External_Audit_Edit.aspx?action=view&Id="+Eval("Id") %>'>
                                <asp:Image ID="Image11" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                    hspace="2" align="absmiddle" />查看</asp:HyperLink>
                        </td>
                        <td width="80px" align="center">
                            <asp:HyperLink ID="hpyAudit" runat="server" NavigateUrl='<%#"~/QC_Data/QC_External_Audit_Edit.aspx?action=audit&Id="+Eval("Id") %>'>
                                <asp:Image ID="Image3" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                    hspace="2" align="absmiddle" />审核</asp:HyperLink>
                        </td>
                        <td width="80px" align="center">
                            <asp:LinkButton ID="LinkButDel" runat="server" OnClick="LinkButDel_Click" CommandArgument='<%#Eval("Id")%>'>
                                <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                    hspace="2" align="absmiddle" />删除</asp:LinkButton>
                        </td>
                         <td width="80px" align="center">
                            <asp:LinkButton ID="linButBack" runat="server" OnClick="LinkButBack_Click" CommandArgument='<%#Eval("Id")%>'>
                                <asp:Image ID="Image4" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                    hspace="2" align="absmiddle" />反审</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <asp:Panel ID="NoDataPanel" runat="server">
            没有记录!</asp:Panel>
        <uc1:UCPaging runat="server" ID="UCPaging1" />
    </div>
</asp:Content>
