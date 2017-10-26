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
    </style>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="box-outer">
        <table class="top-table">
            <tr>
                <td>
                    任务号
                </td>
                <td>
                    <asp:TextBox ID="txt_task_code" class="txtbox" runat="server"></asp:TextBox>
                </td>
                <td>
                    合同号
                </td>
                <td>
                    <asp:TextBox ID="txt_contract_code" class="txtbox" runat="server"></asp:TextBox>
                </td>
                <td>
                    项目名称
                </td>
                <td>
                    <asp:TextBox ID="txt_project_name" class="txtbox" runat="server"></asp:TextBox>
                </td>
                <td>
                    编制进度
                </td>
                <td>
                    <asp:DropDownList ID="ddl_state" runat="server" class="txtbox" AutoPostBack="true"
                        onselectedindexchanged="ddl_state_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:LinkButton ID="btn_search" runat="server" OnClick="btn_search_Click">
                        <asp:Image ID="Image1" runat="server" ImageUrl="../Assets/images/search.gif" Style="vertical-align: top" />
                        查 询
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pal_container" runat="server">
            <div style="height: 350px;">
                <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1" class="toptable grid">
                    <asp:Repeater ID="rpt_task_list" runat="server">
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
                                    设备名称
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
                            <tr align="center" class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                <td>
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
                                    <%#Eval("equipment_name").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("task_type").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("c_total_task_budget").ToString()%>
                                </td>
                                <td>
                                    <%#getTaskState(Eval("state").ToString())%>
                                </td>
                                <td>
                                    <asp:HyperLink ID="HyperLink1" NavigateUrl='<%#"ys_task_budget_detail.aspx?tsak_id="+Eval("task_code").ToString()%>'
                                        runat="server">
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
