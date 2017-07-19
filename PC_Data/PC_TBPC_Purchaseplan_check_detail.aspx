<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Purchaseplan_check_detail.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Purchaseplan_check_detail"
    Title="物料占用管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagName="UCPaging" TagPrefix="uc1" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    审核
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script language="JavaScript" type="text/javascript">
        function document.onreadystatechange() {
            if (document.readyState == "complete") {
                var ptc = "<%=gloabptc%>";
                if (ptc != "") {
                    var col = 1;
                    var table = document.getElementById("tab");
                    var tr = table.getElementsByTagName("tr");
                    for (i = 1; i < tr.length - 1; i++) {
                        var Qptc = tr[i].getElementsByTagName("td")[col].getElementsByTagName("span")[0].innerHTML;
                        if (Qptc == ptc) {
                            tr[i].style.backgroundColor = 'Orange';
                        }
                        else {
                            tr[i].style.backgroundColor = '';
                        }
                    }
                }
            }
        }   
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
   <%-- <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>--%>
    <div class="RightContent">
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td align="left" width="10%">
                                        材料计划单：
                                    </td>
                                    <td align="left" width="15%">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="selectall" runat="server" Text="全选" AutoPostBack="true" TextAlign="left"
                                            Checked="false" OnCheckedChanged="selectall_CheckedChanged" />
                                        <asp:Button ID="btn_LX" runat="server" Text="连选" OnClick="btn_LX_click" />
                                        <asp:Button ID="btn_QX" runat="server" Text="取消" OnClick="btn_QX_click" />
                                    </td>
                                    <td width="25%">
                                        <asp:RadioButton ID="Radio_quanbu" runat="server" Text="全部" GroupName="1" OnCheckedChanged="Radio_quanbu_CheckedChanged"
                                            AutoPostBack="True" Checked="true" />
                                        <asp:RadioButton ID="Radio_weish" runat="server" Text="未审核" GroupName="1" OnCheckedChanged="Radio_weish_CheckedChanged"
                                            AutoPostBack="True" />
                                        <asp:RadioButton ID="Radio_buty" runat="server" Text="驳回" GroupName="1" OnCheckedChanged="Radio_buty_CheckedChanged"
                                            AutoPostBack="True" />
                                        <asp:RadioButton ID="Radio_tg" runat="server" Text="通过" GroupName="1" OnCheckedChanged="Radio_tg_CheckedChanged"
                                            AutoPostBack="True" />
                                    </td>
                                    <td align="center" width="20%">
                                        <asp:Panel ID="Panel_view" runat="server">
                                            <asp:RadioButton ID="radio_ty" runat="server" Text="同意" GroupName="view" OnCheckedChanged="radio_ty_CheckedChanged"
                                                AutoPostBack="True" />
                                            <asp:RadioButton ID="radio_bh" runat="server" Text="驳回" GroupName="view" OnCheckedChanged="radio_bh_CheckedChanged"
                                                AutoPostBack="True" />&nbsp;&nbsp;
                                            <asp:Button ID="confirm" runat="server" Text="确定" OnClick="confirm_Click" />&nbsp;
                                        </asp:Panel>
                                    </td>
                                    <td align="right" runat="server" width="10%">
                                        <asp:Panel ID="pan_edit" runat="server">
                                            <asp:Button ID="btn_tijiao" runat="server" Text="提交" OnClick="btn_tijiao_Click" />&nbsp;&nbsp;
                                            <asp:Button ID="btn_xiugai" runat="server" Text="修改" OnClick="btn_xiugai_Click" Visible="false" />&nbsp;&nbsp;
                                        </asp:Panel>
                                    </td>
                                    <td width="20%" align="right">
                                        <asp:Button ID="Btn_MTO" runat="server" Text="MTO" OnClick="Btn_MTO_Click" Visible="false"/>&nbsp;&nbsp;
                                        <asp:Button ID="btn_fanshen" runat="server" Text="反审" OnClick="btn_fanshen_click" Visible="false" />&nbsp;&nbsp;
                                        <asp:Button ID="btn_delete" runat="server" Text="删除" OnClick="btn_delete_click" />&nbsp;&nbsp;
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
                                        备库占用信息<asp:Label runat="server" ID="LabelMessage" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;" align="left">
                                        日&nbsp;&nbsp;&nbsp;期：&nbsp;&nbsp;<asp:TextBox ID="Tb_shijian" runat="server" Enabled="false"
                                            Width="150px"></asp:TextBox>
                                    </td>
                                    <td style="width: 25%;" align="left">
                                        编&nbsp;&nbsp;&nbsp;号：&nbsp;&nbsp;<asp:TextBox ID="TextBoxNO" runat="server" Enabled="false"
                                            Width="200px"></asp:TextBox>
                                        <asp:Label ID="Label_view" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="Label_action" runat="server" Text="" Visible="false"></asp:Label>
                                    </td>
                                    <td style="width: 25%;" align="left">
                                        项&nbsp;&nbsp;&nbsp;目：
                                        <asp:TextBox ID="tb_pj" runat="server" Enabled="false" Text=""></asp:TextBox>
                                        <asp:TextBox ID="tb_pjid" runat="server" Visible="false" Text=""></asp:TextBox>
                                    </td>
                                    <td style="width: 25%;" align="left">
                                        设&nbsp;&nbsp;&nbsp;备：
                                        <asp:TextBox ID="tb_eng" runat="server" Enabled="false" Text=""></asp:TextBox>
                                        <asp:TextBox ID="tb_engid" runat="server" Visible="false" Text=""></asp:TextBox>
                                        <asp:Button runat="server" ID="btnwin" Text="审核" />
                                        <cc1:ModalPopupExtender ID="ModalPopupExtenderSearch" TargetControlID="btnwin" runat="server" PopupControlID="PanelCondition" Drag="True" Enabled="True" DynamicServicePath="" Y="-100" X="350">
                                        </cc1:ModalPopupExtender>
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        <asp:Panel ID="PanelCondition" runat="server" Width="500px" Height="300px" Style="display: none">
                            <table width="90%" style="background-color: #CCCCFF; border: solid 1px black;">
                                <tr>
                                    <td align="center" height="60px">
                                        <p class="red" style="font-size:Larger">是否全部全部审批通过！</p>
                                    </td> 
                                </tr>
                                <tr>
                                    <td align="center" height="60px">
                                        
                                        <asp:Button ID="QueryButton" Font-Size="Larger" runat="server" OnClick="QueryButton_Click" Text="确认" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnClose" Font-Size="Larger" runat="server" OnClick="btnClose_Click" Text="取消" />
                                    </td>
                                </tr>
                                
                            </table>
                        </asp:Panel>
                        </div>
                        <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                            ActiveTabIndex="0">
                            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="占用信息" TabIndex="0">
                                <ContentTemplate>
                                    <div style="height: 450px; overflow: auto; width: 100%">
                                        <div class="cpbox1 xscroll">
                                            <table id="tab" class="nowrap cptable fullwidth" align="center" width="100%">
                                                <asp:Repeater ID="tbpc_purshaseplancheck_datialRepeater" runat="server" OnItemDataBound="tbpc_purshaseplancheck_datialRepeater_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                                            <td>
                                                                <strong>行号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>计划号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料编码</strong>
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
                                                                <strong>计划数量</strong>
                                                            </td>
                                                            <td>
                                                                <strong>使用库存数量</strong>
                                                            </td>
                                                            <td>
                                                                <strong>采购备注</strong>
                                                            </td>
                                                            <td>
                                                                <strong>审核意见</strong>
                                                            </td>
                                                            <td>
                                                                <strong>储运备注</strong>
                                                            </td>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                            <td>
                                                                <asp:Label ID="ROWSNUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                                <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                                    Checked="false"></asp:CheckBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PUR_PTCODE" runat="server" Text='<%#Eval("PUR_PTCODE")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PUR_MARID" runat="server" Text='<%#Eval("PUR_MARID")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PUR_MARNAME" runat="server" Text='<%#Eval("PUR_MARNAME")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PUR_MARNORM" runat="server" Text='<%#Eval("PUR_MARNORM")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PUR_MARTERIAL" runat="server" Text='<%#Eval("PUR_MARTERIAL")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PUR_GUOBIAO" runat="server" Text='<%#Eval("PUR_GUOBIAO")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PUR_NUNIT" runat="server" Text='<%#Eval("PUR_NUNIT")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PUR_NUM" runat="server" Text='<%#Eval("PUR_NUM")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PUR_USTNUM" runat="server" Text='<%#Eval("PUR_USTNUM")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PUR_NOTE" runat="server" Text='<%#Eval("PUR_NOTE")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PUR_SHTATE" runat="server" Text='<%#get_shstate(Eval("allshstate").ToString())%>'></asp:Label>
                                                                <asp:Label ID="allshstate" runat="server" Visible="false" Text='<%#Eval("allshstate")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PUR_SHYJ" runat="server" Text='<%#Eval("PUR_SHYJ")%>'></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="LabelPlanNum"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="LabelZhanNum"></asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                                <tr>
                                                    <td colspan="19" align="center">
                                                        <asp:Panel ID="NoDataPane1" runat="server" Visible="false">
                                                            没有数据！</asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                <td>
                                                    审批人:<asp:TextBox ID="Tb_shenpiren" runat="server"></asp:TextBox>
                                                    <asp:TextBox ID="Tb_shenpirenid" runat="server" Visible="false"></asp:TextBox>
                                                    <%--<cc1:ComboBox ID="cob_shenheren" runat="server" Height="15px" Width="120px" AutoCompleteMode="SuggestAppend"
                                                        DropDownStyle="DropDownList">
                                                    </cc1:ComboBox>--%>
                                                </td>
                                                <td>
                                                    主管:<asp:TextBox ID="Tb_zhuguan" runat="server"></asp:TextBox>
                                                    <asp:TextBox ID="Tb_zhuguanid" runat="server" Visible="false"></asp:TextBox>
                                                    <%--<cc1:ComboBox ID="com_zhuguan" runat="server" Height="15px" Width="120px" AutoCompleteMode="SuggestAppend"
                                                        DropDownStyle="DropDownList">
                                                    </cc1:ComboBox>--%>
                                                </td>
                                                <td>
                                                    制单:<asp:TextBox ID="TextBoxexecutor" runat="server"></asp:TextBox>
                                                    <asp:TextBox ID="TextBoxexecutorid" runat="server" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="审核结果" TabIndex="1">
                                <ContentTemplate>
                                    <div style="height: 430px; overflow: auto; width: 100%">
                                        <div class="cpbox xscroll">
                                            <asp:Panel ID="Panel_shenhe" runat="server" Enabled="false">
                                                <table width="100%" style="border: 1px" cellpadding="4" cellspacing="0">
                                                    <tr align="center">
                                                        <td>
                                                            审&nbsp;核&nbsp;意&nbsp;见:
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="suggestion" Columns="100" Rows="3" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="right" style="border-bottom: solid 1px black; border">
                                                            审&nbsp;核&nbsp;人:<asp:TextBox ID="TextBox_shenheren" runat="server" Text="" CssClass="text1style"></asp:TextBox>&nbsp;&nbsp;
                                                            审&nbsp;核&nbsp;时&nbsp;间:<asp:TextBox ID="TextBox_shenhesj" runat="server" Text=""
                                                                CssClass="text1style"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </cc1:TabPanel>
                        </cc1:TabContainer>
                    </div>
                </div>
<%--            </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
