<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="PC_TBPC_Purchang_new_detail.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Purchang_new_detail" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
 <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
         <table width="100%">
              <tr>
                    <td align="left">
                        <asp:Label ID="notice1" runat="server" Text="提示:提交审批按钮为提交物料减少数据,提交按钮为提交审核结论！" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Button ID="btntjsp" runat="server" Text="提交审批"                     
                            onclick="btn_tj_Click" Visible="false"/> 
                        <asp:Button ID="btnsc" runat="server" Text="删除" onclick="btnsc_click" Visible="false"/>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; 
                        <asp:Button ID="btnsh1" runat="server" Text="提交" onclick="btnsh1_click" Visible="false"/> 
                            &nbsp;&nbsp;&nbsp;            
                        <asp:Button ID="btnsh2" runat="server" Text="提交" onclick="btnsh2_click" Visible="false"/> 
                            &nbsp;&nbsp;&nbsp; 
                        <asp:Button ID="btnsh3" runat="server" Text="提交" onclick="btnsh3_click" Visible="false"/> 
                            &nbsp;&nbsp;&nbsp;  
                        <asp:Button ID="btnsh4" runat="server" Text="提交" onclick="btnsh4_click" Visible="false"/> 
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;   
                    </td>
               </tr>
          </table>
         </div>
       </div>
    </div>
<asp:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
        ActiveTabIndex="1" AutoPostBack="false">
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="变更明细" TabIndex="0">
        <ContentTemplate>
    <div class="RightContent">
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
                                    <th>
                                        单位
                                    </th>
                                    <th align="center">
                                        计划辅助数量
                                    </th>
                                    <th>
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
                                    <th>
                                        变更状态
                                    </th>
                                    <th>
                                        审核状态
                                    </th>
                                    <th align="center">
                                        备注
                                    </th>
                                    <th>
                                        物料执行状态
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
                                    <asp:TextBox ID="BG_NUM" runat="server" BorderStyle="None" Enabled="false"
                                        Text='<%#Eval("BG_NUM")%>'></asp:TextBox>
                                </td>
                                <td runat="server" align="center">
                                    <asp:TextBox ID="BG_FZNUM" runat="server" BorderStyle="None" Enabled="false"
                                        Text='<%#Eval("BG_FZNUM")%>'></asp:TextBox>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="RESULT" runat="server" BorderStyle="None" Enabled="false"
                                        Text='<%#Eval("RESULT")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbshzt" runat="server" BorderStyle="None" Enabled="false" Text='<%#Eval("shzt")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:TextBox ID="BG_NOTE" runat="server" BorderStyle="None" Enabled="false"
                                        Text='<%#Eval("BG_NOTE")%>' ToolTip='<%#Eval("BG_NOTE")%>'></asp:TextBox>
                                </td>
                                <td runat="server" align="center">
                                    <asp:TextBox ID="tbzxzt" runat="server" BorderStyle="None" Enabled="false"></asp:TextBox>
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
       </ContentTemplate>
       </asp:TabPanel>
       
       
       
       <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="审  核" TabIndex="1">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div style="height: 6px" class="box_top">
                    </div>
                    <div class="box-outer">
                        <table width="100%">
                            <tr>
                                <td style="font-size: large; text-align: center; height: 43px" colspan="2">
                                    变更明细表
                                    <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                        Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="box-outer">
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr>
                            </tr>
                            <tr>
                            </tr>
                            <tr>
                                <td align="center">
                                    技术员
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_first" runat="server" Width="80px"  Enabled="false"></asp:TextBox>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblfirst" RepeatColumns="2" runat="server" Height="20px" Enabled="false">
                                                    <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="first_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="first_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                    Height="32px" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    技术部长
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_second" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblsecond" RepeatColumns="2" runat="server" Height="20px" Enabled="false">
                                                    <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="second_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="second_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                    Height="32px" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    采购部长
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_third" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblthird" RepeatColumns="2" runat="server" Height="20px" Enabled="false">
                                                    <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="third_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="third_opinion" runat="server" Height="32px" TextMode="MultiLine"
                                                    Width="100%" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    采购员反馈
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td align="center" style="width: 10%">
                                                采购员
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_forth" runat="server" Width="80px"  Enabled="false"></asp:TextBox>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                反馈结果
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblforth" RepeatColumns="2" runat="server" Height="20px" Enabled="false">
                                                    <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                反馈时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="forth_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="forth_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                    Height="32px" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
      </asp:TabContainer>
</asp:Content>
