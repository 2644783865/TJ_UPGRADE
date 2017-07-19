<%@ Page Language="C#" MasterPageFile="~/Masters/PopupBase.master" EnableViewState="true"
    AutoEventWireup="true" CodeBehind="CM_Contract_SW_Add.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_Contract_SW_Add" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="../Controls/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblConForm" runat="server"></asp:Label></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/Setting.css" />
    <link href="StyleFile/Style.css" rel="stylesheet" type="text/css" />
    <link href="../PC_Data/PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/superTables_compressed.js" type="text/javascript"></script>

    <script src="../JS/jquery/jquery-1.4.2.js" type="text/javascript"></script>

    <script src="../JS/KeyControl.js" type="text/javascript"></script>

    <script type="text/javascript">

        //添加要款
        function btnADDCR_onclick() {
            var date = new Date();
            var time = date.getTime();
            var obj = new Object();

            if (document.getElementById("<%=lbl_pszt.ClientID %>").innerHTML == "审批通过") {
                var sRet = window.showModalDialog('CM_SW_Payment.aspx?Action=Add&condetail_id=<%=CondetailID%>&NoUse=' + time, obj, "dialogWidth=750px;dialogHeight=520px;status:no;");
                if (sRet == "refresh") {
                    window.history.go(0);
                }
            }
            else {
                alert("合同还未审批通过，无法添加要款！");
            }
        }
        //修改要款-财务确认
        function BPEditDetail(i) {
            var ID = i.title;
            var date = new Date();
            var time = date.getTime();
            var obj = new Object();

            var sRet = window.showModalDialog("CM_SW_Payment.aspx?Action=Edit&BPid=" + ID + "&NoUse=" + time, obj, "dialogWidth=750px;dialogHeight=520px;status:no;");
            if (sRet == "refresh") {
                window.history.go(0);
            }
        }
        //查看要款记录
        function BPViewDetail(i) {
            var ID = i.title;
            var date = new Date();
            var time = date.getTime();
            var obj = new Object();

            var sRet = window.showModalDialog("CM_SW_Payment.aspx?Action=View&BPid=" + ID + "&NoUse=" + time, obj, "dialogWidth=750px;dialogHeight=520px;status:no;");

        }
        //修改要款
        function BPEditDetail(i) {
            var ID = i.title;
            var date = new Date();
            var time = date.getTime();
            var obj = new Object();

            var sRet = window.showModalDialog("CM_SW_Payment.aspx?Action=Edit&BPid=" + ID + "&NoUse=" + time, obj, "dialogWidth=750px;dialogHeight=520px;status:no;");
            if (sRet == "refresh") {
                window.history.go(0);
            }
        }

        //添加发票
        function btnAddFP_onclick() {
            var date = new Date();
            var time = date.getTime();
            var obj = new Object();

            var sRet = window.showModalDialog('CM_Bill_SW.aspx?Action=Add&condetail_id=<%=CondetailID%>&NoUse=' + time, obj, "dialogWidth=720px;dialogHeight=350px;status:no;");
            if (sRet == "refresh") {
                window.history.go(0);
            }
        }

        //修改发票
        function BREditDetail(i) {
            var ID = i.title;
            var date = new Date();
            var time = date.getTime();
            var obj = new Object();

            var sRet = window.showModalDialog("CM_Bill_SW.aspx?Action=Edit&BRid=" + ID + "&NoUse=" + time, obj, "dialogWidth=620px;dialogHeight=450px;status:no;");
            if (sRet == "refresh") {
                //window.history.go(0); 
            }
        }

        //查看发票
        function BRViewDetail(i) {
            var ID = i.title;
            var obj = new Object();

            window.showModalDialog("CM_Bill_SW.aspx?Action=View&BRid=" + ID, obj, "dialogWidth=620px;dialogHeight=450px;status:no;");
        }

        //查看索赔信息
        function View_SP(i) {
            var url = i.title;
            window.open(url);
        }

        //查看合同评审
        function btnRevInfo_onclick(id) {
            var autonum = Math.round(10000 * Math.random());
            window.open('CM_ContractView_Audit.aspx?Action=add&autonum=' + autonum + '&ID=' + id);
        }

        //检验日期格式如：2012-01-01
        function dateCheck(obj) {
            var value = obj.value;
            if (value != "") {
                var re = new RegExp("^([0-9]{4})(-)([0-9]{2})(-)([0-9]{2})$");
                m = re.exec(value)
                if (m == null) {
                    //obj.style.background = "yellow";
                    obj.value = "";
                    alert('请输入正确的时间格式如：2012-01-01');
                }
            }
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
            window.open('CM_ContractView_Other_Add.aspx?Action=add&Type=6&autonum=' + autonum + '&Conid=<%=CondetailID%>');
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

        function setRows(obj) {
            var a = $("#tab tr").eq(2).find(obj).val();
            $(obj).val(a);
        }

        function btnCheck() {
            var sum = 0;
            $.each($("#tab .price"), function() {
                sum += parseFloat(this.value);
            });
            //$("#checkje").val(sum.toFixed(4));
            $("#<%=checkje.ClientID %>").val(sum.toFixed(6));
            $("#<%=checkje.ClientID %>").css('display', 'block');
        }

        function check_num(obj) {
            var num = obj.value;
            var patten = /^(\+|\-)?[0-9][0-9]{0,9}(\.[0-9]{1,6})?$/;
            if (!patten.test(num)) {
                alert('请输入正确的数据格式！！！');
                obj.value = "0.00";
                obj.focus();
            }
        }

       //计算总重
        function calculate(obj) {
            var value = obj.value;
            var tr = obj.parentNode.parentNode;
            var a = tr.getElementsByTagName('input');
            var b = a[8].value;
            var bl = value * b;
            a[11].value = Math.round(bl * 100000) / 100000;
        }

        //计算合同额
        function calc(obj) {
            var value = obj.value;
            var tr = obj.parentNode.parentNode;
            var a = tr.getElementsByTagName('input');
            var b = a[8].value;
            var bl = value * b;
            a[7].value = Math.round(bl * 100000) / 100000;
        }
        
        function autosize() {
            var div = document.getElementById("dframe");
            var tb = document.getElementById("tab");

            if (tb.offsetHeight >= 400) {
                div.style.height = "400px";
            }
            else {
                div.style.height = (tb.offsetHeight + 30) + "px";
            } //使高度自适应，超过400px，出现滚动条。
            (function() {
                new superTable("tab", {
                //                    fixedCols: 2
            });
        })();
    }
    function hj() {
        var zj = 0;
        $("#tab tbody tr #td1 input").each(function() {
            zj += parseFloat($(this).val());
        });
        $("#<%=txtPCON_JINE.ClientID%>").val(zj.toFixed(6));
    }

    window.onload = function() {
        var wid = window.screen.width;
        var adiv = document.getElementById("adiv");
        adiv.style.width = wid * 0.97 + "px";
        autosize();
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
        .center
        {
            text-align: center;
        }
    </style>

    <script type="text/javascript">
        $(function() {
            $("#tab .CM_YZHTH:first").blur(function() {
                var CM_YZHTH = $(this).val();
                //                alert(CM_YZHTH);
                $("#tab .CM_YZHTH").each(function() {
                    $(this).val(CM_YZHTH);
                });
            })
        })

        function aa() {
            $("#tab .CM_YZHTH:first").blur(function() {
                var CM_YZHTH = $(this).val();
                //                alert(CM_YZHTH);
                $("#tab .CM_YZHTH").each(function() {
                    $(this).val(CM_YZHTH);
                });
            })
        }
        
    </script>

    <div id="adiv" class="RightContent">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="lblAddOREdit" runat="server"></asp:Label>
                                (带<span class="red">*</span>号的为必填项)
                                <%--唯一编号--%>
                                <asp:Label ID="lbl_UNID" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblConID" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <div id="lock_tip" style="display: none; color: Green">
                                    <span id="tip_content" runat="server"></span>
                                </div>
                            </td>
                            <td align="right">
                                <asp:LinkButton ID="LinkLock" runat="server" OnClientClick="javascript:return confirm('点击确定为该用户锁定60分钟,此时间段内不会被其他人占用\r60分钟后若未提交则该合同号仍然可能被占用\r\r要立即锁定吗？');"
                                    OnClick="LinkLock_Click" Visible="false" CausesValidation="false">
                                    <asp:Image ID="Image12" Style="cursor: hand" ToolTip="锁定合同号" ImageUrl="~/Assets/images/lock.jpg"
                                        Height="18" Width="18" runat="server" />
                                    锁定合同号
                                </asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="LbtnNO" runat="server" OnClientClick="javascript:return confirm('取消后释放锁定，该合同号可继续使用\r\r确认取消锁定并关闭该页面吗？');"
                                    Visible="false" CausesValidation="false" OnClick="btnNO_Click">
                                    <asp:Image ID="Image13" Style="cursor: hand" ToolTip="放弃添加" ImageUrl="~/Assets/icons/delete.gif"
                                        Height="18" Width="18" runat="server" />
                                    放弃添加
                                </asp:LinkButton>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnConfirm" runat="server" Text="" UseSubmitBehavior="false" OnClientClick="Javascript:if(this.value=='确认修改'){if(confirm(&quot;确认要修改？&quot;)){}else return false;}else {this.value=='确认添加';}"
                                    OnClick="btnConfirm_Click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="关 闭" BorderStyle="Solid" OnClientClick="javascript:window.close();"
                                    CausesValidation="False" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div style="height: 8px" class="box_top">
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <table width="100%">
                    <tr>
                        <td style="width: 10px">
                            <asp:Image ID="Image10" Style="cursor: hand" ToolTip="隐藏" ImageUrl="~/Assets/images/bar_down.gif"
                                onClick="switchGridVidew(this,'htjbxx')" Height="15" Width="15" runat="server" />
                        </td>
                        <td>
                            合同基本信息
                        </td>
                        <td align="right" style="width: 40%;">
                            <asp:Label ID="lblRemind" runat="server" ForeColor="Red" Text="操作成功！"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div id="htjbxx" style="display: block;">
                    <asp:Label ID="lb_addtips" runat="server" ForeColor="Red" Visible="false" Text="提示：添加时想要页面中生成的合同号不被其他人占用，点击【锁定合同号】即可.锁定前请先生成正确的合同号"></asp:Label>
                    <asp:Panel ID="palHT" runat="server">
                        <table class="tabGg" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="r_bg">
                                    项目名称:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_ENGNAME" runat="server"></asp:TextBox>
                                    <font color="#ff0000">*</font>
                                </td>
                                <td class="r_bg">
                                    责任部门:
                                </td>
                                <td class="right_bg">
                                    <asp:DropDownList ID="dplPCON_RSPDEPID" runat="server">
                                    </asp:DropDownList>
                                    <font color="#ff0000">*</font>
                                </td>
                            </tr>
                            <tr>
                                <td class="r_bg" rowspan="3" style="vertical-align: middle">
                                    设备类型:
                                </td>
                                <td class="right_bg">
                                    <asp:DropDownList ID="ddl_engtype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_engtype_Changed">
                                        <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="回转窑" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="管墨机" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="立磨" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="篦冷机" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="辊压机" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="破碎机" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="选粉机" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="堆取料机" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="电收尘器" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="袋收尘器" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="板喂机" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="提升机" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="预热器" Value="13"></asp:ListItem>
                                        <asp:ListItem Text="增湿塔" Value="14"></asp:ListItem>
                                        <asp:ListItem Text="承接的分交件" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="钢结构及铆焊件" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="备品备件" Value="17"></asp:ListItem>
                                        <asp:ListItem Text="其他" Value="18"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="r_bg">
                                    合同类别:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_TYPE" runat="server" Enabled="false" Text="销售合同"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="right_bg" rowspan="2">
                                    <asp:TextBox ID="txtPCON_ENGTYPE" runat="server" Width="280px" TextMode="MultiLine"
                                        Rows="3"></asp:TextBox>
                                </td>
                                <td class="r_bg">
                                    订单时间:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_FILLDATE" runat="server" onchange="dateCheck(this)" />
                                    <asp:CalendarExtender ID="calender_filldate" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                        TodaysDateFormat="yyyy年MM月dd日" TargetControlID="txtPCON_FILLDATE">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="r_bg">
                                    评审时间:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_VALIDDATE" runat="server" onchange="dateCheck(this)" />
                                    &nbsp;
                                    <asp:CalendarExtender ID="calender_validate" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                        TodaysDateFormat="yyyy年MM月dd日" TargetControlID="txtPCON_VALIDDATE">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="r_bg">
                                    合同号:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_BCODE" Text="" runat="server" Width="280px"></asp:TextBox>
                                    <asp:Label ID="lb_lock" runat="server" Text="未锁定" ForeColor="Red" Visible="false"></asp:Label>
                                    <asp:HiddenField runat="server" ID="Hid_Contr" />
                                </td>
                                <td class="r_bg">
                                    负责人:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_RESPONSER" Text="" runat="server"></asp:TextBox>
                                    <font color="#ff0000">*</font>
                                </td>
                            </tr>
                            <tr>
                                <td class="r_bg">
                                    业主合同号:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txt_YZHTH" runat="server" Width="280px"></asp:TextBox>
                                </td>
                                <td class="r_bg">
                                    合同金额:
                                </td>
                                <td class="right_bg">
                                    <span style="vertical-align: middle">
                                        <asp:TextBox ID="txtPCON_JINE" runat="server" onblur="javascript:check_num(this)"
                                            Text="0"></asp:TextBox>(万元) <font color="#ff0000">*</font>&nbsp;&nbsp;&nbsp;
                                        <input type="button" value="校验金额" id="checkbt" onclick="btnCheck()" />
                                        <br />
                                        <asp:TextBox ID="checkje" runat="server" Style="display: none;" ReadOnly="true"></asp:TextBox>
                                    </span>
                                </td>
                            </tr>
                            <%--<tr>
                                <td class="r_bg">
                                    合同名称:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_NAME" Text="" runat="server" Width="280px"></asp:TextBox>
                                    <font color="#ff0000">*</font>
                                </td>
                                <td class="r_bg">
                                    其他币种：
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
                                    业主名称:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_CUSTMNAME" runat="server" Style="width: 280px" OnTextChanged="Textkehu_TextChanged"
                                        AutoPostBack="true"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="kehu_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                        Enabled="True" FirstRowSelected="True" MinimumPrefixLength="1" ServiceMethod="get_kehu"
                                        ServicePath="~/Ajax.asmx" TargetControlID="txtPCON_CUSTMNAME" UseContextKey="True">
                                    </asp:AutoCompleteExtender>
                                    <font color="#ff0000">*</font>
                                </td>
                                <td class="r_bg">
                                    结算金额:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_BALANCEACNT" runat="server" onblur="javascript:check_num(this)"
                                        Text="0"></asp:TextBox>(万元)
                                </td>
                            </tr>
                            <tr>
                                <td class="r_bg">
                                    项目编号:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_PJID" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="r_bg" rowspan="2">
                                    备注:
                                </td>
                                <td rowspan="2" class="right_bg">
                                    <asp:TextBox ID="txtPCON_NOTE" runat="server" TextMode="MultiLine" Rows="4" Width="500px"></asp:TextBox>
                                </td>
                                <%--  <td class="r_bg">
                                    发货时间:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txt_FHSJ" runat="server" onchange="dateCheck(this)"></asp:TextBox>&nbsp;<span
                                        style="color: Red">（最后一批）</span>
                                    <asp:CalendarExtender ID="calender_fhsj" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                        TodaysDateFormat="yyyy年MM月dd日" TargetControlID="txt_FHSJ">
                                    </asp:CalendarExtender>
                                </td>--%>
                            </tr>
                            <tr>
                                <td class="r_bg">
                                    合同评审状态:
                                </td>
                                <td class="right_bg">
                                    <asp:Label ID="lbl_pszt" runat="server" Text=""></asp:Label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="tb_revid" runat="server" Visible="false"></asp:TextBox>
                                    <asp:Button ID="btn_RevInfo" runat="server" Visible="false" Text="查看评审信息" OnClick="btn_RevInfo_Click" />
                                </td>
                            </tr>
                            <%--  <tr>
                                <td class="r_bg">
                                    计入成本:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_COST" runat="server"></asp:TextBox>
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="r_bg">
                                    任务号:
                                </td>
                                <td class="right_bg">
                                    <asp:TextBox ID="txtPCON_SCH" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnadd" runat="server" Text="增 加" OnClick="btnadd_Click" />
                                </td>
                                <td class="r_bg">
                                </td>
                                <td class="right_bg">
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td>
                                    合同状态:
                                </td>
                                <td style="text-align: left">
                                    <asp:RadioButtonList ID="rblState" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblState_SelectedIndexChanged"
                                        AutoPostBack="true" Visible="false">
                                        <asp:ListItem Value="0">未开始</asp:ListItem>
                                        <asp:ListItem Value="1">进行中</asp:ListItem>
                                        <asp:ListItem Value="2">已完成</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div id="dframe" style="width: 100%">
                                <table id="tab" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                                    border="1" style="width: 2100px;">
                                    <tbody>
                                        <asp:Panel ID="Panel2" runat="server">
                                            <asp:Repeater ID="addRep" runat="server" OnItemDataBound="addRep_ItemDataBound">
                                                <HeaderTemplate>
                                                    <tr align="center" class="tableTitle headcolor">
                                                        <th width="50px">
                                                            <strong>序号</strong>
                                                        </th>
                                                        <th>
                                                            <strong>任务号</strong>
                                                        </th>
                                                        <th>
                                                            <strong>项目名称</strong>
                                                        </th>
                                                        <th>
                                                            <strong>业主合同号</strong>
                                                        </th>
                                                        <th>
                                                            <strong>产品名称</strong>
                                                        </th>
                                                        <th>
                                                            <strong>图号</strong>
                                                        </th>
                                                        <th>
                                                            <strong>阀板材质</strong>
                                                        </th>
                                                        <th>
                                                            <strong>单价（万元）</strong>
                                                        </th>
                                                        <th>
                                                            <strong>合同额（万元）</strong>
                                                        </th>
                                                        <th>
                                                            <strong>数量</strong>
                                                        </th>
                                                        <th>
                                                            <strong>单位</strong>
                                                        </th>
                                                        <th>
                                                            <strong>单重（吨）</strong>
                                                        </th>
                                                        <th>
                                                            <strong>总重（吨）</strong>
                                                        </th>
                                                        <th>
                                                            <strong>合同签订日期</strong>
                                                        </th>
                                                        <th>
                                                            <strong>合同要求交货期</strong>
                                                        </th>
                                                        <th>
                                                            <strong>业主负责人</strong>
                                                        </th>
                                                        <th>
                                                            <strong>合同特殊要求及说明</strong>
                                                        </th>
                                                        <th>
                                                            <strong>到图时间</strong>
                                                        </th>
                                                        <th>
                                                            <strong>成品入库时间</strong>
                                                        </th>
                                                        <th>
                                                            <strong>通知要求<br />
                                                                发货时间</strong>
                                                        </th>
                                                        <th>
                                                            <strong>出库时间<br />
                                                                （发货时间）</strong>
                                                        </th>
                                                        <th>
                                                            <strong>顶发说明</strong>
                                                        </th>
                                                        <th>
                                                            <strong>类型</strong>
                                                        </th>
                                                    </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                        <td>
                                                            <div style="width: 50px">
                                                                <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                                                <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                                                </asp:CheckBox></div>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TSA_ID" runat="server" Width="130px" Text='<%# Eval("TSA_ID")%>'
                                                                onkeydown="grControlFocus(this)" CssClass="center"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="CM_PROJ" runat="server" Width="100px" Text='<%# Eval("CM_PROJ")%>'
                                                                onkeydown="grControlFocus(this)" CssClass="center proj"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="CM_YZHTH" runat="server" Text='<%# Eval("CM_YZHTH")%>' Width="100px"
                                                                CssClass="CM_YZHTH"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="CM_ENGNAME" runat="server" TextMode="MultiLine" Width="100px" Text='<%# Eval("CM_ENGNAME")%>'
                                                                onkeydown="grControlFocus(this)" CssClass="center"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="CM_MAP" runat="server" Width="100px" Text='<%# Eval("CM_MAP")%>'
                                                                onkeydown="grControlFocus(this)" CssClass="center"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="CM_MATERIAL" runat="server" Width="100px" Text='<%# Eval("CM_MATERIAL")%>'
                                                                onkeydown="grControlFocus(this)" CssClass="center"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="CM_PRICE" runat="server" Width="100px" Text='<%# Eval("CM_PRICE")%>'
                                                                onkeydown="grControlFocus(this)" onblur="calc()" CssClass="center"></asp:TextBox>
                                                        </td>
                                                        <td id="td1">
                                                            <asp:TextBox ID="CM_COUNT" runat="server" Width="100px" Text='<%# Eval("CM_COUNT")%>'
                                                                onkeydown="grControlFocus(this)" onchange="hj()" CssClass="center price"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="CM_NUMBER" runat="server" Width="50px" Text='<%# Eval("CM_NUMBER")%>'
                                                                onkeydown="grControlFocus(this)" onblur="check_num(this)" CssClass="center"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="CM_UNIT" runat="server" Width="50px" Text='<%# Eval("CM_UNIT")%>'
                                                                onkeydown="grControlFocus(this)" CssClass="center"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="CM_WEIGHT" runat="server" Width="100px" Text='<%# Eval("CM_WEIGHT")%>'
                                                                onkeydown="grControlFocus(this)" onblur="calculate(this);check_num(this)" CssClass="center"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="CM_ALL" runat="server" Width="100" Text='<%# Eval("CM_ALL")%>' onkeydown="grControlFocus(this)"
                                                                CssClass="center" onblur="check_num(this)"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="CM_SIGN" runat="server" Width="100px" Text='<%# Eval("CM_SIGN")%>'
                                                                onkeydown="grControlFocus(this)" CssClass="center sign"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="CM_JIAO" runat="server" Width="100px" Text='<%# Eval("CM_JIAO")%>'
                                                                onkeydown="grControlFocus(this)" CssClass="center jiao"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="CM_DUTY" runat="server" Width="80px" Text='<%# Eval("CM_DUTY")%>'
                                                                onkeydown="grControlFocus(this)" CssClass="center duty"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="CM_NOTE" runat="server" TextMode="MultiLine" Width="200px" Text='<%# Eval("CM_NOTE")%>'
                                                                onkeydown="grControlFocus(this)" CssClass="center" ToolTip='<%# Eval("CM_NOTE")%>'></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="CM_DTSJ" runat="server" Text='<%# Eval("CM_DTSJ")%>' onkeydown="grControlFocus(this)"
                                                                CssClass="center dtsj"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="CM_CPRK" runat="server" Text='<%# Eval("CM_CPRK")%>' onkeydown="grControlFocus(this)"
                                                                CssClass="center cprk"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="CM_TZFH" runat="server" Text='<%# Eval("CM_TZFH")%>' onkeydown="grControlFocus(this)"
                                                                CssClass="center tzfh"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="CM_CKSJ" runat="server" Text='<%# Eval("CM_CKSJ")%>' onkeydown="grControlFocus(this)"
                                                                CssClass="center cksj"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="CM_DFSM" runat="server" TextMode="MultiLine" Width="200px" Text='<%# Eval("CM_DFSM")%>'
                                                                onkeydown="grControlFocus(this)" CssClass="center dfsm"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="CM_JHDTYPE" onfocus="this.blur()" runat="server" Width="15px" ForeColor="Red"
                                                                Text='<%# Eval("CM_JHDTYPE")%>' Visible="false" ToolTip="-1:初始计划单项;0:此页面修改的空类型项;1:初始计划单增补项;2:新增计划单项;3:新增计划单增补项;4:取消计划单项(不显示);无值:空类型项;"
                                                                CssClass="center dfsm"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </asp:Panel>
                                    </tbody>
                                </table>
                                <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                                    没有记录!</asp:Panel>
                            </div>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="delete" runat="server" Text="删除" OnClick="delete_Click" OnClientClick="autosize()"
                                            Visible="false" />
                                    </td>
                                    <td width="95%" align="right">
                                        <asp:Label ID="TipsError" runat="server" Text="本次修改前，上表中某任务号还存在增补或变更未审核通过的项，提交时请确认表中不存在类似数据！"
                                            Visible="false" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td width="100px">
                                        汇总信息：
                                    </td>
                                    <td>
                                        单价：
                                    </td>
                                    <td width="150px" align="left">
                                        <asp:Label runat="server" ID="djhz"></asp:Label>（万元）
                                    </td>
                                    <td>
                                        合同额：
                                    </td>
                                    <td width="150px" align="left">
                                        <asp:Label runat="server" ID="hthz"></asp:Label>（万元）
                                    </td>
                                    <td>
                                        数量：
                                    </td>
                                    <td width="150px" align="left">
                                        <asp:Label runat="server" ID="smhz"></asp:Label>
                                    </td>
                                    <td>
                                        总重：
                                    </td>
                                    <td width="150px" align="left">
                                        <asp:Label runat="server" ID="zzhz"></asp:Label>（吨）
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnadd" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="addRep" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <table width="100%">
                        <tr>
                            <td style="width: 100%">
                                <uc1:UploadAttachments ID="UploadAttachments1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0">
                            <asp:TabPanel ID="Tab1" runat="server" HeaderText="市场部收款" Height="90%" Width="100%">
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 10px">
                                                <asp:Image ID="Image2" Style="cursor: hand" ToolTip="隐藏" ImageUrl="~/Assets/images/bar_down.gif"
                                                    onClick="switchGridVidew(this,'yk')" Height="15px" Width="15px" runat="server" />
                                            </td>
                                            <td align="left">
                                                要款记录
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="Label1" runat="server" Text="已付金额（万元）:"></asp:Label>
                                                <asp:Label ID="lblYFJE" runat="server"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="Label2" runat="server" Text="合同金额（万元）:"></asp:Label>
                                                <asp:Label ID="lblHTJE" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="Label3" runat="server" Text="支付比例:"></asp:Label>
                                                <asp:Label ID="lblZFBL" runat="server"></asp:Label>
                                            </td>
                                            <td align="right">
                                                <input id="btnADDCR" type="button" class="button-outer" value="添加收款记录" onclick="return btnADDCR_onclick()"
                                                    runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="yk" style="display: block;">
                                        <asp:Panel ID="palYK" runat="server">
                                            <asp:GridView ID="grvYKJL" Width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                                                CellPadding="4" ForeColor="#333333" OnRowDataBound="grvYK_RowDataBound" ShowFooter="True">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="序号">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="BP_ID" HeaderText="收款单号">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BP_KXMC" HeaderText="款项名称">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BP_JE" HeaderText="收款金额（万元）">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BP_NOTEFST" HeaderText="收款比例">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BP_YKRQ" HeaderText="收款日期" DataFormatString="{0:d}">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="收款状态">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblstate" Text='<%#Eval("BP_STATE").ToString()=="0"?"未到账":"已到账" %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="查看">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hlContract" runat="server" CssClass="hand" onClick="BPViewDetail(this);"
                                                                ToolTip='<%# Eval("BP_ID")%>'>
                                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Assets/images/res.gif" ToolTip='<%# Eval("BP_ID")%>' />
                                                                查看</asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="删除">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="Lbtn_Del" runat="server" ForeColor="Red" CommandArgument='<%#Eval("BP_ID") %>'
                                                                OnClick="Lbtn_Del_OnClick" OnClientClick="javascript:return confirm('确定要删除吗？');"
                                                                Visible='<%#Eval("BP_STATE").ToString()=="0"?true:false %>'>
                                                                <asp:Image ID="ImageVoid" ImageUrl="~/Assets/images/erase.gif" runat="server" />删除
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle Wrap="False" />
                                                        <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" HorizontalAlign="Center" />
                                                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            </asp:GridView>
                                            <asp:Panel ID="palSWYK" runat="server" HorizontalAlign="Center">
                                                <hr style="width: 100%; height: 0.1px; color: Blue;" />
                                                没有记录!</asp:Panel>
                                        </asp:Panel>
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="Tab2" runat="server" HeaderText="收款情况" Height="90%" Width="100%">
                                <ContentTemplate>
                                    <asp:Panel ID="palDBSK" runat="server">
                                        <table>
                                            <tr>
                                                <td style="width: 10px">
                                                    <asp:Image ID="Image1" Style="cursor: hand" ToolTip="隐藏" ImageUrl="~/Assets/images/bar_down.gif"
                                                        onClick="switchGridVidew(this,'dqryk')" Height="15" Width="15" runat="server" />
                                                </td>
                                                <td>
                                                    待确认收款
                                                </td>
                                            </tr>
                                        </table>
                                        <div id="dqryk" style="display: block;">
                                            <asp:GridView ID="grvDBSK" Width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                                                CellPadding="4" ForeColor="#333333">
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="序号">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="BP_ID" HeaderText="收款单号" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="BP_KXMC" HeaderText="款项名称" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="BP_JE" HeaderText="收款金额（万元）" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="BP_YKRQ" HeaderText="收款日期" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:TemplateField HeaderText="编辑">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hyl" runat="server" CssClass="hand" onClick="BPEditDetail(this);"
                                                                ToolTip='<%# Eval("BP_ID")%>'>
                                                                <asp:Image ID="Image4" runat="server" ImageUrl="~/Assets/images/res.gif" ToolTip='<%# Eval("BP_ID")%>' />
                                                                编辑
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                            <asp:Panel ID="palDQRYK" runat="server" HorizontalAlign="Center">
                                                <hr style="width: 100%; height: 0.1px; color: Blue;" />
                                                没有记录!</asp:Panel>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="palYKRecord" runat="server">
                                        <table>
                                            <tr>
                                                <td style="width: 10px">
                                                    <asp:Image ID="Image8" Style="cursor: hand" ToolTip="隐藏" ImageUrl="~/Assets/images/bar_down.gif"
                                                        onClick="switchGridVidew(this,'skjl')" Height="15" Width="15" runat="server" />
                                                </td>
                                                <td>
                                                    收款记录
                                                </td>
                                            </tr>
                                        </table>
                                        <div id="skjl" style="display: block;">
                                            <asp:GridView ID="grvYBYK" Width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                                                CellPadding="4" ForeColor="#333333" OnRowDataBound="grvSK_RowDataBound" ShowFooter="true">
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="序号">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="BP_ID" HeaderText="收款单号" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="BP_KXMC" HeaderText="款项名称" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="BP_JE" HeaderText="收款金额（万元）" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="BP_YKRQ" HeaderText="收款日期" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                                                    <%--                                                    <asp:BoundField DataField="BP_SKRQ" HeaderText="收款日期" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />--%>
                                                    <asp:TemplateField HeaderText="收款状态">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblstate1" Text='<%#Eval("BP_STATE").ToString()=="0"?"未到账":"已到账" %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="有无凭证" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBox5" Enabled="false" runat="server" Checked='<%#Eval("BP_PZ").ToString()=="0"?false:true %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="凭证号" DataField="BP_PZH" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:TemplateField HeaderText="编辑">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hyly1k" runat="server" CssClass="hand" onClick="BPEditDetail(this);"
                                                                ToolTip='<%# Eval("BP_ID")%>'>
                                                                <asp:Image ID="Image15" runat="server" ImageUrl="~/Assets/images/res.gif" ToolTip='<%# Eval("BP_ID")%>' />
                                                                编辑</asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="查看">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hylyk" runat="server" CssClass="hand" onClick="BPViewDetail(this);"
                                                                ToolTip='<%# Eval("BP_ID")%>'>
                                                                <asp:Image ID="Image5" runat="server" ImageUrl="~/Assets/images/res.gif" ToolTip='<%# Eval("BP_ID")%>' />
                                                                查看</asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                            <asp:Panel ID="palYKJL" runat="server" HorizontalAlign="Center">
                                                <hr style="width: 100%; height: 0.1px; color: Blue;" />
                                                没有记录!</asp:Panel>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="Tab3" runat="server" HeaderText="发票单据" Height="90%" Width="100%">
                                <ContentTemplate>
                                    <asp:Panel ID="palFP" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 10px">
                                                    <asp:Image ID="Image9" Style="cursor: hand" ToolTip="隐藏" ImageUrl="~/Assets/images/bar_down.gif"
                                                        onClick="switchGridVidew(this,'fp')" Height="15" Width="15" runat="server" />
                                                </td>
                                                <td>
                                                    发票记录
                                                </td>
                                                <td align="right">
                                                    <input id="btnAddFP" type="button" class="button-outer" value="添加发票记录" runat="server"
                                                        onclick="return btnAddFP_onclick()" />
                                                </td>
                                            </tr>
                                        </table>
                                        <div id="fp" style="display: block;">
                                            <asp:GridView ID="grvFP" Width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                                                CellPadding="4" ForeColor="#333333" OnRowDataBound="grvFP_RowDataBound" ShowFooter="true">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="序号">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="BR_HTBH" HeaderText="合同编号" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="BR_KPRQ" HeaderText="开票日期" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="BR_KPJE" HeaderText="开票金额" ItemStyle-HorizontalAlign="Center" />
                                                    <%--                   <asp:BoundField DataField="BR_JSHJ" HeaderText="加税合计" ItemStyle-HorizontalAlign="Center" />
--%>
                                                    <asp:BoundField DataField="BR_SL" HeaderText="数量" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:TemplateField HeaderText="有无凭证" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBox4" runat="server" Enabled="false" Checked='<%# Eval("BR_PZ").ToString()=="0"?false:true %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="编辑">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hylfp" runat="server" CssClass="hand" onClick="BREditDetail(this);"
                                                                ToolTip='<%# Eval("BR_ID")%>'>
                                                                <asp:Image ID="Image6" runat="server" ImageUrl="~/Assets/images/res.gif" ToolTip='<%# Eval("BR_ID")%>' />
                                                                编辑</asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="查看">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hylpz" runat="server" CssClass="hand" onClick="BRViewDetail(this);"
                                                                ToolTip='<%# Eval("BR_ID")%>'>
                                                                <asp:Image ID="Image7" runat="server" ImageUrl="~/Assets/images/res.gif" ToolTip='<%# Eval("BR_ID")%>' />
                                                                查看</asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="删除">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="linkdel_FP" runat="server" ForeColor="Red" CommandArgument='<%# Eval("BR_ID")%>'
                                                                OnClick="linkdel_FP_Click" OnClientClick="return confirm('确定要删除此记录吗？？？');">
                    删除</asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                            <asp:Panel ID="palFPJL" runat="server" HorizontalAlign="Center">
                                                <hr style="width: 100%; height: 0.1px; color: Blue;" />
                                                没有记录!</asp:Panel>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="Tab5" runat="server" HeaderText="补充协议" Height="90%" Width="100%">
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
