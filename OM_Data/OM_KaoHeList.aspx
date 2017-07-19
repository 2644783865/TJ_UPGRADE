<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_KaoHeList.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_KaoHeList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    员工绩效考核记录
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <table width="100%">
        <tr width="100%">
            <td style="width: 300px" align="left">
                时间：
                <asp:DropDownList ID="dplYear" runat="server">
                </asp:DropDownList>
                &nbsp;年&nbsp;
                <asp:DropDownList ID="dplMoth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Year_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;月&nbsp;&nbsp;&nbsp;&nbsp;
                部门：
                <asp:DropDownList ID="ddl_Depart" runat="server" Width="100px" AutoPostBack="true"
                    OnSelectedIndexChanged="ddl_Year_SelectedIndexChanged">
                </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                考核类型：
                <asp:DropDownList ID="ddlType" runat="server" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddl_Year_SelectedIndexChanged">
                    <asp:ListItem Text="请选择" Value="00"></asp:ListItem>
                    <asp:ListItem Text="人员月度考核" Value="人员月度考核"></asp:ListItem>
                    <asp:ListItem Text="人员年度考核" Value="人员年度考核"></asp:ListItem>
                    <asp:ListItem Text="入职转正考核" Value="入职转正考核"></asp:ListItem>
                    <asp:ListItem Text="合同续订考核" Value="合同续订考核"></asp:ListItem>
                    <asp:ListItem Text="合同主体转移考核" Value="合同主体转移考核"></asp:ListItem>
                    <asp:ListItem Text="实习生实习期考核" Value="实习生实习期考核"></asp:ListItem>
                    <asp:ListItem Text="员工岗位调整考核" Value="员工岗位调整考核"></asp:ListItem>
                </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                姓名：
                <asp:TextBox ID="txtName" runat="server" Width="90px"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="查 询" OnClick="ddl_Year_SelectedIndexChanged" />
            </td>
            
        </tr>
    </table>
    <table width="100%">
        <tr width="100%">
            <td>
                评价状态：
            </td>
            <td>
                <asp:RadioButtonList runat="server" ID="rblState" OnSelectedIndexChanged="ddl_Year_SelectedIndexChanged"
                    RepeatColumns="5" AutoPostBack="true">
                    <asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="未发起" Value="0"></asp:ListItem>
                    <asp:ListItem Text="待评价" Value="1"></asp:ListItem>
                    <asp:ListItem Text="已评价" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                审核状态：
            </td>
            <td>
                <asp:RadioButtonList runat="server" ID="rblshstate" OnSelectedIndexChanged="rblshstate_SelectedIndexChanged"
                    RepeatColumns="5" AutoPostBack="true">
                    <asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="审核中" Value="1"></asp:ListItem>
                    <asp:ListItem Text="已通过" Value="2"></asp:ListItem>
                    <asp:ListItem Text="已驳回" Value="3"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            
            <td align="right">
                <asp:HyperLink ID="HyperLink4" NavigateUrl="OM_KaoHe_Deadline.aspx?action=add" runat="server">
                    <asp:Image ID="Image4" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                        align="absmiddle" runat="server" />
                    考核日期确定</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="hpTask" NavigateUrl="OM_KaoHe.aspx?action=add" runat="server">
                    <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                        align="absmiddle" runat="server" />
                    添加人员考核</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="HyperLink3" NavigateUrl="OM_KaoHeAddPiLiang.aspx?action=add" runat="server">
                    <asp:Image ID="Image3" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                        align="absmiddle" runat="server" />
                    批量添加人员考核</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDelete" ForeColor="Red" runat="server" Text="删除" OnClick="btnDelete_OnClick" Visible="false" />
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
                                    <strong>考核年月</strong>
                                </td>
                                <td>
                                    <strong>考核时间</strong>
                                </td>
                                <td>
                                    <strong>考核分数</strong>
                                </td>
                                <td>
                                    <strong>评价状态</strong>
                                </td>
                                <td>
                                    <strong>审核状态</strong>
                                </td>
                                <td>
                                    <strong>添加人</strong>
                                </td>
                                <td>
                                    <strong>查看</strong>
                                </td>
                                <td>
                                    <strong>编辑</strong>
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
                                    <%#Eval("kh_State").ToString().Contains("0") ? "未发起" : Eval("kh_State").ToString().Contains("7") || Eval("kh_State").ToString().Contains("6")?"已评价":"待评价"%>
                                </td>
                                <td>
                                    <%#Eval("Kh_shtoltalstate").ToString().Contains("2") ? "已审核" : Eval("Kh_shtoltalstate").ToString().Contains("3")? "已驳回" : "审核中"%>
                                </td>
                                <td>
                                    <%#Eval("kh_Add")%>
                                </td>
                                <td>
                                    <asp:HyperLink NavigateUrl='<%#"~/OM_Data/OM_KaoHe.aspx?action=view&id="+Eval("kh_Context")%>'
                                        runat="server" ID="HyperLink1">
                                        <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                            runat="server" />查看
                                    </asp:HyperLink>
                                </td>
                                <td width="100px">
                                    <asp:HyperLink NavigateUrl='<%#"~/OM_Data/OM_KaoHe.aspx?action=edit&id="+Eval("kh_Context")%>'
                                        runat="server" ID="HyperLink2">
                                        <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                            runat="server" />编辑
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
