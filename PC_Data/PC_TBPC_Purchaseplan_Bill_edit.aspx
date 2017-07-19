<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Purchaseplan_Bill_edit.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Purchaseplan_Bill_edit"
    Title="物料占用管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagName="UCPaging" TagPrefix="uc1" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    采购计划单
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent" style="overflow: hidden">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td>
                                        备库占用
                                    </td>
                                    <td align="left">
                                        <asp:Panel ID="Panel_view" runat="server" Visible="false">
                                            显示:&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="RadioButton_disagree" runat="server" TextAlign="Right" Text="拒绝项"
                                                GroupName="view" AutoPostBack="True" OnCheckedChanged="RadioButton_disagree_CheckedChanged" />
                                            <asp:RadioButton ID="RadioButton_agree" runat="server" Text="同意项" GroupName="view"
                                                AutoPostBack="True" OnCheckedChanged="RadioButton_agree_CheckedChanged" />
                                            <asp:RadioButton ID="RadioButton_all" runat="server" Text="所有项" GroupName="view"
                                                Checked="true" AutoPostBack="True" OnCheckedChanged="RadioButton_all_CheckedChanged" />
                                        </asp:Panel>
                                        <asp:Panel ID="Panel_baocun" runat="server" Visible="false">
                                            <asp:Label ID="view" runat="server" Text="视图："></asp:Label>
                                            <asp:DropDownList ID="DropDownListview" runat="server" OnSelectedIndexChanged="DropDownListview_SelectedIndexChanged"
                                                AutoPostBack="True">
                                                <asp:ListItem Text="待保存" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="已保存" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="tijiao" runat="server" Text="保存" OnClick="tijiao_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="finish" runat="server" Text="完成" OnClick="finish_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="rightornot" runat="server" Text="1" Visible="false"></asp:Label><%--判断页面输入是否出错--%>
                                        <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_Click" />&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <div>
                            <table width="100%">
                                <tr>
                                    <td style="font-size: small; text-align: center;" colspan="4">
                                        物料需用计划
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;使&nbsp;&nbsp;用&nbsp;&nbsp;部&nbsp;&nbsp;门：
                                        <asp:TextBox ID="tb_dep" runat="server" Enabled="false" Text=""></asp:TextBox>
                                    </td>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：&nbsp;&nbsp;<asp:TextBox ID="Tb_shijian" runat="server"
                                            Text="" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 34%;" align="left">
                                        &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：&nbsp;&nbsp;<asp:TextBox ID="TextBox_pid"
                                            runat="server" Text="" Enabled="false" Width="200px"></asp:TextBox>
                                        <asp:TextBox ID="Label_view" runat="server" Visible="false" Text=""></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;项&nbsp;&nbsp;&nbsp;目：
                                        <asp:TextBox ID="tb_pj" runat="server" Enabled="false" Text=""></asp:TextBox>
                                        <asp:TextBox ID="tb_pjid" runat="server" Visible="false" Text=""></asp:TextBox>
                                    </td>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;工&nbsp;&nbsp;&nbsp;程：
                                        <asp:TextBox ID="tb_eng" runat="server" Enabled="false" Text=""></asp:TextBox>
                                        <asp:TextBox ID="tb_engid" runat="server" Visible="false" Text=""></asp:TextBox>
                                    </td>
                                    <td style="width: 34%;" align="left">
                                        &nbsp;&nbsp;&nbsp;备&nbsp;&nbsp;&nbsp;注：
                                        <asp:TextBox ID="tb_note" runat="server" Text="" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Bottom"
                            ActiveTabIndex="0" BorderStyle="None">
                            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText=" 物料需用计划表" TabIndex="0" BorderStyle="None">
                                <ContentTemplate>
                                    <div style="border: 1px solid #000000; height: 430px; overflow: auto">
                                        <div class="fixbox xscroll">
                                            <table width="100%" class="nowrap fixtable fullwidth">
                                                <asp:Repeater ID="tbpc_purshaseplanbillRepeater" runat="server" OnItemDataBound="tbpc_purshaseplanbillRepeater_ItemDataBound">
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
                                                                <strong>定尺</strong>
                                                            </td>
                                                            <td>
                                                                <strong>剩余库存量</strong>
                                                            </td>
                                                            <td>
                                                                <strong>最大可使用量</strong>
                                                            </td>
                                                            <td>
                                                                <strong>所需数量</strong>
                                                            </td>
                                                            <td>
                                                                <strong>使用库存量</strong>
                                                            </td>
                                                            <td>
                                                                <strong>计划采购量</strong>
                                                            </td>
                                                            <td>
                                                                <strong>数量单位</strong>
                                                            </td>
                                                            <td>
                                                                <strong>状态</strong>
                                                            </td>
                                                            <td>
                                                                <strong>审核意见</strong>
                                                            </td>
                                                            <td>
                                                                <strong>备注</strong>
                                                            </td>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                                <asp:CheckBox ID="CKBOX_USEKCALTER" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                                    AutoPostBack="true" Checked="false" OnCheckedChanged="CheckedChanged"></asp:CheckBox>
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
                                                                <asp:Label ID="PUR_FIXEDSIZETEXT" runat="server" Text='<%#get_pur_fixed(Eval("PUR_FIXEDSIZE").ToString())%>'></asp:Label>
                                                                <asp:Label ID="PUR_FIXEDSIZE" runat="server" Text='<%#Eval("PUR_FIXEDSIZE")%>' Visible="false"></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <%-- <asp:Label ID="PUR_KCNUM" runat="server" Text='<%#Eval("PUR_KCNUM")%>'></asp:Label>--%>
                                                                <asp:Label ID="PUR_KCNUM" runat="server" Text=""></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <%--<asp:Label ID="PUR_MAXUSENUM" runat="server" Text='<%#Eval("PUR_MAXUSENUM")%>' ></asp:Label>--%>
                                                                <asp:Label ID="PUR_MAXUSENUM" runat="server" Text=""></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PUR_NEDDNUM" runat="server" Text='<%#Eval("PUR_NEDDNUM")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="PUR_USEKCNUM" runat="server" BorderStyle="None" Enabled="False"
                                                                    OnTextChanged="Textchanged" AutoPostBack="True" Text='<%#Eval("PUR_USEKCNUM")%>'
                                                                    Width="80px" Style="text-align: center" onclick="javascript:this.select();"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PUR_PLANNUM" runat="server" BorderStyle="None" Enabled="False" Text='<%#Eval("PUR_PLANNUM")%>'
                                                                    Width="80px" Style="text-align: center"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PUR_NUNIT" runat="server" Text='<%#Eval("PUR_NUNIT")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PR_STATETEXT" runat="server" Text='<%#get_pr_state(Eval("PUR_STATE").ToString())%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PUR_STATETEXT" runat="server" Text='<%#get_pur_state(Eval("PUR_STATE").ToString())%>'></asp:Label>
                                                                <asp:Label ID="PUR_STATE" runat="server" Text='<%#Eval("PUR_STATE")%>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PUR_NOTE" runat="server" Text='<%#Eval("PUR_NOTE")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr>
                                                    <td colspan="17" align="center">
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
                                                        审批人:<asp:TextBox ID="Tb_shenpiren" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="Tb_shenpirenid" runat="server" Visible="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        申请人:<asp:TextBox ID="Tb_shenqingren" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="Tb_shenqingrenid" runat="server" Visible="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        制单:<asp:TextBox ID="TextBoxexecutor" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="TextBoxexecutorid" runat="server" Visible="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText=" 黑色金属" TabIndex="0" BorderStyle="None">
                                <ContentTemplate>
                                    <div style="border: 1px solid #000000; height: 430px; overflow: auto">
                                        <div class="fixbox xscroll">
                                            <table width="100%" class="nowrap fixtable fullwidth">
                                                <%-- OnItemDataBound="hesejinshuRepeater_ItemDataBound"--%>
                                                <asp:Repeater ID="hesejinshuRepeater" runat="server">
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
                                                                <strong>所需重量</strong>
                                                            </td>
                                                            <td>
                                                                <strong>使用库存重量</strong>
                                                            </td>
                                                            <td>
                                                                <strong>计划采购重量</strong>
                                                            </td>
                                                            <td>
                                                                <strong>所需数量</strong>
                                                            </td>
                                                            <td>
                                                                <strong>使用库存数量</strong>
                                                            </td>
                                                            <td>
                                                                <strong>采购数量</strong>
                                                            </td>
                                                            <td>
                                                                <strong>状态</strong>
                                                            </td>
                                                            <td>
                                                                <strong>审核意见</strong>
                                                            </td>
                                                            <td>
                                                                <strong>备注</strong>
                                                            </td>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                                <asp:CheckBox ID="CKBOX_USEKCALTER" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                                    AutoPostBack="true" Checked="false" OnCheckedChanged="CheckedChanged"></asp:CheckBox>
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
                                                                <asp:Label ID="PUR_WEIGHT" runat="server" Text='<%#Eval("PUR_WEIGHT")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="PUR_USTWEIGHT" runat="server" BorderStyle="None" Enabled="False"
                                                                    Text='<%#Eval("PUR_USTWEIGHT")%>' Width="80px" Style="text-align: center" onclick="javascript:this.select();"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PUR_RPWEIGHT" runat="server" BorderStyle="None" Enabled="False"
                                                                    Text='<%#Eval("PUR_RPWEIGHT")%>' Width="80px" Style="text-align: center"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PUR_NUM" runat="server" Text='<%#Eval("PUR_NUM")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="PUR_USTNUM" runat="server" BorderStyle="None" Enabled="False" Text='<%#Eval("PUR_USTNUM")%>'
                                                                    Width="80px" Style="text-align: center" onclick="javascript:this.select();"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PUR_RPNUM" runat="server" BorderStyle="None" Enabled="False" Text='<%#Eval("PUR_RPNUM")%>'
                                                                    Width="80px" Style="text-align: center"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PR_STATETEXT" runat="server" Text='<%#get_pr_state(Eval("PUR_STATE").ToString())%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PUR_STATETEXT" runat="server" Text='<%#get_pur_state(Eval("PUR_STATE").ToString())%>'></asp:Label>
                                                                <asp:Label ID="PUR_STATE" runat="server" Text='<%#Eval("PUR_STATE")%>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PUR_NOTE" runat="server" Text='<%#Eval("PUR_NOTE")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr>
                                                    <td colspan="17" align="center">
                                                        <asp:Panel ID="Panel1" runat="server" Visible="false">
                                                            没有数据！</asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div>
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        审批人:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox2" runat="server" Visible="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        申请人:<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox4" runat="server" Visible="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        制单:<asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="TextBox6" runat="server" Visible="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="审核结果" TabIndex="1" BorderStyle="None">
                                <ContentTemplate>
                                    <div style="border: 1px solid #000000; height: 430px; overflow: auto">
                                        <div style="height: 240px; overflow: auto">
                                            <asp:Panel ID="Panel_shenhe" runat="server" Enabled="false">
                                                <table width="100%" style="border: 1px" cellpadding="2" cellspacing="0">
                                                    <tr align="center">
                                                        <td style="border-bottom: solid 1px black; border-right: solid 1px black; border-collapse: collapse">
                                                            审&nbsp;核&nbsp;意&nbsp;见:
                                                        </td>
                                                        <td style="border-bottom: solid 1px black;" align="left">
                                                            <asp:TextBox ID="suggestion" Columns="100" Rows="3" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td style="border-bottom: solid 1px black; border-right: solid 1px black;">
                                                            审&nbsp;核&nbsp;结&nbsp;论:
                                                        </td>
                                                        <td style="border-bottom: solid 1px black;" align="left">
                                                            同意<asp:RadioButton ID="radcon_agree" GroupName="shenhe" runat="server" />&nbsp;&nbsp;&nbsp;
                                                            拒绝<asp:RadioButton ID="radcon_disagree" GroupName="shenhe" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="right">
                                                            审&nbsp;核&nbsp;人:<asp:TextBox ID="TextBox_shenheren" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;
                                                            审&nbsp;核&nbsp;时&nbsp;间:<asp:TextBox ID="TextBox_shenhesj" runat="server" Text=""></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </cc1:TabPanel>
                        </cc1:TabContainer>
                        <div align="center">
                            <asp:Label ID="Labelerror" runat="server" Text="" Visible="False"></asp:Label><%--显示操作结果成功/失败--%>
                            <asp:Label ID="container" runat="server" Text="" Visible="false"></asp:Label><%--所有工程是否完成保存--%>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
