<%@ Page Language="C#" MasterPageFile="~/Masters/NoScollRightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="ys_task_budget_list.aspx.cs" Inherits="ZCZJ_DPF.YS_Data.UI.ys_task_budget_list"
    Title="" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    任务预算编制列表
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        th
        {
            border-bottom: 0;
            border-style: dotted;
        }
        .top-table
        {
            width: 100%;
        }
        .number
        {
            text-align: right;
        }
    </style>
    <div class="box-outer">
        <asp:Panel ID="pal_contract" runat="server">
            <table class="top-table">
                <tr>
                    <td>
                        合同号：<asp:Label ID="lb_contract_code" runat="server"></asp:Label></td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pal_search" runat="server">
            <table class="top-table">
                <tr>
                    <td>
                            
                        <asp:RadioButtonList ID="rbl_myState" runat="server" RepeatDirection="Horizontal"
                        AutoPostBack="True" OnSelectedIndexChanged="btn_search_Click">
                            <asp:ListItem Text="全部"  ></asp:ListItem>                          
                            <asp:ListItem Text="我的任务" Value="1" Selected="true" ></asp:ListItem>
                            <asp:ListItem Text="被驳回" Value="3"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        任务号：
                        <asp:TextBox ID="txt_task_code" class="txtbox" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        合同号：
                        <asp:TextBox ID="txt_contract_code" class="txtbox" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        产品名称：
                        <asp:TextBox ID="txt_project_name" class="txtbox" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        编制进度：
                        <asp:DropDownList ID="ddl_state" runat="server" class="txtbox">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btn_search" runat="server" Text=" 查 询 " OnClick="btn_search_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pal_container" runat="server">
            <div style="height: 350px;">
                <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1" class="toptable grid">
                    <asp:Repeater ID="rpt_task_list" runat="server" OnItemDataBound="rpt_task_list_databound">
                        <HeaderTemplate>
                            <tr class="tableTitle">
                                <th>
                                    序号
                                </th>
                                <th>
                                    任务号
                                </th>
                                <th>
                                    合同号
                                </th>
                                <th>
                                    项目名称
                                </th>
                                <th>
                                    产品名称
                                </th>
                                <th>
                                    任务号类型
                                </th>
                                <th>
                                    预算总额
                                </th>
                                <th>
                                    状态
                                </th>
                                <th>
                                    操作
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr align="center" class="baseGadget" id="row" runat="Server" onmouseover="this.className='highlight'"
                                onmouseout="this.className='baseGadget'">
                                <td id="td_1" runat="server">
                                    <%#Container.ItemIndex + 1%>
                                </td>
                                <td>
                                    <%#Eval("task_code").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("contract_code").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("project_name").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("product_name").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("task_type").ToString()%>
                                </td>
                                <td class="number">
                                    <%#Eval("c_total_task_budget").ToString()%>
                                </td>
                                <td id='td_state' runat="Server">
                                    <%#Eval("state").ToString()%>
                                </td>
                                <td>
                                    <asp:HyperLink ID="HyperLink1" NavigateUrl='<%#"ys_task_budget_detail.aspx?tsak_code="+Eval("task_code").ToString()%>'
                                        Target="_blank" runat="server">
                                        <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                            runat="server" />
                                        进入</asp:HyperLink>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <uc1:UCPaging ID="UCPaging" runat="server" />
        </asp:Panel>
        <asp:Panel ID="NoDataPanel" runat="server">
            没有记录!
        </asp:Panel>
    </div>

    <script src="../JS/jquery/jquery-3.2.1.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        
    </script>

</asp:Content>
