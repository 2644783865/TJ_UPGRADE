<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Purchange_new.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Purchange_new"
    Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <p>
        物料计划变更</p>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script language="javascript" type="text/javascript">
function redirectw(obj) {
            var ph;
            ph = obj.getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
           // window.open("PC_TBPC_Purchang_new_detail.aspx?pid=" + escape(ph));
            window.location = "PC_TBPC_Purchang_new_detail.aspx?pid=" + escape(ph);
        }
    </script>

    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="98%">
                        <tr>
                            <td>
                                <asp:RadioButton ID="radio_all" runat="server" Text="全部" GroupName="shenhe" OnCheckedChanged="radio_all_CheckedChanged"
                                    AutoPostBack="True" />
                                <asp:RadioButton ID="radio_mytask" runat="server" Text="我的任务" GroupName="shenhe"
                                    OnCheckedChanged="radio_mytask_CheckedChanged" AutoPostBack="True" Checked="true" />
                            </td>
                            <td>
                                变更批号：<asp:TextBox ID="tbbgph" runat="server" BorderStyle="Solid"></asp:TextBox>
                            </td>
                            <td>
                                计划跟踪号：<asp:TextBox ID="tbptc" runat="server" BorderStyle="Solid"></asp:TextBox>
                            </td>
                            <td>
                                审批状态：<asp:DropDownList ID="drp_state" runat="server" OnSelectedIndexChanged="drp_state_SelectedIndexChanged"
                                    AutoPostBack="True">
                                    <asp:ListItem Value="">-请选择-</asp:ListItem>
                                    <asp:ListItem Value="0">初始化</asp:ListItem>
                                    <asp:ListItem Value="1">已提交</asp:ListItem>
                                    <asp:ListItem Value="审核中">审核中</asp:ListItem>
                                    <asp:ListItem Value="已执行">已通过</asp:ListItem>
                                    <asp:ListItem Value="已驳回">已驳回</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp
                                <asp:Button ID="btncx" runat="server" Text="查询" OnClick="btncx_click" />
                            </td>
                            <td>
                                <asp:HyperLink ID="hlchangenote" Target="_blank" NavigateUrl="~/PC_Data/PC_changenote.aspx"
                                    CssClass="link" runat="server">
                                    <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" runat="server" />
                                    备注变更信息
                                </asp:HyperLink>
                                <asp:Label ID="Label5" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer" style="overflow: scroll">
                <table id="table1" cellpadding="2" width="100%" cellspacing="1" class="toptable grid nowrap"
                    border="1">
                    <asp:Repeater ID="rptProNumCost" runat="server">
                        <HeaderTemplate>
                            <tr align="center">
                                <th align="center">
                                    序号
                                </th>
                                <th align="center">
                                    变更批号
                                </th>
                                <th align="center">
                                    计划跟踪号
                                </th>
                                <th align="center">
                                    类型
                                </th>
                                <th align="center">
                                    申请人
                                </th>
                                <th align="center">
                                    申请时间
                                </th>
                                <th align="center">
                                    变更数量
                                </th>
                                <th align="center">
                                    变更辅助数量
                                </th>
                                <th>
                                    变更状态
                                </th>
                                <th>
                                    审核状态
                                </th>
                                <th align="center">
                                    备注
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="this.className='highlight'" ondblclick="redirectw(this)"
                                onclick="javascript:change(this);" onmouseout="this.className='baseGadget'">
                                <td>
                                    &nbsp;
                                    <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;
                                </td>
                                <td id="Td1" runat="server" align="center">
                                    <asp:Label ID="BG_PH" runat="server" BorderStyle="None" BackColor="Transparent" Text='<%#Eval("BG_PH")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Aptcode" runat="server" ForeColor="#1A438E" Style="background-color: Transparent;
                                        text-align: center" Width="150px" Text='<%#Eval("Aptcode")%>' ToolTip='<%#Eval("Aptcode")%>'></asp:TextBox>
                                </td>
                                <td id="Td2" runat="server" align="center">
                                    <asp:Label ID="PUR_MASHAPE" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("PUR_MASHAPE")%>'></asp:Label>
                                </td>
                                <td id="Td3" runat="server" align="center">
                                    <asp:Label ID="BG_NAME" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("BG_NAME")%>'></asp:Label>
                                </td>
                                <td id="Td4" runat="server" align="center">
                                    <asp:Label ID="BG_DATE" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("BG_DATE")%>'></asp:Label>
                                </td>
                                <td id="Td5" runat="server" align="center">
                                    <asp:Label ID="BG_NUM" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("BG_NUM")%>'></asp:Label>
                                </td>
                                <td id="Td8" runat="server" align="center">
                                    <asp:Label ID="BG_FZNUM" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("BG_FZNUM")%>'></asp:Label>
                                </td>
                                <td id="Td9" runat="server" align="center">
                                    <asp:Label ID="RESULT" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("RESULT")%>'></asp:Label>
                                </td>
                                <td id="Td6" runat="server" align="center">
                                    <asp:Label ID="lbshzt" runat="server" BorderStyle="None" Enabled="false" Text='<%#Eval("shzt")%>'></asp:Label>
                                </td>
                                <td id="Td14" runat="server" align="center">
                                    <asp:TextBox ID="BG_NOTE" runat="server" BorderStyle="None" Text='<%#Eval("BG_NOTE")%>'
                                        ToolTip='<%#Eval("BG_NOTE")%>'></asp:TextBox>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Panel ID="NoDataPanel" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                    没有记录！</asp:Panel>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
