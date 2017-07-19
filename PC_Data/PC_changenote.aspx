<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_changenote.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_changenote"
    Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="98%">
                        <tr>
                            <td>
                                <asp:RadioButton ID="radio_all" runat="server" Text="全部" GroupName="shenhe" OnCheckedChanged="radio_CheckedChanged"
                                    AutoPostBack="True" />
                                <asp:RadioButton ID="radio_weiqueren" runat="server" Text="未确认" GroupName="shenhe"
                                    OnCheckedChanged="radio_CheckedChanged" AutoPostBack="True" Checked="true" />
                                <asp:RadioButton ID="radio_tongguo" runat="server" Text="确认通过" GroupName="shenhe"
                                    OnCheckedChanged="radio_CheckedChanged" AutoPostBack="True" />
                                <asp:RadioButton ID="radio_butongguo" runat="server" Text="确认不通过" GroupName="shenhe"
                                    OnCheckedChanged="radio_CheckedChanged" AutoPostBack="True" />
                            </td>
                            <td>
                                计划跟踪号：<asp:TextBox ID="tbptc" runat="server" BorderStyle="Solid"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp
                                <asp:Button ID="btnsearch" runat="server" Text="查询" OnClick="btnsearch_click" />
                            </td>
                            <td>
                                &nbsp;&nbsp;&nbsp
                                <asp:Button ID="btntongguo" runat="server" Text="确认通过" OnClick="btntongguo_click" />
                                &nbsp;&nbsp;&nbsp
                                <asp:Button ID="btnbutongguo" runat="server" Text="确认不通过" OnClick="btnbutongguo_click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                                <asp:Button ID="btndelete" runat="server" Text="删除" OnClick="btndelete_click" />
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
                    <asp:Repeater ID="purchaseplan_start_list_Repeater" runat="server">
                        <HeaderTemplate>
                            <tr align="center">
                                <th align="center">
                                    序号
                                </th>
                                <th align="center">
                                    计划跟踪号
                                </th>
                                <th align="center">
                                    类型
                                </th>
                                <th align="center">
                                    物料编码
                                </th>
                                <th align="center">
                                    物料名称
                                </th>
                                <th align="center">
                                    规格
                                </th>
                                <th align="center">
                                    材质
                                </th>
                                <th align="center">
                                    国标
                                </th>
                                <th align="center">
                                    数量
                                </th>
                                <th>
                                    单位
                                </th>
                                <th align="center">
                                    辅助数量
                                </th>
                                <th>
                                    辅助单位
                                </th>
                                <th align="center">
                                    物料申请人
                                </th>
                                <th align="center">
                                    物料申请时间
                                </th>
                                <th align="center">
                                    操作人
                                </th>
                                <th align="center">
                                    操作时间
                                </th>
                                <th align="center">
                                    确认人
                                </th>
                                <th align="center">
                                    确认时间
                                </th>
                                <th align="center">
                                    备注内容
                                </th>
                                <th align="center">
                                    状态
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="this.className='highlight'" ondblclick="redirectw(this)"
                                onclick="javascript:change(this);" onmouseout="this.className='baseGadget'">
                                <td>
                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                        Checked="false" onclick="checkme(this)"></asp:CheckBox>&nbsp; &nbsp;
                                    <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="Aptcode" runat="server" ForeColor="#1A438E" Style="background-color: Transparent;
                                        text-align: center" Width="120px" Text='<%#Eval("Aptcode")%>' ToolTip='<%#Eval("Aptcode")%>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="PUR_MASHAPE" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("PUR_MASHAPE")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="marid" runat="server" BorderStyle="None" BackColor="Transparent" Text='<%#Eval("marid")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="marnm" runat="server" BorderStyle="None" BackColor="Transparent" Text='<%#Eval("marnm")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="margg" runat="server" BorderStyle="None" BackColor="Transparent" Text='<%#Eval("margg")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="marcz" runat="server" BorderStyle="None" BackColor="Transparent" Text='<%#Eval("marcz")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="margb" runat="server" BorderStyle="None" BackColor="Transparent" Text='<%#Eval("margb")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="num" runat="server" BorderStyle="None" BackColor="Transparent" Text='<%#Eval("num")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbunit" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("marunit")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="fznum" runat="server" BorderStyle="None" BackColor="Transparent" Text='<%#Eval("fznum")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbfzunit" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("marfzunit")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="sqrnm" runat="server" BorderStyle="None" BackColor="Transparent" Text='<%#Eval("sqrnm")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="sqrtime" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("sqrtime")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="changername" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("changername")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="changetime" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("changetime")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="querenren" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("querenren")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="querentime" runat="server" BorderStyle="None" Enabled="false" Text='<%#Eval("querentime")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="changecontent" runat="server" BorderStyle="None" Width="120px" Text='<%#Eval("changecontent")%>'
                                        ToolTip='<%#Eval("changecontent")%>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="changestate" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("changestate").ToString().Trim()==""?"未确认":Eval("changestate").ToString().Trim()=="0"?"未确认":Eval("changestate").ToString().Trim()=="1"?"确认通过":"确认未通过"%>'></asp:Label>
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
