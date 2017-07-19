<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Marreplace_cl.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Marreplace_cl"
    Title="物料代用管理" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="right" width="44%">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:Button ID="btn_rep" runat="server" Text="代用" OnClick="btn_rep_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_Click" />
                                    &nbsp;&nbsp;
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <asp:Panel ID="Paneleng_pj" runat="server">
                    <table width="100%">
                        <tr>
                            <td align="center" colspan="3">
                                物料需用单
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 33%;" align="left">
                                &nbsp;&nbsp;&nbsp;使&nbsp;&nbsp;用&nbsp;&nbsp;部&nbsp;&nbsp;门：
                                <asp:TextBox ID="tb_dep" runat="server" Enabled="false"></asp:TextBox>
                                <asp:TextBox ID="tb_depid" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td style="width: 33%;" align="left">
                                &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：&nbsp;&nbsp;<asp:TextBox ID="Tb_shijian" runat="server"
                                    Enabled="false"></asp:TextBox>
                            </td>
                            <td style="width: 34%;" align="left">
                                &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：&nbsp;&nbsp;<asp:TextBox ID="Tb_mpid" runat="server"
                                    Enabled="false" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 33%;" align="left">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;项&nbsp;&nbsp;&nbsp;目：
                                <asp:TextBox ID="tb_pjinfo" runat="server" Enabled="false" Text=""></asp:TextBox>
                                <asp:TextBox ID="tb_pj" runat="server" Visible="false" Text=""></asp:TextBox>
                                <asp:TextBox ID="tb_pjid" runat="server" Visible="false" Text=""></asp:TextBox>
                            </td>
                            <td style="width: 33%;" align="left">
                                &nbsp;&nbsp;&nbsp;工&nbsp;&nbsp;&nbsp;程：
                                <asp:TextBox ID="tb_enginfo" runat="server" Enabled="false" Text=""></asp:TextBox>
                                <asp:TextBox ID="tb_eng" runat="server" Visible="false" Text=""></asp:TextBox>
                                <asp:TextBox ID="tb_engid" runat="server" Visible="false" Text=""></asp:TextBox>
                            </td>
                            <td id="Td4" style="width: 34%;" align="left" runat="server">
                                &nbsp;&nbsp;&nbsp;备&nbsp;&nbsp;&nbsp;注：
                                <asp:TextBox ID="tb_note" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div style="height: 430px; overflow: auto; width: 100%">
                        <div class="cpbox xscroll">
                            <table id="tab" class="nowrap cptable fullwidth" align="center">
                                <asp:Repeater ID="Purchaseplan_startcontentRepeater" runat="server">
                                    <HeaderTemplate>
                                        <tr align="center" class="tableTitle" style="background-color:#5CACEE">
                                            <td>
                                                <strong>行号</strong>
                                            </td>
                                            <td>
                                                <strong>计划号</strong>
                                            </td>
                                            <td>
                                                <strong>材料ID</strong>
                                            </td>
                                            <td>
                                                <strong>名称</strong>
                                            </td>
                                            <td>
                                                <strong>规格</strong>
                                            </td>
                                            <td>
                                                <strong>材质</strong>
                                            </td>
                                            <td>
                                                <strong>国标</strong>
                                            </td>
                                            <td>
                                                <strong>单位</strong>
                                            </td>
                                            <td>
                                                <strong>数量</strong>
                                            </td>
                                            <td>
                                                <strong>建议采购日期</strong>
                                            </td>
                                            <td>
                                                <strong>到货日期</strong>
                                            </td>
                                            <td>
                                                <strong>备库占用数量</strong>
                                            </td>
                                            <td>
                                                <strong>计划模式</strong>
                                            </td>
                                            <td>
                                                <strong>执行人</strong>
                                            </td>
                                            <td>
                                                <strong>执行数量</strong>
                                            </td>
                                            <td>
                                                <strong>是否生成比价单</strong>
                                            </td>
                                            <td>
                                                <strong>是否生成订单</strong>
                                            </td>
                                            <td>
                                                <strong>备注</strong>
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                            <td>
                                                <asp:Label ID="Lab_NUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                    Checked="false"></asp:CheckBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_PTCODE" runat="server" Text='<%#Eval("PUR_PTCODE")%>'></asp:Label>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_MARID" runat="server" Text='<%#Eval("PUR_MARID")%>'></asp:Label>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_MARNAME" runat="server" Text='<%#Eval("PUR_MARNAME")%>'></asp:Label>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_MARNORM" runat="server" Text='<%#Eval("PUR_MARNORM")%>'></asp:Label>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_MARTERIAL" runat="server" Text='<%#Eval("PUR_MARTERIAL")%>'></asp:Label>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_GUOBIAO" runat="server" Text='<%#Eval("PUR_GUOBIAO")%>'></asp:Label>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_NUNIT" runat="server" Text='<%#Eval("PUR_NUNIT")%>'></asp:Label>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_NUM" runat="server" Text='<%#Eval("PUR_NUM")%>'></asp:Label>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_PFDATE" runat="server" Text='<%#Eval("PUR_PFDATE")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_RFDATE" runat="server" Text='<%#Eval("PUR_RFDATE")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_USTNUM" runat="server" Text='<%#Eval("PUR_USTNUM")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="MTO"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_CGMAN" runat="server" Text='<%#Eval("PUR_CGMAN")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="PUR_CGMANNM" runat="server" Text='<%#Eval("PUR_CGMANNM")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_RPNUM" runat="server" Text='<%#Eval("PUR_RPNUM")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_BJD" runat="server" Text='<%#get_pur_bjd(Eval("PUR_PRONODE").ToString(),Eval("PUR_STATE").ToString())%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_DD" runat="server" Text='<%#get_pur_dd(Eval("PUR_PRONODE").ToString(),Eval("PUR_STATE").ToString())%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="PUR_NOTE" runat="server" Text='<%#Eval("PUR_NOTE")%>'></asp:Label>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td colspan="18" align="center">
                                        <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                            没有数据！</asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <table width="100%">
                                <tr>
                                    <td>
                                        负责人:<asp:TextBox ID="Tb_fuziren" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="Tb_fuzirenid" runat="server" Visible="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        申请人:<asp:TextBox ID="Tb_shenqingren" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="Tb_shenqingrenid" runat="server" Visible="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        制单:<asp:TextBox ID="TextBoxexecutor" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="TextBoxexecutorid" runat="server" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
