<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true"
    CodeBehind="CM_ContractView_Add.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_ContractView_Add" %>

<%@ Register Src="../Controls/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <asp:Label ID="lblState" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <%--<script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>--%>
    <link href="StyleFile/Style.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function CheckBoxList_Click(sender) {
            var container = sender.parentNode;
            if (container.tagName.toUpperCase() == "TD")
            // 服务器控件设置呈现为 table 布局（默认设置），否则使用流布局即为td
            {
                container = container.parentNode.parentNode; // 层次： <table><tr><td><input />
            }
            var chkList = container.getElementsByTagName("input");
            var senderState = sender.checked;
            for (var i = 0; i < chkList.length; i++) {
                chkList[i].checked = false;
            }
            sender.checked = senderState;
        }
        //选择厂商
        function SupplierSelect() {
            var i = window.showModalDialog('SupplierSelect.aspx', '', "dialogHeight:500px;dialogWidth:750px;status:no;scroll:no;center:yes;toolbar=no;menubar=no");
            if (i != null) {
                document.getElementById('<%=txtPCON_CUSTMNAME.ClientID%>').value = i[1];
                document.getElementById('<%=txtPCON_CUSTMID.ClientID%>').value = i[0];

            }
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

        //检验是否填写了合同金额
        function Check_HTJE() {
            var str = "";
            var je_value = parseFloat(document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel1_txtPCON_JINE").value);
            if (je_value == 0) {
                str = "合同金额为0，请确定后再提交\r\r确定要提交吗？";
            }
            else {
                str = "提交后开始审批\r\r确认提交吗？";
            }
            var ok = confirm(str);
            return ok;
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

        //检验合同号是否重复
        function Test() {
            var xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            var bh = document.getElementById("<%=txtPCON_BCODE.ClientID%>").value;
            var val = document.getElementById("<%=Hidden.ClientID%>").value;
            if (!xmlhttp) {
                alert("创建异常");
                return false;
            }
            xmlhttp.open("GET", "Test.ashx?txtPCON_BCODE=" + bh + "&ts=" + new Date(), false);
            xmlhttp.onreadystatechange = function() {
                if (xmlhttp.readystate == 4) {//发送请求成功
                    if (xmlhttp.status == 200) { //如果代码是200则成功
                        var json = xmlhttp.responseText; //responseText属性为服务器返回文本
                        if (json == "1") {
                            alert('已存在此合同号！');
                            document.getElementById("<%=txtPCON_BCODE.ClientID%>").value = val;
                            event.returnValue = false;
                        }
                    }
                }
            }
            xmlhttp.send(); //发送请求
        }
    </script>

    <script type="text/javascript">
        function CheckNum(obj) {
            var pattem = /^[1-9][0-9]*$/; //数量验证
            var testnum = obj.value;
            if (!pattem.test(testnum)) {
                alert("请输入正确的数值！！！");
                obj.value = "1";
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
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:HiddenField ID="Hidden" runat="server" />
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td>
                            评审合同信息
                        </td>
                        <td align="center">
                            评审单号：<asp:Label ID="LBpsdh" runat="server"></asp:Label>
                            <%--唯一编号--%>
                            <asp:Label ID="lbl_UNID" runat="server" Text="" Visible="false"></asp:Label>
                             <asp:Label ID="lblPSZT" runat="server" Text="" Visible="false"></asp:Label>
                        </td>
                        <td align="center">
                            <div id="lock_tip" style="display: none; color: Green">
                                <span id="tip_content" runat="server"></span>
                            </div>
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="LinkLock" runat="server" OnClientClick="javascript:return confirm('点击确定为该用户锁定60分钟,此时间段内不会被其他人占用\r60分钟后若未提交则该合同号仍然可能被占用\r\r要立即锁定吗？');"
                                OnClick="LinkLock_Click" Visible="false" CausesValidation="false">
                                <asp:Image ID="Image1" Style="cursor: hand" ImageUrl="~/Assets/images/lock.jpg" Height="18"
                                    Width="18" runat="server" />
                                锁定合同号
                            </asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="LbtnNO" runat="server" OnClientClick="javascript:return confirm('取消后释放锁定，该合同号可继续使用\r\r确认取消锁定并关闭该页面吗？');"
                                Visible="false" CausesValidation="false" OnClick="btnNO_Click">
                                <asp:Image ID="Image2" Style="cursor: hand" ToolTip="放弃添加" ImageUrl="~/Assets/icons/delete.gif"
                                    Height="18" Width="18" runat="server" />
                                放弃添加
                            </asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="btnSave" OnClick="btnSubmit_Click" Visible="false">
                                <asp:Image ID="Image4" Style="cursor: hand" ToolTip="确认并保存" ImageUrl="~/Assets/icons/save.gif"
                                    Height="18" Width="18" runat="server" />
                                保存
                            </asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="LbtnSubmit" runat="server" OnClientClick="javascript:return Check_HTJE();"
                                OnClick="btnSubmit_Click">
                                <asp:Image ID="Image3" Style="cursor: hand" ToolTip="确认并提交" ImageUrl="~/Assets/icons/positive.gif"
                                    Height="18" Width="18" runat="server" />
                                提交
                            </asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="LbtnBack" runat="server" OnClick="btnBack_Click" CausesValidation="False">
                                <asp:Image ID="Image7" Style="cursor: hand" ToolTip="返回" ImageUrl="~/Assets/icons/back.png"
                                    Height="17" Width="17" runat="server" />
                                返回
                            </asp:LinkButton>&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lb_addtips" runat="server" ForeColor="Red" Visible="false" Text="提示：添加时想要页面中生成的合同号不被其他人占用，点击【锁定合同号】即可.锁定前请先生成正确的合同号"></asp:Label>
                    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                        ActiveTabIndex="0">
                        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="合同基本信息" TabIndex="0">
                            <ContentTemplate>
                                <table class="tabGg" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="r_bg" style="width: 80px">
                                            业主名称:
                                        </td>
                                        <td class="right_bg" style="width: 360px">
                                            <asp:TextBox ID="txtPCON_CUSTMNAME" runat="server" Style="width: 280px" OnTextChanged="Textkehu_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="kehu_AutoCompleteExtender" runat="server" 
                                                DelimiterCharacters="" Enabled="True" FirstRowSelected="True" 
                                                MinimumPrefixLength="1" ServiceMethod="get_kehu" ServicePath="~/Ajax.asmx" 
                                                TargetControlID="txtPCON_CUSTMNAME" UseContextKey="True">
                                            </asp:AutoCompleteExtender>
                                            <input id="txtPCON_CUSTMID" type="hidden" runat="server" />
                                            <font color="#ff0000">*</font>
                                            <br />
                                        </td>
                                        <td class="r_bg" style="width: 70px">
                                            合同号:
                                        </td>
                                        <td class="right_bg">
                                            <asp:TextBox ID="txtPCON_BCODE" Text="" runat="server" Width="280px"></asp:TextBox>
                                            &nbsp;
                                            <asp:Label ID="lb_lock" runat="server" Text="未锁定" ForeColor="Red" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="r_bg">
                                            市场部合同号:
                                        </td>
                                        <td class="right_bg">
                                            <asp:TextBox ID="txtTask" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="r_bg">
                                            业主合同号:
                                        </td>
                                        <td class="right_bg">
                                            <asp:TextBox ID="txt_YZHTH" runat="server" Width="280px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="r_bg" style="width: 80px">
                                            项目名称:
                                        </td>
                                        <td class="right_bg" style="width: 360px">
                                            <asp:TextBox ID="tb_pjinfo" runat="server" Text=""></asp:TextBox>
                                        </td>
                                        <td class="r_bg">
                                            责任部门:
                                        </td>
                                        <td class="right_bg">
                                            <asp:DropDownList ID="dplPCON_RSPDEPID" runat="server" Enabled="false" AutoPostBack="true"
                                                OnSelectedIndexChanged="dplPCON_RSPDEPID_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <font color="#ff0000">*</font>(根据部门生成合同号)
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td class="r_bg">
                                            合同金额:
                                        </td>
                                        <td class="right_bg">
                                            <span style="vertical-align: middle">
                                                <asp:TextBox ID="txtPCON_JINE" runat="server" onblur="javascript:check_num(this)"
                                                    Text="0"></asp:TextBox>(￥) <font color="#ff0000">*</font> </span>
                                        </td>
                                        <td class="r_bg">
                                            其他币种:
                                        </td>
                                        <td class="right_bg">
                                            <asp:TextBox ID="Other_MONUNIT" Text="0" runat="server" onblur="javascript:check_num(this)"></asp:TextBox>
                                            <asp:DropDownList ID="ddl_PCONMONUNIT" runat="server">
                                                <asp:ListItem Text="-请选择-" Value=""></asp:ListItem>
                                                <asp:ListItem Text="美元" Value="美元"></asp:ListItem>
                                                <asp:ListItem Text="欧元" Value="欧元"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td class="r_bg">
                                            业主负责人:
                                        </td>
                                        <td class="right_bg">
                                            <asp:TextBox ID="txtPCON_RESPONSER" Text="" runat="server" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td class="r_bg">
                                            合同类别:
                                        </td>
                                        <td class="right_bg">
                                            <asp:TextBox ID="txtPCON_TYPE" runat="server" Enabled="false" Text=""></asp:TextBox>
                                        </td>
                                    </tr>
                                    <asp:Panel ID="Panel2" runat="server" Visible="false">
                                        <tr>
                                            <td class="r_bg">
                                                订单时间:
                                            </td>
                                            <td class="right_bg">
                                                <asp:TextBox ID="txtPCON_FILLDATE" runat="server" Text="" onchange="dateCheck(this)" />
                                                <cc1:CalendarExtender ID="calender_filldate" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                                    TodaysDateFormat="yyyy年MM月dd日" TargetControlID="txtPCON_FILLDATE">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <%--<td class="r_bg">
                                            交货日期:
                                        </td>
                                        <td class="right_bg">
                                            <asp:TextBox ID="txtPCON_DELIVERYDATE" Text="" runat="server" onchange="dateCheck(this)" />
                                            <cc1:CalendarExtender ID="calender_deliverydate" runat="server" Format="yyyy-MM-dd"
                                                DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月dd日" TargetControlID="txtPCON_DELIVERYDATE">
                                            </cc1:CalendarExtender>
                                            <font color="#ff0000">*</font>
                                        </td>--%>
                                            <td class="r_bg">
                                                评审日期:
                                            </td>
                                            <td class="right_bg">
                                                <asp:TextBox ID="txtPCON_VALIDDATE" Text="" runat="server" onchange="dateCheck(this)" />
                                                <cc1:CalendarExtender ID="calender_validdate" runat="server" Format="yyyy-MM-dd"
                                                    DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月dd日" TargetControlID="txtPCON_VALIDDATE">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <%--   <tr>
                                        
                                        <td class="r_bg">
                                            发货时间:
                                        </td>
                                        <td class="right_bg">
                                            <asp:TextBox ID="txt_FHSJ" runat="server" onchange="dateCheck(this)"></asp:TextBox>
                                            &nbsp;<span style="color: Red">（最后一批）</span>
                                            <cc1:CalendarExtender ID="calender_fhsj" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                                TodaysDateFormat="yyyy年MM月dd日" TargetControlID="txt_FHSJ">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td class="r_bg">
                                            备注:
                                        </td>
                                        <td class="right_bg">
                                            <asp:TextBox ID="txtPCON_NOTE" runat="server" TextMode="MultiLine" Rows="4" Width="80%"></asp:TextBox>
                                        </td>
                                        <asp:Panel ID="pal" runat="server">
                                            <td class="r_bg">
                                                <asp:Label ID="lbType" runat="server"></asp:Label>:
                                            </td>
                                            <td class="right_bg">
                                                <asp:TextBox ID="txtPCON_ORDERID" runat="server" TextMode="MultiLine" Rows="4" Width="80%"></asp:TextBox><span
                                                    style="color: Red">(多项时，用“、”号隔开，否则会出错误)</span>
                                            </td>
                                        </asp:Panel>
                                        <asp:Panel ID="pal1" runat="server" Visible="false">
                                            <td class="r_bg">
                                            </td>
                                            <td class="r_bg">
                                            </td>
                                        </asp:Panel>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="市场部合同评审" TabIndex="2" Visible="false">
                            <ContentTemplate>
                                <div class="box-wrapper1">
                                    <div class="box-outer">
                                        <asp:Panel ID="PanelAll" runat="server">
                                            <div style="text-align: center; padding-top: 10px; margin-bottom: 20px">
                                                <h2 style="height: 20px">
                                                    合同/订单&nbsp;&nbsp;评审表</h2>
                                            </div>
                                            <table width="90%" style="margin: auto;">
                                                <tr>
                                                    <td style="text-align: left; font-size: small">
                                                        编号：<asp:TextBox runat="server" ID="CM_BIANHAO"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right; font-size: small">
                                                        文件号：TJZJ-R-M-01&nbsp;&nbsp;&nbsp;&nbsp;版 本：1
                                                    </td>
                                                </tr>
                                            </table>
                                            <table width="90%" cellpadding="4" cellspacing="1" class="grid" border="1" style="margin: auto;
                                                text-align: center;">
                                                <tr>
                                                    <td rowspan="13" width="50px" style="vertical-align: middle">
                                                        合<br />
                                                        <br />
                                                        同<br />
                                                        <br />
                                                        信<br />
                                                        <br />
                                                        息<br />
                                                        <br />
                                                        及<br />
                                                        <br />
                                                        成<br />
                                                        <br />
                                                        本<br />
                                                        <br />
                                                        核<br />
                                                        <br />
                                                        算<br />
                                                        <br />
                                                        部<br />
                                                        <br />
                                                        分
                                                    </td>
                                                    <td>
                                                        顾客名称：
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="CM_CUSNAME"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        设备名称：
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="CM_EQUIP"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        设备图号：
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="CM_MAP"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        合同单价：
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="CM_PAY"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        项目名称：
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="CM_PROJ"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        设备重量：
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="CM_SBZL"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        评审时间：
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="CM_PSTIME"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        图纸提供：
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="CM_TZ"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        订货数量,地点：
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="CM_NUMPLACE"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        交货期：
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="CM_JHTIME"></asp:TextBox>
                                                    </td>
                                                    <td colspan="4">
                                                        成本核算
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle">
                                                        供货范围
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox runat="server" ID="CM_GHFW" TextMode="MultiLine" Height="50px" Width="90%"></asp:TextBox>
                                                    </td>
                                                    <td colspan="4" rowspan="10" style="vertical-align: text-top; text-align: center">
                                                        <table style="margin: auto; width: 400px">
                                                            <asp:Panel runat="server" ID="panel">
                                                                <tr>
                                                                    <td align="right">
                                                                        行数：
                                                                    </td>
                                                                    <td width="60px">
                                                                        <asp:TextBox runat="server" ID="num" onblur="CheckNum(this)" Width="50px" Style="text-align: center"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Button runat="server" ID="btn_Add" Text="确定" OnClick="btn_add_Click" />
                                                                    </td>
                                                                </tr>
                                                            </asp:Panel>
                                                            <tr>
                                                                <td colspan="3">
                                                                    <asp:GridView runat="server" ID="GridHeSuan" AutoGenerateColumns="False" CellPadding="4"
                                                                        CssClass="toptable grid" ForeColor="#333333" Style="margin: auto; width: 400px">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="项目">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txt0" runat="server" Text='<%#Eval("CM_XM") %>' Width="80px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="重量/t">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txt1" runat="server" Text='<%#Eval("CM_ZL") %>' Width="50px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="单价/万元/t">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txt2" runat="server" Text='<%#Eval("CM_DJ") %>' Width="80px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="总价/万元">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txt3" runat="server" Text='<%#Eval("CM_ZJ") %>' Width="80px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="Delete" runat="server" OnClick="Delete_Click" CommandArgument='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'>
                                    <img src="../Assets/images/no.gif" />
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <RowStyle BackColor="#EFF3FB" />
                                                                        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    说明：
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
                                                                    <asp:TextBox runat="server" ID="CM_SHUOM" TextMode="MultiLine" Height="50px" Width="400px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle">
                                                        工艺/技术要求
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox runat="server" ID="CM_REQUEST" TextMode="MultiLine" Height="50px" Width="90%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle">
                                                        质量与检验
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox runat="server" ID="CM_CHECK" TextMode="MultiLine" Height="50px" Width="90%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        质保
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox runat="server" TextMode="MultiLine" Rows="2" ID="CM_ZHIBAO" Width="90%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        油漆
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox runat="server" TextMode="MultiLine" Rows="2" ID="CM_YOUQI" Width="90%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        包装
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox runat="server" TextMode="MultiLine" Rows="2" ID="CM_PACK" Width="90%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        运输
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox runat="server" TextMode="MultiLine" Rows="2" ID="CM_YUNSHU" Width="90%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        付款
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox runat="server" TextMode="MultiLine" Rows="2" ID="CM_FUKUAN" Width="90%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        当地法律法规
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox runat="server" TextMode="MultiLine" Rows="2" ID="CM_DDFG" Width="90%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        其他
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox runat="server" TextMode="MultiLine" Rows="2" ID="CM_QITA" Width="90%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        附件
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox runat="server" ID="CM_FUJIAN" Width="90%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="指定评审人" TabIndex="1">
                            <ContentTemplate>
                                <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                                    border="1">
                                    <tr>
                                        <td align="right" width="150px">
                                            制单人意见：
                                        </td>
                                        <td class="category">
                                            <asp:TextBox ID="txt_zdrYJ" runat="server" TextMode="MultiLine" Width="90%" Height="40px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            指定评审人员：
                                        </td>
                                        <td class="category">
                                            <asp:Panel ID="Panel1" runat="server" EnableViewState="False">
                                                <asp:Label ID="errorlb" runat="server" EnableViewState="False" ForeColor="Red" Visible="False"></asp:Label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            *注意事项
                                        </td>
                                        <td>
                                            1.请<span style="color: Red">不要全选！！！</span>仅选择需要审批的部门，全选将导致审批时间延长;<br />
                                            2.同一部门只能选择一个人，多选无效;<br />
                                            3.<span style="color: Red">只选择部门负责人</span>，审批领导根据合同金额自动添加，无需选择;<br />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                </ContentTemplate>
            </asp:UpdatePanel>
            <uc1:UploadAttachments ID="UploadAttachments1" runat="server" />
        </div>
        <!--box-outer END -->
    </div>
    <!--box-wrapper END -->
</asp:Content>
