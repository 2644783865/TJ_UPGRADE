﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/PopupBase.Master" EnableViewState="true"
    AutoEventWireup="true" CodeBehind="CM_Contract_addlot.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_Contract_addlot" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="../Controls/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblConForm" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/Setting.css" />
    <link href="StyleFile/Style.css" rel="stylesheet" type="text/css" />

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="../PC_Data/PcJs/pricesearch.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        //选择厂商
        function SupplierSelect() {
            var CON_TYPE=<%=ContactForm %>;        
            var i = window.showModalDialog('SupplierSelect.aspx?type=' + CON_TYPE, '', "dialogHeight:500px;dialogWidth:750px;status:no;scroll:no;center:yes;toolbar=no;menubar=no");
            if (i != null) {
                document.getElementById('<%=txtPCON_CUSTMNAME.ClientID%>').value = i[1];
            }
        }
        //添加结算单
        function btnADDJSD_onclick(i) {
            document.getElementById('<%=btnADDJSD.ClientID %>').style.display = "none"; //隐藏，防止再次添加
            var autonum = Math.round(10000 * Math.random());
            window.open('CM_JSD_DETAIL.aspx?condetail_id=<%=CondetailID%>&autonum=' + autonum + '&Action=add');
        }
        //查看、修改结算单
        function JSDView(i) {
            var action = i.title;
            var autonum = Math.round(10000 * Math.random());
            window.open('CM_JSD_DETAIL.aspx?condetail_id=<%=CondetailID%>&autonum=' + autonum + '&Action=' + action);

        }

        //添加请款
        function btnADDCR_onclick() {
            if (document.getElementById("<%=lbl_pszt.ClientID %>").innerHTML == "审批通过") {
                document.getElementById('<%=btnADDCR.ClientID %>').style.display = "none"; //隐藏，防止再次添加
                var autonum = Math.round(10000 * Math.random());
                window.open('CM_CHECKREQUEST.aspx?Action=Add&autonum=' + autonum + '&condetail_id=<%=CondetailID%>&contactform=<%=ContactForm %>');
            }
            else {
                alert("合同还未审批通过，无法添加请款！");
            }
        }
        //修改请款
        function CREdit(i) {
            var ID = i.title;
            var autonum = Math.round(10000 * Math.random());
            window.open("CM_CHECKREQUEST.aspx?Action=Edit&autonum=" + autonum + "&CRid=" + ID);
        }


        //查看索赔信息
        function View_SP(i) {
            var url = i.title;
            window.open(url);
        }

        //查看请款
        function CRView(i) {
            var ID = i.title;
            var now = new Date();
            var seconds = now.getTime();
            window.showModalDialog("CM_CHECKREQUEST.aspx?Action=View&CRid=" + ID + "&NoUse=" + seconds, obj, "dialogHeight:800px;dialogWidth:1100px");
        }


        //查看付款记录
        function FKView(i) {
            var ID = i.title;
            var now = new Date();
            var seconds = now.getTime();
            window.showModalDialog("CM_PAYMENT.aspx?Action=View&PRid=" + ID + "&NoUse=" + seconds, obj, "dialogWidth=800px;dialogHeight=400px");
        }

        //待办请款
        function DBQK(i) {
            var ID = i.title;
            var autonum = Math.round(10000 * Math.random());
            window.showModalDialog("CM_Payment.aspx?Action=AddFK&autonum=" + autonum + "&CRid=" + ID, obj, "dialogWidth=800px;dialogHeight=520px;status:no;");
            window.history.go(0);
        }

        //查看发票
        function FPView(i) {
            var ID = i.title;
            var now = new Date();
            var seconds = now.getTime();
            window.showModalDialog("CM_Bill.aspx?Action=View&BillID=" + ID + "&NoUse=" + seconds, obj, "dialogWidth=700px;dialogHeight=400px;");
        }
        //修改发票
        function FPEdit(i) {
            var ID = i.title;
            var now = new Date();
            var seconds = now.getTime();
            var sRet = window.showModalDialog("CM_Bill.aspx?Action=Edit&BillID=" + ID + "&NoUse=" + seconds, obj, "dialogWidth=700px;dialogHeight=400px;");
            if (sRet == "refresh") {
                //window.history.go(0);  
            }
        }

        //添加发票
        function btnAddFP_onclick() {
            var now = new Date();
            var seconds = now.getTime();
            var sRet = window.showModalDialog('CM_Bill.aspx?Action=Add&condetail_id=<%=CondetailID%>' + "&NoUse=" + seconds, obj, "dialogWidth=700px;dialogHeight=400px;");
            if (sRet == "refresh") {
                window.location.reload();
            }
        }

        //修改付款记录凭证
        function Edit_PZ(i) {
            var ID = i.title;
            var autonum = Math.round(10000 * Math.random());
            window.showModalDialog("CM_Payment.aspx?Action=Edit&autonum=" + autonum + "&PRid=" + ID, obj, "dialogWidth=800px;dialogHeight=520px;status:no;");
            window.href = "CM_CheckRequestRecord.aspx";
        }

        //检验日期格式如：2012-01-01
        function dateCheck(obj) {
            var value = obj.value;
            if (value != "") {
                var re = new RegExp("^([0-9]{4})(-)([0-9]{2})(-)([0-9]{2})$");
                m = re.exec(value)
                if (m == null) {
                    obj.style.background = "yellow";
                    obj.value = "";
                    alert('请输入正确的时间格式如：2012-01-01');
                }
            }
        }

        //查看合同评审
        function btnRevInfo_onclick(id) {
            var autonum = Math.round(10000 * Math.random());
            window.open('CM_ContractView_Audit.aspx?autonum=' + autonum + '&Action=view&Type=1&ID=' + id);
        }

        //查看补充协议
        function BCXYView(i) {
            var crid = i.title;
            var autonum = Math.round(10000 * Math.random());
            window.open('CM_ContractView_Audit.aspx?Action=view&autonum=' + autonum + '&ID=' + crid + '&Type=6');

        }

        //添加补充协议
        function add_bcxy_onclick(i) {
            document.getElementById('<%=add_bcxy.ClientID %>').style.display = "none"; //隐藏，防止再次添加
            var autonum = Math.round(10000 * Math.random());
            window.open('CM_ContractView_Other_Add.aspx?Action=add&Type=8&autonum=' + autonum + '&Conid=<%=CondetailID%>');
        }

        //锁定合同号倒计时
        function AutoLock() {
            lock_tip.style.display = "block";
            var t1 = 3600;
            countDown(t1);
        }
        function countDown(secs) {
            var hour = parseInt(secs / 3600);
            var min = parseInt((secs - hour * 3600) / 60);
            var sec = secs - hour * 3600 - min * 60
            ctl00_PrimaryContent_tip_content.innerText = "锁定倒计时：" + hour + "时" + min + "分" + sec + "秒";
            if (--secs >= 0) {
                setTimeout("countDown(" + secs + ")", 1000);
            }
            else {
                ctl00_PrimaryContent_tip_content.style.color = "Red";
                ctl00_PrimaryContent_TabContainer1_TabPanel1_lb_lock.innerText = "取消锁定";
                ctl00_PrimaryContent_TabContainer1_TabPanel1_lb_lock.style.color = "Red";

            }
        }
    </script>

    <style type="text/css">
        .autocomplete_completionListElement
        {
            margin: 0px;
            background-color: #1C86EE;
            color: windowtext;
            cursor: 'default';
            text-align: left;
            list-style: none;
            padding: 0px;
            border: solid 1px gray;
            width: 400px !important;
        }
        .autocomplete_listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            background-color: #EEDC82;
            color: windowtext;
        }
        .autocomplete_highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            padding: 1px;
        }
    </style>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td style="width: 30%">
                                <asp:Label ID="lblAddOREdit" runat="server"></asp:Label>
                                (带<span class="red">*</span>号的为必填项)
                                <%--唯一编号--%>
                                <asp:Label ID="lbl_UNID" runat="server" Text="" Visible="false"></asp:Label>
                            </td>
                            <td align="center">
                                <div id="lock_tip" style="display: none; color: Green">
                                    <span id="tip_content" runat="server"></span>
                                </div>
                            </td>
                            <td align="right">
                                <asp:LinkButton ID="LinkLock" runat="server" OnClientClick="javascript:return confirm('点击确定为该用户锁定60分钟,此时间段内不会被其他人占用\r60分钟后若未提交则该合同号仍然可能被占用\r\r要立即锁定吗？');"
                                    OnClick="LinkLock_Click" Visible="false" CausesValidation="false">
                                    <asp:Image ID="Image1" Style="cursor: hand" ToolTip="锁定合同号" ImageUrl="~/Assets/images/lock.jpg"
                                        Height="18" Width="18" runat="server" />
                                    锁定合同号
                                </asp:LinkButton>&nbsp;&nbsp; &nbsp;
                                <asp:LinkButton ID="LbtnNO" runat="server" OnClientClick="javascript:return confirm('取消后释放锁定，该合同号可继续使用\r\r确认取消锁定并关闭该页面吗？');"
                                    Visible="false" CausesValidation="false" OnClick="btnNO_Click">
                                    <asp:Image ID="Image10" Style="cursor: hand" ToolTip="放弃添加" ImageUrl="~/Assets/icons/delete.gif"
                                        Height="18" Width="18" runat="server" />
                                    放弃添加
                                </asp:LinkButton>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnConfirm" runat="server" Text="" UseSubmitBehavior="false" OnClientClick="Javascript:if(this.value=='确认修改'){if(confirm(&quot;确认要修改？&quot;)){}else return false;}else {this.value=='确认添加';}"
                                    OnClick="btnConfirm_Click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="关 闭" CausesValidation="false" OnClientClick="javascript:window.close();" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <asp:Label ID="lb_addtips" runat="server" ForeColor="Red" Visible="false" Text="提示：添加时想要页面中生成的合同号不被其他人占用，点击【锁定合同号】即可.锁定前请先生成正确的合同号"></asp:Label>
                <table width="100%">
                    <tr>
                        <td style="width: 10px">
                            <asp:Image ID="Image2" Style="cursor: hand" ToolTip="隐藏" ImageUrl="~/Assets/images/bar_down.gif"
                                onClick="switchGridVidew(this,'htjbxx')" Height="15" Width="15" runat="server" />
                        </td>
                        <td>
                            合同基本信息
                        </td>
                    </tr>
                </table>
                <!--合同基本信息开始-->
                <div id="htjbxx" style="display: block">
                    <asp:Panel ID="palBasicInfo" runat="server">
                        <table class="tabGg" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="r_bg">
                                    合同日期:
                                </td>
                                <td class="right_bg" valign="middle">
                                    <asp:Label ID="lb_Time" runat="server"></asp:Label>
                                </td>
                                <td class="r_bg">
                                    责任部门:
                                </td>
                                <td class="right_bg">
                                    <asp:DropDownList ID="dplPCON_RSPDEPID" runat="server" Enabled="false">
                                    </asp:DropDownList>
                                    <font color="#ff0000">*</font> (按部门生成合同号)
                                </td>
                            </tr>
                            <tr>
                                <td class="r_bg">
                                    项目名称:
                                </td>
                                <td class="right_bg" valign="middle">
                                    <asp:TextBox ID="tb_pjinfo" runat="server" Text=""></asp:TextBox>
                                    <font color="#ff0000">*</font>
                                </td>
                                <td class="r_bg">
                                    签订日期:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_FILLDATE" runat="server" Text="" onchange="dateCheck(this)" />
                                    <asp:CalendarExtender ID="calender_filldate" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                        TodaysDateFormat="yyyy年MM月dd日" TargetControlID="txtPCON_FILLDATE">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="r_bg">
                                    合同号:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_BCODE" Text="" runat="server" Enabled="false" Width="280px"></asp:TextBox>
                                    <font color="#ff0000">*</font>
                                    <asp:Label ID="lb_lock" runat="server" Text="未锁定" ForeColor="Red" Visible="false"></asp:Label>
                                </td>
                                <td class="r_bg">
                                    交货日期:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_DELIVERYDATE" Text="" runat="server" onchange="dateCheck(this)" />
                                    <asp:CalendarExtender ID="calender_deliverydate" runat="server" Format="yyyy-MM-dd"
                                        DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月dd日" TargetControlID="txtPCON_DELIVERYDATE">
                                    </asp:CalendarExtender>
                                    <font color="#ff0000">*</font>
                                </td>
                            </tr>
                            <tr>
                                <td class="r_bg">
                                    合同类别:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_TYPE" runat="server" Width="280px"></asp:TextBox>
                                    <font color="#ff0000">*</font>
                                </td>
                                <td class="r_bg">
                                    合同金额:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_JINE" runat="server" onblur="javascript:check_num(this)"
                                        Text="0"></asp:TextBox>(￥) <font color="#ff0000">*</font> &nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="r_bg">
                                    <asp:Label ID="Lbl_sup" runat="server" Text="厂家名称:"></asp:Label>
                                </td>
                                <td class="right_bg">
                                    <input type="text" id="txtPCON_CUSTMNAME" runat="server" style="width: 280px" />
                                    <font color="#ff0000">*</font>
                                </td>
                                <td class="r_bg">
                                    结算金额:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_BALANCEACNT" runat="server" onblur="javascript:check_num(this)"
                                        Text="0"></asp:TextBox>
                                    (￥)
                                </td>
                            </tr>
                            <tr>
                                <td class="r_bg">
                                    合同评审状态:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_ADUITDATE" runat="server" onclick="setday(this);" Visible="false"></asp:TextBox>
                                    <asp:Label ID="lbl_pszt" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="tb_revid" runat="server" Visible="false"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btn_RevInfo" runat="server" Text="查看评审信息" OnClick="btn_RevInfo_Click" />
                                </td>
                                <td class="r_bg">
                                    负责人:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_RESPONSER" Text="" runat="server"></asp:TextBox>
                                    <font color="#ff0000">*</font>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="r_bg">
                                </td>
                                <td colspan="3" class="right_bg">
                                    <asp:RadioButtonList ID="rblState" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0">未开始</asp:ListItem>
                                        <asp:ListItem Value="1">进行中</asp:ListItem>
                                        <asp:ListItem Value="2">已完成</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="r_bg" valign="middle">
                                    备注:
                                </td>
                                <td class="right_bg" colspan="3">
                                    <asp:TextBox ID="txtPCON_NOTE" runat="server" TextMode="MultiLine" Rows="5" Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table width="100%">
                        <tr>
                            <td>
                                <uc1:UploadAttachments ID="UploadAttachments1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
                <!--合同基本信息结束-->
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0">
                            <asp:TabPanel ID="Tab1" runat="server" HeaderText="请款单" Height="90%" Width="100%">
                                <HeaderTemplate>
                                    请款单
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 10px">
                                                <asp:Image ID="Image4" runat="server" Height="15px" ImageUrl="~/Assets/images/bar_down.gif"
                                                    onClick="switchGridVidew(this,'qk')" Style="cursor: hand" ToolTip="隐藏" Width="15px" />
                                            </td>
                                            <td align="left">
                                                请款记录
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="Label1" runat="server" Text="已付金额:"></asp:Label>
                                                <asp:Label ID="lblYFJE" runat="server"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="Label2" runat="server" Text="合同总价:"></asp:Label>
                                                <asp:Label ID="lblHTJE" runat="server"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="Label3" runat="server" Text="支付比例:"></asp:Label>
                                                <asp:Label ID="lblZFBL" runat="server"></asp:Label>
                                            </td>
                                            <td align="right">
                                                <input id="btnADDCR" type="button" runat="server" class="button-outer" value="添加请款单"
                                                    onclick="return btnADDCR_onclick()" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="qk" style="display: block">
                                        <asp:Panel ID="palQK" runat="server">
                                            <asp:GridView ID="grvQK" runat="server" AutoGenerateColumns="False" OnRowCommand="grvQK_RowCommand"
                                                CellPadding="4" CssClass="toptable grid" ForeColor="#333333" Width="100%" OnRowDataBound="grvQK_RowDataBound"
                                                ShowFooter="True">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("CR_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="序号">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CR_ID" HeaderText="请款单号">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CR_BQSFK" HeaderText="请款金额">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CR_USE" HeaderText="请款用途">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CR_DATE" DataFormatString="{0:d}" HeaderText="请款时间">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CR_JBR" HeaderText="请款人">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="请款状态">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label4" runat="server" Text='<%#Eval("CR_STATE").ToString()=="0"?"保存":Eval("CR_STATE").ToString()=="1"?"正在签字":Eval("CR_STATE").ToString()=="2"?"提交财务-未付款":Eval("CR_STATE").ToString()=="3"?"提交财务-部分付款":"提交财务-已付款" %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="编辑" ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hlContrac" runat="server" CssClass="hand" Visible='<%#Eval("CR_STATE").ToString()=="0"?true:Eval("CR_STATE").ToString()=="1"?true:false %>'
                                                                onClick="CREdit(this);" ToolTip='<%# Eval("CR_ID")%>'>
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Assets/images/res.gif" ToolTip='<%# Eval("CR_ID")%>' />编辑
                                                            </asp:HyperLink>&nbsp;&nbsp;
                                                            <asp:LinkButton ID="btnDelete" runat="server" ToolTip="删除" CausesValidation="False"
                                                                Visible='<%#Eval("CR_STATE").ToString()=="0"?true:Eval("CR_STATE").ToString()=="1"?true:false %>'
                                                                EnableViewState="true" OnClientClick="javascript:return confirm('确认删除该条记录吗？');"
                                                                CommandArgument='<%# Eval("CR_ID")%>' CommandName="Del" Text="删除"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="查看">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hlContract" runat="server" CssClass="hand" onClick="CRView(this);"
                                                                ToolTip='<%# Eval("CR_ID")%>'>
                                                                <asp:Image ID="Imagee1" runat="server" ImageUrl="~/Assets/images/res.gif" ToolTip='<%# Eval("CR_ID")%>' />查看
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <asp:Panel ID="NoDataPanelQK" runat="server" HorizontalAlign="Center">
                                            <hr style="width: 100%; height: 0.1px; color: Blue;" />
                                            没有记录!</asp:Panel>
                                    </div>
                                    </input>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="Tab2" runat="server" HeaderText="付款情况" Height="90%" Width="100%">
                                <ContentTemplate>
                                    <asp:Panel ID="palDBQK" runat="server">
                                        <table>
                                            <tr>
                                                <td style="width: 10px">
                                                    <asp:Image ID="Image5" Style="cursor: hand" ToolTip="隐藏" ImageUrl="~/Assets/images/bar_down.gif"
                                                        onClick="switchGridVidew(this,'ddqk')" Height="15" Width="15" runat="server" />
                                                </td>
                                                <td align="left">
                                                    待办请款
                                                </td>
                                            </tr>
                                        </table>
                                        <div id="ddqk" style="display: block">
                                            <asp:GridView ID="grvDBQK" Width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                                                CellPadding="4" ForeColor="#333333" ShowFooter="true">
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:TemplateField Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("CR_ID") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="序号">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CR_ID" HeaderText="请款单号" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="DEP_NAME" HeaderText="请款部门">
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CR_DATE" HeaderText="请款日期" DataFormatString="{0:d}" HtmlEncode="False">
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CR_USE" HeaderText="请款用途">
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CR_BQSFK" HeaderText="请款金额">
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="付款" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hlContrac" runat="server" CssClass="hand" onClick="DBQK(this);"
                                                                ToolTip='<%# Eval("CR_ID")%>'>
                                                                <asp:Image ID="Image22" runat="server" ImageUrl="~/Assets/images/res.gif" ToolTip='<%# Eval("CR_ID")%>' />编辑
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
                                            </asp:GridView>
                                            <asp:Panel ID="NoDataPanelDBQK" runat="server" HorizontalAlign="Center">
                                                <hr style="width: 100%; height: 0.1px; color: Blue;" />
                                                没有记录!</asp:Panel>
                                        </div>
                                        <table>
                                            <tr>
                                                <td style="width: 10px">
                                                    <asp:Image ID="Image6" Style="cursor: hand" ToolTip="隐藏" ImageUrl="~/Assets/images/bar_down.gif"
                                                        onClick="switchGridVidew(this,'fkjl')" Height="15" Width="15" runat="server" />
                                                </td>
                                                <td>
                                                    付款记录
                                                </td>
                                            </tr>
                                        </table>
                                        <div id="fkjl" style="display: block">
                                            <asp:GridView ID="grvFKJL" Width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                                                CellPadding="4" ForeColor="#333333" ShowFooter="true" OnRowDataBound="grvFK_RowDataBound">
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="序号">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'> </asp:Label>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("PR_ID") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_bczfje" runat="server" Text='<%# Eval("PR_BCZFJE") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="QKQC" HeaderText="请款期次" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="PR_BCZFJE" HeaderText="支付金额">
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PR_ZCRQ" HeaderText="支付日期" DataFormatString="{0:d}">
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="有无凭证" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="Cbx_FK" runat="server" Enabled="false" Checked='<%# Eval("PR_PZ").ToString()=="0"?false:true %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PR_PZH" HeaderText="凭证号">
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="编辑" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hlFK" CssClass="hand" ToolTip='<%# Eval("PR_ID")%>' onClick='Edit_PZ(this);'
                                                                runat="server">
                                                                <asp:Image ID="img_editfk" ImageUrl="~/Assets/images/modify.gif" runat="server" /></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
                                            </asp:GridView>
                                            <asp:Panel ID="NoDataPanelFKJL" runat="server" HorizontalAlign="Center">
                                                <hr style="width: 100%; height: 0.1px; color: Blue;" />
                                                没有记录!</asp:Panel>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="Tab3" runat="server" HeaderText="发票单据" Height="90%" Width="100%">
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 10px">
                                                <asp:Image ID="Image7" Style="cursor: hand" ToolTip="隐藏" ImageUrl="~/Assets/images/bar_down.gif"
                                                    onClick="switchGridVidew(this,'fpjl')" Height="15" Width="15" runat="server" />
                                            </td>
                                            <td>
                                                发票记录
                                            </td>
                                            <td align="right">
                                                <input id="btnAddFP" type="button" value="添加发票记录" class="button-outer" runat="server"
                                                    onclick="return btnAddFP_onclick()" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="fpjl" style="display: block">
                                        <asp:GridView ID="grvFP" Width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                                            CellPadding="4" ForeColor="#333333" ShowFooter="true" OnRowDataBound="grvFP_RowDataBound">
                                            <RowStyle BackColor="#EFF3FB" />
                                            <Columns>
                                                <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("GIV_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="GIV_KPDW" HeaderText="开票单位">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GIV_KPRQ" HeaderText="开票日期" DataFormatString="{0:d}" HtmlEncode="False">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GIV_KPJE" HeaderText="开票金额">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GIV_FPDH" HeaderText="发票单号">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GIV_SPRQ" HeaderText="收票日期" DataFormatString="{0:d}" HtmlEncode="False">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="有无凭证" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox4" runat="server" Enabled="false" Checked='<%# Eval("GIV_PZ").ToString()=="0"?false:true %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="GIV_PZH" HeaderText="凭证号">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GIV_JBR" HeaderText="经办人">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="编辑">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hl1FP" CssClass="hand" ToolTip='<%# Eval("GIV_ID")%>' onClick='FPEdit(this);'
                                                            runat="server">
                                                            <asp:Image ID="Image113" ImageUrl="~/Assets/images/res.gif" runat="server" />
                                                            编辑</asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="查看">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hl11FP" CssClass="hand" ToolTip='<%# Eval("GIV_ID")%>' onClick='FPView(this);'
                                                            runat="server">
                                                            <asp:Image ID="Image1212" ImageUrl="~/Assets/images/res.gif" runat="server" />
                                                            查看</asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="删除">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="linkdel_FP" runat="server" ForeColor="Red" CommandArgument='<%# Eval("GIV_ID")%>'
                                                            OnClick="linkdel_FP_Click" OnClientClick="return confirm('确定要删除此记录吗？？？');">
                    删除</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
                                        </asp:GridView>
                                        <asp:Panel ID="NoDataPanelFPJL" runat="server" HorizontalAlign="Center">
                                            <hr style="width: 100%; height: 0.1px; color: Blue;" />
                                            没有记录!</asp:Panel>
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="Tab5" runat="server" HeaderText="结算单" Height="90%" Width="100%">
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 10px">
                                                <asp:Image ID="Image9" Style="cursor: hand" ToolTip="隐藏" ImageUrl="~/Assets/images/bar_down.gif"
                                                    onClick="switchGridVidew(this,'jsd')" Height="15px" Width="15px" runat="server" />
                                            </td>
                                            <td align="left">
                                                结算单记录
                                            </td>
                                            <td align="right">
                                                <input id="btnADDJSD" type="button" runat="server" class="button-outer" value="添加结算单"
                                                    onclick="return btnADDJSD_onclick()" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="jsd" style="display: block">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:GridView ID="grvjsd" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                CssClass="toptable grid" ForeColor="#333333" Width="100%">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("CONID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="序号">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CONID" HeaderText="合同号">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JSDDATE" HeaderText="添加结算单日期">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="查看">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hlviewjsd" runat="server" CssClass="hand" onClick="JSDView(this);"
                                                                ToolTip="view">
                                                                <asp:Image ID="ImgViewjsd" runat="server" ImageUrl="~/Assets/images/search.gif" ToolTip='<%# Eval("CONID")%>' />查看
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="编辑">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hleditjsd" runat="server" CssClass="hand" onClick="JSDView(this);"
                                                                ToolTip="edit">
                                                                <asp:Image ID="ImgEditjsd" runat="server" ImageUrl="~/Assets/images/modify.gif" ToolTip='<%# Eval("CONID")%>' />编辑
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="Lbtn_Del" runat="server" ForeColor="Red" CommandArgument='<%#Eval("CONID") %>'
                                                                OnClick="Lbtn_Del_OnClick" OnClientClick="javascript:return confirm('确定要删除吗？');">
                                                                <asp:Image ID="ImageDel" ImageUrl="~/Assets/images/erase.gif" runat="server" />
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="False" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center">
                                            <hr style="width: 100%; height: 0.1px; color: Blue;" />
                                            没有记录!</asp:Panel>
                                    </div>
                                    </input>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="Tab6" runat="server" HeaderText="补充协议" Height="90%" Width="100%">
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 10px">
                                                <asp:Image ID="Image3" Style="cursor: hand" ToolTip="隐藏" ImageUrl="~/Assets/images/bar_down.gif"
                                                    onClick="switchGridVidew(this,'bcxy')" Height="15" Width="15" runat="server" />
                                            </td>
                                            <td align="left">
                                                补充协议记录
                                            </td>
                                            <td align="right">
                                                <input id="add_bcxy" type="button" runat="server" class="button-outer" value="添加补充协议"
                                                    onclick="return add_bcxy_onclick()" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="bcxy" style="display: block">
                                        <asp:GridView ID="GV_AddCon" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                            CssClass="toptable grid" ForeColor="#333333" Width="100%" EmptyDataText="没有记录">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CON_ID" HeaderText="补充协议编号">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CON_XMMC" HeaderText="项目名称">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CON_SBMC" HeaderText="设备名称">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CON_FBSMC" HeaderText="厂商">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CON_FBFW" HeaderText="分包范围">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CON_JIN" HeaderText="金额" DataFormatString="{0:C}">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CON_ZDRQ" HeaderText="编制日期">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="查看">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hlviewbcxy" runat="server" CssClass="hand" onClick="BCXYView(this);"
                                                            ToolTip='<%# Eval("CR_ID")%>'>
                                                            <asp:Image ID="ImgViewbcxy" runat="server" ImageUrl="~/Assets/images/search.gif"
                                                                ToolTip='<%# Eval("CON_ID")%>' />查看
                                                        </asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                        </asp:TabContainer>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
