<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="PC_BGYM.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_BGYM" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <p>物料计划变更</p>
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
    <link type="text/css" href="commanrepeater.css" rel="stylesheet" />
    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<div class="RightContent">
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="98%">
                    <tr>
                        <td style="width:80%" align="right">
                            <asp:Button ID="btnqrbg" runat="server" Text="保存"                     
                                onclick="btn_save_click" OnClientClick="javascript:return confirm('确认变更吗？')"/> 
                                &nbsp;&nbsp;&nbsp              
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="提交审批"                     
                                onclick="btn_tj_Click" Visible="false"/> 
                        </td>
                        <td>
                            <asp:Button ID="Button2" runat="server" Text="删除"                     
                                onclick="btn_delete_click"/> 
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    
     <div class="box-wrapper">
                    <div class="box-outer" style="overflow:scroll">
                    <table id="table1" cellpadding="2" width="100%" cellspacing="1" class="toptable grid nowrap"
                                border="1">
                    <asp:Repeater ID="rptProNumCost" runat="server">
                        <HeaderTemplate>
                                <tr align="center">
                                    <th align="center">
                                        序号
                                    </th>
                                    <th align="center">
                                        批号
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
                                        计划数量
                                    </th>
                                    <th align="center">
                                        单位
                                    </th>
                                    <th align="center">
                                        计划辅助数量
                                    </th>
                                    <th align="center">
                                        辅助单位
                                    </th>
                                    <th align="center">
                                        申请人
                                    </th>
                                    <th align="center">
                                        申请日期
                                    </th>
                                    <th align="center">
                                        变更数量
                                    </th>
                                    <th align="center">
                                        变更辅助数量
                                    </th>
                                    <th align="center">
                                        备注
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                <td>
                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                        Checked="false"></asp:CheckBox>&nbsp;
                                    <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="planno" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("planno")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Aptcode" runat="server" ForeColor="#1A438E" Style="background-color: Transparent;
                                        text-align: center" Width="150px" Text='<%#Eval("Aptcode")%>' ToolTip='<%#Eval("Aptcode")%>'></asp:TextBox>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="PUR_MASHAPE" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("PUR_MASHAPE")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="marid" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("marid")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="marnm" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("marnm")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="margg" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("margg")%>'></asp:Label>
                                </td>
                                
                                <td runat="server" align="center">
                                    <asp:Label ID="marcz" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("marcz")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="margb" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("margb")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="num" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("num")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbunit" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("marunit")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="fznum" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("fznum")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbfzunit" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("marfzunit")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="sqrnm" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("sqrnm")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="sqrtime" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("sqrtime")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:TextBox ID="BG_NUM" runat="server" BorderStyle="None"
                                        Text='<%#Eval("BG_NUM")%>'></asp:TextBox>
                                </td>
                                <td runat="server" align="center">
                                    <asp:TextBox ID="BG_FZNUM" runat="server" BorderStyle="None"
                                        Text='<%#Eval("BG_FZNUM")%>'></asp:TextBox>
                                </td>
                                <td runat="server" align="center">
                                    <asp:TextBox ID="BG_NOTE" runat="server" BorderStyle="None"
                                        Text='<%#Eval("BG_NOTE")%>'></asp:TextBox>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </table>
                    <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                        没有记录!<br />
                        <br />
                    </asp:Panel>
            </div>
            </div>
        </div>
</asp:Content>
